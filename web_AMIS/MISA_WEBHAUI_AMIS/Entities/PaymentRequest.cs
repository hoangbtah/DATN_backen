using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class PaymentRequest
    {
        public string OrderId { get; set; }
       // public string OrderInfo { get; set; }
        public int Amount { get; set; } // Amount in VND (e.g., 1000000 for 10,000 VND)
        public string BankCode { get; set; }
       // public string IpAddress { get; set; }
    }
}
