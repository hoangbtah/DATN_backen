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
    public class OrderDetailService:BaseService<OrderDetail>,IOrderDetailService
    {
        IOrderDetailRepository _orderDetailService;
        #region Contructor
        public OrderDetailService(IOrderDetailRepository orderDetailService) : base(orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        #endregion

    }
}
