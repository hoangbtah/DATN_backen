using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;

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

        public VoucherController(IVoucherRepository orderProductRepository,
            IVoucherService orderProductService)
            : base(orderProductRepository, orderProductService)
        {
            _orderProductRepository = orderProductRepository;
            _orderProductService = orderProductService;

        }
        #endregion

        #region
        [HttpPost("createVoucher")]
        public async Task<ActionResult<Voucher>> CreateVoucher(Voucher request)
        {
            try
            {

                var voucher = new Voucher
                {
                    VoucherId = Guid.NewGuid(),
                    StartDateVoucher = request.StartDateVoucher,
                    EndDateVoucher = request.EndDateVoucher,
                    PercentVoucher = request.PercentVoucher,
                    DecriptionUse = request.DecriptionUse,
                    MaxximumUse = request.MaxximumUse,
                    StartPrice = request.StartPrice,
                    EndPrice=request.EndPrice
                };

                int affectedRows = await _orderProductRepository.CreateVoucher(voucher);


                //  var data = _shoppingCartRepository.GetCartByUserId(request.UserId);

                //  return Ok(data);
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

        #endregion
    }

}
