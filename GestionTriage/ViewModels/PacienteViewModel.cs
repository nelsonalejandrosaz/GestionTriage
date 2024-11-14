using System.ComponentModel.DataAnnotations;

namespace GestionTriage.ViewModels;

public class PacienteViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100)]
    public string Nombres { get; set; }
    
    [Required(ErrorMessage = "El apellido es requerido")]
    [StringLength(100)]
    public string Apellidos { get; set; }
    
    [Required(ErrorMessage = "El DUI es requerido")]
    [RegularExpression(@"^\d{8}-\d$", ErrorMessage = "El formato del DUI debe ser 00000000-0")]
    public string DUI { get; set; }
    
    [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Nacimiento")]
    public DateTime FechaNacimiento { get; set; }
    
    [Required(ErrorMessage = "El género es requerido")]
    public string Genero { get; set; }
    
    [StringLength(200)]
    public string Direccion { get; set; }
    
    [Phone]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; }

    public int Edad => CalcularEdad(FechaNacimiento);

    private int CalcularEdad(DateTime fechaNacimiento)
    {
        var hoy = DateTime.Today;
        var edad = hoy.Year - fechaNacimiento.Year;
        if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
        return edad;
    }
}