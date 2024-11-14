namespace GestionTriage.Models;

public class NivelTriage
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Color { get; set; }
    public string Descripcion { get; set; }
    public int TiempoMaximoAtencion { get; set; } // en minutos
    public bool Activo { get; set; }

    // Relaciones
    public List<ReglaAsignacion> ReglasAsignacion { get; set; }
    public List<RegistroTriage> RegistrosTriage { get; set; }
}