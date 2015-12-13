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
    /// Shows the Type Search Window and uses a callback to send the result. 
    /// </summary>
    public static void ShowTypeSerachWindow(Action<Type> onTypeSelected, GeneratorTypes type)
    {
      var typeWindow = EditorWindow.GetWindow<TypeSearchWindow>(true);
      typeWindow.Init(onTypeSelected, type);
    }
  }
}