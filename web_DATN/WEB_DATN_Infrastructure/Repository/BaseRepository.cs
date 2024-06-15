using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.MISAAttribute;
using MySqlConnector;

namespace MISA_WEBHAUI_Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region Connect
        protected readonly string ConnectString = "Host= localhost;Port=3306;Database=dbferfurm;User Id= root;Password=12345678";
        protected MySqlConnection SqlConnection;
        #endregion

        #region Method
        /// <summary>
        /// Xóa dữ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
       ///created by BVHoang(27/01/2024)
        public int Delete(Guid entityId)
        {
            var className = typeof(MISAEntity).Name;
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                var sqlCommand = $"DELETE FROM {className} where {className}Id= @{className}Id";
                // lưu ý : nếu có tham số truyền cho câu lệnh truy vấn sql thì phải sử dụng dynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{className}Id",entityId);
                // 2.2 thực hiên lấy dữ liệu 
                var entity = SqlConnection.Execute(sql: sqlCommand, param: parameters);
                // kết quả trả về 
                return entity;

            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <typeparam name="MISAEntity">kiểu của object</typeparam>
        /// <returns>danh sách object </returns>
        /// created by bvhooang(25/01/2024)
        public IEnumerable<MISAEntity> GetAll()
        {
            var className = typeof(MISAEntity).Name;
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                var employees = SqlConnection.Query<MISAEntity>($"SELECT * FROM {className}");
                return employees;
            }
        }
        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by BVHoang(27/01/2024)
        public int Insert(MISAEntity entity)
        {
            // build câu chuỗi sql thực hiện thêm mới dữ liệu
            var className= typeof(MISAEntity).Name;
            var sqlColumsNames= new StringBuilder();
            var sqlColumsValue = new StringBuilder();
            DynamicParameters parameters= new DynamicParameters();
            // 1. duyệt tất cả các property của đối tượng
            var props= typeof(MISAEntity).GetProperties();
            // ban đâu cho dấu phẩy rỗng sau lần đâu tiên append vào thì gán lại bằng dấu phẩy
            string delimiter = "";
            foreach (var prop in props)
            {
                // 2. lấy tên của property 
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);

                //kiểm tra property có phải là khóa chính của bảng không
                var primaryKey= Attribute.IsDefined(prop,typeof(PrimayKey));
                // kiểm tra property có phải cái không cần khi thêm mới không
                var notMap = Attribute.IsDefined(prop, typeof(NotMapInsert));
                // thực hiện tạo ra giá trị mới cho khóa chính
                if(primaryKey==true || propName==$"{className}Id")
                {
                    // kiểm tra nếu khóa chỉnh là kiểu GUid thì tạo ra giá trị
                    if(prop.PropertyType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }    
                }
                // nếu nó là cái không cho thêm mới vào thì bỏ qua
                if (notMap == true)
                    continue;

                var paramName = $"@{propName}";
                sqlColumsNames.Append($"{delimiter}{propName}");
                sqlColumsValue.Append($"{delimiter}{paramName}");
                delimiter = ",";
                parameters.Add(paramName, propValue);

            }
            //3 Lây ra giá trị của property 
            // 4. Thực hiện build câu lệnh sql
            var sqlCommand = $"INSERT INTO {className}({sqlColumsNames.ToString()}) VALUES ({sqlColumsValue.ToString()})";
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                var result = SqlConnection.Execute(sql:sqlCommand,param:parameters);
               return result;
            }
           // return propValue;

        }
        /// <summary>
        /// Chỉnh sửa dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="enityId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by BVHoang(27/01/2024)
        public int Update(MISAEntity entity, Guid entityId)
        {
            // build câu chuỗi sql thực hiện cập nhật dữ liệu
            var className = typeof(MISAEntity).Name;
            var sqlColumns = new StringBuilder();
           
            DynamicParameters parameters = new DynamicParameters();
            // 1. duyệt tất cả các property của đối tượng
            var props = typeof(MISAEntity).GetProperties();
            // ban đâu cho dấu phẩy rỗng sau lần đâu tiên append vào thì gán lại bằng dấu phẩy
            string delimiter = "";
            foreach (var prop in props)
            {
                // 2. lấy tên của property 
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);

                //kiểm tra property có phải là khóa chính của bảng không
                var primaryKey = Attribute.IsDefined(prop, typeof(PrimayKey));
                // kiểm tra property có phải cái không cần khi thêm mới không
                var notMap = Attribute.IsDefined(prop, typeof(NotMapInsert));
                // thực hiện tạo ra giá trị mới cho khóa chính
                if (primaryKey == true || propName == $"{className}Id")
                {
                    // kiểm tra nếu khóa chỉnh là kiểu GUid thì tạo ra giá trị
                    if (prop.PropertyType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }
                }
                // nếu nó là cái không cho thêm mới vào thì bỏ qua
                if (notMap == true)
                    continue;

             
                var paramName = $"@{propName}";
                sqlColumns.Append($"{delimiter}{propName} = {paramName}");
                delimiter = ",";
                parameters.Add(paramName, propValue);

            }
            //3 Lây ra giá trị của property 
            // 4. Thực hiện build câu lệnh sql
            //var sqlCommand = $"INSERT INTO {className}({sqlColumsNames.ToString()}) VALUES ({sqlColumsValue.ToString()})";
            var sqlCommand = $"UPDATE {className} SET {sqlColumns.ToString()} WHERE {className}Id = @{className}Id";
             parameters.Add($"@{className}Id", entityId);
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                var result = SqlConnection.Execute(sql: sqlCommand, param: parameters);
                return result;
            }

        }

        #endregion
    }
}
