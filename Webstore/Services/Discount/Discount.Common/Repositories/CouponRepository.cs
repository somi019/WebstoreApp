using AutoMapper;
using Dapper;
using Discount.Common.Data;
using Discount.Common.DTOs;
using Discount.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Common.Repositories
{
    public class CouponRepository : ICouponRepository
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

        public async Task<bool> CreateDiscount(CreateCouponDTO couponDTO)
        {
            using var connection = _couponContext.GetConnection();
            var affected = await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName,Description,Amount) VALUES (@ProductName,@Description,@Amount)",
                new { ProductName = couponDTO.ProductName, Description = couponDTO.Description, Amount = couponDTO.Amount }
                );
            // popunimo placeholdere sa atributima anonimnog objekta

            return affected != 0;

        }
        public async Task<bool> UpdateDiscount(UpdateCouponDTO couponDTO)
        {
            using var connection = _couponContext.GetConnection();
            var affected = await connection.ExecuteAsync(
                "UPDATE Coupon SET ProductName=@ProductName,Description =@Description,Amount=@Amount WHERE Id = @Id",
                new { couponDTO.ProductName,  couponDTO.Description, couponDTO.Amount, couponDTO.Id }
                );

            return affected != 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = _couponContext.GetConnection();
            var affected = await connection.ExecuteAsync(
                "DELETE FROM Coupon WHERE ProductName=@ProductName",
                new { ProductName = productName}
                );

            return affected != 0;
        }

        public async Task<IEnumerable<CouponDTO>> GetRandomDiscounts(int numberOfDisconuts)
        {
            // LINQ deo standardne biblioteke .NET-a koji radi sa kolekcijama

            using var connection = _couponContext.GetConnection();

            var allCoupons = await connection.QueryAsync<Coupon>("SELECT * FROM Coupon");

            if (allCoupons.Count() < numberOfDisconuts) {
                return _mapper.Map<IEnumerable<CouponDTO>>(allCoupons);
                // za mapper konverziju mora da vazi : mora da imaju polja koja se identicno zovu i kojima moze da se pristupi javno
                // dobro je koristiti ga za stvari za koje znas kako se preslikavaju jedna u drugu
            }

            var r = new Random();
            return _mapper.Map<IEnumerable<CouponDTO>>(allCoupons
                .Select(c => new { Number = r.Next(), Item = c })
                .OrderBy(obj=> obj.Number)
                .Select(obj=>obj.Item)
                .Take(numberOfDisconuts));
            // dali smo svakom po random broj sortirali po broju, sklonili taj broj i uzeli prvih numberOfDiscounts kupona


     
        }


    }
}
