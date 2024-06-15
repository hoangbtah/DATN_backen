using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : MBaseController<Discount>
    {
        #region Contructor
        public DiscountController(IBaseRepository<Discount> baseRepository,
            IBaseService<Discount> baseService) : base(baseRepository, baseService)
        {

        }
        #endregion
    }
}
