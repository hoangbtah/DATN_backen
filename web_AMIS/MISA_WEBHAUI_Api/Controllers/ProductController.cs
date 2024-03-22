using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;

namespace MISA_WEBHAUI_Api.Controllers
{
    
    public class ProductController : MBaseController<Product>
    {
        #region Contructor
        public ProductController(IBaseRepository<Product> baseRepository,
            IBaseService<Product> baseService) : base(baseRepository, baseService)
        {

        }
        #endregion
    }
}
