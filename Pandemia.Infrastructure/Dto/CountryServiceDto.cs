using Newtonsoft.Json;

namespace Pandemia.Infrastructure.Dto
{
  public class CountryServiceDto
  {
    public string Name { get; set; }
    [JsonProperty("alpha2Code")]
    public string IsoAlpha2 { get; set; }
    [JsonProperty("alpha3Code")]
    public string IsoAlpha3 { get; set; }
    public string Region { get; set; }
    public string SubRegion { get; set; }
    public string Capital { get; set; }
  }
}
