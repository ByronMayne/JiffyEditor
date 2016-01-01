using System;
using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Linq;
using UnityEditor;


namespace Jiffy
{
  public enum GeneratorTypes
  {
    CustomEditor,
    PropertyDrawer
  }

  /// <summary>
  /// We inherit from ScriptableObject so that we can serialize this class in an
  /// Editor window. 
  /// </summary>
  [Serializable]
  public class Essence : ScriptableObject
  {
    [SerializeField]
    private string m_ClassName;
    private string m_Indent = "  ";
    [SerializeField, Range(0, 10)]
    private int m_IndentCount = 4;
    [SerializeField]
    private bool m_CustomName;
    [SerializeField]
    private bool m_CreateContent;
    internal Type m_ClassType;
    private FieldInfo[] m_SerializedFields;
    private GeneratorTypes m_OutputEditorType;

    public Type classType
    {
      get { return m_ClassType; }
      set { m_ClassType = value; }
    }

    public string indent
    {
      set { m_Indent = value; }
      get 
      {
        if (m_IndentCount != m_Indent.Length)
        {
          m_Indent = string.Empty;

          for (int i = 0; i < m_Indent.Length; i++)
          {
            m_Indent += " ";
          }
        }

        return m_Indent; 
      }
    }

    public int indentCount
    {
      get { return m_Indent.Length; }
      set
      {
 
      }
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

    public GeneratorTypes outputEditorType
    {
      get { return m_OutputEditorType; }
      set { m_OutputEditorType = value; }
    }

    /// <summary>
    /// This function will return all the fields a that are at the root of the class you are creating the edtior for. This will only return Serialized fields
    /// </summary>
    public FieldInfo[] GetSerializedFields()
    {
      return this.m_SerializedFields.ToArray();
    }

    /// <summary>
    /// This function is used to take the current class and find all fields that can be Serialized. It then stores them locally
    /// </summary>
    private void PopulateSerializedFields()
    {
      m_SerializedFields = TemplateUtility.GetSerializedFieldsFromType(classType);
    }

    /// <summary>
    /// The is a helper function used for drawing all fields.
    /// </summary>
    public void Foreach(Action<FieldInfo> action)
    {
      if (m_SerializedFields == null)
      {
        PopulateSerializedFields();
      }

      for (int i = 0; i < m_SerializedFields.Length; i++)
      {
        action.Invoke(m_SerializedFields[i]);
      }
    }
  } 
}