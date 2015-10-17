﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = global::UnityEngine.Object;

namespace Jiffy.TypeSerach
{
  [System.Serializable]
  public partial class TypeSearchWindow : EditorWindow
  {
    /// <summary>
    /// If you are using the imGUI event callback this is the name of the
    /// event that will be sent to your Editor Window. 
    /// </summary>
    public const string ITEM_SELECTED_COMMAND = "TypeSearchWindow:Select";
    /// <summary>
    /// This EditorWindow is used with the imGUI event system. This is
    /// who will receive the event. 
    /// </summary>
    private EditorWindow m_Owner;
    private GUIContent m_AssemblyTitle;
    [SerializeField]
    private Object m_Target;

    private IntEditorPref    m_SelectedItem = new IntEditorPref("Jiffy:SelectedItem", 0);
    private StringEditorPref m_SearchFilter = new StringEditorPref("Jiffy:SerachFilter", string.Empty);
    private StringEditorPref m_MethodName = new StringEditorPref("Jiffy:MethodName", string.Empty);
    private StringEditorPref m_MethodAssemblyQualifiedName = new StringEditorPref("Jiffy:MethodAssemblyQualifiedName", string.Empty);
    private BoolEditorPref   m_ShowUserAssemblies = new BoolEditorPref("Jiffy:ShowUserAssemblies", true);
    private BoolEditorPref   m_ShowUnityAssemblies = new BoolEditorPref("Jiffy:ShowUnityAssemblies", true);
    private BoolEditorPref   m_CreateAnother = new BoolEditorPref("Jiffy:CreateAnother", true);

    private Styles m_Styles;
    private List<Type> m_Types;

    public List<AssemblyToggle> m_UserAssemblies;
    public List<AssemblyToggle> m_UnityAssemblies;

    //Scroll Positions
    private Vector2 m_TypesScrollPos = new Vector2();
    private Vector2 m_AssemblyScrollPos = new Vector2();

    public EditorWindow owner
    {
      get { return m_Owner; }
      set { m_Owner = value; }
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
      DrawButton();
    }

    public void AddCallback(Action<Type> callback)
    {
      m_Target = callback.Target as Object;
      m_MethodName.value = callback.Method.Name;
      m_MethodAssemblyQualifiedName.value = callback.Method.DeclaringType.AssemblyQualifiedName;
    }

    private void DrawButton()
    {
      GUILayout.Label(GUIContent.none, GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.Height(3));

      GUILayout.Space(5);
      GUILayout.BeginHorizontal();
      {
        GUILayout.FlexibleSpace();


        m_CreateAnother.value = GUILayout.Toggle(m_CreateAnother.value, "Create another");

        if (GUILayout.Button("Create", GUILayout.ExpandWidth(false)))
        {
          OnTypeSelected();

          if (!m_CreateAnother.value)
          {
            this.Close();
          }
        }

        if (GUILayout.Button("Cancel", GUILayout.ExpandWidth(false)))
        {
          this.Close();
        }
      }
      GUILayout.EndHorizontal();
      GUILayout.Space(5);

    }

    protected void OnEnable()
    {
      m_SelectedItem.Load();
      m_SearchFilter.Load();
      m_MethodName.Load();
      m_MethodAssemblyQualifiedName.Load();
      m_ShowUserAssemblies.Load();
      m_ShowUnityAssemblies.Load();
      m_CreateAnother.Load();

      titleContent = new GUIContent("Select Type");
      m_AssemblyTitle = new GUIContent("Used Assemblies");
      m_Types = new List<Type>();
      GetAssemblies();
      PopulateTypes();
    }

    protected void OnDisable()
    {
      m_SelectedItem.Save();
      m_SearchFilter.Save();
      m_MethodName.Save();
      m_MethodAssemblyQualifiedName.Save();
      m_ShowUserAssemblies.Save();
      m_ShowUnityAssemblies.Save();
      m_CreateAnother.Save();
    }

    private void Cancel()
    {
      base.Close();
      GUI.changed = true;
      GUIUtility.ExitGUI();
    }

    /// <summary>
    /// This function is used to allow the user to toggle the Assemblies 
    /// that they want to look for types in. 
    /// </summary>
    private void DrawAssemblies()
    {
      GUILayout.BeginVertical(GUILayout.MinHeight(150));
      {
        m_AssemblyScrollPos = GUILayout.BeginScrollView(m_AssemblyScrollPos);
        {
          GUILayout.Label("User Assemblies", EditorStyles.boldLabel);
          for (int i = 0; i < m_UserAssemblies.Count; i++)
          {
            EditorGUI.BeginChangeCheck();
            {
              m_UserAssemblies[i].useAssembly = GUILayout.Toggle(m_UserAssemblies[i].useAssembly, m_UserAssemblies[i].assemblyName);
            }
            if (EditorGUI.EndChangeCheck())
            {
              PopulateTypes();
            }
          }

          if (m_UserAssemblies.Count == 0)
          {
            GUILayout.Label("[ No user Assemblies could be loaded]", EditorStyles.helpBox);
          }

          GUILayout.Label("Unity Assemblies", EditorStyles.boldLabel);
          for (int i = 0; i < m_UnityAssemblies.Count; i++)
          {
            EditorGUI.BeginChangeCheck();
            {
              m_UnityAssemblies[i].useAssembly = GUILayout.Toggle(m_UnityAssemblies[i].useAssembly, m_UnityAssemblies[i].assemblyName);
            }
            if (EditorGUI.EndChangeCheck())
            {
              PopulateTypes();
            }
          }


          if (m_UnityAssemblies.Count == 0)
          {
            GUILayout.Label("[ No Unity Assemblies could be loaded ]", EditorStyles.helpBox);
          }
        }
        GUILayout.EndScrollView();
      }
      GUILayout.EndVertical();
    }

    /// <summary>
    /// This draws the serach header at the top. Depending on if there
    /// is content or not it will draw an cancel x. 
    /// </summary>
    private void DrawSearchField()
    {
      GUILayout.BeginHorizontal(m_Styles.searchBg);
      {
        m_SearchFilter.value = GUILayout.TextField(m_SearchFilter.value, m_Styles.toolbarSearchField);
        if (string.IsNullOrEmpty(m_SearchFilter.value))
        {
          GUILayout.Box(GUIContent.none, m_Styles.searchEmpty);
        }
        else
        {
          if (GUILayout.Button(GUIContent.none, m_Styles.searchFull))
          {
            m_SearchFilter.value = "";
            Repaint();
          }
        }
      }
      GUILayout.EndHorizontal();
    }

    /// <summary>
    /// This is the function that draws all our types with 
    /// the Script Icon beside them.
    /// </summary>
    private void DrawTypeTree()
    {
      GUILayout.BeginVertical(m_Styles.bottomBarBg);
      {
        m_TypesScrollPos = GUILayout.BeginScrollView(m_TypesScrollPos, GUILayout.ExpandHeight(true));
        {
          for (int i = 0; i < m_Types.Count; i++)
          {
            if (string.IsNullOrEmpty(m_SearchFilter.value) || m_Types[i].Name.Contains(m_SearchFilter.value))
            {
              var content = EditorGUIUtility.IconContent("cs Script Icon");
              content.text = m_Types[i].Name;

              Rect rect = GUILayoutUtility.GetRect(content, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));

              if (m_SelectedItem.value == i)
              {
                GUI.Label(rect, content, m_Styles.selectedLabel);
              }
              else
              {
                GUI.Label(rect, content);
              }

              if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
              {
                if (Event.current.clickCount == 2 && m_SelectedItem.value == i)
                {
                  OnTypeSelected();
                  this.Close();
                }

                m_SelectedItem.value = i;
                this.Repaint();
              }
            }
          }
        }
        GUILayout.EndVertical();
      }
      GUILayout.EndScrollView();
    }

    private void OnTypeSelected()
    {
      if (owner != null)
      {
        Event selected = EditorGUIUtility.CommandEvent(ITEM_SELECTED_COMMAND);
        owner.SendEvent(selected);
      }

      MethodInfo method = null;
      Type type = Type.GetType(m_MethodAssemblyQualifiedName.value);

      if (type != null)
      {
        method = type.GetMethod(m_MethodName.value, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
      }

      if (method != null)
      {
        method.Invoke(m_Target, new object[] { m_Types[m_SelectedItem.value] });
      }
      else
      {
        //We lost our callback so we have to close
        Debug.Log("Jiffy | Scripts were recompiled and the callback was lost. Closing Jiffy.");
        this.Close();
      }

    }

    /// <summary>
    /// This function i used to handle the esc button. It just closes the window. 
    /// </summary>
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

    private void GetAssemblies()
    {
      m_UserAssemblies = new List<AssemblyToggle>();
      m_UnityAssemblies = new List<AssemblyToggle>();

      string libraryUserAssemblyDirectory = Application.dataPath.Replace("/Assets", "/Library/ScriptAssemblies");
      string libraryUnityAssemblyDirectory = Application.dataPath.Replace("/Assets", "/Library/UnityAssemblies");

      string[] userAssemblies = new string[0];
      if (Directory.Exists(libraryUserAssemblyDirectory))
      {
        userAssemblies = Directory.GetFiles(libraryUserAssemblyDirectory, "*.dll");
      }

      string[] unityAssemblies = new string[0];
      if (Directory.Exists(libraryUnityAssemblyDirectory))
      {
        unityAssemblies = Directory.GetFiles(libraryUnityAssemblyDirectory, "*.dll");
      }

      //User Assemblies
      for (int i = 0; i < userAssemblies.Length; i++)
      {
        try
        {
          Assembly asm = Assembly.LoadFile(userAssemblies[i]);

          if (asm != null)
          {
            m_UserAssemblies.Add(new AssemblyToggle(true, asm.FullName.Split(',')[0]));
          }
        }
        catch
        {
          //ingore loading it. 
        }
      }

      //Unity Assemblies
      for (int i = 0; i < unityAssemblies.Length; i++)
      {
        try
        {
          Assembly asm = Assembly.LoadFile(unityAssemblies[i]);

          if (asm != null)
          {
            m_UnityAssemblies.Add(new AssemblyToggle(true, asm.FullName.Split(',')[0]));
          }
        }
        catch
        {
          //ingore loading it. 
        }
      }
    }

    /// <summary>
    /// This function tries to grab all our C# Assemblies
    /// and gets all the types that are contained in them. It
    /// then adds them to a list to be dispalyed in our Editor Window. 
    /// </summary>
    private void PopulateTypes()
    {
      m_Types = new List<Type>();
      foreach (AssemblyToggle toggle in m_UserAssemblies)
      {
        if (toggle.useAssembly)
        {
          try
          {
            Assembly asm = Assembly.Load(toggle.assemblyName);
            if (asm != null)
            {
              if (toggle.useAssembly)
              {
                m_Types.AddRange(asm.GetTypes());
              }
            }
          }
          catch
          {
            //Assembly.Load will throw an exception if it can't be found. We don't care
            //if we could not load it. Just move on. 
          }
        }
      }

      foreach (AssemblyToggle toggle in m_UnityAssemblies)
      {
        if (toggle.useAssembly)
        {
          try
          {
            Assembly asm = Assembly.Load(toggle.assemblyName);
            if (asm != null)
            {
              if (toggle.useAssembly)
              {
                m_Types.AddRange(asm.GetTypes());
              }
            }
          }
          catch
          {
            //Assembly.Load will throw an exception if it can't be found. We don't care
            //if we could not load it. Just move on. 
          }
        }
      }
      if (m_SelectedItem.value < 0 || m_SelectedItem.value > m_Types.Count)
      {
        m_SelectedItem.value = 0;
      }
    }
  }
}