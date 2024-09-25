using IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication();

builder.Services.ConfigurePerstistence(builder.Configuration);
builder.Services.ConfigureIdentity();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// uvek ovaj redosled prvo auth pa autor
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
