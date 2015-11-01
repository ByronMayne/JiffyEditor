using Jiffy.Templates;
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = global::UnityEngine.Object; 

namespace Jiffy
{
  public class JiffyEditor : EditorWindow
  {
    /// <summary>
    /// This function takes a type and checks to see if it meets the conditions
    /// to become it's own Editor.
    /// </summary>
    /// <param name="type">The class type you are trying to make the Editor for</param>
    /// <returns>Returns true if the class can support a custom Editor.</returns>
    public static bool IsValidForCustomEditor(Type type)
    {
      //Custom Editor only work for MonoBehaviours and ScriptableObjects. We make sure they are creating one for the correct type here.
      if (typeof(MonoBehaviour).IsAssignableFrom(type) || typeof(ScriptableObject).IsAssignableFrom(type))
      {
        return true; 
      }

      return false;
    }

    /// <summary>
    /// This function takes a type and finds out if it will make a valid Property Drawer. This only
    /// really checks to see if the class is Serializable. 
    /// </summary>
    public static bool IsValidForPropertyDrawer(Type type)
    {
      //First we check if it can be serialized
      if(Attribute.GetCustomAttribute(type, typeof(System.SerializableAttribute)) == null)
      {
        return false;
      }


      return true; 
    }

    /// <summary>
    /// This function takes MonoScript and a Generator Type and write a new class. It
    /// first checks to see if the type meets all the conditions then asks the user to
    /// where they would like to save it. 
    /// </summary>
    /// <param name="script">The script file you want to create your Editor for.</param>
    /// <param name="editorType">The type of Editor you want to create.</param>
    public static void CreateEditor(MonoScript script, GeneratorTypes editorType)
    {
      if (script != null)
      {
        Assembly asm = Assembly.Load("Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

        if (asm != null)
        {
          Type type = asm.GetType(script.name);

          CreateEditor(type, editorType);
        }
        else
        {
          Debug.Log("Assembly count not be found");
        }
      }
    }

    /// <summary>
    /// This function takes System.Type and a Generator Type and write a new class. It
    /// first checks to see if the type meets all the conditions then asks the user to
    /// where they would like to save it. 
    /// </summary>
    /// <param name="type">The type of class that you want to creat an Editor for.</param>
    /// <param name="editorType">The type of Editor you want to create.</param>
    public static void CreateEditor(Type type, GeneratorTypes editorType)
    {

      if (editorType == GeneratorTypes.CustomEditor && !IsValidForCustomEditor(type))
      {
          //We can't make an Editor for this class.
          return;
      }
      else if (editorType == GeneratorTypes.PropertyDrawer && !IsValidForPropertyDrawer(type))
      {
        //We can't make an Property Drawer for this type.
        return;
      }

      if (type != null)
      {
        string assetPath = EditorUtility.SaveFilePanelInProject("Save Location", type.Name + "Editor", "cs", "The location you want to save your Editor");

        if (string.IsNullOrEmpty(assetPath))
        {
          //They cancelled selecting a path.
          return;
        }

        string assetName = Path.GetFileNameWithoutExtension(assetPath);

        Essence essence = ScriptableObject.CreateInstance<Essence>();
        essence.classType = type;
        essence.outputEditorType = editorType;
        essence.className = assetName;

        var processor = new JiffyGeneratorPreprocessor(essence);

        string @class = processor.TransformText();

        File.WriteAllText(assetPath, @class);

        AssetDatabase.ImportAsset(assetPath);

        Debug.LogFormat("Jiffy | {0}.cs was created for {1}.cs. The scirpt is located {2}", type.Name, assetName, assetPath);

        Object script = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));

        if(script != null)
        {
          EditorGUIUtility.PingObject(script);
        }

        AssetDatabase.Refresh();
      }
    }
  }
}
