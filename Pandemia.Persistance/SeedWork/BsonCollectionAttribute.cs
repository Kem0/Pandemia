using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Persistance.SeedWork
{
  [AttributeUsage(AttributeTargets.Class, Inherited = false)]
  public class BsonCollectionAttribute : Attribute
  {
    public string CollectionName { get; }

    public BsonCollectionAttribute(string collectionName)
    {
      CollectionName = collectionName;
    }
  }
}
