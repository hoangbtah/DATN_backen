﻿using Dapper;
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

                string query = @"
                INSERT INTO OrderProduct (OrderProductId,UserId, OrderDate)
                VALUES (@OrderProductId,@UserId, @OrderDate);";


                return await SqlConnection.ExecuteAsync(query, orderProduct);
            }
        }
    }
}
