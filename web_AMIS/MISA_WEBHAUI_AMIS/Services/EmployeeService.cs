using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISA_WEBHAUI_AMIS_Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeService _employeeService;
        public EmployeeService(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public int InsertServie(Employee employee)
        {
           
            // validate dữ liệu
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {

                throw new MISAvalidateException("Mã nhân viên không được phép để trống");
            }
            
            return 1;
        }

        public int UpdateServie(Employee employee, Guid employeeId)
        {
            return 1;
        }
    }
}
