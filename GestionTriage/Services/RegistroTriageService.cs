using GestionTriage.Data;
using GestionTriage.Models;
using GestionTriage.ViewModels;
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
        registroTriage.CodigoAtencion = GenerarCodigoAtencion();
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
    
    public async Task<ResumenTriageViewModel> ObtenerResumenRegistros()
    {
        var registros = await _context.RegistrosTriage
            .Include(r => r.NivelTriage)
            .ToListAsync();

        var resumen = new ResumenTriageViewModel
        {
            TotalRegistros = registros.Count,
            RegistrosEnEspera = registros.Count(r => !r.FechaHoraAtencion.HasValue),
            RegistrosEnAtencion = registros.Count(r => r.FechaHoraAtencion.HasValue && !r.EstadoAtendido),
            RegistrosAtendidos = registros.Count(r => r.EstadoAtendido),
            RegistrosPorNivel = await ObtenerResumenPorNivel(),
            UltimosRegistros = await ObtenerUltimosRegistros(5) // Obtener los últimos 5 registros
        };

        return resumen;
    }

    private async Task<List<ResumenNivelTriageViewModel>> ObtenerResumenPorNivel()
    {
        return await _context.NivelesTriage
            .Select(n => new ResumenNivelTriageViewModel
            {
                Nombre = n.Nombre,
                Color = n.Color,
                EnEspera = n.RegistrosTriage.Count(r => !r.FechaHoraAtencion.HasValue),
                EnAtencion = n.RegistrosTriage.Count(r => r.FechaHoraAtencion.HasValue && !r.EstadoAtendido),
                Atendidos = n.RegistrosTriage.Count(r => r.EstadoAtendido)
            })
            .ToListAsync();
    }

    private async Task<List<RegistroTriageViewModel>> ObtenerUltimosRegistros(int cantidad)
    {
        return await _context.RegistrosTriage
            .Include(r => r.NivelTriage)
            .OrderByDescending(r => r.FechaHoraIngreso)
            .Take(cantidad)
            .Select(r => new RegistroTriageViewModel
            {
                Id = r.Id,
                CodigoAtencion = r.CodigoAtencion,
                NivelTriageNombre = r.NivelTriage.Nombre,
                ColorNivelTriage = r.NivelTriage.Color,
                FechaHoraIngreso = r.FechaHoraIngreso,
                FechaHoraAtencion = r.FechaHoraAtencion,
                EstadoAtendido = r.EstadoAtendido
            })
            .ToListAsync();
    }
    
    // Funcion privada para generar un codigo de atencion
    private string GenerarCodigoAtencion()
    {
        var fecha = DateTime.UtcNow;
        return $"AT-{fecha:yyyyMMddHHmmss}";
    }
}