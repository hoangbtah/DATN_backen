using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Services
{
    public class BaseService<MISAEntity>:IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public int InsertServie(MISAEntity entity)
        {

            // validate dữ liệu
            ValidateDate(entity);
            ValidateEmployee(entity);
           
           //thực hiện thêm mới vào database
            var res = _baseRepository.Insert(entity);
            return res;
        }

      
      
        public int UpdateServie(MISAEntity entity, Guid entityId)
        {
            // validate dữ liệu
            ValidateDate(entity);
            ValidateEmployee(entity);

            //thực hiện thêm mới vào database
          
            var res = _baseRepository.Update(entity,entityId);
            return res;
        }
        private void ValidateDate(MISAEntity entity)
        {
           
            var props = entity.GetType().GetProperties();
            // lấy ra các property được đánh dấu là không được phép để trống
            var propNotEmpties = entity.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(NotEmpty)));

            foreach (var prop in propNotEmpties)
            {
                var propValue= prop.GetValue(entity);
                var propName = prop.Name;
                var nameDisplay = string.Empty;
                var proppertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                if(proppertyNames.Length>0)
                {
                    nameDisplay= (proppertyNames[0] as PropertyName).Name;


                }
             
                if(propValue== null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    nameDisplay= (nameDisplay==string.Empty? propName:nameDisplay);
                    throw new MISAvalidateException($"Thông tin {nameDisplay} không được phép để trống");
                }    

            }
            

        }
        protected virtual void ValidateEmployee(MISAEntity entity)
        {

        }
    }
}
