using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_Infrastructure.Repository
{
    public class AuthRepository:BaseRepository<User>,IAuthRepository
    {
        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateUserAsync(User user)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                string query = @"
            UPDATE User
            SET Name = @Name,
                PasswordHash = @PasswordHash,
                PasswordSalt = @PasswordSalt,
                Role = @Role,
                Email = @Email,
                PhoneNumber = @PhoneNumber,
                Address = @Address,
                Active = @Active
            WHERE UserId = @UserId;";

                await SqlConnection.ExecuteAsync(query, user);
            }
        }
        /// <summary>
        /// lấy thông tin người dùng theo tên
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = "SELECT * FROM User WHERE Name = @username";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                return await SqlConnection.QueryFirstOrDefaultAsync<User>(query,parameters);
            }
           
        }
        /// <summary>
        /// lấy người dùng theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = "SELECT * FROM User WHERE Email = @email";
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                return await SqlConnection.QueryFirstOrDefaultAsync<User>(query, parameters);
            }

        }
        /// <summary>
        /// kiểm tra người dùng theo tên và mật khẩu
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordhand"></param>
        /// <param name="passwordsalt"></param>
        /// <returns></returns>
        public async Task<User> ExamUser(string username, byte[] passwordhand, byte[] passwordsalt)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = "SELECT * FROM User WHERE Name = @username " +
                    "AND PasswordHash =@passwordHash AND PasswordSalt =@passwordSalt AND Active=1 ";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@passwordHash", passwordhand);
                parameters.Add("@passwordSalt", passwordsalt);
                return await SqlConnection.QueryFirstOrDefaultAsync<User>(query, parameters);
            }

        }
        /// <summary>
        /// thêm mới người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> CreateUserAsync(User user)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = @"
                INSERT INTO User (USerId ,Name, PasswordHash, PasswordSalt, Role,Email,PhoneNumber,Address,Active)
                VALUES (@UserId,@Name, @PasswordHash, @PasswordSalt, @Role,@Email,@PhoneNumber,@Address,@Active);";

                return await SqlConnection.ExecuteAsync(query, user);
            }
        }
        /// <summary>
        /// xác thực mật khẩu người dùng
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordhand"></param>
        /// <param name="passwordsalt"></param>
        /// <returns></returns>
        public async Task<bool> VerifyPasswordHash(string password, byte[] passwordhand, byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var comptedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return comptedHash.SequenceEqual(passwordhand);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resetToken"></param>
        /// <returns></returns>
        public async Task SaveResetPasswordTokenAsync(Guid userId, string resetToken)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                string query = @"
            UPDATE User
            SET ResetPasswordToken = @resetToken
            WHERE UserId = @userId;";

                var parameters = new DynamicParameters();
                parameters.Add("@resetToken", resetToken);
                parameters.Add("@userId", userId);

                await SqlConnection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<List<string>> GetAllUserEmails()
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                //var sqlCommand = "SELECT Email " +
                //    "from User";

                //var emails = SqlConnection.Query<string>(sqlCommand);
                //return await emails.ToList();
                await SqlConnection.OpenAsync();
                var sqlCommand = "SELECT Email FROM User";
                var emails = await SqlConnection.QueryAsync<string>(sqlCommand);
                return emails.ToList();
            }
        }



    }


}
