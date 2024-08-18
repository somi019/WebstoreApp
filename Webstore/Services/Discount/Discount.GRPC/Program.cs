
using Discount.Common.DTOs;
using Discount.Common.Extensions;
using Discount.GRPC.Protos;
using Discount.GRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDiscountCommonServices();

// Mapiranja : CouponDTO -> GetDiscountResponse,
// CouponDTO -> GetRandomDiscountsResponse.Types.Coupon
builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<CouponDTO,GetDiscountResponse>().ReverseMap();
    configuration.CreateMap<CouponDTO, GetRandomDiscountsResponse.Types.Coupon>().ReverseMap();
});


var app = builder.Build();

// Configure the HTTP request pipeline.

// koristi ovaj GRPC servis
app.MapGrpcService<CouponService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
