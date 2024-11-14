using System.ComponentModel.DataAnnotations;

namespace GestionTriage.ViewModels;

public class NuevoIngresoViewModel
{
    [Required]
    public string Nombres { get; set; }
    
    [Required]
    public string Apellidos { get; set; }
    
    [Required]
    public string DUI { get; set; }
    
    [Required]
    public DateTime FechaNacimiento { get; set; }
    
    [Required]
    public string Genero { get; set; }
    
    public string Observaciones { get; set; }
}