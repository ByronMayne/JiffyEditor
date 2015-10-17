using UnityEditor;

namespace Jiffy
{
  public abstract class EditorPrefValue<T>
  {
    protected string m_Key;
    protected T m_DefaultValue;
    protected T m_Value; 

    public EditorPrefValue(string key, T defaultValue = default(T))
    {
      m_Key = key;
      m_DefaultValue = defaultValue;
      Load(); 
    }

    public T value
    {
      get
      {
        return m_Value;
      }
      set
      {
        if(!m_Value.Equals(value))
        {
          m_Value = value;
          Save(); 
        }
      }
    }

    public abstract void Save();
    public abstract void Load();
  }

  public class StringEditorPref : EditorPrefValue<string>
  {
    public StringEditorPref(string key, string defaultValue = default(string))
      : base(key, defaultValue) { }

    public override void Save()
    {
      EditorPrefs.SetString(m_Key, value);
    }

    public override void Load()
    {
      m_Value = EditorPrefs.GetString(m_Key, m_DefaultValue);
    }
  }

  public class BoolEditorPref : EditorPrefValue<bool>
  {
    public BoolEditorPref(string key, bool defaultValue = default(bool))
      : base(key, defaultValue) { }

    public override void Save()
    {
      EditorPrefs.SetBool(m_Key, value);
    }

    public override void Load()
    {
      m_Value = EditorPrefs.GetBool(m_Key, m_DefaultValue);
    }
  }

  public class IntEditorPref : EditorPrefValue<int>
  {
    public IntEditorPref(string key, int defaultValue = default(int))
      : base(key, defaultValue) { }

    public override void Save()
    {
      EditorPrefs.SetInt(m_Key, value);
    }

    public override void Load()
    {
      m_Value = EditorPrefs.GetInt(m_Key, m_DefaultValue);
    }
  }
}
