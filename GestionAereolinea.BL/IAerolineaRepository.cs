using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    public interface IAerolineaRepository
    {
        Task<Aerolinea?>ObtenerPorIdAsync (int id);
        Task<IEnumerable<Aerolinea>> ObtenerAsync();

        Task<Aerolinea?> ObtenerPorNombreAsync(string nombre);

        Task<Aerolinea?> ObtenerPorTelefonoAsync(string telefono);

        Task<IEnumerable<Avion>> ObtenerAvionesPorAerolineasAsync(string nombreAerolinea);

        Task AgregarAsync(Aerolinea aerolinea);

        Task ActualizarAsync(Aerolinea aerolinea);

        Task EliminarAsync(Aerolinea aerolinea);

    }
}
