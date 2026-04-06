using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    public interface IAvionRepository
    {
        Task<IEnumerable<Avion>> ObtenerTodosAsync();
        Task<Avion?> ObtenerPorIdAsync(int id);

        Task<IEnumerable<Avion>> ObtenerPorNombreDeAerolineaAsync(string nombreAerolinea);

        Task AgregarAsync(Avion avion);
        Task ActualizarAsync(Avion avion);

        Task EliminarAsync(int id);

        Task ActivarAsync(int id);

        Task DesActivarAsync(int id);

        Task<IEnumerable<Avion>> ObtenerActivosAsync();

        Task<IEnumerable<Avion>> ObtenerInActivosAsync();


    }
}
