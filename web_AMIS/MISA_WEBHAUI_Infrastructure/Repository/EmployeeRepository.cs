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

        public int Delete(Guid employeeId)
        {
            throw new NotImplementedException();
        }

      

        public IEnumerable<Employee> Getpaging(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee employee)
        {
          
            // .khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(ConnectString);
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

        public int Update(Employee employee,Guid employeeId)
        {
            // .khởi tạo chuỗi kết nối với maria db
            var sqlConnection = new MySqlConnection(ConnectString);
            var sqlCommand = "Proc_UpdateEmployee";

            // 4 trả thông tin về cho client 
            // thực hiện thêm mới mã nhân viên
            //employee.EmployeeId = Guid.NewGuid();

            var result = sqlConnection.Execute(sql: sqlCommand, param: employee, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
