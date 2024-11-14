using GestionTriage.Models;

namespace GestionTriage.Services;

public interface IPacienteService
{
    Task<IEnumerable<Paciente>> ObtenerTodosPacientes();
    Task<Paciente?> ObtenerPacientePorId(int id);
    Task<Paciente> ObtenerPacientePorDUI(string dui);
    Task<Paciente?> CrearPaciente(Paciente paciente);
    Task<Paciente> ActualizarPaciente(Paciente paciente);
    Task<bool> EliminarPaciente(int id);
    Task<bool> ExistePaciente(string dui);
}