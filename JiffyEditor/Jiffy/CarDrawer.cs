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
