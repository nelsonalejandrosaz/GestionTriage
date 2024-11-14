namespace GestionTriage.ViewModels;

public class ListaReglasAsignacionViewModel
{
    public IEnumerable<ReglaAsignacionViewModel> Reglas { get; set; }
    public IDictionary<int, string> NivelesTriage { get; set; }
    public string FiltroNivelTriage { get; set; }
}