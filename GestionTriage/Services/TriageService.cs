using GestionTriage.Data;
using GestionTriage.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Services;

public class TriageService : ITriageService
{
    private readonly ApplicationDbContext _context;

    public TriageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RegistroTriage> CrearNuevoIngreso(RegistroTriage registro)
    {
        registro.FechaHoraIngreso = DateTime.UtcNow;
        registro.EstadoAtendido = false;
        registro.CodigoAtencion = await GenerarCodigoAtencion(registro.HospitalId);
        
        _context.RegistrosTriage.Add(registro);
        await _context.SaveChangesAsync();
        return registro;
    }

    public async Task<RegistroTriage> ActualizarEstadoAtencion(int registroId, bool atendido)
    {
        var registro = await _context.RegistrosTriage.FindAsync(registroId);
        if (registro != null)
        {
            registro.EstadoAtendido = atendido;
            registro.FechaHoraFinalizacion = atendido ? DateTime.UtcNow : null;
            await _context.SaveChangesAsync();
        }
        return registro;
    }

    public async Task<IEnumerable<RegistroTriage>> ObtenerRegistrosActivos(int hospitalId)
    {
        return await _context.RegistrosTriage
            .Where(r => r.HospitalId == hospitalId && !r.EstadoAtendido)
            .Include(r => r.Paciente)
            .Include(r => r.NivelTriage)
            .OrderBy(r => r.FechaHoraIngreso)
            .ToListAsync();
    }

    public async Task<string> GenerarCodigoAtencion(int hospitalId)
    {
        // Implementar lógica de generación de códigos
        var fecha = DateTime.UtcNow.ToString("yyyyMMdd");
        var ultimoRegistro = await _context.RegistrosTriage
            .Where(r => r.HospitalId == hospitalId && r.FechaHoraIngreso.Date == DateTime.UtcNow.Date)
            .OrderByDescending(r => r.Id)
            .FirstOrDefaultAsync();

        int numeroSecuencial = (ultimoRegistro?.Id ?? 0) + 1;
        return $"{fecha}-{hospitalId:D2}-{numeroSecuencial:D4}";
    }
}