using GestionTriage.Services;
using GestionTriage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestionTriage.Controllers;

public class TriageController : Controller
{
    private readonly ITriageService _triageService;

    public TriageController(ITriageService triageService)
    {
        _triageService = triageService;
    }

    public async Task<IActionResult> Index()
    {
        // Asumiendo que obtenemos el hospitalId de alguna forma (sesión/configuración)
        int hospitalId = 1; 
        var registrosActivos = await _triageService.ObtenerRegistrosActivos(hospitalId);
        return View(registrosActivos);
    }

    public IActionResult NuevoIngreso()
    {
        return View(new NuevoIngresoViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> NuevoIngreso(NuevoIngresoViewModel modelo)
    {
        if (!ModelState.IsValid)
            return View(modelo);

        // Implementar la lógica de creación de nuevo ingreso
        return RedirectToAction(nameof(Index));
    }
}