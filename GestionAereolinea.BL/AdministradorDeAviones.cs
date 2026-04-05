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

        public  async Task ActiveAsync(int id)
        {
            await _avionRepository.ActivarAsync(id);
        }

        public async Task DesActiveAsync(int id)
        {
            await _avionRepository.DesActivarAsync(id);
        }

        public async Task<IEnumerable<Avion>> ObtengaLaListaDeActivosAsync()
        {
            return await _avionRepository.ObtenerActivosAsync();
        }

        public async Task<IEnumerable<Avion>> ObtengaLaListaDeInActivosAsync()
        {
            return await _avionRepository.ObtenerInActivosAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _avionRepository.EliminarAsync(id);
        }
    }
}