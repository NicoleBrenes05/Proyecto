using GestionAereolinea.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración del HttpClient
var apiConfig = builder.Configuration.GetSection("AerolineaApi");
var urlBase = apiConfig.GetValue<string>("BaseUrl");

builder.Services.AddHttpClient("AerolineaApi", client =>
{
    client.BaseAddress = new Uri(urlBase);
    
});
// Inyección del servicio
builder.Services.AddScoped<ServicioApi>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();