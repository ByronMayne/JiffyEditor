 




using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Family))]
public class FamilyEditor : Editor
{
    
    /// <summary>
    /// OnEnable is called by Unity every time you look at the
    /// Inspector. In this function we setup all our serialized 
    /// values. 
    /// </summary>
    public void OnEnable()
    {
      location = serializedObject.FindProperty("location");
      familyCount = serializedObject.FindProperty("familyCount");
      time = serializedObject.FindProperty("time");
      Mom = new Person(serializedObject.FindProperty("Mom"));
      Dad = new Person(serializedObject.FindProperty("Dad"));
    }
    
    /// <summary>
    /// Inside this function you can add your own custom GUI for the 
    /// inspector of a specific object class. The one provided will
    /// draw the class to look like the default inspector.
    /// </summary>
    public override void OnInspectorGUI()
    {
      EditorGUILayout.PropertyField(location);
      EditorGUILayout.PropertyField(familyCount);
      EditorGUILayout.PropertyField(time);
      EditorGUILayout.PropertyField(Mom.value, true);
      EditorGUILayout.PropertyField(Dad.value, true);
    }
}
