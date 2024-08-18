using Discount.Common.Data;
using Discount.Common.DTOs;
using Discount.Common.Entities;
using Discount.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Common.Extensions
{
    public static class DiscountCommonExtensions
    {
        // napravili smo extensions folder da napisemo ovde
        // program.cs iz drugih mikroservisa rucno
        // jer ovaj class library nema to u sebi (nije WebAPI)
        // napravili smo da je klasa staticka (i svaka metoda onda mora da bude)
        // i argument ima specifikator this da bi mogli u bilo kom program.cs
        // fajlu nekog mikroservisa da napisemo services.AddDiscountCommonServices();
        // znaci to je argument koji je zapravo pozivac funkcije
        public static void AddDiscountCommonServices(this IServiceCollection services) 
        {
            services.AddScoped<ICouponContext, CouponContext>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddAutoMapper(configuration => 
            {
                configuration.CreateMap<CouponDTO, Coupon>().ReverseMap();

            });


        }

    }
}
