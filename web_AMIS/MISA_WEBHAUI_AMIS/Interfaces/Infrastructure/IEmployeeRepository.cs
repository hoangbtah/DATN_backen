using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA_WEBHAUI_AMIS_Core.Entities;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        IEnumerable<Employee> Getpaging(int pageSize, int pageIndex);
        Employee Search(Guid employeeId);
        bool CheckDuplicateCode(string employeeCode);
    }
}
