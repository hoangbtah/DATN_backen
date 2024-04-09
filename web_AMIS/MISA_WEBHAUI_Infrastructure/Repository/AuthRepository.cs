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

        public async Task<int> CreateUserAsync(User user)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = @"
                INSERT INTO User (USerId ,Name, PasswordHash, PasswordSalt, Role)
                VALUES (@UserId,@Name, @PasswordHash, @PasswordSalt, @Role);";

                return await SqlConnection.ExecuteAsync(query, user);
            }
        }

        public async Task<bool> VerifyPasswordHash(string password, byte[] passwordhand, byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var comptedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return comptedHash.SequenceEqual(passwordhand);
            }
        }

        
    }

 
}
