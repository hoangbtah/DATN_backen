using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Services
{
    public class OrderProductService : BaseService<OrderProduct>, IOrderProductService
    {
        #region Fields
        IOrderProductRepository _shoppingCartRepository;
        #endregion

        #region Contructor
        public OrderProductService(IOrderProductRepository productRepository) : base(productRepository)
        {
            _shoppingCartRepository = productRepository;
        }
        #endregion
    }
}