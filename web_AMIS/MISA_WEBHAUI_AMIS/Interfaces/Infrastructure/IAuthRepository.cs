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
        Task<int> CreateUserAsync(User user);
        Task<bool> VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

        Task<User> ExamUser(string username, byte[] storedHash, byte[] storedSalt);



    }
}
