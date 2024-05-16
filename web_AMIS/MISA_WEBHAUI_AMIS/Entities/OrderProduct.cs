using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public  class OrderProduct
    {
        public Guid OrderProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Status {  get; set; }
        public string Receiver { get;set; }
        public string Phone { get; set; }
        public string OrderAddress { get; set; }
        public int? Payment {  get; set; }
        public int? StatusPayment { get; set; }
        public Decimal? OrderTotal { get; set; }
    }
}
