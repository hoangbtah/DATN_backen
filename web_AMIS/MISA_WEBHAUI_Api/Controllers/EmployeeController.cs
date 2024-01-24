using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_Infrastructure.Repository;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;

        public EmployeeController(IEmployeeRepository employeeRepository,IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var employees= _employeeRepository.GetAll();

            
            return Ok(employees);
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetById(Guid employeeId)
        {
            var employee= _employeeRepository.GetbyId(employeeId);
            return Ok();
        }
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                var res= _employeeService.InsertServie(employee);
                return StatusCode(201, res);
            }
            catch(MISAvalidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    useMsg = ex.Message,
                    data = employee,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                throw;
            }
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
