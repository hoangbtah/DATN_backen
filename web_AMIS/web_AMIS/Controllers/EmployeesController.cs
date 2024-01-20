using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using web_AMIS.Model;
using System.Text.Unicode;

namespace web_AMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // lấy danh sách nhân viên 
        //200 lấy thành công danh sách
        // 204 không có dữ liệu
        [HttpGet]
        public IActionResult Get()
        {
            // khai báo thông tin database
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection= new MySqlConnection(connectString);
            //2 . lấy dữ liệu
            //2.1 câu lệnh truy vấn dữ liệu
            var sqlCommand = "SELECT * FROM Employee";
            // 2.2 thực hiên lấy dữ liệu 
            var employees= sqlConnection.Query<Employee>(sql:sqlCommand);
            // kết quả trả về 
            return Ok(employees);
        }
        // lấy danh sách nhân viên theo mã nhân viên
        //200 lấy thành công danh sách
        // 204 không có dữ liệu
        [HttpGet("{employeeCode}")]
        public IActionResult GetByEmployeeCode(string employeeCode)
        {
            // khai báo thông tin database
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(connectString);
            //2 . lấy dữ liệu
            //2.1 câu lệnh truy vấn dữ liệu
            var sqlCommand = $"SELECT * FROM Employee where EmployeeCode= @employeeCode";
            // lưu ý : nếu có tham số truyền cho câu lệnh truy vấn sql thì phải sử dụng dynamicParameter
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@employeeCode", employeeCode);
            // 2.2 thực hiên lấy dữ liệu 
            var employee = sqlConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand,param:parameters);
            // kết quả trả về 
            return Ok(employee);
        }
        /// <summary>
        /// phương thức sửa nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            // khai báo thông tin database
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(connectString);

            //2 . lấy dữ liệu
            //2.1 câu lệnh truy vấn dữ liệu
            //var sqlCommand = $"SELECT * FROM Employee where EmployeeCode= @employeeCode";
            // lưu ý : nếu có tham số truyền cho câu lệnh truy vấn sql thì phải sử dụng dynamicParameter
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@employeeCode", employeeCode);
            // 2.2 thực hiên lấy dữ liệu 
            //var employee = sqlConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, param: parameters);
            // kết quả trả về 
            //return Ok(employee);
            using (sqlConnection)
            {
                var sqlCommand = "INSERT INTO Employee (EmployeeId, EmployeeCode, EmployeeName, Gender) " +
                    "VALUES (@employeeId, @employeeCode, @employeeName, @gender)";

                var result = sqlConnection.Execute(sqlCommand, employee);
                return result > 0 ? (IActionResult)Ok() : BadRequest("Insert failed");
            }
        }

        //[HttpPut("{employeeCode}")]
        //public IActionResult UpdateEmployee(string employeeCode, [FromBody] Employee employee)
        //{
        //    using (var sqlConnection = new MySqlConnection(connectionString))
        //    {
        //        var sqlCommand = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber WHERE EmployeeCode = @EmployeeCode";
        //        employee.EmployeeCode = employeeCode; // Ensure the correct employee is updated
        //        var result = sqlConnection.Execute(sqlCommand, employee);
        //        return result > 0 ? (IActionResult)Ok() : NotFound();
        //    }
        //}
        /// <summary>
        /// phương thức xóa nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpDelete("{employeeCode}")]
        public IActionResult DeleteEmployee(string employeeCode)
        {
            // khai báo thông tin database
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(connectString);
            using (sqlConnection )
            {
                var sqlCommand = "DELETE FROM Employee WHERE EmployeeCode = @EmployeeCode";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employeeCode);
                var result = sqlConnection.Execute(sqlCommand, parameters);
                return result > 0 ? (IActionResult)Ok() : NotFound();
            }
        }
    }
}
