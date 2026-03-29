using GestionAereolinea.BL;
using GestionAereolinea.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAereolinea.DA
{
    public class AvionRepository : IAvionRepository
    {

        private readonly DBContexto _context;

        public AvionRepository(DBContexto
            context)
        {
            _context = context;
        }
        public async Task ActualizarAsync(Avion avion)
        {
            _context.Aviones.Update(avion);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Avion avion)
        {
            await _context.Aviones.AddAsync(avion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Avion>> ObtenerPorIdAsync()
        {
            return await _context.Aviones.ToListAsync();
        }

        public async Task<Avion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Aviones.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Avion>> ObtenerPorNombreDeAerolineaAsync(string nombreAerolinea)
        {
            return await _context.Aviones.Include(a => a.Aerolinea).Where(a => a.Aerolinea.Nombre == nombreAerolinea).ToListAsync();
        }
    }
}
