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

    public Essence essence;
    public SerializedObject m_EssenceObject;

    public static void CreateEditor(MonoScript script, Essence.GeneratorTypes editorType)
    {
      if(script != null)
      {
        Assembly asm = Assembly.Load("Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

        if(asm != null)
        {
          Type type = asm.GetType(script.name);

          if(typeof(Editor).IsAssignableFrom(type))
          {
            
            EditorUtility.DisplayDialog("Invalid Class Type", "You can't make a custom Editor for Editor class. Please choses a script that inheirts from MonoBehaviour or ScriptableObject class.", "Okay");
            
            return;
          }

          if(type != null)
          {
            string savePath =  AssetDatabase.GetAssetPath(script).Replace(".cs","Editor.cs");
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
          else
          {
            Debug.Log("Cound not find: " + script.name);
          }
        }
        else
        {
          Debug.Log("Assembly count not be found");
        }
      }
    }

    public void OnEnable()
    {
      essence = ScriptableObject.CreateInstance<Essence>();
      m_EssenceObject = new SerializedObject(essence); 
    }

    public void OnDisable()
    {
      essence = null;
      m_EssenceObject.Dispose();
    }

    public void OnGUI()
    {
    }
  }
}
