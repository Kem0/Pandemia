using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Domain.ValueObjects
{
  public class Country
  {
    public string Name { get; set; }
    public string IsoAplha2 { get; set; }
    public string IsoAplha3 { get; set; }
    public string Region { get; set; }
    public string SubRegion { get; set; }
  }
}
