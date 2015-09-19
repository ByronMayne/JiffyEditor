using System;
using System.Reflection;
public class Essence
{
  public enum GeneratorTypes
  {
    SimpleEditor,
    PropertyDrawer
  }

  private string m_ClassName;
  private Type m_ClassType;
  private string m_Indent = " ";
  private FieldInfo[] m_SerializedFields;
  private bool m_CreateContent;
  private GeneratorTypes m_Type;

  public Type classType
  {
    get { return m_ClassType; }
    set { m_ClassType = value; }
  }

  public string indent
  {
    get { return m_Indent; }
    set { m_Indent = value; }
  }

  public bool createContent
  {
    get { return m_CreateContent; }
    set { m_CreateContent = value; }
  }

  /// <summary>
  /// The is the name of the class that is going to be generated
  /// </summary>
  public string className
  {
    get { return m_ClassName; }
    set { m_ClassName = value; }
  }

  public GeneratorTypes type
  {
    get { return m_Type; }
    set { m_Type = value; }
  }

  /// <summary>
  /// This function will return all the fields a that are at the root of the class you are creating the edtior for. This will only return Serialized fields
  /// </summary>
  public FieldInfo[] GetSerializedFields()
  {
    return this.m_SerializedFields;
  }

  /// <summary>
  /// This function is used to take the current class and find all fields that can be Serialized. It then stores them locally
  /// </summary>
  private void PopulateSerializedFields()
  {
    throw new System.NotImplementedException();
  }

  /// <summary>
  /// The is a helper function used for drawing all fields.
  /// </summary>
  public void Foreach(Action<FieldInfo> action)
  {
    for (int i = 0; i < m_SerializedFields.Length; i++)
    {
      action.Invoke(m_SerializedFields[i]);
    }
  }

  public Essence(Type classType, GeneratorTypes type)
  {
    this.classType = classType;
    this.m_SerializedFields = TemplateUtility.GetSerializedFieldsFromType(classType);
    this.m_Type = type; 
  }
}