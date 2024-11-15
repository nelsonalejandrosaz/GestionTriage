using System.ComponentModel.DataAnnotations;

namespace GestionTriage.ViewModels;

public class ReglaAsignacionViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(500)]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El nivel de triage es requerido")]
    [Display(Name = "Nivel de Triage")]
    public int NivelTriageId { get; set; }

    [Required(ErrorMessage = "La prioridad es requerida")]
    [Range(1, 100, ErrorMessage = "La prioridad debe estar entre 1 y 100")]
    public int Prioridad { get; set; }

    public bool Activo { get; set; }

    public string? NombreNivelTriage { get; set; }
    public string? ColorNivelTriage { get; set; }
}