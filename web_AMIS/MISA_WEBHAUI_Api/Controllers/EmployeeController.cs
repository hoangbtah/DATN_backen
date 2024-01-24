using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetById(Guid employeeId)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult Put(Guid employeeId,Employee employee)
        {
            return Ok();
        }
        [HttpDelete("{employeeId}")]
        public IActionResult Delete(Guid employeeId)
        {
            return Ok();
        }

    }
}
