

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof( LangSettings ))]
public class LangSettingsEditor : Editor
{

  /// <summary>
  /// Type: Monoscript
  /// <summary>
  protected SerializedProperty m_Script;
  /// <summary>
  /// Type: bool
  /// </summary>
  protected SerializedProperty forceLanguage;
  /// <summary>
  /// Type: List`1
  /// </summary>
  protected SerializedProperty supportedLanguages;
  /// <summary>
  /// Type: List`1
  /// </summary>
  protected SerializedProperty googleDocs;
  /// <summary>
  /// Type: List`1
  /// </summary>
  protected SerializedProperty dictionaries;

  /// <summary>
  /// This function is called when the object is loaded.
  /// We use it to init all our properties
  /// </summary>
  protected virtual void OnEnable()
  {
    m_Script = serializedObject.FindProperty("m_Script");
    forceLanguage = serializedObject.FindProperty("forceLanguage");
    supportedLanguages = serializedObject.FindProperty("supportedLanguages");
    googleDocs = serializedObject.FindProperty("googleDocs");
    dictionaries = serializedObject.FindProperty("dictionaries");
  }

  /// <summary>
  /// Inside this function you can add your own custom GUI
  /// for the inspector of a specific object class.
  /// </summary>
  public override void OnInspectorGUI()
  {
    EditorGUI.BeginChangeCheck();
    {
      EditorGUILayout.PropertyField(m_Script);
      
      EditorGUILayout.PropertyField( property:forceLanguage);
      
      EditorGUILayout.PropertyField( property:supportedLanguages, includeChildren:true);
      
      EditorGUILayout.PropertyField( property:googleDocs, includeChildren:true);
      
      EditorGUILayout.PropertyField( property:dictionaries, includeChildren:true);
    }
    if(EditorGUI.EndChangeCheck())
    {
      serializedObject.ApplyModifiedProperties();
    }
  }
}
