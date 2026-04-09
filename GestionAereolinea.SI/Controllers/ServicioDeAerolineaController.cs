using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionAereolinea.SI.Controllers
{

    // Indica que esta clase es un controlador de API
    [ApiController]
    //Define la ruta base (api/ServicioDeAerolinea)
    [Route("api/[controller]")]
    public class ServicioDeAerolineaController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio
        private readonly IAdministradorDeAerolineas _admin;
        // Constructor: recibe la dependencia por inyección
        public ServicioDeAerolineaController(IAdministradorDeAerolineas admin)
        {
            _admin = admin;// Se guarda la referencia para usarla en los métodos
        }

        //GET: api/ServicioDeAerolinea
        //obtiene todas las aerolíneas
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var lista = await _admin.ObtengaLaListaAsync(); // Llama a la capa BL para obtener la lista
            return Ok(lista);//petición exitosa 
        }

        //GET: api/ServicioDeAerolinea/{id}
      //metodo GET por ID
        [HttpGet("{id}")]  // La URL lleva un parámetro id
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var aerolinea = await _admin.ObtengaAsync(id);  // Busca la aerolínea en la BL
            // Si esta no existe, retorna 404
            if (aerolinea == null)
                return NotFound();

            return Ok(aerolinea); //petición exitosa 
        }

        // GET: api/ServicioDeAerolinea/nombre/{nombre}

        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombre)
        {
            // Busca por nombre en la BL
            var aerolinea = await _admin.ObtengaPorNombreAsync(nombre);
            // Si no existe, retorna 404
            if (aerolinea == null)
                return NotFound();

            return Ok(aerolinea); //petición exitosa 
        }
        // GET: api/ServicioDeAerolinea/telefono/{telefono}
        [HttpGet("telefono/{telefono}")]
        public async Task<IActionResult> ObtenerPorTelefono(string telefono)
        {
            // Busca por teléfono en la BL
            var aerolinea = await _admin.ObtengaPorTelefonoAsync(telefono);
            // Si no existe, retorna 404
            if (aerolinea == null)
                return NotFound(); 

            return Ok(aerolinea); //peticion exitosa
        }
        // GET: api/ServicioDeAerolinea/aerolinea/{nombre}/aviones
        // Método GET para obtener aviones de una aerolínea
        [HttpGet("aerolinea/{nombre}/aviones")]
        public async Task<IActionResult> ObtenerAviones(string nombre)
        {
            // Llama a la BL para traer los aviones asociados
            var lista = await _admin.ObtengaAvionesPorAerolineaAsync(nombre);
            return Ok(lista);//petición exitosa
        }
        // POST: api/ServicioDeAerolinea
        [HttpPost]
        // [FromBody] indica que el objeto viene en el cuerpo del request (JSON)
        public async Task<IActionResult> Agregar([FromBody] Aerolinea aerolinea)
        {
            // Llama a la BL para guardar la nueva aerolínea
            await _admin.AgregueAsync(aerolinea);// Guarda en BD mediante la BL
            return Ok(); //petición exitosa
        }
        // PUT: api/ServicioDeAerolinea
        //Método para actualizar la aerolínea
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Aerolinea aerolinea)
        {
            // Llama a la BL para editar la aerolínea existente
            await _admin.EditeAsync(aerolinea);
            return Ok();
        }
        // DELETE: api/ServicioDeAerolinea/{id}
        //Método para eliminar aerolínea por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var aerolinea = await _admin.ObtengaAsync(id); // Busca la aerolínea primero
            if (aerolinea == null)// Si no existe, devuelve 404
                return NotFound();
            // Si existe, la elimina usando la BL
            await _admin.ElimineAsync(aerolinea); 
            return NoContent(); //se eliminó correctamente
        }
    }
}