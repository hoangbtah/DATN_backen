using MISA_WEBHAUI_AMIS_Core.Enum;
using MISA_WEBHAUI_AMIS_Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class Employee
    {
        #region Property
        /// <summary>
        /// khóa chính bảng nhân viên
        /// </summary>
        /// created by BVHoang(27/01/2024)
        [PrimayKey]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        [NotEmpty]
        [PropertyName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        [NotEmpty]
        [PropertyName("Tên nhân viên")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public DateTime? DateOfbrith { get; set; }
        /// <summary>
        /// giới tính kiểu 0 ,1 
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// tên giới tính là nam hay nữ
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        [NotMapInsert]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Female:
                        return Resources.ResourceVN.Gender_Female;
                    case Enum.Gender.Male:
                        return Resources.ResourceVN.Gender_Male;
                    case Enum.Gender.Other:
                        return Resources.ResourceVN.Gender_Other;
                    default:
                        return null;
                }

            }
        }
        /// <summary>
        /// số chứng minh nhân dân
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? IdentityCode { get; set; }
        /// <summary>
        /// ngày cấp chứng minh nhân dân
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// vị trí công việc
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? Position { get; set; }
        /// <summary>
        /// Nơi cấp chúng minh nhân dân
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? Address { get; set; }
        /// <summary>
        /// số điện thoại đi động của nhân viên
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// số điện thoại cố định
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? LandlinePhone { get; set; }
        /// <summary>
        /// địa chỉ email
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? Email { get; set; }
        /// <summary>
        /// số tài khoản ngân hàng
        /// </summary>
        /// created by BVHoang(27/01/2024)
        public string? BankAccount { get; set; }
        /// <summary>
        /// tên ngân hàng
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public string? BankName { get; set; }
        /// <summary>
        /// chi nhánh
        /// </summary>
        /// created by BVHoang(27/01/2024)
        public string? Branch { get; set; }
        /// <summary>
        /// ngày tạo
        /// </summary>
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
        /// <summary>
        /// khóa chính bảng phòng ban
        /// </summary>
        /// /// created by BVHoang(27/01/2024)
        public Guid DepartmentId { get; set; }
        #endregion

    }
}
