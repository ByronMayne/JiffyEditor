using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Jiffy.TypeSerach
{
  public partial class TypeSearchWindow : EditorWindow
  {
    public const string ITEM_SELECTED_COMMAND = "TypeSearchWindow:Select";

    private GUIContent m_AssemblyTitle = new GUIContent("Used Assemblies");

    private string m_SearchFilter = string.Empty;
    private List<Type> m_Types = new List<Type>();
    private Vector2 m_AssemblyScrollPos = new Vector2();
    private Vector2 m_TypesScrollPos = new Vector2();
    private int m_SelectedItem = -1;
    private Styles m_Styles;
    private TypeSelectedCallback m_Callback;

    public TypeSelectedCallback callback
    {
      get { return m_Callback; }
      set { m_Callback = value; }
    }

    private EditorWindow m_Owner;

    public EditorWindow owner
    {
      get { return m_Owner; }
      set { m_Owner = value; }
    }

    protected void OnEnable()
    {
      this.titleContent = new GUIContent("Select Type");

      PopulateTypes();
    }

    private void PopulateTypes()
    {
      foreach (AssemblyToggle toggle in AssemblyToggles)
      {
        if (toggle.useAssembly)
        {
          try
          {
            Assembly asm = Assembly.Load(toggle.assemblyName);
            if (asm != null)
            {
              m_Types.AddRange(asm.GetTypes());
            }
          }
          catch
          {

          }
        }
      }
    }

    public void OnGUI()
    {
      if (m_Styles == null)
      {
        m_Styles = new Styles();

      }
      HandleKeyboard();
      DrawSearchField();
      DrawTypeTree();
      DrawAssemblies();
    }

    private void DrawSearchField()
    {
      GUILayout.BeginHorizontal(m_Styles.searchBg);
      {
        m_SearchFilter = GUILayout.TextField(m_SearchFilter, m_Styles.toolbarSearchField);
        if (string.IsNullOrEmpty(m_SearchFilter))
        {
          GUILayout.Box(GUIContent.none, m_Styles.searchEmpty);
        }
        else
        {
          if (GUILayout.Button(GUIContent.none, m_Styles.searchFull))
          {
            m_SearchFilter = "";
            Repaint();
          }
        }
      }
      GUILayout.EndHorizontal();
    }

    private void DrawTypeTree()
    {
      GUILayout.BeginVertical(m_Styles.bottomBarBg);
      {

        m_TypesScrollPos = GUILayout.BeginScrollView(m_TypesScrollPos, GUILayout.ExpandHeight(true));
        {
          for (int i = 0; i < m_Types.Count; i++)
          {
            if (string.IsNullOrEmpty(m_SearchFilter) || m_Types[i].Name.Contains(m_SearchFilter))
            {
              var content = EditorGUIUtility.IconContent("cs Script Icon");
              content.text = m_Types[i].Name;

              Rect rect = GUILayoutUtility.GetRect(content, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));

              if (m_SelectedItem == i)
              {
                GUI.Label(rect, content, m_Styles.selectedLabel);
              }
              else
              {
                GUI.Label(rect, content);
              }

              if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
              {
                if(Event.current.clickCount == 2 && m_SelectedItem == i)
                {
                  if(owner != null)
                  {
                    Event selected = EditorGUIUtility.CommandEvent(ITEM_SELECTED_COMMAND);
                    owner.SendEvent(selected); 
                  }

                  if(callback != null)
                  {
                    callback(m_Types[i]);
                    this.Close();
                  }
                }

                m_SelectedItem = i;
                this.Repaint();
              }
            }
          }
        }
        GUILayout.EndVertical();
      }
      GUILayout.EndScrollView();
    }

    private void DrawAssemblies()
    {
      GUILayout.BeginVertical();
      {
        GUILayout.Label(m_AssemblyTitle, EditorStyles.boldLabel);
        m_AssemblyScrollPos = GUILayout.BeginScrollView(m_AssemblyScrollPos);
        {
          for (int i = 0; i < AssemblyToggles.Count; i++)
          {
            AssemblyToggles[i].useAssembly = GUILayout.Toggle(AssemblyToggles[i].useAssembly, AssemblyToggles[i].assemblyName);
          }
        }
        GUILayout.EndScrollView();
      }
      GUILayout.EndVertical();
    }

    private void RefreshTypes()
    {

    }

    private void HandleKeyboard()
    {
      if (Event.current.type != EventType.KeyDown)
      {
        return;
      }
      KeyCode keyCode = Event.current.keyCode;
      if (keyCode != KeyCode.Return && keyCode != KeyCode.KeypadEnter)
      {
        return;
      }
      base.Close();
      GUI.changed = true;
      GUIUtility.ExitGUI();
      Event.current.Use();
      GUI.changed = true;
    }

    private void Cancel()
    {
      base.Close();
      GUI.changed = true;
      GUIUtility.ExitGUI();
    }

  }
}
