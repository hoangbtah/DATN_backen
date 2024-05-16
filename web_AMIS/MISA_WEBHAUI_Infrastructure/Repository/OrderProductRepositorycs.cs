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
    public class OrderProductRepositorycs:BaseRepository<OrderProduct>,IOrderProductRepository
    {
         public async Task<int> CreateOrder(OrderProduct orderProduct)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                //string query = @"
                //INSERT INTO OrderProduct (OrderProductId,UserId, OrderDate,Phone,OrderAddress)
                //VALUES (@OrderProductId,@UserId, @OrderDate,@Phone,@OrderAddress);";
                string query = @"
                INSERT INTO OrderProduct (OrderProductId,UserId, OrderDate,Phone,
                OrderAddress,Receiver,Status,Payment,OrderTotal,StatusPayment)
                VALUES (@OrderProductId,@UserId, @OrderDate,@Phone,@OrderAddress,@Receiver,
                @Status,@Payment,@OrderTotal,@StatusPayment);";


                return await SqlConnection.ExecuteAsync(query, orderProduct);
            }
        }
        public object GetOrderAllInfor()
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT * FROM OrderProduct e INNER JOIN User d ON e.UserId = d.UserId " +
                    " ORDER BY e.OrderDate DESC ";
                   
                var orders = SqlConnection.Query<object>(sqlCommand);
                return orders;
            }
        }
    }
}
