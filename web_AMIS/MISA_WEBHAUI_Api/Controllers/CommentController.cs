using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_Infrastructure.Repository;

namespace MISA_WEBHAUI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CommentController : MBaseController<Comment>
    {
        ICommentRepository _commentRepository;
        #region Contructor
        public CommentController(ICommentRepository commentRepository,
            IBaseService<Comment> baseService) : base(commentRepository, baseService)
        {
            _commentRepository = commentRepository;
        }
        [HttpGet("comment")]
        public IActionResult GetCommontByProduct(Guid ProductId)
        {
            try
            {
                var data = _commentRepository.GetCommentByProduct(ProductId);

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
        #endregion
    }
}
