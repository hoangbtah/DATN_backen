using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class Voucher
    {
        public Guid VoucherId { get; set; }
        public string? VoucherCode { get; set; }
        public DateTime StartDateVoucher { get; set; }
        public DateTime EndDateVoucher { get; set; }
        public string? DecriptionUse { get; set; }  
        public int? MaxximumUse { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public decimal? PercentVoucher { get; set; }
        public decimal ? DiscountMoney { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
