using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Jiffy
{
  public class MenuItems
  {
    #region -= Create Menu =-
    [MenuItem("Assets/Create/Editor")]
    public static void MenuCreateEditor()
    {
      //Try to grab the first instance of Monoscript in our selection
      var script = Selection.objects.FirstOrDefault<Object>(t => { return t.GetType() == typeof(MonoScript); }) as MonoScript;

      if (script != null)
      {
        JiffyEditor.CreateEditor(script, GeneratorTypes.CustomEditor);
      }
      else
      {
        EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForEditorSelected, GeneratorTypes.CustomEditor);
      }
    }

    [MenuItem("Assets/Create/Property Drawer")]
    public static void MenuCreatePropertyDrawer()
    {
      //Try to grab the first instance of Monoscript in our selection
      var script = Selection.objects.FirstOrDefault<Object>(t => { return t.GetType() == typeof(MonoScript); }) as MonoScript;

      if (script != null)
      {
        JiffyEditor.CreateEditor(script, GeneratorTypes.PropertyDrawer);
      }
      else
      {
        EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForProperyDrawerSelected, GeneratorTypes.PropertyDrawer);
      }
    }
    #endregion

    #region -= Jiffy Menu =-
    [MenuItem("Tools/Jiffy/Create Custom Editor..")]
    public static void ContextCreateCustomEditor(MenuCommand cmd)
    {
      EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForEditorSelected, GeneratorTypes.CustomEditor);
    }

    [MenuItem("Tools/Jiffy/Create Property Drawer..")]
    public static void ContextCreatePropertyDrawer(MenuCommand cmd)
    {
      EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForProperyDrawerSelected, GeneratorTypes.PropertyDrawer);
    }
    #endregion

    #region -= Context Menu =-
    [MenuItem("CONTEXT/MonoScript/Create Editor..")]
    public static void ContextCreateEditor(MenuCommand cmd)
    {
      JiffyEditor.CreateEditor(cmd.context as MonoScript, GeneratorTypes.CustomEditor);
    }
    #endregion 

    #region -= Callbacks =-
    /// <summary>
    /// This is the callback used my the Type Select Window used for creating a Custom Editor. 
    /// </summary>
    private static void OnTypeForEditorSelected(System.Type type)
    {
      JiffyEditor.CreateEditor(type, GeneratorTypes.CustomEditor);
    }

    /// <summary>
    /// This is the callback used my the Type Select Window used for creating a custom Property Drawer.
    /// </summary>
    private static void OnTypeForProperyDrawerSelected(System.Type type)
    {
      JiffyEditor.CreateEditor(type, GeneratorTypes.PropertyDrawer);
    }
    #endregion 
  }
}
