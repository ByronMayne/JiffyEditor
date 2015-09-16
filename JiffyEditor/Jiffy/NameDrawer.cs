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
