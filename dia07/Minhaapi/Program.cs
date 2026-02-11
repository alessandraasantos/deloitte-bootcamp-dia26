
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using StackExchange.Redis; // ← ADICIONADO

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

// 🔴 REGISTRO DO REDIS (ADICIONADO)
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration["Redis:ConnectionString"] ?? "localhost:6379";
    return ConnectionMultiplexer.Connect(configuration);
});

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
