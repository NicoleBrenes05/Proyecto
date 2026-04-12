using GestionAereolinea.BL;
using GestionAereolinea.DA;
using GestionAereolinea.SI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API Gestión de Aerolíneas", Version = "v1" });

    c.AddSecurityDefinition("ApiKey", new()
    {
        Description = "Clave de API en el header: X-API-KEY",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new() { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });




// Base de datos en memoria 
builder.Services.AddDbContext<DBContexto>(options =>
    options.UseInMemoryDatabase("AereolineaDB"));



builder.Services.AddScoped<IAvionRepository, AvionRepository>();
builder.Services.AddScoped<IAdministradorDeAviones, AdministradorDeAviones>();


builder.Services.AddScoped<IAerolineaRepository, AerolineaRepository>();
builder.Services.AddScoped<IAdministradorDeAerolineas, AdministradorDeAerolineas>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();