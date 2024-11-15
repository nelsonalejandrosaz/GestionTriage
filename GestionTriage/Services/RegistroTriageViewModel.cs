namespace GestionTriage.Services;

public class RegistroTriageViewModel
{
    public int Id { get; set; }
    public string CodigoAtencion { get; set; }
    public int PacienteId { get; set; }
    public string NombrePaciente { get; set; }
    public int NivelTriageId { get; set; }
    public string NivelTriageNombre { get; set; }
    public string ColorNivelTriage { get; set; }
    public DateTime FechaHoraIngreso { get; set; }
    public DateTime? FechaHoraAtencion { get; set; }
    public DateTime? FechaHoraFinalizacion { get; set; }
    public string ObservacionesIngreso { get; set; }
    public string ObservacionesAtencion { get; set; }
    public bool EstadoAtendido { get; set; }
    public int HospitalId { get; set; }
    public string NombreHospital { get; set; }
}