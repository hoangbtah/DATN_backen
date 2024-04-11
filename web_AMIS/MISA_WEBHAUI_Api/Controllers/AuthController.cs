using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
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
        
        public AuthController(IAuthRepository authRepository,IConfiguration configuration)
        {
            _conficguration = configuration;
            _authRepository = authRepository;
           
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
                    Address=request.Address,
                    Email=request.Email,
                    PhoneNumber=request.PhoneNumber
                };

                int affectedRows = await _authRepository.CreateUserAsync(user);

               
                 return Ok(user);
                
            }
           
            catch (Exception ex)
            {

                return   StatusCode(500, ex.Message);
            }
           
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
    }
}
