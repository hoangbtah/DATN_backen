using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<MISAEntity>
    {
        IEnumerable<MISAEntity> GetAll();

        MISAEntity GetById(Guid entityId);

        int Insert(MISAEntity entity);

        int Update(MISAEntity entity,Guid enityId);

        int Delete(Guid entityId);

    }
}
