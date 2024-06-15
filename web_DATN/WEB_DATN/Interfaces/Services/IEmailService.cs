using MISA_WEBHAUI_AMIS_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string email, string resetToken);
        Task SendVoucher(Voucher voucher,string email);

    }
}
