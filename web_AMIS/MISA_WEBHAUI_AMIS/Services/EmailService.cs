using Microsoft.Extensions.Configuration;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Services
{
  
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(IConfiguration configuration)
        {
            _smtpClient = new SmtpClient
            {
                Host = configuration["SmtpSettings:Host"],
                Port = int.Parse(configuration["SmtpSettings:Port"]),
                Credentials = new NetworkCredential(
                    configuration["SmtpSettings:UserName"],
                    configuration["SmtpSettings:Password"]),
                EnableSsl = true // Tùy thuộc vào cấu hình của bạn
            };
        }

        public async Task SendResetPasswordEmailAsync(string email, string newPassword)
        {
            
            string body = $"Mật khẩu mới cho tài khoản của bạn là {newPassword} vui lòng không cung cấp cho bất kì ai." +
                $"Để nâng cao bảo mật bạn hãy thay đổi mật khẩu sau lần đăng nhập đầu tiên .";

            var message = new MailMessage("your@email.com", email, "Mật khẩu mới", body);
            message.IsBodyHtml = true;
            await _smtpClient.SendMailAsync(message);
          
        }
        public async Task SendVoucher(Voucher voucher,string email)
        {
            string body = "";
            string formattedNumber = String.Format("{0:N0}", voucher.DiscountMoney); // "N0" nghĩa là không có số thập phân
            if (voucher.DiscountMoney>0)
            {
                body += $"ESHOPER tặng bạn Voucher giảm giá {voucher.DiscountMoney } đ.{voucher.DecriptionUse} kể từ" +
                    $" ngày {voucher.StartDateVoucher} đến ngày {voucher.EndDateVoucher} !";
            }    
            else if (voucher.PercentVoucher > 0)
            {
                body += $"ESHOPER tặng bạn Voucher giảm giá {voucher.PercentVoucher*100} %.{voucher.DecriptionUse} kể từ" +
                    $" ngày {voucher.StartDateVoucher} đến ngày {voucher.EndDateVoucher} !";
            }
            body += $"Mã voucher ({voucher.VoucherCode})";
           

            var message = new MailMessage("your@email.com", email, "Thông báo", body);
            message.IsBodyHtml = true;
            await _smtpClient.SendMailAsync(message);

        }
    }

}
