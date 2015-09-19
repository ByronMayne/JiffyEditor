using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiffyEditor.Jiffy.Templates
{
  public partial class JiffyGeneratorPreprocessor : JiffyGeneratorPreprocessorBase
  {
    public Essence essence; 

    public JiffyGeneratorPreprocessor(Essence essence)
    {
      this.essence = essence;
    }
  }
}
