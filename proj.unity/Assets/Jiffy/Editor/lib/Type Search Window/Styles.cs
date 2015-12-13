using UnityEditor;
using UnityEngine;

namespace Jiffy.TypeSerach
{
  public class Styles
  {
    public Styles()
    {
      selectedLabel.alignment = TextAnchor.MiddleLeft;
      selectedLabel.stretchWidth = true;
      selectedLabel.margin = new RectOffset(0, 0, 0, 0);
      selectedLabel.padding = new RectOffset(0, 0, 0, 0);
      selectedLabel.normal.textColor = EditorStyles.label.normal.textColor;

      this.searchBg.padding = new RectOffset(0, 5, 5, 0);
      this.searchBg.fixedHeight = 30f;
      this.bottomBarBg.alignment = TextAnchor.MiddleLeft;
      this.bottomBarBg.fontSize = EditorStyles.label.fontSize;
      this.bottomBarBg.padding = new RectOffset(0, 0, 0, 0);
    }

    public GUIStyle background = "ObjectPickerBackground";
    public GUIStyle toolbarSearchField = "SearchTextField";
    public GUIStyle previewTextureBackground = "ObjectPickerPreviewBackground";
    public GUIStyle searchEmpty = "SearchCancelButtonEmpty";
    public GUIStyle searchFull = "SearchCancelButton";

    public GUIStyle searchBg = new GUIStyle("ProjectBrowserTopBarBg");
    public GUIStyle bottomBarBg = new GUIStyle("ProjectBrowserBottomBarBg");

    public GUIStyle normalLabel = new GUIStyle(EditorStyles.label);
    public GUIStyle selectedLabel = new GUIStyle((GUIStyle)"TL SelectionButton PreDropGlow");
  }
}
