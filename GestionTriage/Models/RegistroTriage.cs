namespace GestionTriage.Models;

public class RegistroTriage
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public int NivelTriageId { get; set; }
    public string CodigoAtencion { get; set; }
    public DateTime FechaHoraIngreso { get; set; }
    public DateTime? FechaHoraAtencion { get; set; }
    public DateTime? FechaHoraFinalizacion { get; set; }
    public string ObservacionesIngreso { get; set; }
    public string? ObservacionesAtencion { get; set; }
    public bool EstadoAtendido { get; set; }
    public int HospitalId { get; set; }

    // Relaciones
    public Paciente Paciente { get; set; }
    public NivelTriage NivelTriage { get; set; }
    public Hospital Hospital { get; set; }
    
}