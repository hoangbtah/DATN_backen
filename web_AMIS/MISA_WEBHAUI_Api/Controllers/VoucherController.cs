using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Services;
using System.Text;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : MBaseController<Voucher>
    {
        #region Contructor
        //public VoucherController(IBaseRepository<Voucher> baseRepository,
        //    IBaseService<Voucher> baseService) : base(baseRepository, baseService)
        //{

        //}
        IVoucherRepository _orderProductRepository;
        IVoucherService _orderProductService;
        IEmailService _emailService;
        IAuthRepository _authRepository;

        public VoucherController(IVoucherRepository orderProductRepository,
            IVoucherService orderProductService, IEmailService emailService, IAuthRepository authRepository)
            : base(orderProductRepository, orderProductService)
        {
            _orderProductRepository = orderProductRepository;
            _orderProductService = orderProductService;
            _emailService = emailService;
            _authRepository = authRepository;
        }
        #endregion

        #region
        [HttpPost("createVoucher")]
        public async Task<ActionResult<Voucher>> CreateVoucher(Voucher request)
        {
            try
            {
                string newVoucherCode = GenerateRandomVoucherCode();
                var voucher = new Voucher
                {
                    VoucherId = Guid.NewGuid(),
                    VoucherCode= newVoucherCode,
                    StartDateVoucher = request.StartDateVoucher,
                    EndDateVoucher = request.EndDateVoucher,
                    PercentVoucher = request.PercentVoucher,
                    DecriptionUse = request.DecriptionUse,
                    MaxximumUse = request.MaxximumUse,
                    StartPrice = request.StartPrice,
                    EndPrice=request.EndPrice,
                    DiscountMoney= request.DiscountMoney,
                    CreateDate= DateTime.Now
                };
                // lấy ra email của tất cả người dùng


                int affectedRows = await _orderProductRepository.CreateVoucher(voucher);

                if (affectedRows > 0)
                {
                    // Lấy danh sách email của tất cả người dùng
                    var userEmails = await _authRepository.GetAllUserEmails();

                    // Gửi email voucher cho tất cả người dùng
                    foreach (var email in userEmails)
                    {
                        await _emailService.SendVoucher(voucher, email);
                    }
                }


                return StatusCode(200, voucher);

            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("voucher/{userId}")]
        public IActionResult GetVoucherByUser(Guid userId)
        {
            try
            {

                var data =  _orderProductRepository.getVoucherByUser(userId);


                //  var data = _shoppingCartRepository.GetCartByUserId(request.UserId);

                 return Ok(data);
                //return StatusCode(200, voucher);

            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("voucherbyVoucherCode/{voucherCode}")]
        public IActionResult GetVoucherByVoucherCode(string voucherCode)
        {
            try
            {

                var data = _orderProductRepository.getVoucherByVoucherCode(voucherCode);
               


                //  var data = _shoppingCartRepository.GetCartByUserId(request.UserId);

                return Ok(data);
                //return StatusCode(200, voucher);

            }

            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        private string GenerateRandomVoucherCode()
        {
            // Tạo một mật khẩu ngẫu nhiên có độ dài mong muốn
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder newPassword = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                newPassword.Append(chars[random.Next(chars.Length)]);
            }
            return newPassword.ToString();
        }

        #endregion
    }

}
