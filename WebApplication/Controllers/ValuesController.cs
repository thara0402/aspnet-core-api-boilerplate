using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MySettings _settings;

        public ValuesController(IOptions<MySettings> optionsAccessor)
        {
            _settings = optionsAccessor.Value;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(_settings.SqlConnection);
        }
    }
}
