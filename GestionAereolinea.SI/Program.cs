using GestionAereolinea.BL;
using GestionAereolinea.DA;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DBContexto>(options =>
    options.UseInMemoryDatabase("AereolineaDB"));



builder.Services.AddScoped<IAvionRepository, AvionRepository>();
builder.Services.AddScoped<IAdministradorDeAviones, AdministradorDeAviones>();


builder.Services.AddScoped<IAerolineaRepository, AerolineaRepository>();
builder.Services.AddScoped<IAdministradorDeAerolineas, AdministradorDeAerolineas>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();