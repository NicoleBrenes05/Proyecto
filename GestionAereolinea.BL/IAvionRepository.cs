using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    // Interfaz que define las operaciones de acceso a datos para los aviones
    public interface IAvionRepository
    {
        // Obtiene la lista completa de todos los aviones
        Task<IEnumerable<Avion>> ObtenerTodosAsync();
        // Obtiene un avión específico según su ID
        Task<Avion?> ObtenerPorIdAsync(int id);

        // Obtiene los aviones que pertenecen a una aerolínea específica
        Task<IEnumerable<Avion>> ObtenerPorNombreDeAerolineaAsync(string nombreAerolinea);

        // Agrega un nuevo avión a la base de datos
        Task AgregarAsync(Avion avion);
        // Actualiza la información de un avión existente
        Task ActualizarAsync(Avion avion);
        // Elimina un avión de la base de datos según su ID
        Task EliminarAsync(int id);
        // Activa un avión
        Task ActivarAsync(int id);
        // Desactiva un avión
        Task DesActivarAsync(int id);
        // Obtiene la lista de aviones que están activos
        Task<IEnumerable<Avion>> ObtenerActivosAsync();
        // Obtiene la lista de aviones que están inactivos
        Task<IEnumerable<Avion>> ObtenerInActivosAsync();


    }
}
