using Dapper;
using Discount.Grpc.Entities;
using Microsoft.AspNetCore.Connections.Features;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;


        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetCoupon(string productName)
        {

            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Description = "No Discount Desc", Amount = 0 };
            }

            return coupon;
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName,Description,Amount) VALUES (@ProductName,@Description,@Amount)",
                new
                {
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount
                });

            return affected > 0;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new
                {
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount,
                    coupon.Id
                });

            return affected > 0;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            return affected > 0;

        }

    }
}
