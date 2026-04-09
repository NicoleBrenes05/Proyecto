using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionAereolinea.DA
{
    // Clase que implementa el repositorio de aerolíneas usando Entity Framework
    public class AerolineaRepository : IAerolineaRepository
    {

        private readonly DBContexto _context;

        public AerolineaRepository(DBContexto context)
        {
            _context = context;
        }
        // Actualiza la información de una aerolínea existente en la base de datos
        public async Task ActualizarAsync(Aerolinea aerolinea)
        {
            _context.Aerolineas.Update(aerolinea);
            await    _context.SaveChangesAsync();
        }
        // Agrega una nueva aerolínea a la base de datos
        public async Task AgregarAsync(Aerolinea aerolinea)
        {
            await _context.Aerolineas.AddAsync(aerolinea);
            await _context.SaveChangesAsync();
        }
        // Obtiene la lista completa de aerolíneas incluyendo sus aviones relacionados
        public async Task<IEnumerable<Aerolinea>> ObtenerAsync()
        {
            return await _context.Aerolineas
            .Include(a => a.Aviones) // Carga los aviones asociados
            .ToListAsync();
        }

        // Obtiene los aviones que pertenecen a una aerolínea específica por nombre
        public async Task<IEnumerable<Avion>> ObtenerAvionesPorAerolineasAsync(string nombreAerolinea)
        {
            return await _context.Aviones.Include(v => v.Aerolinea).Where(v => v.Aerolinea.Nombre == nombreAerolinea).ToListAsync();
        }


        // Obtiene una aerolínea por su ID incluyendo sus aviones
        public async Task<Aerolinea?> ObtenerPorIdAsync(int id)
        {
            return await _context.Aerolineas
            .Include(a => a.Aviones)
            .FirstOrDefaultAsync(a => a.Id == id);
        }
        // Busca una aerolínea por su nombre
        public async Task<Aerolinea?> ObtenerPorNombreAsync(string nombre)
        {
            return await _context.Aerolineas.FirstOrDefaultAsync(a => a.Nombre == nombre);
        }
        // Busca una aerolínea por su número de teléfono
        public async Task<Aerolinea?> ObtenerPorTelefonoAsync(string telefono)
        {
            return await _context.Aerolineas.FirstOrDefaultAsync(a => a.Telefono == telefono);
        }
        // Elimina una aerolínea de la base de datos
        public async Task EliminarAsync(Aerolinea aerolinea)
        {
            _context.Aerolineas.Remove(aerolinea);
            await _context.SaveChangesAsync();
        }
    }
}
