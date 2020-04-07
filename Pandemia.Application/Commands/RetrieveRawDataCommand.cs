using Pandemia.Application.SeedWork;
using Pandemia.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandemia.Application.Commands
{
  public class RetrieveRawDataCommand
  {

    private readonly IPandemiaDataService _pandemiaDataService;


    public RetrieveRawDataCommand(IPandemiaDataService pandemiaDataService)
    {
      this._pandemiaDataService = pandemiaDataService;
    }

    public DateTime FromDate { get; set; } = new DateTime(year: 2020, month: 01, day: 22);
    public DateTime ToDate { get; set; } = DateTime.Today;

    public async Task<CommandResult<string>> Execute()
    {

      CommandResult<string> result = new CommandResult<string>();
      
      try
      {
        _pandemiaDataService.RetrieveRawDataAsync(this.FromDate, this.ToDate);
      }
      catch(Exception e)
      {
        result.Errors.Add(e);
        //must return error instead of throwing exception
        throw;
      }

      return result;
    }
  }
}
