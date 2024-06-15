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
        #region Fields
        IBaseRepository<MISAEntity> _baseRepository;
     //   private IOrderDetailService orderDetailService;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        //public BaseService(IOrderDetailService orderDetailService)
        //{
        //    this.orderDetailService = orderDetailService;
        //}
        #endregion

        #region Method
        /// <summary>
        /// thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        public int InsertServie(MISAEntity entity)
        {

            // validate dữ liệu
            ValidateData(entity);
            ValidateEmployee(entity);
           
           //thực hiện thêm mới vào database
            var res = _baseRepository.Insert(entity);
            return res;
        }


        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// created by BVHoang(27/01/2024)
        public int UpdateServie(MISAEntity entity, Guid entityId)
        {
            // validate dữ liệu
            ValidateData(entity);
            ValidateEmployeeForUpdate(entity,entityId);

            //thực hiện thêm mới vào database
          
            var res = _baseRepository.Update(entity,entityId);
            return res;
        }
        /// <summary>
        /// kiểm tra dữ liệu không được để trống.
        /// </summary>
        /// <param name="entity"></param>
        /// created by BVHoang(27/01/2024)
        /// created by BVHoang(27/01/2024)
        private void ValidateData(MISAEntity entity)
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
                    throw new MISAvalidateException(string.Format(MISA_WEBHAUI_AMIS_Core.Resources.ResourceVN.InforNotEmpty,nameDisplay));
                }    

            }
            

        }
        /// <summary>
        /// kiểm tra dữ liệu của nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// created by BVHoang(27/01/2024)
        protected virtual void ValidateEmployee(MISAEntity entity)
        {

        }
        /// <summary>
        /// kiểm tra dữ liệu của nhân viên đối với việc cập nhật nhân viên 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// created by BVHoang(27/01/2024)
        protected virtual void ValidateEmployeeForUpdate(MISAEntity entity,Guid entityId)
        {

        }
        #endregion
    }
}
