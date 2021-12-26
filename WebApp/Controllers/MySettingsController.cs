using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class MySettingsController : ControllerBase
    {
        private readonly MySettings _settings;

        public MySettingsController(IOptions<MySettings> optionsAccessor)
        {
            _settings = optionsAccessor.Value;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(new string[] {
                _settings.SqlConnection,
                _settings.CosmosConnection,
                _settings.StorageConnection,
                _settings.AppInsightsInstrumentationKey,
            });
        }
    }
}
