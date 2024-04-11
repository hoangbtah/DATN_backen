using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        //[HttpPost("add")]
        //public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        //{
        //    // Kiểm tra xem người dùng đã đăng nhập chưa
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Unauthorized("Bạn phải đăng nhập trước khi thêm sản phẩm vào giỏ hàng.");
        //    }

        //    try
        //    {
        //        // Lấy thông tin sản phẩm từ cơ sở dữ liệu
        //        var product = await _context.Products.FindAsync(request.ProductId);

        //        if (product == null)
        //        {
        //            return NotFound("Không tìm thấy sản phẩm.");
        //        }

        //        // Lấy thông tin người dùng từ người dùng hiện tại đã xác thực
        //        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        //        // Tìm giỏ hàng của người dùng
        //        var cartItem = await _context.ShoppingCart
        //            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == request.ProductId);

        //        if (cartItem == null)
        //        {
        //            // Tạo một mục mới trong giỏ hàng nếu chưa tồn tại
        //            cartItem = new ShoppingCart
        //            {
        //                UserId = userId,
        //                ProductId = request.ProductId,
        //                Quantity = 1  // Số lượng mặc định khi thêm sản phẩm vào giỏ hàng
        //            };

        //            // Thêm mục vào giỏ hàng
        //            _context.ShoppingCart.Add(cartItem);
        //        }
        //        else
        //        {
        //            // Nếu sản phẩm đã có trong giỏ hàng, tăng số lượng lên 1
        //            cartItem.Quantity++;
        //        }

        //        // Lưu thay đổi vào cơ sở dữ liệu
        //        await _context.SaveChangesAsync();

        //        return Ok("Sản phẩm đã được thêm vào giỏ hàng.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
        //    }
        //}
    }
}
