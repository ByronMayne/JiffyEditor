using JiffyEditor.Jiffy.Templates;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine; 

namespace JiffyEditor
{
  public class JiffyEditor
  {
    [MenuItem("Tools/Jiffy Editor/Create Editor...")]
    public static void CreateTypeFromMenu()
    {
      MonoScript script = Selection.activeObject as MonoScript;
      CreateEditor(script);
    }

    public static void CreateEditor(MonoScript script)
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

            Essence essence = new Essence(type, Essence.GeneratorTypes.SimpleEditor);
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

    [MenuItem("CONTEXT/MonoScript/Create Editor..")]
    public static void MenuCreate(MenuCommand cmd)
    {
      CreateEditor(cmd.context as MonoScript);
    }
  }
}
