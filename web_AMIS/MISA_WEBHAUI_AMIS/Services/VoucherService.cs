using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Services
{
    public class VoucherService:BaseService<Voucher>,IVoucherService
    {

        #region Fields
        IVoucherRepository _voucherRepository;
        #endregion

        #region Contructor
        public VoucherService(IVoucherRepository voucherRepository) : base(voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }
        #endregion
    }
}
