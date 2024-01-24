using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA_WEBHAUI_AMIS_Core.Entities;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Created By : BVHoang(24/1/2024)
        int InsertServie(Employee employee);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        int UpdateServie(Employee employee,Guid employeeId);
    }
}
