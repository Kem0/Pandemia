using MongoDB.Bson.Serialization.Attributes;
using Pandemia.Persistance.SeedWork;

namespace Pandemia.Persistance.Model
{
  [BsonCollection("Countries")]
 public class CountryModel
  {
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("isoAlpha2")]
    public string IsoAlpha2 { get; set; }
    [BsonId]
    [BsonElement("isoAlpha3")]
    public string IsoAlpha3 { get; set; }
    [BsonElement("region")]
    public string Region { get; set; }
    [BsonElement("subRegion")]
    public string SubRegion { get; set; }
  }
}
