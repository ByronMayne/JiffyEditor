using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TemplateUtility
{
  /// <summary>
  /// This is a list of all the base types in C# that Unity 
  /// can serialize. 
  /// </summary>
  public static readonly Type[] BASE_TYPES = new Type[]
    {
      typeof(int),
      typeof(float), 
      typeof(bool),
      typeof(string),
      typeof(char),
      typeof(long),
    };

  /// <summary>
  /// This is a list of all the Unity types that can be
  /// serialized. 
  /// </summary>
  public static readonly Type[] UNITY_TYPES = new Type[]
    {
      typeof(Vector2),
      typeof(Vector3),
      typeof(Vector4),
      typeof(Transform), 
      typeof(GameObject),
      typeof(ScriptableObject),
      typeof(Bounds),
      typeof(Color),
    };

  public static string NiceTypeName(Type type)
  {
    if (type == typeof(int))
    {
      return "int";
    }

    if (type == typeof(float))
    {
      return "float";
    }

    if (type == typeof(double))
    {
      return "double";
    }

    if (type == typeof(string))
    {
      return "string";
    }

    return type.Name;
  }

  /// <summary>
  /// This function takes in a type and returns to you all fields that Unity can serialize.
  /// </summary>
  public static FieldInfo[] GetSerializedFieldsFromType(Type type)
  {
    List<FieldInfo> serializableFields = new List<FieldInfo>();

    foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
    {
      if (IsFieldSerializeable(field))
      {
        serializableFields.Add(field);
      }
    }

    return serializableFields.ToArray();
  }

  /// <param name="Field">Send in a field and this function will tell you if it's seriazliedable or not.</param>
  public static bool IsFieldSerializeable(FieldInfo field)
  {
    if (field.IsStatic)
    {
      //Static fields can't be serialized
      return false;
    }

    //It has to be public or have the [SerializeField] attribute. 
    if (field.IsPublic || TypeHasAttribute<SerializeField>(field.FieldType))
    {
      //We are serializeble. 
      if (IsBaseType(field.FieldType) || IsUnityType(field.FieldType))
      {
        //We know we can serialize these values.
        return true;
      }

      //It's not a normal type so lets see if it's value is Serializable
      if (TypeHasAttribute<SerializableAttribute>(field.FieldType))
      {
        return true;
      }
    }

    return false;
  }

  /// <summary>
  /// This will return true of false depending on if the type sent in is a Unity type
  /// </summary>
  public static bool IsUnityType(Type type)
  {
    for (int i = 0; i < UNITY_TYPES.Length; i++)
    {
      if (UNITY_TYPES[i] == type)
      {
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// This will return true of false depending on if the type sent in is a basee type
  /// </summary>
  public static bool IsBaseType(Type type)
  {
    for (int i = 0; i < BASE_TYPES.Length; i++)
    {
      if (BASE_TYPES[i] == type)
      {
        return true;
      }
    }
    return false;
  }

  public static bool TypeHasAttribute<T>(Type type)
  {
    return type.GetCustomAttributes(typeof(T), false).Length != 0;
  }
}