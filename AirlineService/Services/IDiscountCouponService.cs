using AirlineService.Models;
using System;
using System.Collections.Generic;

namespace AirlineService.Services
{
    public interface IDiscountCouponService
    {
        DiscountCoupon AddCoupon(DiscountCoupon discountCoupon);

        DiscountCoupon GetByCode(string code);

        DiscountCoupon GetById(Guid id);

        List<DiscountCoupon> GetAll();

        DiscountCoupon Update(DiscountCoupon discountCoupon);

        void Delete(Guid id);
    }
}
