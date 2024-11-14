using GestionTriage.Data;
using GestionTriage.Models;
using GestionTriage.Services;
using GestionTriage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Controllers;

public class ReglasAsignacionController : Controller
{
    private readonly IReglaAsignacionService _reglaService;
    private readonly ApplicationDbContext _context;

    public ReglasAsignacionController(
        IReglaAsignacionService reglaService,
        ApplicationDbContext context)
    {
        _reglaService = reglaService;
        _context = context;
    }

    public async Task<IActionResult> Index(int? nivelTriageId)
    {
        var reglas = await _reglaService.ObtenerTodasReglas();
        var nivelesTriage = await _context.NivelesTriage
            .Where(n => n.Activo)
            .ToDictionaryAsync(n => n.Id, n => n.Nombre);

        if (nivelTriageId.HasValue)
        {
            reglas = reglas.Where(r => r.NivelTriageId == nivelTriageId);
        }

        var viewModel = new ListaReglasAsignacionViewModel
        {
            Reglas = reglas.Select(r => new ReglaAsignacionViewModel
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                NivelTriageId = r.NivelTriageId,
                Prioridad = r.Prioridad,
                Activo = r.Activo,
                NombreNivelTriage = r.NivelTriage.Nombre,
                ColorNivelTriage = r.NivelTriage.Color
            }),
            NivelesTriage = nivelesTriage,
            FiltroNivelTriage = nivelTriageId?.ToString()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Crear()
    {
        await CargarNivelesTriage();
        return View(new ReglaAsignacionViewModel { Activo = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(ReglaAsignacionViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await CargarNivelesTriage();
            return View(viewModel);
        }

        var regla = new ReglaAsignacion
        {
            Nombre = viewModel.Nombre,
            Descripcion = viewModel.Descripcion,
            NivelTriageId = viewModel.NivelTriageId,
            Prioridad = viewModel.Prioridad,
            Activo = viewModel.Activo
        };

        await _reglaService.CrearRegla(regla);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int id)
    {
        var regla = await _reglaService.ObtenerReglaPorId(id);
        if (regla == null)
            return NotFound();

        var viewModel = new ReglaAsignacionViewModel
        {
            Id = regla.Id,
            Nombre = regla.Nombre,
            Descripcion = regla.Descripcion,
            NivelTriageId = regla.NivelTriageId,
            Prioridad = regla.Prioridad,
            Activo = regla.Activo
        };

        await CargarNivelesTriage();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(ReglaAsignacionViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await CargarNivelesTriage();
            return View(viewModel);
        }

        var regla = new ReglaAsignacion
        {
            Id = viewModel.Id,
            Nombre = viewModel.Nombre,
            Descripcion = viewModel.Descripcion,
            NivelTriageId = viewModel.NivelTriageId,
            Prioridad = viewModel.Prioridad,
            Activo = viewModel.Activo
        };

        var resultado = await _reglaService.ActualizarRegla(regla);
        if (resultado == null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _reglaService.EliminarRegla(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task CargarNivelesTriage()
    {
        var nivelesTriage = await _context.NivelesTriage
            .Where(n => n.Activo)
            .Select(n => new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.Nombre
            })
            .ToListAsync();
        ViewBag.NivelesTriage = new SelectList(nivelesTriage, "Value", "Text");
    }
}