using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Exceptions
{
    public class MISAvalidateException:Exception
    {
        string? MsgErrorvalidate = null;
        public MISAvalidateException(string msg)
        {
            this.MsgErrorvalidate = msg;
        }
        public override string Message
        {
            get
            {
                return MsgErrorvalidate;
            }
        }
    }
}
