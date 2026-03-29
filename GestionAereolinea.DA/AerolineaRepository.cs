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
    public class AerolineaRepository : IAerolineaRepository
    {

        private readonly DBContexto _context;

        public AerolineaRepository(DBContexto context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Aerolinea aerolinea)
        {
            _context.Aerolineas.Update(aerolinea);
            await    _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Aerolinea aerolinea)
        {
            await _context.Aerolineas.AddAsync(aerolinea);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aerolinea>> ObtenerAsync()
        {
            return await _context.Aerolineas
            .Include(a => a.Aviones)
            .ToListAsync();
        }

        public async Task<IEnumerable<Avion>> ObtenerAvionesPorAerolineasAsync(string nombreAerolinea)
        {
            return await _context.Aviones.Include(v => v.Aerolinea.Nombre == nombreAerolinea).ToListAsync();
        }

        public async Task<Aerolinea?> ObtenerPorIdAsync(int id)
        {
            return await _context.Aerolineas
            .Include(a => a.Aviones)
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Aerolinea?> ObtenerPorNombreAsync(string nombre)
        {
            return await _context.Aerolineas.FirstOrDefaultAsync(a => a.Nombre == nombre);
        }

        public async Task<Aerolinea?> ObtenerPorTelefonoAsync(string telefono)
        {
            return await _context.Aerolineas.FirstOrDefaultAsync(a => a.Telefono == telefono);
        }
    }
}
