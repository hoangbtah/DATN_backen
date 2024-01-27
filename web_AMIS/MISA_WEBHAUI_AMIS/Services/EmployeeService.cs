using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;


namespace MISA_WEBHAUI_AMIS_Core.Services
{
    public class EmployeeService :BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository):base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        protected override void ValidateEmployee(Employee employee)
        {
            //// validate dữ liệu
            //// check ngày sinh
            if (employee.DateOfbrith > DateTime.Now)
            {

                throw new MISAvalidateException(Resources.ResourceVN.ErrorDateOfBrith);
            }
            // check trùng mã 
            var isDuplicate = _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
            if (isDuplicate == true)
            {
                throw new MISAvalidateException(Resources.ResourceVN.EmployeeCodeDuplicate);
            }

        }
        //public int InsertServie(Employee employee)
        //{

        //    // validate dữ liệu
        //    // check empty code
        //    if (string.IsNullOrEmpty(employee.EmployeeCode))
        //    {

        //        throw new MISAvalidateException("Mã nhân viên không được phép để trống");
        //    }
        //    // validate dữ liệu
        //    // check empty name
        //    if (string.IsNullOrEmpty(employee.EmployeeName))
        //    {

        //        throw new MISAvalidateException("Tên nhân viên không được phép để trống");
        //    }
        //    // validate dữ liệu

        //    // validate dữ liệu
        //    // check ngày sinh
        //    if (employee.DateOfbrith>DateTime.Now)
        //    {

        //        throw new MISAvalidateException("Ngày sinh không được lớn hơn ngày hiện tại");
        //    }
        //    // check trùng mã 
        //    var isDuplicate= _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
        //    if (isDuplicate == true)
        //    {
        //        throw new MISAvalidateException("Mã nhân viên này đã tồn tại vui lòng kiểm tra lại");
        //    }
        //    var res= _employeeRepository.Insert(employee);
        //    return res;
        //}

        //public int UpdateServie(Employee employee, Guid employeeId)
        //{
        //    return 1;
        //}
    }
}
