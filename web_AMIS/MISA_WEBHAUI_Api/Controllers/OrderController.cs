using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
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

                    var order  = new OrderProduct
                    {
                        OrderProductId = Guid.NewGuid(),
                        OrderDate = DateTime.Now,
                        UserId= request.UserId
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
    }
}
