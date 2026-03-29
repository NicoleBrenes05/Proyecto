using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    public interface IAdministradorDeAerolineas
    {
        Task<IEnumerable<Aerolinea>> ObtengaLaListaAsync();

        Task<Aerolinea?> ObtengaAsync(int id); 

        Task<Aerolinea?> ObtengaPorNombreAsync(string nombre);

        Task<Aerolinea?> ObtengaPorTelefonoAsync(string telefono);

        Task<IEnumerable<Avion>> ObtengaAvionesPorAerolineaAsync(string nombreAerolinea);

        Task AgregueAsync(Aerolinea aerolinea);

        Task EditeAsync(Aerolinea aerolinea);
    }
}
