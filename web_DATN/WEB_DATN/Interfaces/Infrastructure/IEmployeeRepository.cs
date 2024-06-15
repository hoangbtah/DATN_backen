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
        /// <summary>
        /// phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        IEnumerable<Employee> Getpaging(int pageSize, int pageIndex);
        /// <summary>
        /// tìm kiếm
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
       object Search(string employeeCode);
        /// <summary>
        /// check trùng mã
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        bool CheckDuplicateCode(string searchString);
        /// <summary>
        /// kiểm tra mã nhân viên cho việc cập nhật dữ liệu
        /// </summary>
        /// <param name="enployeeId"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        bool CheckDuplicateForUpdate(Guid enployeeId, string employeeCode);
        /// <summary>
        /// lấy nhân viên kèm theo thông tin phòng ban
        /// </summary>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        object GetEmployeeInnerDepartment();

    }
}
