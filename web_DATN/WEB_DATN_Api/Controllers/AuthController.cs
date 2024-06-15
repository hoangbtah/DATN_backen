using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        IConfiguration _conficguration;
        IAuthRepository _authRepository;
        IEmailService _emailService;
        
        
        
        public AuthController(IAuthRepository authRepository,IConfiguration configuration,IEmailService emailService)
        {
            _conficguration = configuration;
            _authRepository = authRepository;
            _emailService= emailService;
           
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = request.Name,
                    Role = request.Role,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Address = request.Address,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Active = 1,
                };

                int affectedRows = await _authRepository.CreateUserAsync(user);

               
                 return Ok(user);
                
            }
           
            catch (Exception ex)
            {

                return   StatusCode(500, ex.Message);
            }
           
        }
        [HttpPost("changePassword")]
        [Authorize] // Đảm bảo chỉ người dùng hiện tại mới có quyền thay đổi mật khẩu của mình
        public async Task<ActionResult> ChangePassword(ChangePasswordDto request)
        {
            var user = await _authRepository.GetUserByUsernameAsync(request.Username);

            if (user == null)
            {
                return BadRequest("Tên đăng nhập không tồn tại.");
            }

            // Xác thực mật khẩu cũ
            bool isOldPasswordValid = await _authRepository.VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt);

            if (!isOldPasswordValid)
            {
                return BadRequest("Mật khẩu cũ không đúng.");
            }

            // Tạo hash và salt mới cho mật khẩu mới
            byte[] newPasswordHash, newPasswordSalt;
            CreatePasswordHash(request.NewPassword, out newPasswordHash, out newPasswordSalt);

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;

            await _authRepository.UpdateUserAsync(user);

            return Ok("Mật khẩu đã được thay đổi thành công.");
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _authRepository.GetUserByUsernameAsync(request.Name);

            if (user == null)
            {
                return BadRequest("Tên đăng nhập không tồn tại.");
            }

            bool isPasswordValid = await _authRepository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

            if (!isPasswordValid)
            {
                return BadRequest("Mật khẩu không đúng.");
            }
            // kiểm tra tài khoản có đang bị khóa không
            var userExam = await _authRepository.ExamUser(request.Name, user.PasswordHash, user.PasswordSalt);
            if (userExam == null)
            {
                return BadRequest("Tài khoản đã bị khóa");

            }

            // Đăng nhập thành công: tạo và trả về token JWT
            string token = CreateToken(user);
            return Ok(token);     
        }
        [HttpPost("user")]
        public async Task<ActionResult<string>> GetUser(UserDto request)
        {
            var user = await _authRepository.GetUserByUsernameAsync(request.Name);

            if (user == null)
            {
                return BadRequest("Tên đăng nhập không tồn tại.");
            }

            bool isPasswordValid = await _authRepository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

            if (!isPasswordValid)
            {
                return BadRequest("Mật khẩu không đúng.");
            }

            // Đăng nhập thành công: tạo và trả về token JWT
            return Ok(user);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                 new Claim(ClaimTypes.Role,user.Role)
            };
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
                (System.Text.Encoding.UTF8.GetBytes(_conficguration.GetSection("AppSettings:Token").Value));


            var creads = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creads);

           
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
        private void CreatePasswordHash(string password, out byte[] passwordhand, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhand = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getname")]
        public IActionResult getname()
        {
            return Ok("my name is ");
        }
        [Authorize]
        [HttpGet("getpassword")]
        public IActionResult getpassword()
        {
            return Ok("my password");
        }

        [HttpGet("getstring")]
        public IActionResult getstring()
        {
            return Ok("my string");
        }
        /// <summary>
        /// chúc năng quên mật khẩu
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.Email);

            // Kiểm tra xem email đã tồn tại trong cơ sở dữ liệu chưa
            if (user == null)
            {
                return BadRequest("Email không tồn tại.");
            }

            // Tạo mật khẩu mới ngẫu nhiên
            string newPassword = GenerateRandomPassword();

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            byte[] newPasswordHash, newPasswordSalt;
            CreatePasswordHash(newPassword, out newPasswordHash, out newPasswordSalt);
            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;
            await _authRepository.UpdateUserAsync(user);

            // Gửi email chứa mật khẩu mới đến địa chỉ email của người dùng
            await _emailService.SendResetPasswordEmailAsync(request.Email, newPassword);

            return Ok("Vui lòng kiểm tra tài khoản Email .Mật khẩu mới đã được gửi đến email của bạn.");
        }

        private string GenerateRandomPassword()
        {
            // Tạo một mật khẩu ngẫu nhiên có độ dài mong muốn
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder newPassword = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                newPassword.Append(chars[random.Next(chars.Length)]);
            }
            return newPassword.ToString();
        }


    }
}
