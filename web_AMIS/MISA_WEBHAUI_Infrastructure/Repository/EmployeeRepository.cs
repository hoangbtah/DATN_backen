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
        #region Method
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
        /// kiểm tra mã nhân viên sau khi update có giống mã nhân viên cũ không
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// created by BVHoang(30/01/2024)
        public bool CheckDuplicateForUpdate(Guid employeeId, string employeeCode)
        {
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(ConnectString);
            // câu lệnh thực hiện lấy ra nhân viên có mã giống với mã truyền vào 
            var sqlCheck = "Select EmployeeCode FROM Employee WHERE EmployeeId = @employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", employeeId);
            var result = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, parameters);
            if (result != null && result.Equals(employeeCode) == true)
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
        /// Tìm kiếm nhân viên theo mã , tên, số điện thoại
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by BVHoang(27/01/2024)

        public object Search(string searchString)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                var sqlCommand = "SELECT * FROM Employee e INNER JOIN Department d ON e.DepartmentId = d.DepartmentId WHERE EmployeeCode = @employeeCode " +
                    " OR EmployeeName LIKE @employeeName " +
                    " OR PhoneNumber LIKE @phoneNumber ";

                // Sử dụng '%' để thực hiện tìm kiếm một phần của tên
                var parameters = new DynamicParameters();
                parameters.Add("@employeeCode", searchString);
                parameters.Add("@employeeName", "%" + searchString + "%");
                parameters.Add("@phoneNumber", "%" + searchString + "%");

                var employees = SqlConnection.Query<object>(sqlCommand, parameters);
                return employees;
            }
        }
        /// <summary>
        /// Lấy danh sách nhân viên cùng với thông tin phòng ban
        /// </summary>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        public object GetEmployeeInnerDepartment()
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT * FROM Employee e INNER JOIN Department d ON e.DepartmentId = d.DepartmentId";
                var employees = SqlConnection.Query<object>(sqlCommand);
                return employees;
            }
        }
        #endregion
    }
}
