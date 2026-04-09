using GestionAereolinea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.BL
{
    // Clase encargada de la lógica de negocio para la gestión de aviones
    public class AdministradorDeAviones : IAdministradorDeAviones
    {
        //Repositorio que permite acceder a los datos de los aviones
        private readonly IAvionRepository _avionRepository;
        // Constructor que recibe el repositorio mediante inyección de dependencias
        public AdministradorDeAviones(IAvionRepository avionRepository)
        {
            _avionRepository = avionRepository;
        }

        //Obtiene la lista de todos los aviones
        public async Task<IEnumerable<Avion>> ObtengaListaAsync()
        {
            return await _avionRepository.ObtenerTodosAsync();
        }
        // Obtiene un avión específico según su ID
        public async Task<Avion?> ObtengaAsync(int id)
        {
            return await _avionRepository.ObtenerPorIdAsync(id);
        }
        // Obtiene los aviones que pertenecen a una aerolínea específica
        public async Task<IEnumerable<Avion>> ObtengaPorAerolineaAsync(string nombreAerolinea)
        {
            return await _avionRepository.ObtenerPorNombreDeAerolineaAsync(nombreAerolinea);
        }

        // Agrega un nuevo avión al sistema
        public async Task AgregueAsync(Avion avion)
        {
            await _avionRepository.AgregarAsync(avion);
        }
        // Actualiza la información de un avión existente
        public async Task EditeAsync(Avion avion)
        {
            await _avionRepository.ActualizarAsync(avion);
        }
        // Activa un avión 
        public async Task ActiveAsync(int id)
        {
            await _avionRepository.ActivarAsync(id);
        }
        // Desactiva un avión 
        public async Task DesActiveAsync(int id)
        {
            await _avionRepository.DesActivarAsync(id);
        }
        // Obtiene la lista de aviones que están activos
        public async Task<IEnumerable<Avion>> ObtengaLaListaDeActivosAsync()
        {
            return await _avionRepository.ObtenerActivosAsync();
        }
        // Obtiene la lista de aviones que están inactivos
        public async Task<IEnumerable<Avion>> ObtengaLaListaDeInActivosAsync()
        {
            return await _avionRepository.ObtenerInActivosAsync();
        }
        // Elimina un avión del sistema según su ID
        public async Task EliminarAsync(int id)
        {
            await _avionRepository.EliminarAsync(id);
        }
    }
}