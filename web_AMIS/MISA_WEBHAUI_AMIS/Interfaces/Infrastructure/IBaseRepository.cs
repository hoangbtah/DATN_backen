using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        IEnumerable<MISAEntity> GetAll();
        /// <summary>
        /// lấy dữ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        MISAEntity GetById(Guid entityId);
        /// <summary>
        /// thêm mới dữ liêu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        int Insert(MISAEntity entity);
        /// <summary>
        /// chỉnh sửa dữ liệu theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="enityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        int Update(MISAEntity entity,Guid enityId);
        /// <summary>
        /// xóa dứ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        int Delete(Guid entityId);

    }
}
