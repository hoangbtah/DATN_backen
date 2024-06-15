using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class UserVoucher
    {
        public Guid UserVoucherId {  get; set; }
        public Guid UserId { get; set; }
        public Guid VoucherId { get; set; }
    }
}
