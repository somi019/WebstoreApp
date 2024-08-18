using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.GRPC.Protos;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddStackExchangeRedisCache(options => 
{
    // nije dobra praksa stavljati sve u string, najbolje je sve staviti u konfiguracione promenljive
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"); 
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//gRPC
builder.Services.AddGrpcClient<CouponProtoService.CouponProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<CouponGrpcService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
