using AutoMapper;
using Microsoft.Extensions.Options;
using Pandemia.Application.Repositories;
using Pandemia.Domain.ValueObjects;
using Pandemia.Persistance.Model;

namespace Pandemia.Persistance.Repositories
{
  public class CountryRepository : MongoDbRepositoryBase<CountryModel>, IRepository<Country> {

    private readonly IMapper _mapper;
    private const string countryCollectionName = "Countries";

    public CountryRepository(IOptions<PersistenceSettings> persistanceSettingsOptions, IMapper mapper) : base(persistanceSettingsOptions)
    {
      _mapper = mapper;
    }

    public override string GetId(CountryModel model)
    {
      return model.IsoAplha3;
    }
  }
}
