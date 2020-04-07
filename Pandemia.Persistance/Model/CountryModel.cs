using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Persistance.Model
{
 public class CountryModel
  {
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("isoAlpha2")]
    public string IsoAplha2 { get; set; }
    [BsonId]
    [BsonElement("isoAlpha3")]
    public string IsoAplha3 { get; set; }
    [BsonElement("region")]
    public string Region { get; set; }
    [BsonElement("subRegion")]
    public string SubRegion { get; set; }
  }
}
