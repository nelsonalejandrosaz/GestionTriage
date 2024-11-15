using GestionTriage.Models;
using GestionTriage.ViewModels;

namespace GestionTriage.Services;

public interface IRegistroTriageService
{
    Task<IEnumerable<RegistroTriage>> ObtenerTodos();
    Task<RegistroTriage?> ObtenerPorId(int id);
    Task<IEnumerable<RegistroTriage>> ObtenerPorHospital(int hospitalId);
    Task<RegistroTriage> Crear(RegistroTriage registroTriage);
    Task<RegistroTriage> Actualizar(RegistroTriage registroTriage);
    Task<bool> Eliminar(int id);
    Task<IEnumerable<RegistroTriage>> ObtenerRegistrosActivos(int hospitalId);
    Task<RegistroTriage> IniciarAtencion(int id);
    Task<RegistroTriage> FinalizarAtencion(int id, string observaciones);
    Task<ResumenTriageViewModel> ObtenerResumenRegistros();
}