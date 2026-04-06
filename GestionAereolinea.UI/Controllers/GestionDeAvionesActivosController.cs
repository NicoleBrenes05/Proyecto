using GestionAereolinea.Model;
using GestionAereolinea.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


public class GestionDeAvionesActivosController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GestionDeAvionesActivosController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync("api/ServicioDeAviones/ObtengaLaListaDeActivos");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error al llamar a la API. Status: {response.StatusCode}, Mensaje: {error}");
        }

        var json = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(json))
        {
            return Content("La API devolvió vacío");
        }

        var lista = JsonSerializer.Deserialize<List<Avion>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(lista);
    }

   
    
}