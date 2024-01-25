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
        // khai báo chuỗi kết nối database
        readonly string connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
        // lấy danh sách nhân viên 
        //200 lấy thành công danh sách
        // 204 không có dữ liệu
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // khai báo thông tin database
               
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
        [HttpGet("{employeeId}")]
        public IActionResult GetByEmployeeCode(Guid employeeId)
        {
            try
            {
               
                // 1.khởi tạo chuỗi kết nối với maria db
                var sqlConnection = new MySqlConnection(connectString);
                //2 . lấy dữ liệu
                //2.1 câu lệnh truy vấn dữ liệu
                var sqlCommand = $"SELECT * FROM Employee where EmployeeId= @employeeId";
                // lưu ý : nếu có tham số truyền cho câu lệnh truy vấn sql thì phải sử dụng dynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@employeeId", employeeId);
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
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorList.Add("mã nhân viên không được để trống");

                }
                //1.2 thông tin họ và tên không được phép để trống 
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    errorList.Add("tên nhân viên không được để trống");


                }
                //1/3 Email phải đúng định dạng 
                // kiểm tra xem có nhập email không nếu có check định dạng, nếu không thì thôi
                if (!String.IsNullOrEmpty(employee.Email))
                {

                    //errorList.Add("Email không hợp lệ");
                    if (CheckEmail(employee.Email) == false)
                    {
                        errorList.Add("Email không hợp lệ");
                    }
                }
               

                // mã nhân viên không được phép trùng
                if (CheckEmployeeCode(employee.EmployeeCode) == true)
                {
                    errorList.Add("Mã nhân viên không được phép trùng");
                }
                
                // 1.4 ngày sinh không được lớn hơn ngày hiện tại 

                if (errorList.Count > 0)
                {
                    error.UserMsg = "Dữ liệu đầu vào không hợp lệ";
                    error.Data = errorList;
                    return BadRequest(error);
                }
                // 2 khởi tạo kết nối vơi databasse
                // khai báo thông tin database
               
                var sqlConnection = new MySqlConnection(connectString);
                //3 thực hiên thêm mới dữ liệu
                var sqlCommand = "Proc_InsertEmployee";
                //var sqlCommand = "INSERT INTO Employee (EmployeeId,EmployeeCode, EmployeeName, Gender,Email, DepartmentId)" +
                //    "VALUES (@EmployeeId,@EmployeeCode, @EmployeeName,@Gender,@Email,@DepartmentId)";
                // 4 trả thông tin về cho client 
                // thực hiện thêm mới mã nhân viên
                employee.EmployeeId = Guid.NewGuid();

                var result = sqlConnection.Execute(sql:sqlCommand,param: employee, commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(201,result);

            }

            catch (Exception ex)
            {
                var error = new ErrorService();
                error.DevMsg = ex.Message;
                error.UserMsg = Resource.ResourceVN.Error_Exception;
                
                return StatusCode(500, error);
            }
          

        }
        /// <summary>, commandType : System.Data.CommandType.StoredProcedure
        /// phương thức sửa nhân viên theo mã nhân viên
        /// chú ý phải loại trừ chính cái muốn sửa 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>

        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(Guid employeeId,Employee employee)
        {
            try
            {
                // khai báo thông tin 

                var error = new ErrorService();
                var errorList = new List<string>();

                // 1. validate dữ liệu
                //1.1 thông tin mã nhân viên bắt buộc nhập 
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorList.Add("mã nhân viên không được để trống");

                }
                //1.2 thông tin họ và tên không được phép để trống 
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    errorList.Add("tên nhân viên không được để trống");


                }
                //1/3 Email phải đúng định dạng 
                // kiểm tra xem có nhập email không nếu có check định dạng, nếu không thì thôi
                if (!String.IsNullOrEmpty(employee.Email))
                {

                    //errorList.Add("Email không hợp lệ");
                    if (CheckEmail(employee.Email) == false)
                    {
                        errorList.Add("Email không hợp lệ");
                    }
                }


                // mã nhân viên không được phép trùng
                if (CheckEmployeeCode(employee.EmployeeCode) == true)
                {
                    errorList.Add("Mã nhân viên không được phép trùng");
                }

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
                var sqlCommand = "Proc_UpdateEmployee";
                //var sqlCommand = "INSERT INTO Employee (EmployeeId,EmployeeCode, EmployeeName, Gender,Email, DepartmentId)" +
                //    "VALUES (@EmployeeId,@EmployeeCode, @EmployeeName,@Gender,@Email,@DepartmentId)";
                // 4 trả thông tin về cho client 
                // thực hiện thêm mới mã nhân viên
                //employee.EmployeeId = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("mEmployeeId", employeeId);
                parameters.Add("mEmployeeCode", employee.EmployeeCode);
                parameters.Add("mEmployeeName", employee.EmployeeName);
                parameters.Add("mGender", employee.Gender);
                parameters.Add("mIdentityCode", employee.IdentityCode);
                parameters.Add("mIdentityDate", employee.IdentityDate);
                parameters.Add("mPosition", employee.Position);
                parameters.Add("mIdentityPlace", employee.IdentityPlace);
                parameters.Add("mAddress", employee.Address);
                parameters.Add("mPhonenumber", employee.PhoneNumber);
                parameters.Add("mLandlinePhone", employee.LandlinePhone);
                parameters.Add("mEmail", employee.Email);
                parameters.Add("mBankAccount", employee.BankAccount);
                parameters.Add("mBankName", employee.BankName);
                parameters.Add("mBranch", employee.Branch);
                parameters.Add("mCreateDate", employee.CreateDate);
                parameters.Add("mCreateBy", employee.CreateBy);
                parameters.Add("mModifyDate", employee.ModifileDate);
                parameters.Add("mModifyBy", employee.ModifileBy);
               
                parameters.Add("mDepartmentId", employee.DepartmentId);

                var result = sqlConnection.Execute(sql: sqlCommand ,param:parameters,commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(201, result);

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
        /// phương thức xóa nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(Guid employeeId)
        {
            try
            {
                // khai báo thông tin database
                var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
                // 1.khởi tạo chuỗi kết nối với maria db
                var sqlConnection = new MySqlConnection(connectString);
                // câu lệnh thực hiện xóa 
                 var sqlCommand = "DELETE FROM Employee WHERE EmployeeCode = @EmployeeId";
                 var parameters = new DynamicParameters();
                 parameters.Add("@EmployeeId", employeeId);
                 var employee = sqlConnection.Query(sqlCommand, parameters);
                 return Ok(employee);
                
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
           
        }
        /// <summary>
        /// xử lý ngoại lệ xảy ra gửi thông báo lỗi
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = Resource.ResourceVN.Error_Exception;
            return StatusCode(500, error);
        }
        /// <summary>
        /// kiểm tra mã nhân viên không được trùng 
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        private bool CheckEmployeeCode(string employeeCode)
        {
            // khai báo thông tin database
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
            // 1.khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(connectString);
            // câu lệnh thực hiện lấy ra nhân viên có mã giống với mã truyền vào 
            var sqlCheck = "Select EmployeeCode FROM Employee WHERE EmployeeCode = @employeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeCode", employeeCode);
            var result = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, parameters);
            if(result!= null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool CheckEmail(string email)
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
    }
}
