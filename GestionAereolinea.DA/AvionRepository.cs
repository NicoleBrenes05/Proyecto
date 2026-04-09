using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.EntityFrameworkCore;

namespace GestionAereolinea.DA
{
    // Clase que implementa el repositorio de aviones usando Entity Framework
    public class AvionRepository : IAvionRepository
    {
        private readonly DBContexto _context;
        // Constructor que recibe el contexto mediante inyección de dependencias
        public AvionRepository(DBContexto context)
        {
            _context = context;
        }
        // Agrega un nuevo avión a la base de datos
        public async Task AgregarAsync(Avion avion)
        {
            await _context.Aviones.AddAsync(avion);
            await _context.SaveChangesAsync();
        }
        // Actualiza la información de un avión existente
        public async Task ActualizarAsync(Avion avion)
        {
            _context.Aviones.Update(avion);
            await _context.SaveChangesAsync();
        }
        // Elimina un avión según su ID, verificando que exista
        public async Task EliminarAsync(int id)
        {
            var avion = await ObtenerPorIdAsync(id);
            if (avion != null)
            {
                _context.Aviones.Remove(avion);
                await _context.SaveChangesAsync();
            }
        }
        // Activa un avión cambiando su estado a "Activo"
        public async Task ActivarAsync(int id)
        {
            var avion = await ObtenerPorIdAsync(id);
            if (avion != null)
            {
                avion.Estado = Estado.Activo;
                _context.Aviones.Update(avion);
                await _context.SaveChangesAsync();
            }
        }
        // Desactiva un avión cambiando su estado a "InActivo"
        public async Task DesActivarAsync(int id)
        {
            var avion = await ObtenerPorIdAsync(id);
            if (avion != null)
            {
                avion.Estado = Estado.InActivo;
                _context.Aviones.Update(avion);
                await _context.SaveChangesAsync();
            }
        }

        // Obtiene la lista completa de aviones incluyendo su aerolínea
        public async Task<IEnumerable<Avion>> ObtenerTodosAsync()
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .ToListAsync();
        }
        // Obtiene los aviones que están en estado activo
        public async Task<IEnumerable<Avion>> ObtenerActivosAsync()
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .Where(a => a.Estado == Estado.Activo)
                .ToListAsync();
        }
        // Obtiene los aviones que están en estado inactivo
        public async Task<IEnumerable<Avion>> ObtenerInActivosAsync()
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .Where(a => a.Estado == Estado.InActivo)
                .ToListAsync();
        }
        // Obtiene un avión específico por su ID incluyendo su aerolínea
        public async Task<Avion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        // Obtiene los aviones que pertenecen a una aerolínea específica
        public async Task<IEnumerable<Avion>> ObtenerPorNombreDeAerolineaAsync(string nombreAerolinea)
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .Where(a => a.Aerolinea.Nombre == nombreAerolinea)
                .ToListAsync();
        }
    }
}