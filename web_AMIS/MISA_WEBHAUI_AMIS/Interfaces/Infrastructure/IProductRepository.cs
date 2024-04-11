using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA_WEBHAUI_AMIS_Core.Entities;

namespace MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetProductAllInfor();
        object GetProductByManufactorer(Guid manufactorerId, string search, decimal? from, decimal? to, int pagenumber, int pagesize);
        object GetProductByCatagory(Guid catagoryId);

        object GetProductSearch(string search,decimal? from, decimal? to, int pagenumber, int pagesize);
    }
}
