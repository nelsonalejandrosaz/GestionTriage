using GestionTriage.Models;
using GestionTriage.Services;
using GestionTriage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestionTriage.Controllers;

public class PacientesController : Controller
{
    private readonly IPacienteService _pacienteService;

    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    public async Task<IActionResult> Index(string busqueda)
    {
        var pacientes = await _pacienteService.ObtenerTodosPacientes();
        
        if (!string.IsNullOrEmpty(busqueda))
        {
            pacientes = pacientes.Where(p => 
                p.Nombres.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                p.Apellidos.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                p.DUI.Contains(busqueda));
        }

        var viewModel = new ListaPacientesViewModel
        {
            Pacientes = pacientes.Select(p => new PacienteViewModel
            {
                Id = p.Id,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                DUI = p.DUI,
                FechaNacimiento = p.FechaNacimiento,
                Genero = p.Genero,
                Direccion = p.Direccion,
                Telefono = p.Telefono
            }),
            TerminoBusqueda = busqueda,
            TotalPacientes = pacientes.Count()
        };

        return View(viewModel);
    }

    public IActionResult Crear()
    {
        return View(new PacienteViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(PacienteViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        if (await _pacienteService.ExistePaciente(viewModel.DUI))
        {
            ModelState.AddModelError("DUI", "Ya existe un paciente con este DUI");
            return View(viewModel);
        }

        var paciente = new Paciente
        {
            Nombres = viewModel.Nombres,
            Apellidos = viewModel.Apellidos,
            DUI = viewModel.DUI,
            FechaNacimiento = viewModel.FechaNacimiento,
            Genero = viewModel.Genero,
            Direccion = viewModel.Direccion,
            Telefono = viewModel.Telefono
        };

        await _pacienteService.CrearPaciente(paciente);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int id)
    {
        var paciente = await _pacienteService.ObtenerPacientePorId(id);
        if (paciente == null)
            return NotFound();

        var viewModel = new PacienteViewModel
        {
            Id = paciente.Id,
            Nombres = paciente.Nombres,
            Apellidos = paciente.Apellidos,
            DUI = paciente.DUI,
            FechaNacimiento = paciente.FechaNacimiento,
            Genero = paciente.Genero,
            Direccion = paciente.Direccion,
            Telefono = paciente.Telefono
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(PacienteViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var paciente = new Paciente
        {
            Id = viewModel.Id,
            Nombres = viewModel.Nombres,
            Apellidos = viewModel.Apellidos,
            DUI = viewModel.DUI,
            FechaNacimiento = viewModel.FechaNacimiento,
            Genero = viewModel.Genero,
            Direccion = viewModel.Direccion,
            Telefono = viewModel.Telefono
        };

        var resultado = await _pacienteService.ActualizarPaciente(paciente);
        if (resultado == null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _pacienteService.EliminarPaciente(id);
        return RedirectToAction(nameof(Index));
    }
}