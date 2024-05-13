using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_Infrastructure.Repository
{
    public class VoucherRepository:BaseRepository<Voucher>,IVoucherRepository
    {
        public async Task<int> CreateVoucher(Voucher voucher)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {
                string query = @"
            INSERT INTO Voucher (VoucherId, StartDateVoucher, EndDateVoucher, PercentVoucher,
            MaxximumUse, StartPrice, EndPrice, DecriptionUse)
            VALUES (@VoucherId, @StartDateVoucher, @EndDateVoucher, @PercentVoucher, @MaxximumUse
            , @StartPrice, @EndPrice, @DecriptionUse);

            INSERT INTO UserVoucher (UserVoucherId, UserID, VoucherID)
            SELECT UUID(), u.UserID, v.VoucherID
            FROM User u
            CROSS JOIN Voucher v
            WHERE v.VoucherId = @VoucherId;";


                return await SqlConnection.ExecuteAsync(query, voucher);
            }
        }
        public object getVoucherByUser(Guid userId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "select * from User Inner Join UserVoucher "+
                               " On User.UserId = UserVoucher.UserId Inner Join Voucher "+
                              "  On UserVoucher.VoucherId = Voucher.VoucherId " +
                              "  Where User.UserId = @userId ";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId);

                var vouchers = SqlConnection.Query<object>(sqlCommand, parameters);
                return vouchers;
            }
        }
    }
}
