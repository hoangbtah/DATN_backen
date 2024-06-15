using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>

    {
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// Created By : BVHoang(24/1/2024)
        int InsertServie(MISAEntity entity);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        int UpdateServie(MISAEntity entity, Guid entityId);
    }
}
