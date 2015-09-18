 




using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Family))]
public class FamilyEditor : Editor
{
    public SerializedProperty location;
    public SerializedProperty familyCount;
    public Person Mom;
    public class Person
    {
      public SerializedProperty value;
      public Name m_Name;
      public class Name
      {
        public SerializedProperty value;
        public SerializedProperty firstName;
        public SerializedProperty lastName;
        public Name(SerializedProperty property)
        {
          firstName = property.FindPropertyRelative("firstName");
          lastName = property.FindPropertyRelative("lastName");
        }
        public static implicit operator SerializedProperty(Name @class)
        {
          return @class.value;
        }
        public static implicit operator Name(SerializedProperty property)
        {
          return new Name(property);
        }
      }
