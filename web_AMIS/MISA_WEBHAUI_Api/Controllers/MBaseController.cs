using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
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
        private IEmployeeRepository employeeRepository;
        private IEmployeeService employeeService;
        #endregion

        #region Contructor
        public MBaseController(IBaseService<MISAEntity> baseService, IBaseRepository<MISAEntity> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }

        public MBaseController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            this.employeeRepository = employeeRepository;
            this.employeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
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
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được giúp đỡ",
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
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được giúp đỡ",
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
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var data = _baseRepository.Insert(entity);
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
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được giúp đỡ",
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
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được giúp đỡ",
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
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được giúp đỡ",
                    data = ex.Data,
                };
                return StatusCode(500, response);
            }
        }
        #endregion
      
    }
}
