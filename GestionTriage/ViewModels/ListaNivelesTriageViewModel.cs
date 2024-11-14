namespace GestionTriage.ViewModels;

public class ListaNivelesTriageViewModel
{
    public IEnumerable<NivelTriageViewModel> Niveles { get; set; }
    public bool IncluirInactivos { get; set; }
}