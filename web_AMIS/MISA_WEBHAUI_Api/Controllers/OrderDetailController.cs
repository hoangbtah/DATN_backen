using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_Infrastructure.Repository;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : MBaseController<OrderDetail>
    {
        IOrderDetailRepository _orderDetailRepository;
        IOrderDetailService _orderDetailServeice;
        public OrderDetailController(IOrderDetailRepository orderDetailRepository,
            IOrderDetailService orderDetailServeice)
            : base(orderDetailRepository, orderDetailServeice)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderDetailServeice= orderDetailServeice;
        }
        [HttpGet("getorderDetail/{orderId}")]
        public IActionResult GetOrderDetail(Guid orderId)
        {
            try
            {
                var data = _orderDetailRepository.GetOrderDetailByOrderId(orderId);

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
             
        }
     
    }

}
