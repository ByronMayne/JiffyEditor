using UnityEditor;
using UnityEngine;
using System.Linq;
using Jiffy;

namespace JiffyEditor
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
        JiffyEditor.CreateEditor(script, Essence.GeneratorTypes.CustomEditor);
      }
      else
      {
        EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForEditorSelected);
      }
    }

    [MenuItem("Assets/Create/Property Drawer")]
    public static void MenuCreatePropertyDrawer()
    {
      //Try to grab the first instance of Monoscript in our selection
      var script = Selection.objects.FirstOrDefault<Object>(t => { return t.GetType() == typeof(MonoScript); }) as MonoScript;

      if (script != null)
      {
        JiffyEditor.CreateEditor(script, Essence.GeneratorTypes.PropertyDrawer);
      }
      else
      {
        EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForProperyDrawerSelected);
      }
    }
    #endregion

    #region -= Jiffy Menu =-
    [MenuItem("Tools/Jiffy/Create Custom Editor..")]
    public static void ContextCreateCustomEditor(MenuCommand cmd)
    {
      EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForEditorSelected);
    }

    [MenuItem("Tools/Jiffy/Create Property Drawer..")]
    public static void ContextCreatePropertyDrawer(MenuCommand cmd)
    {
      EditorGUIUtilityEx.ShowTypeSerachWindow(OnTypeForProperyDrawerSelected);
    }
    #endregion

    #region -= Context Menu =-
    [MenuItem("CONTEXT/MonoScript/Create Editor..")]
    public static void ContextCreateEditor(MenuCommand cmd)
    {
      JiffyEditor.CreateEditor(cmd.context as MonoScript, Essence.GeneratorTypes.CustomEditor);
    }
    #endregion 

    #region -= Callbacks =-
    /// <summary>
    /// This is the callback used my the Type Select Window used for creating a Custom Editor. 
    /// </summary>
    private static void OnTypeForEditorSelected(System.Type type)
    {
      JiffyEditor.CreateEditor(type, Essence.GeneratorTypes.CustomEditor);
    }

    /// <summary>
    /// This is the callback used my the Type Select Window used for creating a custom Property Drawer.
    /// </summary>
    private static void OnTypeForProperyDrawerSelected(System.Type type)
    {
      JiffyEditor.CreateEditor(type, Essence.GeneratorTypes.PropertyDrawer);
    }
    #endregion 
  }
}
