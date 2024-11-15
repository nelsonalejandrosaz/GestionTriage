namespace GestionTriage.Models;

public class Paciente
{
    public int Id { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string DUI { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Genero { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaRegistro { get; set; }
    
    // Relaciones
    public List<RegistroTriage> RegistrosTriage { get; set; }
    
    // Propiedades de solo lectura
    public string NombreCompleto => $"{Nombres} {Apellidos}";
}