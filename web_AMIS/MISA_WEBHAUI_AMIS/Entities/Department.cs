using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class Department
    {
        #region Property
        /// <summary>
        /// khóa chính 
        /// </summary>
        /// created by BVHoang(27/01/2024)
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// created by BVHoang(27/01/2024)
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// created by BVHoang(27/01/2024)
        public string DepartmentName { get; set; }
        /// <summary>
        /// ngày tạo
        /// </summary>
        /// created by BVHoang(27/01/2024)
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// người tạo
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? CreateBy { get; set; }
        /// <summary>
        /// ngày xác thực
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public DateTime? ModifileDate { get; set; }
        /// <summary>
        /// người xác thực
        /// </summary>
        /// created by BVHoang(27/01/2024)
        public string? ModifileBy { get; set; }
        #endregion
    }
}
