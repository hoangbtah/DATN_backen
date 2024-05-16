using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MySqlConnector;
using System.Text.RegularExpressions;

namespace MISA_WEBHAUI_Infrastructure.Repository
{

    public class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public object GetProductAllInfor()
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price,e.ProductSize, " +
                         "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                         "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                         "FROM Product e " +
                         "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                         "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                         "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId ";
                        
                var products = SqlConnection.Query<object>(sqlCommand);
                return products;
            }
        }
        public object GetProduct(Guid productId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price,e.ProductSize, " +
                         "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                         "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                         "FROM Product e " +
                         "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                         "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                         "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId WHERE e.ProductId= @productId";
                var parameters = new DynamicParameters();
                parameters.Add("@productId", productId);

                var product = SqlConnection.QueryFirstOrDefault<object>(sqlCommand,parameters);
                return product;
            }
        }
        public object GetProductDiscount(Guid? manufactorerId, Guid? catagoryId, string search,
            decimal? from, decimal? to, int pagenumber, int pagesize)
        {

            using (SqlConnection = new MySqlConnection(ConnectString))
            {


                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price,e.ProductSize, " +
                          "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                          "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                          "FROM Product e " +
                          "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                          "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                          "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId " +
                          "WHERE discount.DiscountPercent > 0";


                // Sử dụng '%' để thực hiện tìm kiếm một phần của tên
                var parameters = new DynamicParameters();
                // Kiểm tra xem manufactorerId có giá trị không
                if (manufactorerId.HasValue)
                {
                    sqlCommand += "AND e.ManufactorerId = @manufactorerId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@manufactorerId", manufactorerId);
                }
                if (catagoryId.HasValue)
                {
                    sqlCommand += "AND e.CatagoryId = @catagoryId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@catagoryId", catagoryId);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    sqlCommand += "AND e.ProductName LIKE @productName ";
                    parameters.Add("@productName", "%" + search + "%");
                }
                // Thêm điều kiện lọc theo giá nếu có giá trị từ và đến
                if (from.HasValue)
                {
                    sqlCommand += "AND e.Price >= @from ";
                    parameters.Add("@from", from.Value);
                }
                if (to.HasValue)
                {
                    sqlCommand += "AND e.Price <= @to ";
                    parameters.Add("@to", to.Value);
                }



                var employees = SqlConnection.Query<object>(sqlCommand, parameters);
                int totalCount = employees.AsList().Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / pagesize);
                //  return totalPages;

                employees = employees.Skip((pagenumber - 1) * pagesize).Take(pagesize);

                return new
                {
                    Data = employees,
                    TotalPages = totalPages,
                    total = totalCount
                };
            }
        }
        public object GetProductByManufactorer(Guid? manufactorerId,Guid? catagoryId, string search, 
            decimal? from, decimal? to, int pagenumber, int pagesize) 
        {
            
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

               
                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price,e.ProductSize, " +
                       "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                       "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                       "FROM Product e " +
                       "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                       "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                       "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId " +
                      // "WHERE e.ManufactorerId=@manufactorerId " +
                       "WHERE 1=1 ";


                // Sử dụng '%' để thực hiện tìm kiếm một phần của tên
                var parameters = new DynamicParameters();
                // Kiểm tra xem manufactorerId có giá trị không
                if (manufactorerId.HasValue)
                {
                    sqlCommand += "AND e.ManufactorerId = @manufactorerId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@manufactorerId", manufactorerId);
                }
                if (catagoryId.HasValue)
                {
                    sqlCommand += "AND e.CatagoryId = @catagoryId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@catagoryId", catagoryId);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    sqlCommand += "AND e.ProductName LIKE @productName ";
                    parameters.Add("@productName", "%" + search + "%");
                }
                // Thêm điều kiện lọc theo giá nếu có giá trị từ và đến
                if (from.HasValue)
                {
                    sqlCommand += "AND e.Price >= @from ";
                    parameters.Add("@from", from.Value);
                }
                if (to.HasValue)
                {
                    sqlCommand += "AND e.Price <= @to ";
                    parameters.Add("@to", to.Value);
                }



                var employees = SqlConnection.Query<object>(sqlCommand, parameters);
                int totalCount = employees.AsList().Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / pagesize);
              //  return totalPages;

                employees = employees.Skip((pagenumber - 1) * pagesize).Take(pagesize);

                return new
                {
                    Data = employees,
                    TotalPages = totalPages,
                    total=totalCount
                };
            }
        }
     /// <summary>
     /// lấy danh sach sản phẩm bán được theo tháng và năm
     /// </summary>
     /// <param name="month"></param>
     /// <param name="year"></param>
     /// <returns></returns>
        public object GetProductSale(int month,int year)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {


                var sqlCommand = "SELECT n.ProductId, e.ProductName, e.Price,e.Image,h.ManufactorerName, SUM(n.Quantity) AS TotalQuantity " +
                    "FROM OrderDetail n LEFT JOIN Product e ON n.ProductId = e.ProductId " +
                    "INNER JOIN OrderProduct m ON n.OrderId = m.OrderProductId " +
                    "INNER JOIN Manufactorer h ON e.ManufactorerId= h.ManufactorerId " +
                    "   WHERE YEAR(m.OrderDate) = @year AND MONTH(m.OrderDate) = @month " +
                    "GROUP BY n.ProductId, e.ProductName, e.Price ,e.Image,h.ManufactorerName " +
                    "ORDER BY TotalQuantity DESC, e.Price DESC "; // Sắp xếp giảm dần theo số lượng, nếu số lượng bằng nhau thì sắp xếp giảm dần theo đơn giá;
                var parameters = new DynamicParameters();
                parameters.Add("@year", year);
                parameters.Add("@month", month);
                var products = SqlConnection.Query<object>(sqlCommand,parameters);
                return products;
            }
        }
        /// <summary>
        /// lấy danh sách sản phẩm bán được theo 1 khoang thời gian
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public object GetProductsSaleByStartAndEnd(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                if (startDate == endDate || startDate.AddDays(1) == endDate) // Nếu ngày bắt đầu và kết thúc trùng nhau
                {
                    endDate = endDate.AddDays(1); // Tăng ngày kết thúc lên 1 ngày

                }

                var sqlCommand = "SELECT n.ProductId, e.ProductName, e.Price,e.Image,h.ManufactorerName, SUM(n.Quantity) AS TotalQuantity " +
                    "FROM OrderDetail n LEFT JOIN Product e ON n.ProductId = e.ProductId " +
                    "INNER JOIN OrderProduct m ON n.OrderId = m.OrderProductId " +
                    "INNER JOIN Manufactorer h ON e.ManufactorerId= h.ManufactorerId " +
                    "WHERE m.OrderDate >= @start AND m.OrderDate < @end " +
                    "GROUP BY n.ProductId, e.ProductName, e.Price ,e.Image,h.ManufactorerName " +
                    "ORDER BY TotalQuantity DESC, e.Price DESC "; // Sắp xếp giảm dần theo số lượng, nếu số lượng bằng nhau thì sắp xếp giảm dần theo đơn giá;
                var parameters = new DynamicParameters();
                parameters.Add("@start", startDate);
                parameters.Add("@end", endDate);
                var products = SqlConnection.Query<object>(sqlCommand, parameters);
                return products;
            }
        }



        /// <summary>
        /// lay thống kê theo khoảng thời gina truyền va
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public object GetProductSaleByMonthAndYear(int month,int year)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = " SELECT(m.ManufactorerName) AS Hang, SUM(s.Quantity) AS Quantity ," +
                                 "SUM(p.Price * s.Quantity) AS SalesAmount" +
                                 " FROM OrderDetail s" +
               " JOIN Product p ON s.ProductId = p.ProductId " +
               " JOIN OrderProduct o ON s.OrderId = o.OrderProductId " +
               " Join Manufactorer m ON p.ManufactorerId = m.ManufactorerId " +
             "   WHERE YEAR(o.OrderDate) = @year AND MONTH(o.OrderDate) = @month " +
              "  GROUP BY(m.ManufactorerName)";
                var parameters = new DynamicParameters();
                parameters.Add("@year", year);
                parameters.Add("@month", month);

                var products = SqlConnection.Query<object>(sqlCommand, parameters);
                return products;
            }
        }
        /// <summary>
        /// lấy thông ke theo nam
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public object GetProductSaleByYear(int year)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT MONTH(o.OrderDate) AS Month,SUM(s.Quantity) AS Quantity ," +
                                 "SUM(p.Price * s.Quantity) AS SalesAmount " +
                                 "FROM OrderDetail s " +
                                 "JOIN Product p ON s.ProductId = p.ProductId " +
                                 "JOIN OrderProduct o ON s.OrderId = o.OrderProductId " +
                                 "WHERE YEAR(o.OrderDate) = @year " +
                                 "GROUP BY MONTH(o.OrderDate)" +
                                 " ORDER BY Month ";
                var parameters = new DynamicParameters();
                parameters.Add("@year", year);
                var products = SqlConnection.Query<object>(sqlCommand,parameters);
                return products;
            }
        }
        /// <summary>
        /// lấy thông kê theo biểu đồ cột của khoảng thời gian
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public object GetProductSaleByStartAndEnd(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                if (startDate == endDate || startDate.AddDays(1) == endDate) // Nếu ngày bắt đầu và kết thúc trùng nhau
                {
                    endDate = endDate.AddDays(1); // Tăng ngày kết thúc lên 1 ngày
                    
                }
                var sqlCommand = "SELECT DATE(o.OrderDate) AS Date, " +
                                 "SUM(s.Quantity) AS Quantity, " +
                                 "SUM(p.Price * s.Quantity) AS SalesAmount " +
                                 "FROM OrderDetail s " +
                                 "JOIN Product p ON s.ProductId = p.ProductId " +
                                 "JOIN OrderProduct o ON s.OrderId = o.OrderProductId " +
                                 "WHERE o.OrderDate >= @startDate AND o.OrderDate < @endDate " + // Sử dụng "<" thay vì "<=" ở điều kiện WHERE
                                 "GROUP BY DATE(o.OrderDate) " +
                                 "ORDER BY Date";

                var parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate);
                parameters.Add("@endDate", endDate);

                var products = SqlConnection.Query(sqlCommand, parameters).ToList();

                // Tạo danh sách các ngày trong khoảng thời gian
                List<DateTime> allDates = Enumerable.Range(0, (int)(endDate - startDate).TotalDays+1)
                    .Select(offset => startDate.AddDays(offset))
                    .ToList();

                // Nếu ngày bắt đầu và kết thúc trùng nhau, chỉ chứa một ngày trong danh sách
                if (startDate == endDate.AddDays(-1))
                {
                    allDates = new List<DateTime> { startDate };
                }

                // Tạo một danh sách mới chứa tất cả các ngày trong khoảng thời gian và điền dữ liệu nếu có
                var result = allDates.Select(date => new
                {
                    Date = date,
                    Quantity = products.FirstOrDefault(x => x.Date.Date == date)?.Quantity ?? 0,
                    SalesAmount = products.FirstOrDefault(x => x.Date.Date == date)?.SalesAmount ?? 0
                }).ToList();

                return result;
            }
        }
        /// <summary>
        /// lấy thông kê theo biểu đồ tròn của các hãng
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public object GetProductSaleByStartAndEndPie(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                if (startDate == endDate || startDate.AddDays(1) == endDate) // Nếu ngày bắt đầu và kết thúc trùng nhau
                {
                    endDate = endDate.AddDays(1); // Tăng ngày kết thúc lên 1 ngày

                }

                var sqlCommand = " SELECT(m.ManufactorerName) AS Hang, SUM(s.Quantity) AS Quantity ," +
                                 "SUM(p.Price * s.Quantity) AS SalesAmount" +
                                 " FROM OrderDetail s" +
               " JOIN Product p ON s.ProductId = p.ProductId " +
               " JOIN OrderProduct o ON s.OrderId = o.OrderProductId " +
               " Join Manufactorer m ON p.ManufactorerId = m.ManufactorerId " +
             "   WHERE  o.OrderDate >= @startDate AND o.OrderDate < @endDate  " +
              "  GROUP BY(m.ManufactorerName)";
                var parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate);
                parameters.Add("@endDate", endDate);

                var products = SqlConnection.Query<object>(sqlCommand, parameters);
                return products;
            }
        }



    }
}
