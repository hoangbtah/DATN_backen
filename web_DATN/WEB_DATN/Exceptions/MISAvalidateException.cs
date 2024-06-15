using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Exceptions
{
    public class MISAvalidateException:Exception
    {
        #region Property
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        string? MsgErrorvalidate = null;
        #endregion

        #region Method
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="msg"></param>
        public MISAvalidateException(string msg)
        {
            this.MsgErrorvalidate = msg;
        }
        /// <summary>
        /// ghi đè Message trả về thông báo 
        /// </summary>
        public override string Message
        {
            get
            {
                return MsgErrorvalidate;
            }
        }
        #endregion
    }
}
