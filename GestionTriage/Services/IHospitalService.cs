using GestionTriage.Models;

namespace GestionTriage.Services;

public interface IHospitalService
{
    Task<IEnumerable<Hospital>> ObtenerTodosHospitales();
    Task<Hospital?> ObtenerHospitalPorId(int id);
    Task<Hospital> CrearHospital(Hospital hospital);
    Task<Hospital> ActualizarHospital(Hospital hospital);
    Task<bool> EliminarHospital(int id);
    Task<bool> ExisteHospitalConNombre(string nombre);
}