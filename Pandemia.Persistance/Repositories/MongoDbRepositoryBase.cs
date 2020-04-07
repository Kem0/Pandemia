using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

    public void InsertOrUpdate(string collectionName, T record)
    {
      var collection = _mongoDatabase.GetCollection<T>(collectionName);
      collection.ReplaceOne<T>(
        item => GetId(record) == GetId(item) , record, new ReplaceOptions() { IsUpsert = true }
        );
    }

    public abstract string GetId(T model);

  }
}
