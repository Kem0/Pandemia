using AutoMapper;
using Microsoft.Extensions.Options;
using Pandemia.Application.Services;
using Pandemia.Domain.ValueObjects;
using Pandemia.Infrastructure.Dto;
using Pandemia.Infrastructure.SeedWork;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pandemia.Infrastructure.ExternalServices
{
  public class CountryService : ICountryService
  {

    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    

    public CountryService(IOptions<CountryServiceSettings> countryServiceSettings, IMapper mapper)
    {
      _httpClient = new HttpClient() { BaseAddress = new Uri($"{countryServiceSettings.Value.BaseUrl}")};
      _mapper = mapper;
      
    }
    public async Task<IEnumerable<Country>> GetCountriesAsync()
    {
      IEnumerable<CountryServiceDto> result = null;


      string Uri = "all";
      var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{Uri}");
      
      if(response.IsSuccessStatusCode)
      {
        result = await HttpHelper.HandleJsonResponse<IEnumerable<CountryServiceDto>>(response);
      }
      else
      {
        throw new ApplicationException($"Cannot retrieve list of countries @ {_httpClient.BaseAddress}/{Uri}: HttpCode {response.StatusCode}");
      }
      return _mapper.Map<IEnumerable<Country>>(result);
    }
  }
}
