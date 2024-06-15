using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class ErrorService
    {
        #region Property
        /// <summary>
        /// Lỗi cho lập trình viên
        /// </summary>
        public string DevMsg { get; set; }
        /// <summary>
        /// Lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }
        /// <summary>
        /// Dữ liệu trả về khi lỗi
        /// </summary>
        public object Data { get; set; }
        #endregion
    }
}
