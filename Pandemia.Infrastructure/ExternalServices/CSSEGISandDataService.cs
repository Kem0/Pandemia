using Pandemia.Application.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.IO;
using Microsoft.Extensions.Options;

namespace Pandemia.Infrastructure.ExternalServices
{
  public class CSSEGISandDataService : IPandemiaDataService
  {

    private readonly HttpClient _httpClient;
    public CSSEGISandDataService(IOptions<CSSEGISandDataServiceSettings> cssegiRawDataSettings)
    {
      _httpClient = new HttpClient();
      _httpClient.BaseAddress = new Uri($"{cssegiRawDataSettings.Value.BaseUrl}/{cssegiRawDataSettings.Value.DataPath}"); 
    }
    public async void RetrieveRawDataAsync(DateTime fromDate, DateTime toDate)
    {
      
      for(DateTime date = fromDate; date<=toDate; date = date.AddDays(1))
      {

        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{date:MM-dd-yyyy}.csv");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
          continue;
        }
        var content = await response.Content.ReadAsStringAsync();
        //TODO: To move in the persistence layer with Dependency Injection
        using (var stream = File.CreateText($"D:\\CovidData\\{date:yyyy-MM-dd}.csv"))
        stream.Write(content);

      }

    }
  }
}