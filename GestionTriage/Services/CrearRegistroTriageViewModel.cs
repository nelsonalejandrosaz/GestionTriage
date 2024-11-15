using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionTriage.Services;

public class CrearRegistroTriageViewModel
{
    [Required]
    public int PacienteId { get; set; }
    
    [Required]
    public int NivelTriageId { get; set; }
    
    [Required]
    public int HospitalId { get; set; }
    
    [Required]
    [StringLength(500)]
    public string ObservacionesIngreso { get; set; }
    
    public SelectList? Pacientes { get; set; }
    public SelectList? NivelesTriage { get; set; }
    public SelectList? Hospitales { get; set; }
}
