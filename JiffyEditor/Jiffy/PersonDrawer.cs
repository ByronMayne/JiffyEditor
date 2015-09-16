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
