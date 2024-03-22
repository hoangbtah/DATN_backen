using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;

namespace MISA_WEBHAUI_Api.Controllers
{
    public class UserController : MBaseController<User>
    {
        #region Contructor
        public UserController(IBaseRepository<User> baseRepository,
            IBaseService<User> baseService) : base(baseRepository, baseService)
        {

        }
        #endregion
    }
}
