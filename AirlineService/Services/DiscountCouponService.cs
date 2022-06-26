using AirlineService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Exceptions;

namespace AirlineService.Services
{
    public class DiscountCouponService : IDiscountCouponService
    {
        private readonly AppDbContext dbContext;

        public DiscountCouponService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public DiscountCoupon AddCoupon(DiscountCoupon discountCoupon)
        {
            if (dbContext.DiscountCoupons.Any(x => x.CouponCode == discountCoupon.CouponCode))
                throw new AppException($"Discount Coupon with the code {{{discountCoupon.CouponCode}}} already exits");

            dbContext.Add(discountCoupon);
            dbContext.SaveChanges();
            return discountCoupon;
        }

        public void Delete(Guid id)
        {
            dbContext.Remove(new DiscountCoupon { Id = id });
            dbContext.SaveChanges();
        }

        public List<DiscountCoupon> GetAll()
        {
            return dbContext.DiscountCoupons.ToList();
        }

        public DiscountCoupon GetByCode(string code)
        {
            var coupon = dbContext.DiscountCoupons.FirstOrDefault(x => x.CouponCode == code && x.ValidUpto > DateTime.Now);

            if (coupon == null)
                throw new AppException("Invalid Coupon Code");

            return coupon;
        }

        public DiscountCoupon GetById(Guid id)
        {
            return dbContext.DiscountCoupons.Find(id);
        }
        public DiscountCoupon Update(DiscountCoupon discountCoupon)
        {
            dbContext.Entry(discountCoupon).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            return discountCoupon;
        }
    }
}
