using MISA_WEBHAUI_AMIS_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IVoucherRepository:IBaseRepository<Voucher>
    {
        Task<int> CreateVoucher(Voucher voucher);
        object getVoucherByUser(Guid userId);
        object getVoucherByVoucherCode(string voucherCode);
    }
}
