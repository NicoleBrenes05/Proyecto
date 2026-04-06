using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.EntityFrameworkCore;

namespace GestionAereolinea.DA
{
    public class AvionRepository : IAvionRepository
    {
        private readonly DBContexto _context;

        public AvionRepository(DBContexto context)
        {
            _context = context;
        }

        public async Task AgregarAsync(Avion avion)
        {
            await _context.Aviones.AddAsync(avion);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Avion avion)
        {
            _context.Aviones.Update(avion);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var avion = await ObtenerPorIdAsync(id);
            if (avion != null)
            {
                _context.Aviones.Remove(avion);
                await _context.SaveChangesAsync();
            }
        }

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

        public async Task<IEnumerable<Avion>> ObtenerTodosAsync()
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .ToListAsync();
        }

        public async Task<IEnumerable<Avion>> ObtenerActivosAsync()
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .Where(a => a.Estado == Estado.Activo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Avion>> ObtenerInActivosAsync()
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .Where(a => a.Estado == Estado.InActivo)
                .ToListAsync();
        }

        public async Task<Avion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Avion>> ObtenerPorNombreDeAerolineaAsync(string nombreAerolinea)
        {
            return await _context.Aviones
                .Include(a => a.Aerolinea)
                .Where(a => a.Aerolinea.Nombre == nombreAerolinea)
                .ToListAsync();
        }
    }
}