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
    public class EmployeeRepository : IEmployeeRepository
    {
        public bool CheckDuplicateCode(string employeeCode)
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
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Delete(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
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
                return employees;
            

            
        }

        public Employee GetbyId(Guid employeeId)
        {
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
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
            return employee;
        }

        public IEnumerable<Employee> Getpaging(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee employee)
        {
            // khai báo thông tin database
            var connectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
            // .khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(connectString);
            var sqlCommand = "Proc_InsertEmployee";
            
            // 4 trả thông tin về cho client 
            // thực hiện thêm mới mã nhân viên
            employee.EmployeeId = Guid.NewGuid();

            var result = sqlConnection.Execute(sql: sqlCommand, param: employee, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public Employee Search(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public int Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
