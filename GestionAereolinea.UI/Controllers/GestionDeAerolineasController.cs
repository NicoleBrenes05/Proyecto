using GestionAereolinea.Model;
using GestionAereolinea.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GestionAereolinea.UI.Controllers
{
    // Controlador MVC para manejar aerolíneas en la UI
    public class GestionDeAerolineasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; // Fábrica para crear clientes HTTP

        // Constructor: recibe la dependencia
        public GestionDeAerolineasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;// Guarda la referencia
        }

        // Listar y filtrar aerolíneas
        public async Task<IActionResult> Index(string busqueda)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");// Crea cliente HTTP

            var response = await client.GetAsync("api/ServicioDeAerolinea"); // Llama al API para obtener todas


            if (!response.IsSuccessStatusCode) // Si ocurre error
            {
                var error = await response.Content.ReadAsStringAsync(); // Lee mensaje de error
                return Content($"Error al llamar a la API. Status: {response.StatusCode}, Mensaje: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();// Lee la respuesta en JSON

            if (string.IsNullOrEmpty(json)) // Verifica si viene vacío
                return Content("La API devolvió vacío");
            // Convierte JSON a lista de Aerolinea
            var lista = JsonSerializer.Deserialize<List<Aerolinea>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Aerolinea>();

            // Filtrar por Id, Nombre o Teléfono si el usuario escribió algo
            if (!string.IsNullOrEmpty(busqueda))
            {
                lista = lista.Where(x =>
                    x.Id.ToString().Contains(busqueda) ||// Filtra por ID
                    x.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||// Filtra por nombre
                    x.Telefono.Contains(busqueda, StringComparison.OrdinalIgnoreCase)// Filtra por teléfono
                ).ToList();
            }

            return View(lista); // Envía la lista a la vista
        }

        // Crear aerolínea (GET)
        public IActionResult Create()
        {
            return View();// Muestra el formulario
        }

        // Crear aerolínea (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Aerolinea aerolinea)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");  // Crea cliente

            var response = await client.PostAsJsonAsync("api/ServicioDeAerolinea", aerolinea); // Envía datos al API

            if (!response.IsSuccessStatusCode)// Si falla
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content($"Error al crear la aerolínea. Status: {response.StatusCode}, Mensaje: {error}");
            }

            return RedirectToAction("Index");// Regresa a la lista
        }

        // Editar aerolínea (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var response = await client.GetAsync($"api/ServicioDeAerolinea/{id}");// Consulta por ID

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content($"Error al obtener la aerolínea. Status: {response.StatusCode}, Mensaje: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();// Lee JSON

            if (string.IsNullOrEmpty(json))
                return Content("La API devolvió vacío");

            // Convierte JSON a objeto Aerolinea
            var aerolinea = JsonSerializer.Deserialize<Aerolinea>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(aerolinea); // Envía a la vista
        }

        // Editar aerolínea (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Aerolinea aerolinea)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var response = await client.PutAsJsonAsync($"api/ServicioDeAerolinea", aerolinea);// Envía cambios

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content($"Error al editar la aerolínea. Status: {response.StatusCode}, Mensaje: {error}");
            }

            return RedirectToAction("Index");// Regresa a la lista
        }

        // Detalles de aerolínea
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var response = await client.GetAsync($"api/ServicioDeAerolinea/{id}"); // Consulta por ID

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();  // Lee JSON
                return Content($"Error al obtener detalles. Status: {response.StatusCode}, Mensaje: {error}");
            }

            var json = await response.Content.ReadAsStringAsync(); // Lee JSON

            if (string.IsNullOrEmpty(json))
                return Content("La API devolvió vacío");
            // Convierte a objeto Aerolinea
            var aerolinea = JsonSerializer.Deserialize<Aerolinea>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(aerolinea);// Muestra detalles
        }

        // eliminar aerolinea
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var response = await client.DeleteAsync($"api/ServicioDeAerolinea/{id}");// Llama al API para eliminar

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content($"Error al eliminar la aerolínea. Status: {response.StatusCode}, Mensaje: {error}");
            }

            return RedirectToAction("Index"); // Regresa a la lista
        }
    }
}

