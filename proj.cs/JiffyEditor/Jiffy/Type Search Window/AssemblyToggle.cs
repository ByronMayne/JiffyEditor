

namespace Jiffy.TypeSerach
{
  public class AssemblyToggle
  {
    public AssemblyToggle(bool useAssembly, string assemblyName)
    {
      this.m_UseAssembly = new BoolEditorPref("Jiffy:UseAssembly:" + assemblyName, useAssembly);
      this.assemblyName = assemblyName;
    }
    private BoolEditorPref m_UseAssembly;
    public string assemblyName;

    public bool useAssembly
    {
      get
      {
        return m_UseAssembly.value;
      }
      set
      {
        m_UseAssembly.value = value; 
      }
    }
  }
}
