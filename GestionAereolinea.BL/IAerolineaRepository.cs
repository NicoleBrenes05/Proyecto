using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    // Interfaz que define las operaciones de acceso a datos para las aerolíneas
    public interface IAerolineaRepository
    {
        // Obtiene una aerolínea específica según su ID
        Task<Aerolinea?>ObtenerPorIdAsync (int id);
        // Obtiene la lista completa de aerolíneas
        Task<IEnumerable<Aerolinea>> ObtenerAsync();
        // Busca una aerolínea por su nombre
        Task<Aerolinea?> ObtenerPorNombreAsync(string nombre);
        // Busca una aerolínea por su número de teléfono
        Task<Aerolinea?> ObtenerPorTelefonoAsync(string telefono);
        // Obtiene los aviones asociados a una aerolínea según su nombre
        Task<IEnumerable<Avion>> ObtenerAvionesPorAerolineasAsync(string nombreAerolinea);
        // Agrega una nueva aerolínea a la base de datos
        Task AgregarAsync(Aerolinea aerolinea);
        // Actualiza la información de una aerolínea existente
        Task ActualizarAsync(Aerolinea aerolinea);
        // Elimina una aerolínea de la base de datos
        Task EliminarAsync(Aerolinea aerolinea);

    }
}
