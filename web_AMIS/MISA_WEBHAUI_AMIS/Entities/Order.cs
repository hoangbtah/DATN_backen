using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public  class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status {  get; set; }
    }
}
