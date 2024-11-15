namespace GestionTriage.ViewModels;

public class ResumenNivelTriageViewModel
{
    public string Nombre { get; set; }
    public string Color { get; set; }
    public int EnEspera { get; set; }
    public int EnAtencion { get; set; }
    public int Atendidos { get; set; }
    public int Total => EnEspera + EnAtencion + Atendidos;
}