namespace GestionTriage.ViewModels;

public class ListaHospitalesViewModel
{
    public IEnumerable<HospitalViewModel> Hospitales { get; set; }
    public bool IncluirInactivos { get; set; }
}