using System.ComponentModel.DataAnnotations;

namespace GestionTriage.ViewModels;

public class NivelTriageViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El color es requerido")]
    [StringLength(7)]
    [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "El color debe estar en formato hexadecimal (ej: #FF0000)")]
    public string Color { get; set; }

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(200)]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El tiempo máximo de atención es requerido")]
    [Range(1, 1440, ErrorMessage = "El tiempo debe estar entre 1 y 1440 minutos")]
    [Display(Name = "Tiempo Máximo de Atención (minutos)")]
    public int TiempoMaximoAtencion { get; set; }

    public bool Activo { get; set; }

    public int CantidadReglas { get; set; }
    
}