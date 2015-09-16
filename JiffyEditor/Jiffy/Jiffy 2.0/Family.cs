using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class Car
{
  public Name name;
  public object speed;
}

public class Name
{
  public string firstName;
  public string lastName;
}

[System.Serializable]
public class Person
{
  [SerializeField]
  private Name m_Name;
  [SerializeField]
  private object m_Age;
  [SerializeField]
  private Car m_Car;

  public object age
  {
    get { return m_Age; }
    set { m_Age = value; }
  }

  public Name name
  {
    get { return m_Name; }
    set { m_Name = value; }
  }

  public Car car
  {
    get { return m_Car; }
    set { m_Car = value; }
  }
}

[System.Serializable]
public class Family
{
  public Vector2 location;

  public int familyCount;

  public float time; 

  public Person Mom;

  public Person Dad;
}
