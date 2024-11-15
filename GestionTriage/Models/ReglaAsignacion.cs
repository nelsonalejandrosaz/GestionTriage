namespace GestionTriage.Models;

public class ReglaAsignacion
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int NivelTriageId { get; set; }
    public int Prioridad { get; set; }
    public bool Activo { get; set; }

    // Relaciones
    public NivelTriage? NivelTriage { get; set; }
}