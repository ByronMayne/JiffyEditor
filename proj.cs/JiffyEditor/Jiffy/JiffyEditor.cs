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
    #region -= MENU ITEMS =-
    [MenuItem("Tools/Jiffy Editor/Create Editor")]
    public static void MenuCreateEditor()
    {
      MonoScript script = Selection.activeObject as MonoScript;
      CreateEditor(script, Essence.GeneratorTypes.SimpleEditor);
    }

    [MenuItem("Tools/Jiffy Editor/Create Property Drawer")]
    public static void MenuCreatePropertyDrawer()
    {
      MonoScript script = Selection.activeObject as MonoScript;
      CreateEditor(script, Essence.GeneratorTypes.PropertyDrawer);
    }

    [MenuItem("CONTEXT/MonoScript/Create Editor..")]
    public static void ContextCreateEditor(MenuCommand cmd)
    {
      CreateEditor(cmd.context as MonoScript, Essence.GeneratorTypes.SimpleEditor);
    }

    [MenuItem("CONTEXT/MonoScript/Create Property Drawer..")]
    public static void ContextCreatePropertyDrawer(MenuCommand cmd)
    {
      CreateEditor(cmd.context as MonoScript, Essence.GeneratorTypes.PropertyDrawer);
    }

    [MenuItem("Tools/Jiffy Editor/Class Creator...")]
    public static void GetWindow()
    {
      EditorWindow.GetWindow<JiffyEditor>(); 
    }
    #endregion 

    [SerializeField]
    private Essence m_Essence;
    [SerializeField]
    private EssenceEditor m_Editor; 

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
      m_Essence = ScriptableObject.CreateInstance<Essence>();
      m_Editor = (EssenceEditor)EssenceEditor.CreateEditor(m_Essence);
      m_Editor.OnEnable();
    }

    public void OnGUI()
    {
      m_Editor.OnInspectorGUI();
    }
  }
}
