namespace GestionTriage.Models;

public class Hospital
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public bool Activo { get; set; }

    // Relaciones
    public List<RegistroTriage> RegistrosTriage { get; set; }
}