using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pandemia.Application.Repositories;
using Pandemia.Domain.ValueObjects;
using Pandemia.Persistance.Model;
using Pandemia.Persistance.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandemia.Persistance.Repositories
{
  public class CountryRepository : MongoDbRepositoryBase<CountryModel>, IRepository<Country> {

    private readonly IMapper _mapper;
    private const string countryCollectionName = "Countries";

    public CountryRepository(IOptions<PersistenceSettings> persistanceSettingsOptions, IMapper mapper) : base(persistanceSettingsOptions)
    {
      _mapper = mapper;
    }


    public async Task<Country> InsertOrUpdateAsync(Country  country)
    {
      var countryModel = _mapper.Map<CountryModel>(country);
     
      return  _mapper.Map<Country>(await InsertOrUpdateAsync(countryModel));
    }

    public override ExpressionFilterDefinition<CountryModel> FilterIdPredicate(CountryModel model)
    {
      return new ExpressionFilterDefinition<CountryModel>(item => item.IsoAlpha3 == model.IsoAlpha3);
    }

    public async Task InsertManyAsync(IEnumerable<Country> countries)
    {
      var countryModelList = _mapper.Map<IEnumerable<CountryModel>>(countries);
      await InsertManyAsync(countryModelList);
    }
  }
}
