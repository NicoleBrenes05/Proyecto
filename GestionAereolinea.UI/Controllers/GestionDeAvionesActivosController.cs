using GestionAereolinea.Model;
using GestionAereolinea.UI;
using GestionAereolinea.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


public class GestionDeAvionesActivosController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory; // Fábrica para crear clientes HTTP
    private readonly ServicioApi _servicioApi; // Servicio que consume el API

    public GestionDeAvionesActivosController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory; // Guarda la referencia
        _servicioApi = new ServicioApi(httpClientFactory); // Crea instancia del servicio API
    }

    //Método listar
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");
        // Llama al endpoint que trae solo los aviones activos
        var response = await client.GetAsync("api/ServicioDeAviones/ObtengaLaListaDeActivos");
        // Si ocurre un error en la petición
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(); // Lee el mensaje de error
            return Content($"Error al llamar a la API. Status: {response.StatusCode}, Mensaje: {error}");
        }

        var json = await response.Content.ReadAsStringAsync(); // Lee la respuesta en formato JSON
        // Si la API no devolvió datos
        if (string.IsNullOrEmpty(json))
        {
            return Content("La API devolvió vacío");  // Mensaje simple
        }
        // Convierte el JSON a lista de objetos Avion
        var lista = JsonSerializer.Deserialize<List<Avion>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(lista); // Envía la lista a la vista para mostrarla
    }

    public async Task<IActionResult> Edit(int id)
    {
        var avion = await _servicioApi.ObtenerAvionPorIdAsync(id); // Obtiene el avión por ID usando ServicioApi


        if (avion == null) 
            return NotFound(); // Si no existe, devuelve 404

        return View(avion); // Envía el avión a la vista de edición
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string nuevoEstado)
    {
        try
        {
            if (nuevoEstado == "Desactivar")  // Si el usuario selecciona desactivar
            {
                await _servicioApi.DesactivarAvionAsync(id); // Llama al API para desactivar
            }

            return RedirectToAction("Index"); // Redirige a la lista después de editar
        }
        catch
        {
            ViewData["ProblemasAlInsertar"] = true;  // Indica que hubo un error
            var avion = await _servicioApi.ObtenerAvionPorIdAsync(id); // Vuelve a cargar el avión
            return View(avion); // Retorna a la vista con los datos
        }


    }
}