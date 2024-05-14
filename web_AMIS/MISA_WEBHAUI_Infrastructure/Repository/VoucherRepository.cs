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
            INSERT INTO Voucher (VoucherId,VoucherCode, StartDateVoucher, EndDateVoucher, PercentVoucher,
            MaxximumUse, StartPrice, EndPrice, DecriptionUse,DiscountMoney,CreateDate)
            VALUES (@VoucherId,@VoucherCode, @StartDateVoucher, @EndDateVoucher, @PercentVoucher, @MaxximumUse
            , @StartPrice, @EndPrice, @DecriptionUse,@DiscountMoney,@CreateDate);

            INSERT INTO UserVoucher (UserVoucherId, UserID, VoucherID)
            SELECT UUID(), u.UserID, v.VoucherID
            FROM User u
            CROSS JOIN Voucher v
            WHERE v.VoucherCode = @VoucherCode;";


                return await SqlConnection.ExecuteAsync(query, voucher);
            }
        }
        /// <summary>
        /// lấy voucher của người dùng theo mã người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object getVoucherByUser(Guid userId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "select * from User Inner Join UserVoucher "+
                               " On User.UserId = UserVoucher.UserId Inner Join Voucher "+
                              "  On UserVoucher.VoucherId = Voucher.VoucherId " +
                              "  Where User.UserId = @userId " +
                              "ORDER BY Voucher.CreateDate DESC";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId);

                var vouchers = SqlConnection.Query<object>(sqlCommand, parameters);
                return vouchers;
            }
        }
        /// <summary>
        /// lấy thông tin của voucher
        /// </summary>
        /// <param name="voucherCode"></param>
        /// <returns></returns>
        public object getVoucherByVoucherCode(string voucherCode)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "select * from Voucher  " +
                              "  Where VoucherCode = @voucherCode ";
                var parameters = new DynamicParameters();
                parameters.Add("@voucherCode", voucherCode);

                var voucher = SqlConnection.QueryFirstOrDefault<object>(sqlCommand, parameters);
                return voucher;
            }
        }

    }
}
