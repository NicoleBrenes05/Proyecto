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
        var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP
        var response = await client.GetAsync("api/ServicioDeAviones/ObtengaLaListaDeInActivos");// Llama al API para obtener solo los aviones inactivos

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();// Lee mensaje de error
            return Content($"Error al llamar a la API. Status: {response.StatusCode}, Mensaje: {error}");
        }

        var json = await response.Content.ReadAsStringAsync(); // Lee la respuesta en JSON
        if (string.IsNullOrEmpty(json))// Verifica si viene vacío
            return Content("La API devolvió vacío");
        // Convierte JSON a lista de Avion
        var lista = JsonSerializer.Deserialize<List<Avion>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(lista); // Envía la lista a la vista
    }
    // Mostrar avión para editar
    public async Task<IActionResult> Edit(int id)
    {
        var avion = await _servicioApi.ObtenerAvionPorIdAsync(id); // Obtiene el avión por ID usando ServicioApi

        if (avion == null) // Si no existe, devuelve 404
            return NotFound(); 

        return View(avion);// Envía el avión a la vista
    }
    // activar avion
    [HttpPost]
    public async Task<IActionResult> Edit(int id, string nuevoEstado)
    {
        try
        {
            // Si el usuario selecciona activar
            if (nuevoEstado == "Activar")
            {
                await _servicioApi.ActivarAvionAsync(id);// Llama al API para activar el avión
            }

            return RedirectToAction("Index");// Regresa a la lista
        }
        catch
        {
            ViewData["ProblemasAlInsertar"] = true; // Indica error en la vista
            var avion = await _servicioApi.ObtenerAvionPorIdAsync(id); // Vuelve a cargar el avión
            return View(avion);// Retorna a la vista con los datos
        }
    }


}