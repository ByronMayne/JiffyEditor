

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof( IntVector2 ))]
public class IntVector2Editor : PropertyDrawer
{

  /// <summary>
  /// Type: int
  /// </summary>
  protected SerializedProperty x;
  /// <summary>
  /// Type: int
  /// </summary>
  protected SerializedProperty y;

  /// <summary>
  /// Since PropertyDrawers are shared (they create one instance for many items) we can't
  /// store any local variables. This means that everytime we are setn a property we need
  /// to repopulate the SerializedProperties. This function does that.
  /// </summary>
  public void PopulateProperties(SerializedProperty property)
  {
    x = property.FindPropertyRelative("x"); 
    y = property.FindPropertyRelative("y"); 
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
    height += EditorGUI.GetPropertyHeight(x); 
    height += EditorGUI.GetPropertyHeight(y); 
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

    EditorGUI.PropertyField(position, x);
    position.y += EditorGUIUtility.singleLineHeight;

    EditorGUI.PropertyField(position, y);
    position.y += EditorGUIUtility.singleLineHeight;

  }
}
