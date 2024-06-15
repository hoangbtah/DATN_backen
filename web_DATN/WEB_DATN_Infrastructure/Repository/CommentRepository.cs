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

                var sqlCommand = "SELECT c.CommentId, c.ProductId,c.PostDate,c.UserId,c.CommentContent,u.Name ,c.Rating" +
                    " FROM Comment c INNER JOIN User u " +
                    "ON c.UserId = u.UserId " +
                    " Where ProductId = @ProductId";
                var parameters = new DynamicParameters();
                parameters.Add("@ProductId", ProductId);
                var comment = SqlConnection.Query<object>(sqlCommand, parameters);
                return comment;
            }
        }
    }
}
