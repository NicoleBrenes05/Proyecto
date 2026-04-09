using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    // Interfaz que define las operaciones de la lógica de negocio para la gestión de aerolíneas
    public interface IAdministradorDeAerolineas
    {
        // Obtiene la lista completa de aerolíneas
        Task<IEnumerable<Aerolinea>> ObtengaLaListaAsync();
        // Obtiene una aerolínea específica según su ID
        Task<Aerolinea?> ObtengaAsync(int id);
        // Busca una aerolínea por su nombre
        Task<Aerolinea?> ObtengaPorNombreAsync(string nombre);

        // Busca una aerolínea por su número de teléfono
        Task<Aerolinea?> ObtengaPorTelefonoAsync(string telefono);

        // Obtiene los aviones asociados a una aerolínea según su nombre
        Task<IEnumerable<Avion>> ObtengaAvionesPorAerolineaAsync(string nombreAerolinea);
        // Agrega una nueva aerolínea al sistema
        Task AgregueAsync(Aerolinea aerolinea);
        // Actualiza la información de una aerolínea existente
        Task EditeAsync(Aerolinea aerolinea);
        // Elimina una aerolínea del sistema
        Task ElimineAsync(Aerolinea aerolinea);


    }
}
