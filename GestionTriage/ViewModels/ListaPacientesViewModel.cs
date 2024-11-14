namespace GestionTriage.ViewModels;

public class ListaPacientesViewModel
{
    public IEnumerable<PacienteViewModel> Pacientes { get; set; }
    public string TerminoBusqueda { get; set; }
    public int TotalPacientes { get; set; }
}