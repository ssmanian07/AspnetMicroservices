﻿using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCoupon(string productName);
        Task<bool> CreateCoupon (Coupon coupon);
        Task<bool> UpdateCoupon (Coupon coupon);
        Task<bool> DeleteCoupon (string productName);

    }
}
