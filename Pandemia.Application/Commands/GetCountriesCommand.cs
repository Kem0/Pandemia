using Pandemia.Application.Repositories;
using Pandemia.Application.SeedWork;
using Pandemia.Application.Services;
using Pandemia.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandemia.Application.Commands
{
  public class GetCountriesCommand
  {

    private readonly ICountryService _countryService;
    private readonly IRepository<Country> _countryRepository;


    public GetCountriesCommand(ICountryService countryService, IRepository<Country> countryRepository)
    {
      this._countryService = countryService;
      _countryRepository = countryRepository;
    }

    public async Task<CommandResult<string>> Execute()
    {
      CommandResult<string> result = new CommandResult<string>();

      try
      {
        IEnumerable<Country> countries = await _countryService.GetCountriesAsync();
        await _countryRepository.DropAsync();
        await _countryRepository.InsertManyAsync(countries);
      }
      catch (Exception e)
      {
        result.Errors.Add(e);
        //must return error instead of throwing exception
        throw;
      }
      return result;
    }
  }
}
