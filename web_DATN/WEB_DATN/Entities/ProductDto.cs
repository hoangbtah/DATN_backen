using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class ProductDto
    {
        public Guid ProductId { get; set; } 
        public Guid UserId { get; set; } 
        public string ProductName { get; set; } 
        public string Image { get; set; } 
        public int Quantity { get; set; }  
        public decimal Price { get; set; }
    }
}
