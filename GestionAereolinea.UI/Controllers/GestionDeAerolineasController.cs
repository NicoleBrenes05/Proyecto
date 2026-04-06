using GestionAereolinea.Model;
using GestionAereolinea.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class GestionDeAerolineasController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GestionDeAerolineasController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

   
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync("api/ServicioDeAerolinea");

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

        var lista = JsonSerializer.Deserialize<List<Aerolinea>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(lista);
    }

  
    public IActionResult Create()
    {
        return View();
    }

   
    [HttpPost]
    public async Task<IActionResult> Create(Aerolinea aerolinea)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.PostAsJsonAsync("api/ServicioDeAerolinea", aerolinea);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error al crear la aerolínea. Status: {response.StatusCode}, Mensaje: {error}");
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync($"api/ServicioDeAerolinea/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error al obtener la aerolínea. Status: {response.StatusCode}, Mensaje: {error}");
        }

        var json = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(json))
        {
            return Content("La API devolvió vacío");
        }

        var aerolinea = JsonSerializer.Deserialize<Aerolinea>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(aerolinea);
    }

    
    [HttpPost]
    public async Task<IActionResult> Edit(Aerolinea aerolinea)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

       
        var response = await client.PutAsJsonAsync($"api/ServicioDeAerolinea", aerolinea);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error al editar la aerolínea. Status: {response.StatusCode}, Mensaje: {error}");
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync($"api/ServicioDeAerolinea/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error al obtener detalles. Status: {response.StatusCode}, Mensaje: {error}");
        }

        var json = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(json))
        {
            return Content("La API devolvió vacío");
        }

        var aerolinea = JsonSerializer.Deserialize<Aerolinea>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(aerolinea);
    }
}