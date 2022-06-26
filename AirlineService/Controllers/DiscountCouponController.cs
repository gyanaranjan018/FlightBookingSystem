using AirlineService.Models;
using AirlineService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCouponController : ControllerBase
    {
        private readonly IDiscountCouponService couponService;

        public DiscountCouponController(IDiscountCouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet]
        public List<DiscountCoupon> Get()
        {
            return couponService.GetAll();
        }

        [HttpGet("code/{code}")]
        public DiscountCoupon GetByCode(string code)
        {
            return couponService.GetByCode(code);
        }

        [HttpGet("{id}")]
        public DiscountCoupon GetById(Guid id)
        {
            return couponService.GetById(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public DiscountCoupon Post(DiscountCoupon discountCoupon)
        {
            return couponService.AddCoupon(discountCoupon);
        }
    }
}
