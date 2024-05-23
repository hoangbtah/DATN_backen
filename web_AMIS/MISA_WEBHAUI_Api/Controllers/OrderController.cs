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
    public class OrderController : MBaseController<OrderProduct>
    {
        IOrderProductRepository _orderProductRepository;
        IOrderProductService _orderProductService;

        public OrderController(IOrderProductRepository orderProductRepository, 
            IOrderProductService orderProductService)
            : base(orderProductRepository, orderProductService)
        {
            _orderProductRepository = orderProductRepository;
            _orderProductService = orderProductService;

        }
        [HttpPost("createOrder")]
        public async Task<ActionResult<OrderProduct>> CreateOder(OrderProduct request)
        {
            try
            {

                var order = new OrderProduct
                {
                        OrderProductId = Guid.NewGuid(),
                        OrderDate = DateTime.Now,
                        UserId = request.UserId,
                        Phone = request.Phone,
                        Status = 0,
                        OrderAddress = request.OrderAddress,
                        Receiver=request.Receiver,
                        Payment=request.Payment,
                        OrderTotal=request.OrderTotal,
                        StatusPayment=0
                    };

                    int affectedRows = await _orderProductRepository.CreateOrder(order);
               

                //  var data = _shoppingCartRepository.GetCartByUserId(request.UserId);

                //  return Ok(data);
                return StatusCode(200,order);

            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("orders")]
        public IActionResult GetOrderAllInfor()
        {
            try
            {
                var data = _orderProductRepository.GetOrderAllInfor();

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
        [HttpGet("orders/{userId}")]
        public IActionResult GetOrderByUserId(Guid userId)
        {
            try
            {
                var data = _orderProductRepository.GetOrderByUserId(userId);

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
