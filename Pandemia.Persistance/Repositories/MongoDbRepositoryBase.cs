using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pandemia.Persistance.SeedWork;
using System.Linq;
using System;
using System.Reflection;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Pandemia.Persistance.Repositories
{
  public abstract class MongoDbRepositoryBase<T>
  {
    protected readonly MongoDbSettings _mongoDbSettings;
    protected readonly string _collectionName;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbRepositoryBase(IOptions<PersistenceSettings> persistanceSettingsOptions)
    {
      _mongoDbSettings = persistanceSettingsOptions.Value.MongoDbSettings;
      _mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
      _mongoDatabase = _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
    }

    private static string GetCollectionName()
    {
      string collectionName;
      try
      {
        collectionName = (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
           as BsonCollectionAttribute).CollectionName;
      }
      catch
      {
        throw new ArgumentNullException($"Cannot retrieve collection name for type {typeof(T)}, are you missing BsonCollectionAttribute?");
      }
      return collectionName;

    }

    public async Task<T> InsertOrUpdateAsync(T record)
    {
      string collectionName = GetCollectionName();
      var collection = _mongoDatabase.GetCollection<T>(collectionName);
      var result = await collection.ReplaceOneAsync<T>(
         FilterIdPredicate(record).Expression, record, new ReplaceOptions() { IsUpsert = true }
         );

      if (result.IsAcknowledged)
      {
        UpdateId(record, result.UpsertedId.AsString);
      }

      return record;
    }

    public async Task InsertManyAsync(IEnumerable<T> records)
    {
      string collectionName = GetCollectionName();
      var collection = _mongoDatabase.GetCollection<T>(collectionName);
      await collection.InsertManyAsync(records);
    }

    private void UpdateId(T record, string upsertedId)
    {
      var propertyWithIdAttribute = typeof(T).GetProperties()
    // use projection to get properties with their attributes - 
    .Select(pi => new { Property = pi, Attribute = pi.GetCustomAttributes(typeof(BsonIdAttribute), true).FirstOrDefault() })
    // filter only properties with attributes
    .Where(x => x.Attribute != null)
    .FirstOrDefault();

      if (propertyWithIdAttribute != null && propertyWithIdAttribute.Property.CanWrite)
      {
        propertyWithIdAttribute.Property.SetValue(record, upsertedId, null);
      }
    }

    public async Task DropAsync()
    {
      string collectionName = GetCollectionName();
      await _mongoDatabase.DropCollectionAsync(collectionName);
    }

    public abstract ExpressionFilterDefinition<T> FilterIdPredicate(T model);

  }
}
