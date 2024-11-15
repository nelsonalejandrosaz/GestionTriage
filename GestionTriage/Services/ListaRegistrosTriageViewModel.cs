using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionTriage.Services;

public class ListaRegistrosTriageViewModel
{
    public IEnumerable<RegistroTriageViewModel> Registros { get; set; }
    public int? FiltroHospitalId { get; set; }
    public bool? FiltroEstadoAtendido { get; set; }
    public SelectList Hospitales { get; set; }
}