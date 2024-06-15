using MISA_WEBHAUI_AMIS_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IAuthRepository:IBaseRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);

        Task<int> CreateUserAsync(User user);
        Task<bool> VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

        Task<User> ExamUser(string username, byte[] storedHash, byte[] storedSalt);

        Task UpdateUserAsync(User user);

        Task SaveResetPasswordTokenAsync(Guid userId, string resetToken);

        Task<List<string>> GetAllUserEmails();

    }
}
