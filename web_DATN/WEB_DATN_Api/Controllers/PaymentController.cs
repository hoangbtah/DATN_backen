using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create_payment")]
        public IActionResult CreatePayment([FromBody] PaymentRequest request)
        {
            string vnp_TmnCode = _configuration["VNPAY:TmnCode"];
            string vnp_HashSecret = _configuration["VNPAY:HashSecret"];
            string vnp_Url = _configuration["VNPAY:Url"];
            string vnp_Returnurl = _configuration["VNPAY:ReturnUrl"];

            //var vnp_TxnRef = request.OrderId;
            var vnp_TxnRef = request.OrderId;
            //  var vnp_OrderInfo = request.OrderInfo;
            var vnp_OrderInfo = $"Thanh toan don hang thoi gian: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            var vnp_OrderType = "billpayment";
            var vnp_Amount = request.Amount * 100; // Amount in VND, multiplied by 100 to convert to smallest currency unit
            var vnp_Locale = "vn";
            var vnp_BankCode = request.BankCode;
            // var vnp_IpAddr = request.IpAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString(); // Sử dụng IP của request nếu có, nếu không thì lấy IP của client

            // var inputData = new SortedDictionary<string, string>
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (request.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            //if (bankcode_Vnpayqr.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            //}
            //else if (bankcode_Vnbank.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            //}
            //else if (bankcode_Intcard.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            //}
            vnpay.AddRequestData("vnp_Locale", vnp_Locale);
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnpay.AddRequestData("vnp_BankCode", vnp_BankCode);
           
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + vnp_TxnRef);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", vnp_TxnRef); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
           // log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return Ok(new { paymentUrl });
          //  Response.Redirect(paymentUrl);
            //        {
            //    { "vnp_Version", "2.1.0" },
            //    { "vnp_Command", "pay" },
            //    { "vnp_TmnCode", vnp_TmnCode },
            //    { "vnp_Amount", vnp_Amount.ToString() },
            //    { "vnp_CurrCode", "VND" },
            //    { "vnp_TxnRef", vnp_TxnRef },
            //    { "vnp_OrderInfo", vnp_OrderInfo },
            //    { "vnp_OrderType", vnp_OrderType },
            //    { "vnp_Locale", vnp_Locale },
            //    { "vnp_ReturnUrl", vnp_Returnurl },
            //    { "vnp_IpAddr", "127.0.0.1" },
            //    { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
            //  //  { "vnp_OrderDescription", $"Thanh toan don hang thoi gian: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}" },
            //    { "vnp_Language", "vn" },
            //    {"vnp_ExpireDate",DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss") }
            //};

            //        if (!string.IsNullOrEmpty(vnp_BankCode))
            //        {
            //            inputData.Add("vnp_BankCode", vnp_BankCode);
            //        }

            //        var queryString = new StringBuilder();
            //        foreach (var item in inputData)
            //        {
            //            if (queryString.Length > 0)
            //            {
            //                queryString.Append("&");
            //            }
            //            queryString.Append($"{item.Key}={item.Value}");
            //        }

            //        var hashdata = string.Join("&", inputData.Select(x => $"{x.Key}={x.Value}"));
            //        var vnp_SecureHash = HmacSHA512(vnp_HashSecret, hashdata);
            //        // var paymentUrl = $"{vnp_Url}?{queryString}&vnp_SecureHashType=HmacSHA512&vnp_SecureHash={vnp_SecureHash}";
            //        var paymentUrl = $"{vnp_Url}?{queryString}&vnp_SecureHash={vnp_SecureHash}";

            //        return Ok(new { paymentUrl });
        }

        //private static string HmacSHA512(string key, string data)
        //{
        //    using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
        //    {
        //        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        //        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        //    }
        //}

        //[HttpPost("verify")]
        //public IActionResult VerifyPayment([FromBody] Dictionary<string, string> vnp_Params)
        //{
        //    string vnp_HashSecret = _configuration["VNPAY:HashSecret"];
        //    var vnp_SecureHash = vnp_Params["vnp_SecureHash"];
        //    vnp_Params.Remove("vnp_SecureHashType");
        //    vnp_Params.Remove("vnp_SecureHash");

        //    var inputData = new SortedDictionary<string, string>(vnp_Params);
        //    var hashData = string.Join("&", inputData.Select(x => $"{x.Key}={x.Value}"));
        //    var secureHash = HmacSHA512(vnp_HashSecret, hashData);

        //    if (secureHash == vnp_SecureHash)
        //    {
        //        // Xử lý kết quả giao dịch
        //        string vnp_ResponseCode = vnp_Params["vnp_ResponseCode"];
        //        if (vnp_ResponseCode == "00")
        //        {
        //            return Ok(new { message = "Giao dịch thành công." });
        //        }
        //        else
        //        {
        //            return Ok(new { message = $"Giao dịch không thành công. Mã lỗi: {vnp_ResponseCode}" });
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = "Chữ ký không hợp lệ." });
        //    }
        //}

    }
}
