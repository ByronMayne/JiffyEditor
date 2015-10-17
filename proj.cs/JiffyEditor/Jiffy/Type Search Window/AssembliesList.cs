using System.Collections.Generic;

namespace Jiffy.TypeSerach
{
  public partial class TypeSearchWindow
  {
    public static readonly List<AssemblyToggle> AssemblyToggles = new List<AssemblyToggle>()
    {
      new AssemblyToggle(true, "Assembly-CSharp"),
      new AssemblyToggle(true, "Assembly-CSharp-Editor"),
      new AssemblyToggle(true, "Assembly-CSharp-firstpass"),
    };
  }
}
