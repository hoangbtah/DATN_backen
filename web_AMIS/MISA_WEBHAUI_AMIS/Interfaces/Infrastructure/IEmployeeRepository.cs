using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA_WEBHAUI_AMIS_Core.Entities;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetbyId(Guid employeeId);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(Guid employeeId);
        IEnumerable<Employee> Getpaging(int pageSize, int pageIndex);
        Employee Search(Guid employeeId);
    }
}
