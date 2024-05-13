using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserVoucherController : MBaseController<UserVoucher>
    {
        #region Contructor
        public UserVoucherController(IBaseRepository<UserVoucher> baseRepository,
            IBaseService<UserVoucher> baseService) : base(baseRepository, baseService)
        {

        }
        #endregion
    }
}
