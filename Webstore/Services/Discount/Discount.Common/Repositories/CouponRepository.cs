using AutoMapper;
using Dapper;
using Discount.Common.Data;
using Discount.Common.DTOs;
using Discount.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Common.Repositories
{
    internal class CouponRepository : ICouponRepository
    {
        private readonly ICouponContext _couponContext;
        private readonly IMapper _mapper;

        public CouponRepository(ICouponContext couponContext, IMapper mapper)
        {
            _couponContext = couponContext ?? throw new ArgumentNullException(nameof(couponContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CouponDTO> GetDiscount(string productName)
        {
            // using var connection se koristi umesto open i close da ne bi
            // doslo do komplikacija, kako god da se zavrsi funkcija(crash)
            // objekat connection bice automatski unisten
            using var connection = _couponContext.GetConnection();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE ProductName= @ProductName",
                new { ProductName = productName});

            return _mapper.Map<CouponDTO>(coupon);
            // _mapper menja iz tipa Coupon u tip CouponDTO
            // imaju sve iste atribute
        }

        public Task<bool> CreateDiscount(CreateCouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateDiscount(UpdateCouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CouponDTO>> GetRandomDiscounts(int numberOfDisconuts)
        {
            throw new NotImplementedException();
        }


    }
}
