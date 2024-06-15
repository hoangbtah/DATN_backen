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
        object GetProductByManufactorer(Guid? manufactorerId,Guid? catagoryId, string search, decimal? from, decimal? to, int pagenumber, int pagesize);
     
        object GetProductSale(int month,int year);
        object GetProductsSaleByStartAndEnd(DateTime startDate, DateTime endDate);

        object GetProductSaleByStartAndEnd(DateTime startDate, DateTime endDate);
        object GetProductSaleByStartAndEndPie(DateTime startDate, DateTime endDate);

        object GetProductSaleByYear(int year);
        object GetProductSaleByMonthAndYear(int month,int year);
        object GetProduct(Guid productId);
        object GetProductDiscount(Guid? manufactorerId, Guid? catagoryId, string search, decimal? from, decimal? to, int pagenumber, int pagesize);
    
    }
}
