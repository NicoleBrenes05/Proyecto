using GestionAereolinea.Model;
using Newtonsoft.Json;
using System.Text;

namespace GestionAereolinea.UI
{
    public class ServicioApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServicioApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

      
        //AEROLINEAS
        

        public async Task<List<Aerolinea>> ObtenerAerolineasAsync()
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync("api/ServicioDeAerolinea");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Aerolinea>>(result) ?? [];
        }

        public async Task<Aerolinea?> ObtenerAerolineaPorIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAerolinea/{id}");

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Aerolinea>(result);
        }

        public async Task<Aerolinea?> ObtenerAerolineaPorNombreAsync(string nombre)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAerolinea/nombre/{nombre}");

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Aerolinea>(result);
        }

        public async Task<Aerolinea?> ObtenerAerolineaPorTelefonoAsync(string telefono)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAerolinea/telefono/{telefono}");

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Aerolinea>(result);
        }

        public async Task<List<Avion>> ObtenerAvionesPorAerolineaAsync(string nombre)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAerolinea/aerolinea/{nombre}/aviones");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Avion>>(result) ?? [];
        }

        public async Task AgregarAerolineaAsync(Aerolinea aerolinea)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var json = JsonConvert.SerializeObject(aerolinea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/ServicioDeAerolinea", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditarAerolineaAsync(Aerolinea aerolinea)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var json = JsonConvert.SerializeObject(aerolinea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/ServicioDeAerolinea", content);
            response.EnsureSuccessStatusCode();
        }

        //AVIONES
       

        public async Task<List<Avion>> ObtenerAvionesAsync()
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync("api/ServicioDeAviones");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Avion>>(result) ?? [];
        }

        public async Task<Avion?> ObtenerAvionPorIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAviones/{id}");

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Avion>(result);
        }

        public async Task<List<Avion>> ObtenerAvionesPorNombreAerolineaAsync(string nombre)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.GetAsync($"api/ServicioDeAviones/aerolinea/{nombre}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Avion>>(result) ?? [];
        }

        public async Task AgregarAvionAsync(Avion avion)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var json = JsonConvert.SerializeObject(avion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/ServicioDeAviones", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditarAvionAsync(Avion avion)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");

            var json = JsonConvert.SerializeObject(avion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/ServicioDeAviones", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task ActivarAvionAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.PutAsync($"api/ServicioDeAviones/Activar/{id}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task DesactivarAvionAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineaApi");
            var response = await client.PutAsync($"api/ServicioDeAviones/Desactivar/{id}", null);
            response.EnsureSuccessStatusCode();
        }
    }
}