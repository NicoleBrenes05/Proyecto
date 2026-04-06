using GestionAereolinea.Model;
using GestionAereolinea.UI;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class GestionDeAvionesInactivosController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ServicioApi _servicioApi;

    public GestionDeAvionesInactivosController(IHttpClientFactory httpClientFactory, ServicioApi servicioApi)
    {
        _httpClientFactory = httpClientFactory;
        _servicioApi = new ServicioApi(httpClientFactory);
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

    public async Task<IActionResult> Edit(int id)
    {
        var avion = await _servicioApi.ObtenerAvionPorIdAsync(id);

        if (avion == null)
            return NotFound();

        return View(avion);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string nuevoEstado)
    {
        try
        {
            if (nuevoEstado == "Activar")
            {
                await _servicioApi.ActivarAvionAsync(id);
            }

            return RedirectToAction("Index");
        }
        catch
        {
            ViewData["ProblemasAlInsertar"] = true;
            var avion = await _servicioApi.ObtenerAvionPorIdAsync(id);
            return View(avion);
        }
    }


}