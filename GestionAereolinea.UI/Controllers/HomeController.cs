using GestionAereolinea.Model;
using GestionAereolinea.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestionAereolinea.UI.Controllers
{
    public class HomeController : Controller
    {
        // Logger del sistema para registrar información o errores en tiempo de ejecución
        private readonly ILogger<HomeController> _logger;

        // Servicio que permite hacer llamadas HTTP a la API (GestionAereolinea.SI)
        private readonly ServicioApi _servicioApi;

        // Constructor: ASP.NET inyecta automáticamente el logger y el ServicioApi
        // gracias a la configuración de inyección de dependencias en Program.cs
        public HomeController(ILogger<HomeController> logger, ServicioApi servicioApi)
        {
            _logger = logger;
            _servicioApi = servicioApi;
        }

        // Acción Index: se ejecuta cuando el usuario entra a la página de inicio
        // Es "async" porque realiza llamadas a la API que pueden tardar en responder
        public async Task<IActionResult> Index()
        {
            // Se consulta la lista completa de aerolíneas a través del ServicioApi
            var aerolineas = await _servicioApi.ObtenerAerolineasAsync();

            // Se consulta la lista completa de aviones a través del ServicioApi
            var aviones = await _servicioApi.ObtenerAvionesAsync();

            // Se construye el ViewModel con los totales calculados a partir de las listas
            var modelo = new HomeViewModel
            {
                // .Count devuelve la cantidad de elementos en la lista
                TotalAerolineas = aerolineas.Count,
                TotalAviones = aviones.Count,

                // .Count() con filtro cuenta solo los aviones que cumplen la condición
                // En este caso, los que tienen Estado igual a "Activo"
                TotalAvionesActivos = aviones.Count(a => a.Estado == Estado.Activo),

                // Mismo filtro pero para los aviones con Estado "InActivo"
                TotalAvionesInactivos = aviones.Count(a => a.Estado == Estado.InActivo)
            };

            // Se envía el modelo a la Vista para que muestre los datos en pantalla
            return View(modelo);
        }

        // Acción para la página de privacidad — solo retorna su vista sin lógica adicional
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción para mostrar errores del sistema
        // El atributo ResponseCache evita que el navegador guarde en caché esta página
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // RequestId se usa para identificar el error específico en los logs del sistema
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}