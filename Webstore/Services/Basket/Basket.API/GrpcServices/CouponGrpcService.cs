﻿using Discount.GRPC.Protos;

namespace Basket.API.GrpcServices
{
    public class CouponGrpcService
    {
        private readonly CouponProtoService.CouponProtoServiceClient _couponProtoServiceClient;

        public CouponGrpcService(CouponProtoService.CouponProtoServiceClient couponProtoServiceClient)
        {
            _couponProtoServiceClient = couponProtoServiceClient ?? throw new ArgumentNullException(nameof(couponProtoServiceClient));
        }

        public async Task<GetDiscountResponse> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest(); 
            discountRequest.ProductName = productName;

            return await _couponProtoServiceClient.GetDiscountAsync(discountRequest);   

                     
        }

    }
}
