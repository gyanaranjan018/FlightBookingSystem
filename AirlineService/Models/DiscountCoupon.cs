using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Models
{
    public class DiscountCoupon
    {
        public Guid Id { get; set; }

        public string CouponCode { get; set; }

        public DateTime ValidUpto { get; set; }

        public int DiscountPercent { get; set; }
    }
}
