using Jiffy;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Essence))]
public class EssenceEditor : Editor
{
  /// <summary>
  /// Type: string
  /// </summary>
  protected SerializedProperty m_ClassName;
  /// <summary>
  /// Type: string
  /// </summary>
  protected SerializedProperty m_Indent;
  /// <summary>
  /// Type: Boolean
  /// </summary>
  protected SerializedProperty m_CreateContent;

  protected SerializedProperty m_CustomName;

  private GUIContent m_CreateContentContent = new GUIContent("Create Labels", "If this is set to true Jiffy will create a new GUIContent item for each property else it will use the default Unity ones.");
  private GUIContent m_IndentCountContent = new GUIContent("Indent count", "This is the number of indents that Jiffy will apply to the generated code.");
  private GUIContent m_ClassNameContent = new GUIContent("Class Name", "This is the name of the class that will be generated for your Editor. By default Jiffy will just take the class name and apply 'Editor' to the end.");
  /// <summary>
  /// This function is called when the object is loaded.
  /// We use it to init all our properties
  /// </summary>
  public virtual void OnEnable()
  {
    m_ClassName = serializedObject.FindProperty("m_ClassName");
    m_Indent = serializedObject.FindProperty("m_IndentCount");
    m_CreateContent = serializedObject.FindProperty("m_CreateContent");
    m_CustomName = serializedObject.FindProperty("m_CustomName");
  }

  /// <summary>
  /// Inside this function you can add your own custom GUI
  /// for the inspector of a specific object class.
  /// </summary>
  public override void OnInspectorGUI()
  {
    EditorGUI.BeginChangeCheck();
    {
      EditorGUILayout.PropertyField(m_CustomName);
      if (m_CustomName.boolValue)
      {
        EditorGUILayout.PropertyField(m_ClassName, m_ClassNameContent);
      }
      EditorGUILayout.PropertyField(m_Indent, m_IndentCountContent);
      EditorGUILayout.PropertyField(m_CreateContent, m_CreateContentContent);
    }
    if (EditorGUI.EndChangeCheck())
    {
      serializedObject.ApplyModifiedProperties();
    }
  }
}
