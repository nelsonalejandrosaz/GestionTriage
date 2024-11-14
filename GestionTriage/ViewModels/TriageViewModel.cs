namespace GestionTriage.ViewModels;

public class TriageViewModel
{
    public int PacienteId { get; set; }
    public string NombreCompleto { get; set; }
    public int Edad { get; set; }
    public string CodigoAtencion { get; set; }
    public DateTime FechaHoraIngreso { get; set; }
    public string NivelTriage { get; set; }
    public string ColorTriage { get; set; }
    public TimeSpan TiempoEspera { get; set; }
}