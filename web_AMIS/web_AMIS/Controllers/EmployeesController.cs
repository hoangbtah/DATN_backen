using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using web_AMIS.Model;
using System.Text.Unicode;
using System.Reflection;

namespace web_AMIS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // lấy danh sách nhân viên 
        //200 lấy thành công danh sách
        // 204 không có dữ liệu
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // khai báo thông tin database
                var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
                // 1.khởi tạo chuỗi kết nối với maria db
                var sqlConnection = new MySqlConnection(connectString);
                //2 . lấy dữ liệu
                //2.1 câu lệnh truy vấn dữ liệu
                var sqlCommand = "SELECT * FROM Employee";
                // 2.2 thực hiên lấy dữ liệu 
                var employees = sqlConnection.Query<Employee>(sql: sqlCommand);
                // kết quả trả về 
                return Ok(employees);
            }

            catch(Exception ex)
            {
                return HandleException(ex);
            }

        }
        // lấy danh sách nhân viên theo mã nhân viên
        //200 lấy thành công danh sách
        // 204 không có dữ liệu
        [HttpGet("{employeeCode}")]
        public IActionResult GetByEmployeeCode(string employeeCode)
        {
            try
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
                var employee = sqlConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, param: parameters);
                // kết quả trả về 
                return Ok(employee);
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
          
        }
       /// <summary>
       /// thêm mới nhân viên 
       /// </summary>
       /// <param name="employee"></param>
       /// <returns>
       /// 201 thêm mới thành công 
       /// 400 - dữ liệu đầu vào không hợp lệ
       /// 500- có exception 
       /// </returns>
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            try
            {
                // khai báo thông tin 
               
                var error = new ErrorService();
                var errorList= new List<string>();
                // 1. validate dữ liệu
                //1.1 thông tin mã nhân viên bắt buộc nhập 
                if (!string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorList.Add("mã nhân viên không được để trống");
                   
                }
                //1.2 thông tin họ và tên không được phép để trống 
                if (!string.IsNullOrEmpty(employee.EmployeeName))
                {
                    errorList.Add("tên nhân viên không được để trống");
                    
                    
                }
                //1/3 Email phải đúng định dạng 

                // 1.4 ngày sinh không được lớn hơn ngày hiện tại 

                if (errorList.Count > 0)
                {
                    error.UserMsg = "Dữ liệu đầu vào không hợp lệ";
                    error.Data = errorList;
                    return BadRequest(error);
                }
                // 2 khởi tạo kết nối vơi databasse
                // khai báo thông tin database
                var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
                // .khởi tạo chuỗi kết nối với maria db
                var sqlConnection = new MySqlConnection(connectString);
                //3 thực hiên thêm mới dữ liệu
                // 4 trả thông tin về cho client 

                return BadRequest(201);

            }

            catch (Exception ex)
            {
                var error = new ErrorService();
                error.DevMsg = ex.Message;
                error.UserMsg = Resource.ResourceVN.Error_Exception;
                return StatusCode(500, error);
            }
          

        }
        /// <summary>
        /// phương thức sửa nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="employee"></param>
        /// <returns></returns>

        [HttpPut("{employeeCode}")]
        public IActionResult UpdateEmployee(string employeeCode, [FromBody] Employee employee)
        {
            // khai báo thông tin database
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(connectString);
          
                var sqlCommand = "UPDATE Employee SET EmployeeId = @employeeId, EmployeeCode = @employeeCode, Employeename = @employeeName," +
                " Gender = @gender,DepartmentId=@departmentId WHERE EmployeeCode = @EmployeeCode";
                employee.EmployeeCode = employeeCode; // Ensure the correct employee is updated
                var result = sqlConnection.Execute(sqlCommand, employee);
                return result > 0 ? (IActionResult)Ok() : NotFound();
            
        }
        /// <summary>
        /// phương thức xóa nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpDelete("{employeeCode}")]
        public IActionResult DeleteEmployee(string employeeCode)
        {
            try
            {
                // khai báo thông tin database
                var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
                // 1.khởi tạo chuỗi kết nối với maria db
                var sqlConnection = new MySqlConnection(connectString);
                // câu lệnh thực hiện xóa 
                 var sqlCommand = "DELETE FROM Employee WHERE EmployeeCode = @EmployeeCode";
                 var parameters = new DynamicParameters();
                 parameters.Add("@EmployeeCode", employeeCode);
                 var employee = sqlConnection.Query(sqlCommand, parameters);
                 return Ok(employee);
                
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
           
        }
        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = Resource.ResourceVN.Error_Exception;
            return StatusCode(500, error);
        }
    }
}
