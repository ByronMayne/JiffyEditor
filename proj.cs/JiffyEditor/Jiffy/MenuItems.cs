using UnityEditor;
using UnityEngine;

namespace JiffyEditor
{
  public class MenuItems
  {
    [MenuItem("Assets/Create/Editor")]
    public static void MenuCreateEditor()
    {
      MonoScript script = Selection.activeObject as MonoScript;
      JiffyEditor.CreateEditor(script, Essence.GeneratorTypes.SimpleEditor);
    }

    [MenuItem("Assets/Create/Property Drawer")]
    public static void MenuCreatePropertyDrawer()
    {
      MonoScript script = Selection.activeObject as MonoScript;
      JiffyEditor.CreateEditor(script, Essence.GeneratorTypes.PropertyDrawer);
    }

    [MenuItem("Tools/SearchWindow")]
    public static void LaunchSearch()
    {
      EditorWindow.GetWindow<TypeSearchWindow>(true);
    }

    [MenuItem("CONTEXT/MonoScript/Create Editor..")]
    public static void ContextCreateEditor(MenuCommand cmd)
    {
      JiffyEditor.CreateEditor(cmd.context as MonoScript, Essence.GeneratorTypes.SimpleEditor);
    }

    [MenuItem("CONTEXT/MonoScript/Create Property Drawer..")]
    public static void ContextCreatePropertyDrawer(MenuCommand cmd)
    {
      JiffyEditor.CreateEditor(cmd.context as MonoScript, Essence.GeneratorTypes.PropertyDrawer);
    }

    [MenuItem("Tools/Jiffy Editor/Class Creator...")]
    public static void GetWindow()
    {
      EditorWindow.GetWindow<JiffyEditor>(); 
    }

    [MenuItem("Tools/Test Type")]
    public static void TestType()
    {
      Debug.Log("Is Subclass: " + typeof(MonoClass).IsSubclassOf(typeof(MonoBehaviour)));
      Debug.Log("Is Assignable: " + typeof(MonoBehaviour).IsAssignableFrom(typeof(MonoClass)));
    }
  }

  public class MonoClass : UnityEngine.MonoBehaviour
  {

  }
}
