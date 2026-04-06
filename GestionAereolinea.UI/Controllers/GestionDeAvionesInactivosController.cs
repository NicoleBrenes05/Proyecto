using GestionAereolinea.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class GestionDeAvionesInactivosController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GestionDeAvionesInactivosController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Listar solo aviones inactivos
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");
        var response = await client.GetAsync("api/ServicioDeAviones/ObtengaLaListaDeInActivos");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error al llamar a la API. Status: {response.StatusCode}, Mensaje: {error}");
        }

        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(json))
            return Content("La API devolvió vacío");

        var lista = JsonSerializer.Deserialize<List<Avion>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(lista); 
    }

 
}