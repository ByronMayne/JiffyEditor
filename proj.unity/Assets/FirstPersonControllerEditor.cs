

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof( UnityStandardAssets.Characters.FirstPerson.FirstPersonController ))]//TEST
public class FirstPersonControllerEditor : Editor
{

  /// <summary>
  /// Type: Monoscript
  /// <summary>
  protected SerializedProperty m_Script;
  /// <summary>
  /// Type: bool
  /// </summary>
  protected SerializedProperty m_IsWalking;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty m_WalkSpeed;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty m_RunSpeed;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty m_RunstepLenghten;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty m_JumpSpeed;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty m_StickToGroundForce;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty m_GravityMultiplier;
  /// <summary>
  /// Type: bool
  /// </summary>
  protected SerializedProperty m_UseFovKick;
  /// <summary>
  /// Type: bool
  /// </summary>
  protected SerializedProperty m_UseHeadBob;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty m_StepInterval;
  /// <summary>
  /// Type: AudioClip[]
  /// </summary>
  protected SerializedProperty m_FootstepSounds;
  /// <summary>
  /// Type: AudioClip
  /// </summary>
  protected SerializedProperty m_JumpSound;
  /// <summary>
  /// Type: AudioClip
  /// </summary>
  protected SerializedProperty m_LandSound;

  /// <summary>
  /// This function is called when the object is loaded.
  /// We use it to init all our properties
  /// </summary>
  protected virtual void OnEnable()
  {
    m_Script = serializedObject.FindProperty("m_Script");
    m_IsWalking = serializedObject.FindProperty("m_IsWalking");
    m_WalkSpeed = serializedObject.FindProperty("m_WalkSpeed");
    m_RunSpeed = serializedObject.FindProperty("m_RunSpeed");
    m_RunstepLenghten = serializedObject.FindProperty("m_RunstepLenghten");
    m_JumpSpeed = serializedObject.FindProperty("m_JumpSpeed");
    m_StickToGroundForce = serializedObject.FindProperty("m_StickToGroundForce");
    m_GravityMultiplier = serializedObject.FindProperty("m_GravityMultiplier");
    m_UseFovKick = serializedObject.FindProperty("m_UseFovKick");
    m_UseHeadBob = serializedObject.FindProperty("m_UseHeadBob");
    m_StepInterval = serializedObject.FindProperty("m_StepInterval");
    m_FootstepSounds = serializedObject.FindProperty("m_FootstepSounds");
    m_JumpSound = serializedObject.FindProperty("m_JumpSound");
    m_LandSound = serializedObject.FindProperty("m_LandSound");
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
      EditorGUILayout.PropertyField(m_IsWalking);
      EditorGUILayout.PropertyField(m_WalkSpeed);
      EditorGUILayout.PropertyField(m_RunSpeed);
      EditorGUILayout.PropertyField(m_RunstepLenghten);
      EditorGUILayout.PropertyField(m_JumpSpeed);
      EditorGUILayout.PropertyField(m_StickToGroundForce);
      EditorGUILayout.PropertyField(m_GravityMultiplier);
      EditorGUILayout.PropertyField(m_UseFovKick);
      EditorGUILayout.PropertyField(m_UseHeadBob);
      EditorGUILayout.PropertyField(m_StepInterval);
      EditorGUILayout.PropertyField(m_FootstepSounds);
      EditorGUILayout.PropertyField(m_JumpSound);
      EditorGUILayout.PropertyField(m_LandSound);
    }
    if(EditorGUI.EndChangeCheck())
    {
      serializedObject.ApplyModifiedProperties();
    }
  }
}
