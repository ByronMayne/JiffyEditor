

namespace Jiffy.TypeSerach
{
  public class AssemblyToggle
  {
    public AssemblyToggle(bool useAssembly, string assemblyName)
    {
      this.useAssembly = useAssembly;
      this.assemblyName = assemblyName;
    }
    public bool useAssembly;
    public string assemblyName;
  }
}
