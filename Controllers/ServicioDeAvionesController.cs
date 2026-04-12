using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionAereolinea.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioDeAvionesController : ControllerBase
    {
        private readonly IAdministradorDeAviones _adminAviones;

        public ServicioDeAvionesController(IAdministradorDeAviones adminAviones)
        {
            _adminAviones = adminAviones;
        }
        // GET: api/ServicioDeAviones
        // Obtiene la lista de todos los aviones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtenerLista()
        {
            var lista = await _adminAviones.ObtengaListaAsync();// Llama a la BL
            return Ok(lista); //petición exitosa
        }

        // GET: api/ServicioDeAviones/{id}
        // Obtiene un avión por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Avion>> ObtenerPorId(int id)
        {
            var avion = await _adminAviones.ObtengaAsync(id);
            // Si no existe, retorna 404
            if (avion == null)
                return NotFound();

            return Ok(avion); //Si existe, petición exitosa
        }

        // GET: api/ServicioDeAviones/aerolinea/{nombre}
        // Obtiene aviones de una aerolínea específica
        [HttpGet("aerolinea/{nombre}")]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtenerPorAerolinea(string nombre)
        {
            var lista = await _adminAviones.ObtengaPorAerolineaAsync(nombre);
            return Ok(lista);
        }

        // POST: api/ServicioDeAviones
        // Agrega un nuevo avión
        [HttpPost]
        // El objeto avion viene desde el body en formato JSON
        public async Task<ActionResult> Agregar([FromBody] Avion avion)
        {
            await _adminAviones.AgregueAsync(avion);// Se guarda en la BL
            return Ok("Avión agregado correctamente"); //Petición exitosa
        }

        // PUT: api/ServicioDeAviones
        // Actualiza un avión existente
        [HttpPut]
        public async Task<ActionResult> Editar([FromBody] Avion avion)
        {
            await _adminAviones.EditeAsync(avion); // Actualiza en la BL
            return Ok("Avión actualizado correctamente");
        }
        // GET: api/ServicioDeAviones/ObtengaLaLista
        // Obtiene lista de aviones 
        [HttpGet("ObtengaLaLista")]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtengaLaLista()
        {
            var lista = await _adminAviones.ObtengaLaListaDeActivosAsync();
            return Ok(lista);
        }
        // GET: api/ServicioDeAviones/ObtengaLaListaDeActivos
        // Obtiene solo aviones activos
        [HttpGet("ObtengaLaListaDeActivos")]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtengaLaListaDeActivos()
        {
            var lista = await _adminAviones.ObtengaLaListaDeActivosAsync();
            return Ok(lista);
        }
        // GET: api/ServicioDeAviones/ObtengaLaListaDeInActivos
        // Obtiene solo aviones inactivos
        [HttpGet("ObtengaLaListaDeInActivos")]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtengaLaListaDeInActivos()
        {
            var lista = await _adminAviones.ObtengaLaListaDeInActivosAsync();
            return Ok(lista);
        }
        // GET: api/ServicioDeAviones/ObtengaElAvion?id=1
        // Obtiene un avión por ID (otra forma de endpoint)
        [HttpGet("ObtengaElAvion")]
        public async Task<ActionResult<Avion>> ObtengaElAvion(int id)
        {
            var avion = await _adminAviones.ObtengaAsync(id);
            if (avion == null)
                return NotFound();

            return Ok(avion);
        }
        // DELETE: api/ServicioDeAviones/{id}
        // Elimina un avión
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var avion = await _adminAviones.ObtengaAsync(id);  // Busca el avión primero

            if (avion == null) // Si no existe, devuelve error 404 con mensaje
                return NotFound("El Avion no existe");

            await _adminAviones.EliminarAsync(id);  // Elimina el avión por ID
            return Ok("Avión eliminado correctamente");
        }
        // PUT: api/ServicioDeAviones/Activar/{id}
        // Activa un avión
        [HttpPut("Activar/{id}")]
        public async Task<ActionResult> Activar(int id)
        {
            var avion = await _adminAviones.ObtengaAsync(id);
            // Verifica que exista
            if (avion == null)
                return NotFound();
            // Activa el avión
            await _adminAviones.ActiveAsync(id);
            return Ok("Avion activado correctamente");
        }
        // PUT: api/ServicioDeAviones/Desactivar/{id}
        // Desactiva un avión
        [HttpPut("Desactivar/{id}")]
        public async Task<ActionResult> Desactivar(int id)
        {
            var avion = await _adminAviones.ObtengaAsync(id);
            // Verifica existencia
            if (avion == null)
                return NotFound();
            // Desactiva el avión
            await _adminAviones.DesActiveAsync(id);
            return Ok("Avion desactivado correctamente");
        }


    }
}