using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    public class AdministradorDeAerolineas : IAdministradorDeAerolineas
    {
        private readonly IAerolineaRepository _aerolineaRepository;

        public AdministradorDeAerolineas(IAerolineaRepository aerolineaRepository)
        {
            _aerolineaRepository = aerolineaRepository;
        }

       

        public async Task AgregueAsync(Aerolinea aerolinea)
        {
            await _aerolineaRepository.AgregarAsync(aerolinea);
        }

   
        public async Task EditeAsync(Aerolinea aerolinea)
        {
            await _aerolineaRepository.ActualizarAsync(aerolinea);
        }

        public async Task<Aerolinea?> ObtengaAsync(int id)
        {
            return await _aerolineaRepository.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Avion>> ObtengaAvionesPorAerolineaAsync(string nombreAerolinea)
        {
            return await _aerolineaRepository.ObtenerAvionesPorAerolineasAsync(nombreAerolinea);
        }

        public async Task<IEnumerable<Aerolinea>> ObtengaLaListaAsync()
        {
            return await _aerolineaRepository.ObtenerAsync();
        }

        public async Task<Aerolinea?> ObtengaPorNombreAsync(string nombre)
        {
            return await _aerolineaRepository.ObtenerPorNombreAsync(nombre);
        }

        public async Task<Aerolinea?> ObtengaPorTelefonoAsync(string telefono)
        {
            return await _aerolineaRepository.ObtenerPorTelefonoAsync(telefono);
        }
    }
}
