using GestionTriage.Data;
using GestionTriage.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Services;

public class ReglaAsignacionService : IReglaAsignacionService
{
    private readonly ApplicationDbContext _context;

    public ReglaAsignacionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReglaAsignacion>> ObtenerTodasReglas()
    {
        return await _context.ReglasAsignacion
            .Include(r => r.NivelTriage)
            .OrderBy(r => r.NivelTriage.Nombre)
            .ThenBy(r => r.Prioridad)
            .ToListAsync();
    }

    public async Task<ReglaAsignacion?> ObtenerReglaPorId(int id)
    {
        return await _context.ReglasAsignacion
            .Include(r => r.NivelTriage)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<ReglaAsignacion> CrearRegla(ReglaAsignacion regla)
    {
        _context.ReglasAsignacion.Add(regla);
        await _context.SaveChangesAsync();
        return regla;
    }

    public async Task<ReglaAsignacion> ActualizarRegla(ReglaAsignacion regla)
    {
        var reglaExistente = await _context.ReglasAsignacion.FindAsync(regla.Id);
        if (reglaExistente == null)
            return null;

        _context.Entry(reglaExistente).CurrentValues.SetValues(regla);
        await _context.SaveChangesAsync();
        return reglaExistente;
    }

    public async Task<bool> EliminarRegla(int id)
    {
        var regla = await _context.ReglasAsignacion.FindAsync(id);
        if (regla == null)
            return false;

        _context.ReglasAsignacion.Remove(regla);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ReglaAsignacion>> ObtenerReglasPorNivelTriage(int nivelTriageId)
    {
        return await _context.ReglasAsignacion
            .Where(r => r.NivelTriageId == nivelTriageId)
            .OrderBy(r => r.Prioridad)
            .ToListAsync();
    }
}