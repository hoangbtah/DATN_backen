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
    public class CommentRepository:BaseRepository<Comment>,ICommentRepository
    {
       

        public object GetCommentByProduct(Guid ProductId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT * FROM Comment Where ProductId = @ProductId";
                var parameters = new DynamicParameters();
                parameters.Add("@ProductId", ProductId);
                var comment = SqlConnection.Query<object>(sqlCommand, parameters);
                return comment;
            }
        }
    }
}
