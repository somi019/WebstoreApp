using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.GRPC.Protos;
using MassTransit;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

// Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// GetExecutingAssembly(), hvatas assembly BasketApi, automapper prodje kroz sve klase
// i hvata sve one klase koje nasledjuju Profile i nad tim svim klasama pozvace
// konstruktor i napravice sva mapiranja koja smo trazili


//EventBus
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        //ctx - context, cfg - configuration
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

builder.Services.AddMassTransitHostedService();


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
