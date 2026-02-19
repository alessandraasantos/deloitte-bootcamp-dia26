using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using System.Text.Json.Serialization; // Necessário para o conversor de Enum

var builder = WebApplication.CreateBuilder(args);

// Configuração do Swagger/OpenAPI
builder.Services.AddOpenApi();

// Registro do DbContext com Npgsql e Convenção de Snake Case
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = "Host=localhost;Port=5432;Database=apidb_final;Username=postgres;Password=postgres"; 
    options
        .UseNpgsql(cs)
        .UseSnakeCaseNamingConvention();
});


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
       
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
       
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();