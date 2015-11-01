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

  /// <summary>
  /// This function is called when the object is loaded.
  /// We use it to init all our properties
  /// </summary>
  public virtual void OnEnable()
  {
    m_ClassName = serializedObject.FindProperty("m_ClassName");
    m_Indent = serializedObject.FindProperty("m_Indent");
    m_CreateContent = serializedObject.FindProperty("m_CreateContent");
  }

  /// <summary>
  /// Inside this function you can add your own custom GUI
  /// for the inspector of a specific object class.
  /// </summary>
  public override void OnInspectorGUI()
  {
    EditorGUI.BeginChangeCheck();
    {
      EditorGUILayout.PropertyField(m_ClassName);
      EditorGUILayout.PropertyField(m_Indent);
      EditorGUILayout.PropertyField(m_CreateContent);
    }
    if (EditorGUI.EndChangeCheck())
    {
      serializedObject.ApplyModifiedProperties();
    }
  }
}
