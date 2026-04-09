using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    // Esta clase se encarga de la lógica de negocio para la gestión de aerolíneas
    public class AdministradorDeAerolineas : IAdministradorDeAerolineas
    {
        //Repositorio que se utiliza para acceder a los datos de las aerolíneas 
        private readonly IAerolineaRepository _aerolineaRepository;
        //Constructor que recibe el repositorio por inyeccion de dependencias
        public AdministradorDeAerolineas(IAerolineaRepository aerolineaRepository)
        {
            _aerolineaRepository = aerolineaRepository;
        }
       
        //Agrega una nueva aerolínea 
        public async Task AgregueAsync(Aerolinea aerolinea)
        {
            await _aerolineaRepository.AgregarAsync(aerolinea);
        }

        //Edita la información de la aerolínea 
        public async Task EditeAsync(Aerolinea aerolinea)
        {
            await _aerolineaRepository.ActualizarAsync(aerolinea);
        }
        
        // Obtiene una aerolínea específica según su ID
        public async Task<Aerolinea?> ObtengaAsync(int id)
        {
            return await _aerolineaRepository.ObtenerPorIdAsync(id);
        }
        // Obtiene la lista de aviones que pertenecen a una aerolínea según su nombre
        public async Task<IEnumerable<Avion>> ObtengaAvionesPorAerolineaAsync(string nombreAerolinea)
        {
            return await _aerolineaRepository.ObtenerAvionesPorAerolineasAsync(nombreAerolinea);
        }
        // Obtiene la lista completa de aerolíneas
        public async Task<IEnumerable<Aerolinea>> ObtengaLaListaAsync()
        {
            return await _aerolineaRepository.ObtenerAsync();
        }
        // Busca una aerolínea por su nombre
        public async Task<Aerolinea?> ObtengaPorNombreAsync(string nombre)
        {
            return await _aerolineaRepository.ObtenerPorNombreAsync(nombre);
        }

        // Busca una aerolínea por su número de teléfono
        public async Task<Aerolinea?> ObtengaPorTelefonoAsync(string telefono)
        {
            return await _aerolineaRepository.ObtenerPorTelefonoAsync(telefono);
        }

        // Elimina una aerolínea del sistema
        public async Task ElimineAsync(Aerolinea aerolinea)
        {
            await _aerolineaRepository.EliminarAsync(aerolinea);
        }
    }
}
