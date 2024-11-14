using GestionTriage.Data;
using GestionTriage.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Services;

public class PacienteService : IPacienteService
{
    private readonly ApplicationDbContext _context;

    public PacienteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Paciente>> ObtenerTodosPacientes()
    {
        return await _context.Pacientes
            .OrderByDescending(p => p.FechaRegistro)
            .ToListAsync();
    }

    public async Task<Paciente?> ObtenerPacientePorId(int id)
    {
        return await _context.Pacientes
            .Include(p => p.RegistrosTriage)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Paciente> ObtenerPacientePorDUI(string dui)
    {
        return await _context.Pacientes
            .FirstOrDefaultAsync(p => p.DUI == dui);
    }

    public async Task<Paciente?> CrearPaciente(Paciente paciente)
    {
        paciente.FechaRegistro = DateTime.UtcNow;
        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();
        return paciente;
    }

    public async Task<Paciente> ActualizarPaciente(Paciente paciente)
    {
        var pacienteExistente = await _context.Pacientes.FindAsync(paciente.Id);
        if (pacienteExistente == null)
            return null;

        _context.Entry(pacienteExistente).CurrentValues.SetValues(paciente);
        await _context.SaveChangesAsync();
        return pacienteExistente;
    }

    public async Task<bool> EliminarPaciente(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
            return false;

        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistePaciente(string dui)
    {
        return await _context.Pacientes.AnyAsync(p => p.DUI == dui);
    }
}