using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;

namespace MISA_WEBHAUI_Api.Controllers
{
    public class ManufactorerController : MBaseController<Manufactorer>
    {
        #region Contructor
        public ManufactorerController(IBaseRepository<Manufactorer> baseRepository,
            IBaseService<Manufactorer> baseService) : base(baseRepository, baseService)
        {

        }
        #endregion
    }
}
