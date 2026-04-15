using GestionAereolinea.Model;
using Newtonsoft.Json;
using System.Text;

namespace GestionAereolinea.UI
{
    public class ServicioApi // Clase que consume el API
    {
        private readonly IHttpClientFactory _httpClientFactory; // Fábrica para crear clientes HTTP

        public ServicioApi(IHttpClientFactory httpClientFactory) // Constructor con inyección de dependencia
        {
            _httpClientFactory = httpClientFactory; // Guarda la referencia del factory
        }

      
        //AEROLINEAS
        

        public async Task<List<Aerolinea>> ObtenerAerolineasAsync() // Método para obtener todas las aerolíneas
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP
            var response = await client.GetAsync("api/ServicioDeAerolinea"); // Hace petición GET al API
            response.EnsureSuccessStatusCode();// Verifica que la respuesta sea exitosa

            var result = await response.Content.ReadAsStringAsync();// Lee la respuesta en formato texto (JSON)
            return JsonConvert.DeserializeObject<List<Aerolinea>>(result) ?? []; // Convierte JSON a lista de objetos
        }

        public async Task<Aerolinea?> ObtenerAerolineaPorIdAsync(int id)// Busca una aerolínea por ID
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");// Crea cliente HTTP
            var response = await client.GetAsync($"api/ServicioDeAerolinea/{id}"); // GET con parámetro id

            if (!response.IsSuccessStatusCode) return null; // Si falla, retorna null

            var result = await response.Content.ReadAsStringAsync();// Lee JSON
            return JsonConvert.DeserializeObject<Aerolinea>(result);// Convierte a objeto Aerolinea
        }

        public async Task<Aerolinea?> ObtenerAerolineaPorNombreAsync(string nombre) // Busca por nombre
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAerolinea/nombre/{nombre}");

            if (!response.IsSuccessStatusCode) return null; // Si falla, retorna null

            var result = await response.Content.ReadAsStringAsync();// Lee JSON
            return JsonConvert.DeserializeObject<Aerolinea>(result);// Convierte a objeto Aerolinea
        }

        public async Task<Aerolinea?> ObtenerAerolineaPorTelefonoAsync(string telefono) // Busca por teléfono
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAerolinea/telefono/{telefono}");

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();// Lee JSON
            return JsonConvert.DeserializeObject<Aerolinea>(result);// Convierte a objeto Aerolinea
        }

        public async Task<List<Avion>> ObtenerAvionesPorAerolineaAsync(string nombre)  // Obtiene aviones de una aerolínea
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAerolinea/aerolinea/{nombre}/aviones");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();// Lee JSON
            return JsonConvert.DeserializeObject<List<Avion>>(result) ?? [];// Convierte a lista de aviones
        }

        public async Task AgregarAerolineaAsync(Aerolinea aerolinea) // Agrega una nueva aerolínea
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var json = JsonConvert.SerializeObject(aerolinea);// Convierte objeto a JSON
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Prepara contenido HTTP

            var response = await client.PostAsync("api/ServicioDeAerolinea", content); // Envía POST
            response.EnsureSuccessStatusCode();// Verifica éxito
        }

        public async Task EditarAerolineaAsync(Aerolinea aerolinea) // Edita una aerolínea existente
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var json = JsonConvert.SerializeObject(aerolinea);
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Prepara contenido HTTP

            var response = await client.PutAsync("api/ServicioDeAerolinea", content);// Envía PUT
            response.EnsureSuccessStatusCode();// Verifica éxito
        }

        //AVIONES


        // Método asíncrono que devuelve una lista de objetos Avion
        public async Task<List<Avion>> ObtenerAvionesAsync() // Obtiene todos los aviones
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea un cliente HTTP configurado para el API
            var response = await client.GetAsync("api/ServicioDeAviones"); // Realiza una petición GET al endpoint de aviones
            response.EnsureSuccessStatusCode(); // Verifica que la respuesta sea exitosa, si no lanza excepción

            var result = await response.Content.ReadAsStringAsync(); // Lee el contenido de la respuesta en formato JSON (texto)
            return JsonConvert.DeserializeObject<List<Avion>>(result) ?? []; // Convierte el JSON a lista de Avion, si es null devuelve lista vacía
        }

        // Método asíncrono que devuelve un avión específico por ID
        public async Task<Avion?> ObtenerAvionPorIdAsync(int id) // Busca avión por ID
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP
            var response = await client.GetAsync($"api/ServicioDeAviones/{id}"); // Hace GET pasando el id en la URL

            if (!response.IsSuccessStatusCode) return null; // Si la respuesta falla, retorna null

            var result = await response.Content.ReadAsStringAsync(); // Lee el JSON de la respuesta
            return JsonConvert.DeserializeObject<Avion>(result); // Convierte el JSON a un objeto Avion
        }

        // Método que obtiene aviones filtrados por nombre de aerolínea
        public async Task<List<Avion>> ObtenerAvionesPorNombreAerolineaAsync(string nombre) // Aviones por aerolínea
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP
            var response = await client.GetAsync($"api/ServicioDeAviones/aerolinea/{nombre}"); // GET con el nombre de la aerolínea
            response.EnsureSuccessStatusCode(); // Verifica que la respuesta sea exitosa

            var result = await response.Content.ReadAsStringAsync(); // Lee la respuesta en JSON
            return JsonConvert.DeserializeObject<List<Avion>>(result) ?? []; // Convierte a lista de Avion o lista vacía si es null
        }

        // Método para agregar un nuevo avión
        public async Task AgregarAvionAsync(Avion avion)  // Agrega un avión
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP

            var json = JsonConvert.SerializeObject(avion); // Convierte el objeto Avion a formato JSON
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Crea el contenido HTTP con el JSON

            var response = await client.PostAsync("api/ServicioDeAviones", content); // Envía la petición POST con los datos
            response.EnsureSuccessStatusCode(); // Verifica que la operación fue exitosa
        }

        // Método para editar un avión existente
        public async Task EditarAvionAsync(Avion avion) // Edita un avión
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP

            var json = JsonConvert.SerializeObject(avion); // Convierte el objeto a JSON
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Prepara el contenido HTTP

            var response = await client.PutAsync("api/ServicioDeAviones", content); // Envía la petición PUT al API
            response.EnsureSuccessStatusCode(); // Verifica que la actualización fue exitosa
        }

        // Método para activar un avión por su ID
        public async Task ActivarAvionAsync(int id) // Activa un avión
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP
            var response = await client.PutAsync($"api/ServicioDeAviones/Activar/{id}", null); // Envía PUT sin cuerpo para activar
            response.EnsureSuccessStatusCode(); // Verifica que la operación fue exitosa
        }

        // Método para desactivar un avión por su ID
        public async Task DesactivarAvionAsync(int id) // Desactiva un avión
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi"); // Crea cliente HTTP
            var response = await client.PutAsync($"api/ServicioDeAviones/Desactivar/{id}", null); // Envía PUT sin cuerpo para desactivar
            response.EnsureSuccessStatusCode(); // Verifica que la operación fue exitosa
        }
    }
}