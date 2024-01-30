using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_Infrastructure.Repository;

namespace MISA_WEBHAUI_Api.Controllers
{
   
    public class DepartmentsController : MBaseController<Department>

    {

        #region Contructor
        public DepartmentsController(IBaseRepository<Department> baseRepository, 
            IBaseService<Department> baseService) : base(baseRepository, baseService)
        {
           
        }
        #endregion
    }
}
