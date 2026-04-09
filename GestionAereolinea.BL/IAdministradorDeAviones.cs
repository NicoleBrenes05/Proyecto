using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    // Interfaz que define las operaciones de la lógica de negocio para la gestión de aviones
    public interface IAdministradorDeAviones
    {
        // Obtiene la lista completa de todos los aviones
        Task<IEnumerable<Avion>> ObtengaListaAsync();
        // Obtiene un avión específico según su ID
        Task<Avion?> ObtengaAsync(int id);
        // Obtiene los aviones que pertenecen a una aerolínea específica
        Task<IEnumerable<Avion>> ObtengaPorAerolineaAsync(string nombreAerolinea);
        // Agrega un nuevo avión al sistema
        Task AgregueAsync(Avion avion);

        // Actualiza la información de un avión existente
        Task EditeAsync(Avion avion);
        // Activa un avión
        Task ActiveAsync(int id);
        // Desactiva un avión
        Task DesActiveAsync(int id);
        // Obtiene la lista de aviones activos
        Task<IEnumerable<Avion>> ObtengaLaListaDeActivosAsync();
        // Obtiene la lista de aviones inactivos
        Task<IEnumerable<Avion>> ObtengaLaListaDeInActivosAsync();
        // Elimina un avión del sistema según su ID
        Task EliminarAsync (int id);
    }
}
