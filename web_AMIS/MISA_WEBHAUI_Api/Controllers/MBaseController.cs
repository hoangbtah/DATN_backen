using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Resources;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MBaseController<MISAEntity> : ControllerBase
    {
        #region Fields
        IBaseService<MISAEntity> _baseService;
        IBaseRepository<MISAEntity> _baseRepository;
       
        #endregion

        #region Contructor
        public MBaseController(IBaseRepository<MISAEntity> baseRepository,IBaseService<MISAEntity> baseService)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }

       

        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data= _baseRepository.GetAll();
                PrePareHeader();
                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {
                PrePareHeader();
                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {
                PrePareHeader();
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Lấy dứ liệu theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId) 
        {
            try
            {
                var data = _baseRepository.GetById(entityId);
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
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var data = _baseService.InsertServie(entity);
                PrePareHeader();
                return StatusCode(201,data);

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
        /// <summary>
        /// Sửa dữ liệu theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        [HttpPut("{entityId}")]
        public IActionResult Put(MISAEntity entity, Guid entityId)
        {
            try
            {
                var data = _baseService.UpdateServie(entity,entityId);
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
        /// <summary>
        /// Xóa dữ liệu theo mã id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var data = _baseRepository.Delete(entityId);
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
        /// <summary>
        /// xử lý ngoại lệ xảy ra gửi thông báo lỗi
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = MISA_WEBHAUI_AMIS_Core.Resources.ResourceVN.ErrorException;
            return StatusCode(500, error);
           
        }
        /// <summary>
        /// xử lý ngoại lệ xảy ra gửi thông báo lỗi
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private IActionResult HandleMISAException(MISAvalidateException ex)
        {
            var error = new ErrorService(); 
            error.DevMsg = ex.Message;
            error.UserMsg = ex.Message;
            error.Data = ex.Data;
            return BadRequest(error);
        }
        private void PrePareHeader()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //Response.Headers.Add("content-type", "application/json");
            //Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            //Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE");
            //Response.Headers.Add("Access-Control-Allow-Headers", "Origin,Content-Type,Accept");
        }
        #endregion

    }
}
