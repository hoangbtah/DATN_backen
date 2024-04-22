using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Services;
using MISA_WEBHAUI_Infrastructure.Repository;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : MBaseController<Cart>
    {
        
        IShoppingCartRepository _shoppingCartRepository;
        IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,
            IShoppingCartService shoppingCartService)
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
                var product = await _shoppingCartRepository.GetProductById(request.ProductId);
               
                var cartItem = await _shoppingCartRepository.GetCartByUP(request.UserId, request.ProductId);
                if (cartItem == null)
                {
                    if(product.Quantity > 0) {
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
                        return BadRequest("Sản phẩm này đã hết !");
                    }
                }
                else
                {
                    if (product.Quantity > cartItem.Quantity)
                    {
                        cartItem.Quantity++;
                        int affectedRows = await _shoppingCartRepository.UpdateShoppingCart(cartItem);
                    }
                    else
                    {
                        return BadRequest("Sản phẩm này không đủ để cung cấp thêm !");
                    }
                  
                }

               return Ok("Thêm thành công");

            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
       // [Authorize]
        [HttpGet("carts/{userId}")]
        public IActionResult GetCartByUserId(Guid userId)
        {
            try
            {

                var data = _shoppingCartRepository.GetCartByUserId(userId);
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
        [HttpPut("updateShoppingCart")]
        public async Task<IActionResult> UpdateShoppingCart(ProductDto request)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.GetCartByUP(request.UserId, request.ProductId);

               

                var product = await _shoppingCartRepository.GetProductById(request.ProductId);

               

                // Kiểm tra số lượng sửa có hợp lệ không
                //if (request.Quantity <= 0)
                //{
                //    return BadRequest("Số lượng sửa không hợp lệ");
                //}

                // Kiểm tra số lượng sửa có vượt quá số lượng sản phẩm có sẵn trong database không
                if (request.Quantity > product.Quantity)
                {
                    return BadRequest("Rất tiếc sản phẩm trong kho không đủ để cung cấp thêm !");
                }

                else
                {
                    // Cập nhật thông tin giỏ hàng
                    cartItem.Quantity = request.Quantity;
                    cartItem.Price = request.Price;

                    int affectedRows = await _shoppingCartRepository.UpdateShoppingCart(cartItem);

                    return Ok("Cập nhật giỏ hàng thành công");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
