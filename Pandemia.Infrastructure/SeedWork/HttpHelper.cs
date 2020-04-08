using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pandemia.Infrastructure.SeedWork
{

  public static class HttpHelper
  {
    public static async Task<T> HandleJsonResponse<T>(HttpResponseMessage response)
    {
      var strContent = await response.Content.ReadAsStringAsync();

      if (response.IsSuccessStatusCode)
      {
        return JsonConvert.DeserializeObject<T>(strContent);
      }
      return default(T);
    }
  }
}
