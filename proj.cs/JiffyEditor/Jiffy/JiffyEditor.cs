using JiffyEditor.Jiffy.Templates;
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine; 

namespace JiffyEditor
{
  public class JiffyEditor : EditorWindow
  {
    /// <summary>
    /// This function takes a type and checks to see if it meets the conditions
    /// to become it's own Editor.
    /// </summary>
    /// <param name="type">The class type you are trying to make the Editor for</param>
    /// <returns>Returns true if the class can support a custom Editor.</returns>
    private static bool IsValidForCustomEditor(Type type)
    {
      //Custom Editor only work for MonoBehaviours and ScriptableObjects. We make sure they are creating one for the correct type here.
      if (typeof(MonoBehaviour).IsAssignableFrom(type) || typeof(ScriptableObject).IsAssignableFrom(type))
      {
        //They have the correct type. Is there a script in the Unity project that has the same name as the class (Requried to work)
        var guids = AssetDatabase.FindAssets("t:Script " + type.Name);

        bool hasValidClass = false;

        //We have to check all the guids to see if we have a valid script (The script name matches the type)
        for (int i = 0; i < guids.Length; i++)
        {
          //Get the full path
          string path = AssetDatabase.GUIDToAssetPath(guids[i]);
          //Strip the path as we only want the file name
          string fileName = Path.GetFileNameWithoutExtension(path);

          if (string.Compare(type.Name, fileName) == 0)
          {
            hasValidClass = true;
            //We found a valid class so we break;
            return true;
          }
        }

        //If they don't have a valid class we quit here
        if (!hasValidClass)
        {
          Debug.LogError("The type '" + type.Name + "' does not have a matching class in the Unity project. This is required for Unity to create your Editor. Please rename the class containing the class definition for " + type.Name + " to " + type.Name + ".cs");
        }
      }
      else
      {
        Debug.LogError("The type '" + type.Name + "' does not inherit from MonoBehaviour or ScriptableObject. Creating and Custom Editor for this class would have no effect.");
      }

      return false;
    }

    /// <summary>
    /// This function takes a type and finds out if it will make a valid Property Drawer. This only
    /// really checks to see if the class is Serializable. 
    /// </summary>
    private static bool IsValidForPropertyDrawer(Type type)
    {
      //First we check if it can be serialized
      if(Attribute.GetCustomAttribute(type, typeof(System.SerializableAttribute)) == null)
      {
        Debug.LogErrorFormat("'{0}' type can't have a custom Property Drawer made for it because the type is not Serializable. Please add the System.SerializableAttribute", type.Name);
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
    public static void CreateEditor(MonoScript script, Essence.GeneratorTypes editorType)
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
    public static void CreateEditor(Type type, Essence.GeneratorTypes editorType)
    {

      if (editorType == Essence.GeneratorTypes.CustomEditor && !IsValidForCustomEditor(type))
      {
          //We can't make an Editor for this class.
          return;
      }
      else if (editorType == Essence.GeneratorTypes.PropertyDrawer && !IsValidForPropertyDrawer(type))
      {
        //We can't make an Property Drawer for this type.
        return;
      }

      if (type != null)
      {
        string savePath = EditorUtility.SaveFilePanelInProject("Save Location", type.Name + "Editor", "cs", "The location you want to save your Editor");

        if(string.IsNullOrEmpty(savePath))
        {
          //They cancelled selecting a path.
          return;
        }

        string assetName = Path.GetFileNameWithoutExtension(savePath);

        Essence essence = ScriptableObject.CreateInstance<Essence>();
        essence.classType = type;
        essence.outputEditorType = editorType;
        essence.className = assetName;

        var processor = new JiffyGeneratorPreprocessor(essence);

        string @class = processor.TransformText();

        File.WriteAllText(savePath, @class);

        AssetDatabase.Refresh();
      }
    }
  }
}
