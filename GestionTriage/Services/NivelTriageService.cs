using GestionTriage.Data;
using GestionTriage.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Services;

public class NivelTriageService : INivelTriageService
{
    private readonly ApplicationDbContext _context;

    public NivelTriageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NivelTriage>> ObtenerTodosNiveles()
    {
        return await _context.NivelesTriage
            .OrderBy(n => n.TiempoMaximoAtencion)
            .ToListAsync();
    }

    public async Task<NivelTriage?> ObtenerNivelPorId(int id)
    {
        return await _context.NivelesTriage
            .Include(n => n.ReglasAsignacion)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<NivelTriage> CrearNivel(NivelTriage nivel)
    {
        _context.NivelesTriage.Add(nivel);
        await _context.SaveChangesAsync();
        return nivel;
    }

    public async Task<NivelTriage> ActualizarNivel(NivelTriage nivel)
    {
        var nivelExistente = await _context.NivelesTriage.FindAsync(nivel.Id);
        if (nivelExistente == null)
            return null;

        _context.Entry(nivelExistente).CurrentValues.SetValues(nivel);
        await _context.SaveChangesAsync();
        return nivelExistente;
    }

    public async Task<bool> EliminarNivel(int id)
    {
        var nivel = await _context.NivelesTriage.FindAsync(id);
        if (nivel == null)
            return false;

        // Verificar si hay reglas de asignación asociadas
        var tieneReglas = await _context.ReglasAsignacion
            .AnyAsync(r => r.NivelTriageId == id);

        if (tieneReglas)
            return false;

        _context.NivelesTriage.Remove(nivel);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteNivelConNombre(string nombre)
    {
        return await _context.NivelesTriage
            .AnyAsync(n => n.Nombre.ToLower() == nombre.ToLower());
    }
}