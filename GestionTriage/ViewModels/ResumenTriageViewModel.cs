using GestionTriage.Services;

namespace GestionTriage.ViewModels;

public class ResumenTriageViewModel
{
    public int TotalRegistros { get; set; }
    public int RegistrosEnEspera { get; set; }
    public int RegistrosEnAtencion { get; set; }
    public int RegistrosAtendidos { get; set; }
    public List<ResumenNivelTriageViewModel> RegistrosPorNivel { get; set; }
    public List<RegistroTriageViewModel> UltimosRegistros { get; set; }
}