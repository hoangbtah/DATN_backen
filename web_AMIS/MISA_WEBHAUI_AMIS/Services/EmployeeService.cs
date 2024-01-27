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
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Fields
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Contructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Kiểm tra dữ liệu nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="MISAvalidateException"></exception>
        /// created by BVHoang(27/01/2024)
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
            // check email
            // kiểm tra xem có nhập email không nếu có check định dạng, nếu không thì thôi
            if (!String.IsNullOrEmpty(employee.Email))
            {
                if (CheckEmail(employee.Email) == false)
                {
                    throw new MISAvalidateException(Resources.ResourceVN.ErrorEmail);
                }
            }

        }
        /// <summary>
        /// kiểm tra email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        public bool CheckEmail(string email)
        {
            // cắt đi nhưng khoảng trắng của email
            var trimEmail = email.Trim();
            // kiểm tra email có đúng định dạng 
            if (trimEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimEmail;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
