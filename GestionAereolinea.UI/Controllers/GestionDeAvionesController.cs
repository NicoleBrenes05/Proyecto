using GestionAereolinea.Model;
using GestionAereolinea.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class GestionDeAvionesController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GestionDeAvionesController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Index con búsqueda por nombre de aerolínea
    public async Task<IActionResult> Index(string nombreAerolinea)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        

        var response = await client.GetAsync("api/ServicioDeAviones");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error: {error}");
        }

        var json = await response.Content.ReadAsStringAsync();

        var lista = JsonSerializer.Deserialize<List<Avion>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Avion>();

        // Filtrar por nombre de aerolínea si se ingresó algo
        if (!string.IsNullOrEmpty(nombreAerolinea))
        {
            lista = lista.Where(x =>
                x.Aerolinea != null &&
                x.Aerolinea.Nombre.Contains(nombreAerolinea, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        return View(lista);
    }

    // Crear avión
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Avion avion)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        await client.PostAsJsonAsync("api/ServicioDeAviones", avion);

        return RedirectToAction("Index");
    }

    // Editar avión
    public async Task<IActionResult> Edit(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync($"api/ServicioDeAviones/{id}");

        var json = await response.Content.ReadAsStringAsync();

        var avion = JsonSerializer.Deserialize<Avion>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(avion);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Avion avion)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        await client.PutAsJsonAsync("api/ServicioDeAviones", avion);

        return RedirectToAction("Index");
    }

    // Detalles de un avión
    public async Task<IActionResult> Details(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync($"api/ServicioDeAviones/{id}");

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

        var avion = JsonSerializer.Deserialize<Avion>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(avion);
    }

    // Eliminar avión

    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.DeleteAsync($"api/ServicioDeAviones/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return Content($"Error al eliminar: {error}");
        }

        return RedirectToAction("Index");
    }
}