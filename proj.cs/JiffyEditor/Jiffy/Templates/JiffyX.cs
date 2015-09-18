using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof( Family ))]
public class FamilyEditor : PropertyDrawer
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
  /// Since PropertyDrawers are shared (they create one instance for many items) we can't
  /// store any local variables. This means that everytime we are setn a property we need
  /// to repopulate the SerializedProperties. This function does that.
  /// </summary>
  public void PopulateProperties(SerializedProperty property)
  {
    location = property.FindPropertyRelative("location"); 
    familyCount = property.FindPropertyRelative("familyCount"); 
    time = property.FindPropertyRelative("time"); 
    Mom = property.FindPropertyRelative("Mom"); 
    Dad = property.FindPropertyRelative("Dad"); 
  }

  /// <summary>
  /// This property is used to set how much height space we are given to draw our inspector.
  /// We use the helper function to get the height because other property drawers can change
  /// the required height
  /// </summary>
  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
  {
    PopulateProperties(property);

    float height = 0f;
    height += EditorGUI.GetPropertyHeight(location); 
    height += EditorGUI.GetPropertyHeight(familyCount); 
    height += EditorGUI.GetPropertyHeight(time); 
    height += EditorGUI.GetPropertyHeight(Mom); 
    height += EditorGUI.GetPropertyHeight(Dad); 
    return height;
  }

  /// <summary>
  /// Inside this function you can add your own custom GUI
  /// for the inspector of a specific object class.
  /// </summary>
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    PopulateProperties(property);

    position.height = EditorGUIUtility.singleLineHeight;

    EditorGUI.PropertyField(position, location, locationContent);
    position.y += EditorGUIUtility.singleLineHeight;

    EditorGUI.PropertyField(position, familyCount, familyCountContent);
    position.y += EditorGUIUtility.singleLineHeight;

    EditorGUI.PropertyField(position, time, timeContent);
    position.y += EditorGUIUtility.singleLineHeight;

    EditorGUI.PropertyField(position, Mom, MomContent);
    position.y += EditorGUIUtility.singleLineHeight;

    EditorGUI.PropertyField(position, Dad, DadContent);
    position.y += EditorGUIUtility.singleLineHeight;

  }
}



