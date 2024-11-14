using GestionTriage.Models;

namespace GestionTriage.Services;

public interface ITriageService
{
    Task<RegistroTriage> CrearNuevoIngreso(RegistroTriage registro);
    Task<RegistroTriage> ActualizarEstadoAtencion(int registroId, bool atendido);
    Task<IEnumerable<RegistroTriage>> ObtenerRegistrosActivos(int hospitalId);
    Task<string> GenerarCodigoAtencion(int hospitalId);
}