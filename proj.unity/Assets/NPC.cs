

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof( NPC ))]
public class NPC : Editor
{

  /// <summary>
  /// Type: Monoscript
  /// <summary>
  protected SerializedProperty m_Script;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty health;
  /// <summary>
  /// Type: Vector2
  /// </summary>
  protected SerializedProperty movementSpeed;
  /// <summary>
  /// Type: string
  /// </summary>
  protected SerializedProperty pncName;
  /// <summary>
  /// Type: GameObject
  /// </summary>
  protected SerializedProperty m_Head;
  /// <summary>
  /// Type: Transform
  /// </summary>
  protected SerializedProperty m_GunPivot;

  /// <summary>
  /// This function is called when the object is loaded.
  /// We use it to init all our properties
  /// </summary>
  protected virtual void OnEnable()
  {
    m_Script = serializedObject.FindProperty("m_Script");
    health = serializedObject.FindProperty("health");
    movementSpeed = serializedObject.FindProperty("movementSpeed");
    pncName = serializedObject.FindProperty("pncName");
    m_Head = serializedObject.FindProperty("m_Head");
    m_GunPivot = serializedObject.FindProperty("m_GunPivot");
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
      EditorGUILayout.PropertyField(health);
      EditorGUILayout.PropertyField(movementSpeed);
      EditorGUILayout.PropertyField(pncName);
      EditorGUILayout.PropertyField(m_Head);
      EditorGUILayout.PropertyField(m_GunPivot);
    }
    if(EditorGUI.EndChangeCheck())
    {
      serializedObject.ApplyModifiedProperties();
    }
  }
}
