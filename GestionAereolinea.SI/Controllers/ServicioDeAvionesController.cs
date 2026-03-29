using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtenerLista()
        {
            var lista = await _adminAviones.ObtengaListaAsync();
            return Ok(lista);
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Avion>> ObtenerPorId(int id)
        {
            var avion = await _adminAviones.ObtengaAsync(id);

            if (avion == null)
                return NotFound();

            return Ok(avion);
        }

    
        [HttpGet("aerolinea/{nombre}")]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtenerPorAerolinea(string nombre)
        {
            var lista = await _adminAviones.ObtengaPorAerolineaAsync(nombre);
            return Ok(lista);
        }

       
        [HttpPost]
        public async Task<ActionResult> Agregar([FromBody] Avion avion)
        {
            await _adminAviones.AgregueAsync(avion);
            return Ok("Avión agregado correctamente");
        }

      
        [HttpPut]
        public async Task<ActionResult> Editar([FromBody] Avion avion)
        {
            await _adminAviones.EditeAsync(avion);
            return Ok("Avión actualizado correctamente");
        }
    }
}