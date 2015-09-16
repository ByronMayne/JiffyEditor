 




using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Family))]
public class FamilyEditor : Editor
{
    public SerializedProperty location;
    public SerializedProperty familyCount;
    public Family Mom;
    public class Person
    {
      public SerializedProperty value;
      public Person m_Name;
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
      public SerializedProperty m_Age;
      public Person m_Car;
      public class Car
      {
        public SerializedProperty value;
        public Name name;
        public SerializedProperty speed;
        public Car(SerializedProperty property)
        {
          name = property.FindPropertyRelative("name");
          speed = property.FindPropertyRelative("speed");
        }
        public static implicit operator SerializedProperty(Car @class)
        {
          return @class.value;
        }
        public static implicit operator Car(SerializedProperty property)
        {
          return new Car(property);
        }
      }
      public Person(SerializedProperty property)
      {
        m_Name = property.FindPropertyRelative("m_Name");
        m_Age = property.FindPropertyRelative("m_Age");
        m_Car = property.FindPropertyRelative("m_Car");
      }
      public static implicit operator SerializedProperty(Person @class)
      {
        return @class.value;
      }
      public static implicit operator Person(SerializedProperty property)
      {
        return new Person(property);
      }
    }
    public Person Dad;
