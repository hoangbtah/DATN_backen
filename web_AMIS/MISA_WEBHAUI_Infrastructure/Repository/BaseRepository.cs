using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MySqlConnector;

namespace MISA_WEBHAUI_Infrastructure.Repository
{
    public class BaseRepository<MISAEntity>:IBaseRepository<MISAEntity>
    {
        protected readonly string ConnectString = "Host= localhost;Port=3306;Database=misa_webhaui_amis;User Id= root;Password=12345678";
        protected MySqlConnection SqlConnection;

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <typeparam name="MISAEntity">kiểu của object</typeparam>
        /// <returns>danh sách object </returns>
        /// created by bvhooang(25/01/2024)
        public  IEnumerable<MISAEntity> GetAll()
        {
            var className= typeof(MISAEntity).Name;
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                var employees = SqlConnection.Query<MISAEntity>($"SELECT * FROM {className}");
                return employees;
            }
        }
        public MISAEntity GetById(Guid entityID)
        {
            var className = typeof(MISAEntity).Name;
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                var sqlCommand = $"SELECT * FROM {className} where {className}Id= @{className}Id";
                // lưu ý : nếu có tham số truyền cho câu lệnh truy vấn sql thì phải sử dụng dynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{className}Id", entityID);
                // 2.2 thực hiên lấy dữ liệu 
                var entity = SqlConnection.QueryFirstOrDefault<MISAEntity>(sql: sqlCommand, param: parameters);
                // kết quả trả về 
                return entity;

            }
        }

        public int Insert(MISAEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Update(MISAEntity entity, Guid enityId)
        {
            throw new NotImplementedException();
        }
    }
}
