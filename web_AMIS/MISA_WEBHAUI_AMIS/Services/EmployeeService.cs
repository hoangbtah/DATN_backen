using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using System.Reflection.Metadata.Ecma335;


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
           
           CheckEmployee(employee);
            var isDuplicate = _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
            if (isDuplicate == true)
            {
                throw new MISAvalidateException(Resources.ResourceVN.EmployeeCodeDuplicate);
            }

        }
        /// <summary>
        /// kiểm tra nhân viên khi cập nhật
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="entitiId"></param>
        /// <exception cref="MISAvalidateException"></exception>
        /// created by BVHoang(27/01/2024)
        protected override void ValidateEmployeeForUpdate(Employee employee,Guid entitiId)
        {
            var isDuplicateForUpdate = _employeeRepository.CheckDuplicateForUpdate(entitiId, employee.EmployeeCode);
            CheckEmployee(employee);
            var isDuplicate = _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);

            if (isDuplicateForUpdate == false)
            {
                if (isDuplicate == true)
                {
                    throw new MISAvalidateException(Resources.ResourceVN.EmployeeCodeDuplicate);
                }
            }

        }
        /// <summary>
        /// check các thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="MISAvalidateException"></exception>
        /// created by BVHoang(27/01/2024)
        public void CheckEmployee(Employee employee)
        {
            // check email
            // kiểm tra xem có nhập email không nếu có check định dạng, nếu không thì thôi
            if (!String.IsNullOrEmpty(employee.Email))
            {
                if (CheckEmail(employee.Email) == false)
                {
                    throw new MISAvalidateException(Resources.ResourceVN.ErrorEmail);
                }
            }
            //// validate dữ liệu
            //// check ngày sinh
            if (employee.DateOfbrith > DateTime.Now)
            {

                throw new MISAvalidateException(Resources.ResourceVN.ErrorDateOfBrith);
            }

        }
    //    public bool CheckDuplicateForUpdate(string employee)
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
