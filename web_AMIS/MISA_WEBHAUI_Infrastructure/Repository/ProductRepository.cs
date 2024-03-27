using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MySqlConnector;

namespace MISA_WEBHAUI_Infrastructure.Repository
{

    public class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public object GetProductAllInfor()
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT * FROM Product e INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                    "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId ";
                var products = SqlConnection.Query<object>(sqlCommand);
                return products;
            }
        }
        public object GetProductByManufactorer(Guid manufactorerId) 
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT * FROM Product e INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                    "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId WHERE e.ManufactorerId=@manufactorerId ";
                var parameters = new DynamicParameters();
                parameters.Add("@manufactorerId", manufactorerId);
                var products = SqlConnection.Query<object>(sqlCommand,parameters);
                return products;
            }
        }
        public object GetProductByCatagory(Guid catagoryId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT * FROM Product e INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                    "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId WHERE e.CatagoryId=@catagoryId";
                var parameters = new DynamicParameters();
                parameters.Add("@catagoryId", catagoryId);
                var products = SqlConnection.Query<object>(sqlCommand,parameters);
                return products;
            }
        }
    }
}
