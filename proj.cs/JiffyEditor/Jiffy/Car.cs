      public SerializedProperty m_Age;
      public Car m_Car;
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
