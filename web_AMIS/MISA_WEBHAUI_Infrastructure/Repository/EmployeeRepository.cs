using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_Infrastructure.Repository
{
    public class EmployeeRepository :BaseRepository<Employee>, IEmployeeRepository
    {
        // khai báo thông tin database
        /// <summary>
        /// kiểm tra trùng mã nhân viên 
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        public bool CheckDuplicateCode(string employeeCode)
        {
            // khai báo thông tin database
           
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(ConnectString);
            // câu lệnh thực hiện lấy ra nhân viên có mã giống với mã truyền vào 
            var sqlCheck = "Select EmployeeCode FROM Employee WHERE EmployeeCode = @employeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeCode", employeeCode);
            var result = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, parameters);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// phân trang 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by BVHoang(27/01/2024)
        public IEnumerable<Employee> Getpaging(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Tìm kiếm nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by BVHoang(27/01/2024)
        public Employee Search(Guid employeeId)
        {
            throw new NotImplementedException();
        }
        public object GetEmployeeInnerDepartment()
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                //var sqlCommand = " SELECT EmployeeId, EmployeeCode, EmployeeName, Gender,GenderName" +
                //    " IdentityCode, IdentityDate, `Position`, IdentityPlace, Address, PhoneNumber," +
                //    " LandlinePhone, Email, BankAccount, BankName,Branch, e.CreateDate, e.CreateBy," +
                //    " e.ModifileDate, e.ModifileBy, e.DepartmentId,d.DepartmentName ,d.DepartmentCode " +
                //    " FROM employee e INNER JOIN department d ON e.DepartmentId = d.DepartmentId";
                var sqlCommand = "SELECT * FROM employee e INNER JOIN department d ON e.DepartmentId = d.DepartmentId";
                var employees = SqlConnection.Query<object>(sqlCommand);
                return employees;
            }
        }
    }
}
