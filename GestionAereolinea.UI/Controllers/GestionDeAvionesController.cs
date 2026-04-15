using GestionAereolinea.Model;
using GestionAereolinea.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// Controlador MVC para gestionar los aviones (UI)
public class GestionDeAvionesController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory; // Fábrica para crear clientes HTTP

    // Constructor: recibe la dependencia
    public GestionDeAvionesController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory; // Guarda la referencia
    }

    // Index con búsqueda por nombre de aerolínea
    public async Task<IActionResult> Index(string nombreAerolinea) 
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");// Crea cliente HTTP



        var response = await client.GetAsync("api/ServicioDeAviones");// Llama al API para obtener todos los aviones

        if (!response.IsSuccessStatusCode) // Si ocurre un error
        {
            var error = await response.Content.ReadAsStringAsync();// Lee el mensaje
            return Content($"Error: {error}"); // Muestra error en pantalla
        }

        var json = await response.Content.ReadAsStringAsync();// Lee la respuesta en JSON

        // Convierte el JSON a lista de objetos Avion
        var lista = JsonSerializer.Deserialize<List<Avion>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Avion>();

        // Filtrar por nombre de aerolínea si se ingresó algo
        if (!string.IsNullOrEmpty(nombreAerolinea))
        {
            lista = lista.Where(x =>
                x.Aerolinea != null && // Verifica que exista la aerolínea
                x.Aerolinea.Nombre.Contains(nombreAerolinea, StringComparison.OrdinalIgnoreCase) // Coincidencia sin importar mayúsculas
            ).ToList();
        }

        return View(lista);// Envía la lista a la vista
    }

    // Crear avión
    public IActionResult Create()
    {
        return View(); // Muestra el formulario de creación
    }

    [HttpPost]
    public async Task<IActionResult> Create(Avion avion) 
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP

        await client.PostAsJsonAsync("api/ServicioDeAviones", avion);// Envía el objeto como JSON al API

        return RedirectToAction("Index"); // Redirige a la lista
    }

    // Editar avión
    public async Task<IActionResult> Edit(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync($"api/ServicioDeAviones/{id}"); // Obtiene el avión por ID

        var json = await response.Content.ReadAsStringAsync(); // Lee JSON
     // Convierte JSON a objeto Avion
        var avion = JsonSerializer.Deserialize<Avion>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(avion); // Envía el objeto a la vista
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Avion avion)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        await client.PutAsJsonAsync("api/ServicioDeAviones", avion);// Envía los cambios al API

        return RedirectToAction("Index"); // Regresa a la lista
    }

    // Detalles de un avión
    public async Task<IActionResult> Details(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.GetAsync($"api/ServicioDeAviones/{id}"); // Consulta el avión

        if (!response.IsSuccessStatusCode) // Si hay error
        {
            var error = await response.Content.ReadAsStringAsync(); // Lee el mensaje de error que devuelve el API
            return Content($"Error al obtener detalles. Status: {response.StatusCode}, Mensaje: {error}"); // Muestra el error en pantalla
        }

        var json = await response.Content.ReadAsStringAsync();  // Lee la respuesta del API en formato JSON

        if (string.IsNullOrEmpty(json)) // Verifica si la respuesta viene vacía
        {
            return Content("La API devolvió vacío"); // Muestra un mensaje si no hay datos
        }
        // Convierte el JSON recibido en un objeto de tipo Avion
        var avion = JsonSerializer.Deserialize<Avion>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // Ignora mayúsculas/minúsculas en los nombres

        return View(avion); // Envía el objeto Avion a la vista para mostrar los detalles
    }

    // Eliminar avión

    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient("AerolineaApi");

        var response = await client.DeleteAsync($"api/ServicioDeAviones/{id}"); // Llama al API para eliminar

        if (!response.IsSuccessStatusCode) // Si falla
        {
            var error = await response.Content.ReadAsStringAsync(); // Lee el mensaje de error que devuelve el API
            return Content($"Error al eliminar: {error}"); // Muestra el error en pantalla
        }

        return RedirectToAction("Index");  // Regresa a la lista
    }
}