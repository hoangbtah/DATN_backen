using Microsoft.Extensions.Configuration;
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
            //string resetUrl = $"https://yourwebsite.com/resetpassword?token={resetToken}";
            //string body = $"Vui lòng nhấp vào <a href='{resetUrl}'>đây</a> để đặt lại mật khẩu.";
            string body = $"Mật khẩu mới cho tài khoản của bạn là {newPassword}";

            var message = new MailMessage("your@email.com", email, "Mật khẩu mới", body);
            message.IsBodyHtml = true;
            await _smtpClient.SendMailAsync(message);
          
        }
    }

}
