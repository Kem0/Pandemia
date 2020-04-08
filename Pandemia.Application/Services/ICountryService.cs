using Pandemia.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandemia.Application.Services
{
  public interface ICountryService
  {
    Task<IEnumerable<Country>> GetCountriesAsync();
  }
}
