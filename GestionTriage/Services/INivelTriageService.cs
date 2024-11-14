using GestionTriage.Models;

namespace GestionTriage.Services;

public interface INivelTriageService
{
    Task<IEnumerable<NivelTriage>> ObtenerTodosNiveles();
    Task<NivelTriage?> ObtenerNivelPorId(int id);
    Task<NivelTriage> CrearNivel(NivelTriage nivel);
    Task<NivelTriage> ActualizarNivel(NivelTriage nivel);
    Task<bool> EliminarNivel(int id);
    Task<bool> ExisteNivelConNombre(string nombre);
}