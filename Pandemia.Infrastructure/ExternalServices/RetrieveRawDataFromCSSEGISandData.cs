using Pandemia.Application.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.IO;

namespace Pandemia.Infrastructure.ExternalServices
{
  public class RetrieveRawDataFromCSSEGISandData : IPandemiaDataService
  {

    private readonly HttpClient _httpClient;

    public RetrieveRawDataFromCSSEGISandData()
    {
      _httpClient = new HttpClient();
      //TODO: Configuration injection
      _httpClient.BaseAddress = new Uri("https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports");
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