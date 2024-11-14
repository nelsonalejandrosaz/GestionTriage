using GestionTriage.Models;

namespace GestionTriage.Services;

public interface IReglaAsignacionService
{
    Task<IEnumerable<ReglaAsignacion>> ObtenerTodasReglas();
    Task<ReglaAsignacion?> ObtenerReglaPorId(int id);
    Task<ReglaAsignacion> CrearRegla(ReglaAsignacion regla);
    Task<ReglaAsignacion> ActualizarRegla(ReglaAsignacion regla);
    Task<bool> EliminarRegla(int id);
    Task<IEnumerable<ReglaAsignacion>> ObtenerReglasPorNivelTriage(int nivelTriageId);
}