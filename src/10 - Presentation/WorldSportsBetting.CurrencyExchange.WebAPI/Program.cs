using WorldSportsBetting.CurrencyExchange.Infrastructure.OpenExchangeRates.Extensions;
using WorldSportsBetting.CurrencyExchange.Core.Extensions;
using WorldSportsBetting.CurrencyExchange.MySql.Extensions;
using WorldSportsBetting.CurrencyExchange.Cache.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.RegisterOpenExchangeRatesApiOptions(builder.Configuration);
builder.Services.RegisterRedis(builder.Configuration);
builder.Services.RegisterRedisCacheInstance();
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterMySqlDatabase(builder.Configuration);
builder.Services.RegisterMySqlAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
