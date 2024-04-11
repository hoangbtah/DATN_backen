using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : MBaseController<Cart>
    {
        
        IShoppingCartRepository _shoppingCartRepository;
        IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,IShoppingCartService shoppingCartService)
            :base(shoppingCartRepository ,shoppingCartService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartService= shoppingCartService;

        }


        [HttpPost("addshoppingcart")]
        public async Task<ActionResult<Cart>> AddShoppingCart(ProductDto request)
        {
            try
            {
                // Tìm giỏ hàng của người dùng
                var cartItem = await _shoppingCartRepository.GetCartByUP(request.UserId, request.ProductId);
                if (cartItem == null)
                {
                    // Tạo một mục mới trong giỏ hàng nếu chưa tồn tại
                    cartItem = new Cart
                    {
                        CartId = Guid.NewGuid(),
                        ProductId = request.ProductId,
                        ProductName = request.ProductName,
                        Quantity = 1,
                        Price = request.Price,
                        Image = request.Image,
                        UserId = request.UserId,

                    };

                    int affectedRows = await _shoppingCartRepository.AddShoppingCart(cartItem);
                }
                else
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, tăng số lượng lên 1
                     cartItem.Quantity++;
                    int affectedRows = await _shoppingCartRepository.UpdateShoppingCart(cartItem);
                }

                return Ok("Thêm thành công");

            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
       
    }
}
