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
    public class EmployeesController : MBaseController<Employee>
    {
        #region Fiels
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion
        #region Method
        /// <summary>
        /// lấy dữ liệu của bảng employee kết hợp với department
        /// </summary>
        /// <returns></returns>
        /// created by BVHoang(29/01/2024)
        [HttpGet("employees")]
        public IActionResult GetEmployeeInnerDepartment()
        {
            try
            {
                var data = _employeeRepository.GetEmployeeInnerDepartment();

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        /// <summary>
        /// lấy ra nhân viên theo tìm kiếm mã , tên , số điện thoại
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// created by BVHoang(31/01/2024)
        [HttpGet("employee/{searchString}")]
        
        public IActionResult GetEmployeeBySearch(string searchString) 
        {
            try
            {
                var data = _employeeRepository.Search(searchString);

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        
        #endregion


    }
}
