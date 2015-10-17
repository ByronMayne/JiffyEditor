using UnityEditor;
using Jiffy.TypeSerach;
using System;
using UnityEngine.Events;

namespace Jiffy
{
  /// <summary>
  /// This class is used to add a way to show the Type Search Window. 
  /// </summary>
  public static class EditorGUIUtilityEx
  {
    /// <summary>
    /// Shows the Type Serach Window and uses the imGUI event system to send the result back.
    /// </summary>
    public static void ShowTypeSerachWindow(EditorWindow caller)
    {
      var typeWindow = EditorWindow.GetWindow<TypeSearchWindow>(true);
      typeWindow.owner = caller;
    }

    /// <summary>
    /// Shows the Type Search Window and uses a callback to send the result. 
    /// </summary>
    public static void ShowTypeSerachWindow(Action<Type> onTypeSelected)
    {
      var typeWindow = EditorWindow.GetWindow<TypeSearchWindow>(true);
      typeWindow.AddCallback(onTypeSelected);
    }
  }
}