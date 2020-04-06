using Microsoft.AspNetCore.Mvc;
using Pandemia.Application.Commands;
using Pandemia.Application.SeedWork;
using Pandemia.Application.Services;
using System.Net;
using System.Threading.Tasks;

namespace Pandemia.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  [Produces("application/json")]
  [Route("api/v{version:apiVersion}/[controller]")]
  //[Authorize]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public class PandemiaController : ControllerBase
  {

    private readonly IPandemiaDataService _pandemiaDataService;

    public PandemiaController(IPandemiaDataService pandemiaDataService)
    {
      this._pandemiaDataService = pandemiaDataService;
    }

    [HttpPost("/RetrieveRawData")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> RetrieveRawData()
    {
      //Todo: Implement MerdiaTR 
      RetrieveRawDataCommand command = new RetrieveRawDataCommand(_pandemiaDataService);
      await command.Execute();

      return NoContent();
    }

  }
}