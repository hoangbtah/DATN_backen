using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web_AMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hoangbv");
        }
        [HttpPost]
        public int Post()
        {
            return 1;
        }
    }
}
