using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Services
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        public int Delete(Guid employeeId)
        {
            // kết nối daatabase 

            // thực hiện xóa 
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetbyId(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Getpaging(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee employee)
        {
            throw new NotImplementedException();
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
