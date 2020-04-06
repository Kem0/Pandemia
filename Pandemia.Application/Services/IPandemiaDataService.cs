using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Application.Services
{
  public interface IPandemiaDataService
  {
    void RetrieveRawDataAsync(DateTime fromDate, DateTime toDate);
  }
}
