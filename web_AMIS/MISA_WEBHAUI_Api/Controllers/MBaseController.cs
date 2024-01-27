using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Resources;
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
                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                userMsg = ex.Message,
                data = ex.Data,
                };
            return BadRequest(response);
             }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA_WEBHAUI_AMIS_Core.Resources.ResourceVN.ErrorException,
                    data = ex.Data,
                };
                return StatusCode(500,response);
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
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA_WEBHAUI_AMIS_Core.Resources.ResourceVN.ErrorException,
                    data = ex.Data,
                };
                return StatusCode(500, response);
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
                return StatusCode(201,data);

            }
            catch (MISAvalidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA_WEBHAUI_AMIS_Core.Resources.ResourceVN.ErrorException,
                    data = ex.Data,
                };
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Sửa dữ liệu theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        [HttpPut("entityId")]
        public IActionResult Put(MISAEntity entity, Guid entityId)
        {
            try
            {
                var data = _baseRepository.Update(entity,entityId);
                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA_WEBHAUI_AMIS_Core.Resources.ResourceVN.ErrorException,
                    data = ex.Data,
                };
                return StatusCode(500, response);
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
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA_WEBHAUI_AMIS_Core.Resources.ResourceVN.ErrorException,
                    data = ex.Data,
                };
                return StatusCode(500, response);
            }
        }
        #endregion
      
    }
}
