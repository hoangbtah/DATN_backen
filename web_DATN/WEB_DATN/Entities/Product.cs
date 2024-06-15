using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName {  get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int ProductSize { get; set; }
        public Guid CatagoryId { get; set; }
        public Guid ManufactorerId { get; set; }
    }
}
