using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_Infrastructure.Repository;
using System.Timers;



namespace MISA_WEBHAUI_Api.Controllers
{
    
    public class ProductController : MBaseController<Product>
    {
        #region Fiels
        IProductRepository _productRepository;
        IProductService _productService;
        #endregion

        #region Contructor
        public ProductController(IProductRepository productRepository, IProductService productService) 
            : base(productRepository, productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }
        #endregion
        [HttpGet("products")]
        public IActionResult GetProductAllInfor()
        {
            try
            {
                var data = _productRepository.GetProductAllInfor();

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
        //[HttpGet("catagory/{catagoryId}/products")]
        //public IActionResult GetProductByCatagory(Guid catagoryId)
        //{
        //    try
        //    {
        //        var data = _productRepository.GetProductByCatagory(catagoryId);

        //        return Ok(data);

        //    }
        //    catch (MISAvalidateException ex)
        //    {

        //        return HandleMISAException(ex);
        //    }
        //    catch (Exception ex)
        //    {

        //        return HandleException(ex);
        //    }
        //}
        [HttpGet("manufactorer/products")]
        public IActionResult GetProductByManufactorer(Guid? manufactorerId,Guid? catagoryId, string? search, decimal? from, decimal? to, int pagenumber, int pagesize)
        {
            try
            {
                var data = _productRepository.GetProductByManufactorer(manufactorerId, catagoryId, search, from, to, pagenumber, pagesize);

                return Ok(data);
                //var data = _productRepository.GetProductByManufactorer(manufactorerId, search, from, to, pagenumber, pagesize);
                //var totalPages = _productRepository.GetTotal(manufactorerId, search, from, to, pagenumber, pagesize); // Số lượng tổng của các bản ghi
                ////var totalPages = (int)Math.Ceiling((double)totalCount / pagesize); // Tính toán tổng số trang

                //return Ok(new
                //{
                //    Data = data,
                //    TotalPages = totalPages
                //});

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
        //[HttpGet("products/search")]
        //public IActionResult GetProductSearch(string? search, decimal? from, decimal? to,int pagenumber,int pagesize)
        //{
        //    try
        //    {

        //        //var data = _productRepository.GetProductSearch(search,from,to,pagenumber,pagesize);
        //        //return Ok(data);
        //        var data = _productRepository.GetProductSearch(search, from, to, pagenumber, pagesize);
        //        var totalPages = _productRepository.GetToTalPages(search, from, to, pagenumber, pagesize); // Số lượng tổng của các bản ghi
        //        //var totalPages = (int)Math.Ceiling((double)totalCount / pagesize); // Tính toán tổng số trang

        //        return Ok(new
        //        {
        //            Data = data,
        //            TotalPages = totalPages
        //        });
        //    }
        //    catch (MISAvalidateException ex)
        //    {

        //        return HandleMISAException(ex);
        //    }
        //    catch (Exception ex)
        //    {

        //        return HandleException(ex);
        //    }
        //}
        [HttpGet("getProductSale")]
        public IActionResult GetProductSale()
        {
            try
            {
                var data = _productRepository.GetProductSale();

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
        [HttpGet("getProductSaleByYear/{year}")]
        public IActionResult GetProductSaleBYYear(int year)
        {
            try
            {
                var data = _productRepository.GetProductSaleByYear(year);

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
        [HttpGet("getProductSaleByMonthAndYear/{month}/{year}")]
        public IActionResult GetProductSaleBYMonthAndYear(int month,int year)
        {
            try
            {
                var data = _productRepository.GetProductSaleByMonthAndYear(month,year);

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
        //[HttpGet("{manufactorerId}/products")]
        //public IActionResult GetTotalProductByManufactorer(Guid manufactorerId)
        //{
        //    try
        //    {
        //        var data = _productRepository.GetTotalProductByManufactorer(manufactorerId);

        //        return Ok(data);

        //    }
        //    catch (MISAvalidateException ex)
        //    {

        //        return HandleMISAException(ex);
        //    }
        //    catch (Exception ex)
        //    {

        //        return HandleException(ex);
        //    }
        //}



    }
}
