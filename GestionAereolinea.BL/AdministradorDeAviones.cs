using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    public class AdministradorDeAviones : IAdministradorDeAviones
    {
        private readonly IAvionRepository _avionRepository;
        public AdministradorDeAviones(IAvionRepository avionRepository)
        {
            _avionRepository = avionRepository;
        }
        public async Task<IEnumerable<Avion>> ObtengaListaAsync()
        {
            return await _avionRepository.ObtenerPorIdAsync();
        }

        public async Task<Avion?> ObtengaAsync(int id)
        {
            return await _avionRepository.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Avion>> ObtengaPorAerolineaAsync(string nombreAerolinea)
        {
            return await _avionRepository.ObtenerPorNombreDeAerolineaAsync(nombreAerolinea);
        }

        public async Task AgregueAsync(Avion avion)
        {
            await _avionRepository.AgregarAsync(avion);
        }

        public async Task EditeAsync(Avion avion)
        {
            await _avionRepository.ActualizarAsync(avion);
        }
    }
}
