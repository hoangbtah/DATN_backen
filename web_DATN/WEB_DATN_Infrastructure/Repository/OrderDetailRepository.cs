using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_Infrastructure.Repository
{
    public class OrderDetailRepository:BaseRepository<OrderDetail>,IOrderDetailRepository
    {
        public object GetOrderDetailByOrderId(Guid orderId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT o.Image,o.Quantity,o.ProductId,o.Price,o.OrderDetailId,p.ProductName,m.ManufactorerName " +
                    "FROM  OrderDetail o INNER JOIN Product p " +
                    "ON o.ProductId = p.ProductId INNER JOIN Manufactorer m " +
                    "ON p.ManufactorerId = m.ManufactorerId " +
                    "WHERE OrderId= @OrderId ";
                var parameters = new DynamicParameters();
                parameters.Add("@OrderId", orderId);
                var orderDetails = SqlConnection.Query<object>(sqlCommand, parameters);
                return orderDetails;
            }
        }
      
    }
}
