using GestionTriage.Data;
using GestionTriage.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Services;

public class HospitalService : IHospitalService
{
    private readonly ApplicationDbContext _context;

    public HospitalService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hospital>> ObtenerTodosHospitales()
    {
        return await _context.Hospitales
            .OrderBy(h => h.Nombre)
            .ToListAsync();
    }

    public async Task<Hospital?> ObtenerHospitalPorId(int id)
    {
        return await _context.Hospitales
            .Include(h => h.RegistrosTriage)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<Hospital> CrearHospital(Hospital hospital)
    {
        _context.Hospitales.Add(hospital);
        await _context.SaveChangesAsync();
        return hospital;
    }

    public async Task<Hospital> ActualizarHospital(Hospital hospital)
    {
        var hospitalExistente = await _context.Hospitales.FindAsync(hospital.Id);
        if (hospitalExistente == null)
            return null;

        _context.Entry(hospitalExistente).CurrentValues.SetValues(hospital);
        await _context.SaveChangesAsync();
        return hospitalExistente;
    }

    public async Task<bool> EliminarHospital(int id)
    {
        var hospital = await _context.Hospitales
            .Include(h => h.RegistrosTriage)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (hospital == null)
            return false;

        // Verificar si tiene registros de triage asociados
        if (hospital.RegistrosTriage.Count > 0)
            return false;

        _context.Hospitales.Remove(hospital);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteHospitalConNombre(string nombre)
    {
        return await _context.Hospitales
            .AnyAsync(h => h.Nombre.ToLower() == nombre.ToLower());
    }
}