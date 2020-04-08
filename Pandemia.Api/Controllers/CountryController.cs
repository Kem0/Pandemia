using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pandemia.Application.Commands;
using Pandemia.Application.Repositories;
using Pandemia.Application.Services;
using Pandemia.Domain.ValueObjects;

namespace Pandemia.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  [Produces("application/json")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class CountryController : ControllerBase
  {


    private readonly ICountryService _countryService;
    private readonly IRepository<Country> _countryRepository;

    public CountryController(ICountryService countryService, IRepository<Country> countryRepository)
    {
      _countryService = countryService;
      _countryRepository = countryRepository;
    }

    [HttpPost("/populate")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> RetrieveRawData()
    {
      //Todo: Implement MerdiaTR 
      GetCountriesCommand command = new GetCountriesCommand(_countryService, _countryRepository);
      await command.Execute();

      return NoContent();
    }
  }
}