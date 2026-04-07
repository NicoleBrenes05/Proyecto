using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionAereolinea.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioDeAerolineaController : ControllerBase
    {
        private readonly IAdministradorDeAerolineas _admin;

        public ServicioDeAerolineaController(IAdministradorDeAerolineas admin)
        {
            _admin = admin;
        }

        
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var lista = await _admin.ObtengaLaListaAsync();
            return Ok(lista);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var aerolinea = await _admin.ObtengaAsync(id);

            if (aerolinea == null)
                return NotFound();

            return Ok(aerolinea);
        }

      
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombre)
        {
            var aerolinea = await _admin.ObtengaPorNombreAsync(nombre);

            if (aerolinea == null)
                return NotFound();

            return Ok(aerolinea);
        }

        [HttpGet("telefono/{telefono}")]
        public async Task<IActionResult> ObtenerPorTelefono(string telefono)
        {
            var aerolinea = await _admin.ObtengaPorTelefonoAsync(telefono);

            if (aerolinea == null)
                return NotFound();

            return Ok(aerolinea);
        }

        [HttpGet("aerolinea/{nombre}/aviones")]
        public async Task<IActionResult> ObtenerAviones(string nombre)
        {
            var lista = await _admin.ObtengaAvionesPorAerolineaAsync(nombre);
            return Ok(lista);
        }

        
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] Aerolinea aerolinea)
        {
            await _admin.AgregueAsync(aerolinea);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Aerolinea aerolinea)
        {
            await _admin.EditeAsync(aerolinea);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var aerolinea = await _admin.ObtengaAsync(id); // Usamos la BL para obtener
            if (aerolinea == null)
                return NotFound();

            await _admin.ElimineAsync(aerolinea); // Usamos la BL para eliminar
            return NoContent();
        }
    }
}