using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Fila;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Registro do DbContext com Npgsql
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = "Host=localhost;Port=5432;Database=minhaapi_db;Username=postgres;Password=postgres";
    options
        .UseNpgsql(cs)
        .UseSnakeCaseNamingConvention();
});

// 🔴 ADICIONADO — conexão Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetSection("Redis")["ConnectionString"];
    return ConnectionMultiplexer.Connect(configuration!);
});

// 🔴 ADICIONADO — publisher Redis
builder.Services.AddScoped<IRedisPublisher, RedisPublisher>();

// Recomendação do Npgsql para compatibilidade de timestamp (se aplicável)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Mapear controllers
app.MapControllers();

app.Run();
