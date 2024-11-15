using System.ComponentModel.DataAnnotations;

namespace GestionTriage.ViewModels;

public class HospitalViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La dirección es requerida")]
    [StringLength(200)]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El teléfono es requerido")]
    [StringLength(20)]
    [Phone(ErrorMessage = "El formato del teléfono no es válido")]
    public string Telefono { get; set; }

    public bool Activo { get; set; }

    public int CantidadRegistrosTriage { get; set; }
}
