using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singleton2_WebAPI_DBService_Example.Services;

namespace Singleton2_WebAPI_DBService_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("[Action]")]
        public IActionResult X()
        {
            DatabaseService databaseService = DatabaseService.GetInstance;
            databaseService.Connect();
            databaseService.Disconnect();
            return Ok(databaseService.Count);
        }

        [HttpGet("[Action]")]
        public IActionResult Y()
        {
            DatabaseService databaseService = DatabaseService.GetInstance;
            databaseService.Connect();
            databaseService.Disconnect();
            return Ok(databaseService.Count);
        }
    }
}
