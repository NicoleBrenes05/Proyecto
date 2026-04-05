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

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync("api/ServicioDeAviones");

        var json = await response.Content.ReadAsStringAsync();

        var lista = JsonSerializer.Deserialize<List<Avion>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(lista);
    }

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
}