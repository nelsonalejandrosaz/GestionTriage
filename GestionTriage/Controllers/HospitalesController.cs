using GestionTriage.Models;
using GestionTriage.Services;
using GestionTriage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestionTriage.Controllers;

public class HospitalesController : Controller
{
    private readonly IHospitalService _hospitalService;

    public HospitalesController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    public async Task<IActionResult> Index(bool incluirInactivos = false)
    {
        var hospitales = await _hospitalService.ObtenerTodosHospitales();

        if (!incluirInactivos)
        {
            hospitales = hospitales.Where(h => h.Activo);
        }

        var viewModel = new ListaHospitalesViewModel
        {
            Hospitales = hospitales.Select(h => new HospitalViewModel
            {
                Id = h.Id,
                Nombre = h.Nombre,
                Direccion = h.Direccion,
                Telefono = h.Telefono,
                Activo = h.Activo,
                CantidadRegistrosTriage = h.RegistrosTriage?.Count ?? 0
            }),
            IncluirInactivos = incluirInactivos
        };

        return View(viewModel);
    }

    public IActionResult Crear()
    {
        return View(new HospitalViewModel { Activo = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(HospitalViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        if (await _hospitalService.ExisteHospitalConNombre(viewModel.Nombre))
        {
            ModelState.AddModelError("Nombre", "Ya existe un hospital con este nombre");
            return View(viewModel);
        }

        var hospital = new Hospital
        {
            Nombre = viewModel.Nombre,
            Direccion = viewModel.Direccion,
            Telefono = viewModel.Telefono,
            Activo = viewModel.Activo
        };

        await _hospitalService.CrearHospital(hospital);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int id)
    {
        var hospital = await _hospitalService.ObtenerHospitalPorId(id);
        if (hospital == null)
            return NotFound();

        var viewModel = new HospitalViewModel
        {
            Id = hospital.Id,
            Nombre = hospital.Nombre,
            Direccion = hospital.Direccion,
            Telefono = hospital.Telefono,
            Activo = hospital.Activo,
            CantidadRegistrosTriage = hospital.RegistrosTriage?.Count ?? 0
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(HospitalViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var hospital = new Hospital
        {
            Id = viewModel.Id,
            Nombre = viewModel.Nombre,
            Direccion = viewModel.Direccion,
            Telefono = viewModel.Telefono,
            Activo = viewModel.Activo
        };

        var resultado = await _hospitalService.ActualizarHospital(hospital);
        if (resultado == null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        var resultado = await _hospitalService.EliminarHospital(id);
        if (!resultado)
        {
            TempData["Error"] = "No se puede eliminar el hospital porque tiene pacientes asociados";
        }
        return RedirectToAction(nameof(Index));
    }
}