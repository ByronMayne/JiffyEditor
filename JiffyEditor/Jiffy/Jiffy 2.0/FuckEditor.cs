
using UnityEditor;
using UnityEngine;

[CustomDrawerer(typeof( Family ))]
public class fuck : PropertyDrawer
{
  public GUIContent locationContent = new GUIContent("location");
  public GUIContent familyCountContent = new GUIContent("familyCount");
  public GUIContent timeContent = new GUIContent("time");
  public GUIContent MomContent = new GUIContent("Mom");
  public GUIContent DadContent = new GUIContent("Dad");

  /// <summary>
  /// Type: Vector2
  /// </summary>
  protected SerializedProperty location;
  /// <summary>
  /// Type: int
  /// </summary>
  protected SerializedProperty familyCount;
  /// <summary>
  /// Type: float
  /// </summary>
  protected SerializedProperty time;
  /// <summary>
  /// Type: Person
  /// </summary>
  protected SerializedProperty Mom;
  /// <summary>
  /// Type: Person
  /// </summary>
  protected SerializedProperty Dad;

  /// <summary>
  /// This function is called when the object is loaded.
  /// We use it to init all our properties
  /// </summary>
  protected virtual void OnEnable()
  {
    location = serializedObject.FindProperty("location");
    familyCount = serializedObject.FindProperty("familyCount");
    time = serializedObject.FindProperty("time");
    Mom = serializedObject.FindProperty("Mom");
    Dad = serializedObject.FindProperty("Dad");
  }

  /// <summary>
  /// Inside this function you can add your own custom GUI
  /// for the inspector of a specific object class.
  /// </summary>
  public override void OnInspectorGUI()
  {
    EditorGUI.BeginChangeCheck();
    {
      EditorGUILayout.PropertyField(location, locationContent);
      EditorGUILayout.PropertyField(familyCount, familyCountContent);
      EditorGUILayout.PropertyField(time, timeContent);
      EditorGUILayout.PropertyField(Mom, MomContent);
      EditorGUILayout.PropertyField(Dad, DadContent);
    }
    if(EditorGUI.EndChangeCheck())
    {
      serializedObject.ApplyModifiedProperties();
    }
  }
}
