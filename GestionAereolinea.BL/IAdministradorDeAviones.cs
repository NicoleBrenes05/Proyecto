using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    public interface IAdministradorDeAviones
    {
        Task<IEnumerable<Avion>> ObtengaListaAsync();
        Task<Avion?> ObtengaAsync(int id);

        Task<IEnumerable<Avion>> ObtengaPorAerolineaAsync(string nombreAerolinea);

        Task AgregueAsync(Avion avion);

        Task EditeAsync(Avion avion);
    }
}
