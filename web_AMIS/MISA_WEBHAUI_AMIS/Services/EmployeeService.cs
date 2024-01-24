using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public int InsertServie(Employee employee)
        {
            // validate dữ liệu
            return 1;
        }

        public int UpdateServie(Employee employee, Guid employeeId)
        {
            return 1;
        }
    }
}
