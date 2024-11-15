using GestionTriage.Data;
using GestionTriage.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Services;

public class RegistroTriageService : IRegistroTriageService
{
    private readonly ApplicationDbContext _context;

    public RegistroTriageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RegistroTriage>> ObtenerTodos()
    {
        return await _context.RegistrosTriage
            .Include(r => r.Paciente)
            .Include(r => r.NivelTriage)
            .Include(r => r.Hospital)
            .OrderByDescending(r => r.FechaHoraIngreso)
            .ToListAsync();
    }

    public async Task<RegistroTriage?> ObtenerPorId(int id)
    {
        return await _context.RegistrosTriage
            .Include(r => r.Paciente)
            .Include(r => r.NivelTriage)
            .Include(r => r.Hospital)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<RegistroTriage>> ObtenerPorHospital(int hospitalId)
    {
        return await _context.RegistrosTriage
            .Include(r => r.Paciente)
            .Include(r => r.NivelTriage)
            .Include(r => r.Hospital)
            .Where(r => r.HospitalId == hospitalId)
            .OrderByDescending(r => r.FechaHoraIngreso)
            .ToListAsync();
    }

    public async Task<RegistroTriage> Crear(RegistroTriage registroTriage)
    {
        registroTriage.FechaHoraIngreso = DateTime.UtcNow;
        registroTriage.EstadoAtendido = false;
        _context.RegistrosTriage.Add(registroTriage);
        await _context.SaveChangesAsync();
        return registroTriage;
    }

    public async Task<RegistroTriage> Actualizar(RegistroTriage registroTriage)
    {
        var registroExistente = await _context.RegistrosTriage.FindAsync(registroTriage.Id);
        if (registroExistente == null)
            return null;

        _context.Entry(registroExistente).CurrentValues.SetValues(registroTriage);
        await _context.SaveChangesAsync();
        return registroExistente;
    }

    public async Task<bool> Eliminar(int id)
    {
        var registro = await _context.RegistrosTriage.FindAsync(id);
        if (registro == null)
            return false;

        _context.RegistrosTriage.Remove(registro);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<RegistroTriage>> ObtenerRegistrosActivos(int hospitalId)
    {
        return await _context.RegistrosTriage
            .Include(r => r.Paciente)
            .Include(r => r.NivelTriage)
            .Include(r => r.Hospital)
            .Where(r => r.HospitalId == hospitalId && !r.EstadoAtendido)
            .OrderBy(r => r.FechaHoraIngreso)
            .ToListAsync();
    }

    public async Task<RegistroTriage> IniciarAtencion(int id)
    {
        var registro = await _context.RegistrosTriage.FindAsync(id);
        if (registro != null)
        {
            registro.FechaHoraAtencion = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        return registro;
    }

    public async Task<RegistroTriage> FinalizarAtencion(int id, string observaciones)
    {
        var registro = await _context.RegistrosTriage.FindAsync(id);
        if (registro != null)
        {
            registro.FechaHoraFinalizacion = DateTime.UtcNow;
            registro.ObservacionesAtencion = observaciones;
            registro.EstadoAtendido = true;
            await _context.SaveChangesAsync();
        }
        return registro;
    }
}