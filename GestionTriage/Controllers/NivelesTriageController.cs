using GestionTriage.Models;
using GestionTriage.Services;
using GestionTriage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestionTriage.Controllers;

public class NivelesTriageController : Controller
{
    private readonly INivelTriageService _nivelService;

    public NivelesTriageController(INivelTriageService nivelService)
    {
        _nivelService = nivelService;
    }

    public async Task<IActionResult> Index(bool incluirInactivos = false)
    {
        var niveles = await _nivelService.ObtenerTodosNiveles();

        if (!incluirInactivos)
        {
            niveles = niveles.Where(n => n.Activo);
        }

        var viewModel = new ListaNivelesTriageViewModel
        {
            Niveles = niveles.Select(n => new NivelTriageViewModel
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Color = n.Color,
                Descripcion = n.Descripcion,
                TiempoMaximoAtencion = n.TiempoMaximoAtencion,
                Activo = n.Activo,
                CantidadReglas = n.ReglasAsignacion?.Count ?? 0
            }),
            IncluirInactivos = incluirInactivos
        };

        return View(viewModel);
    }

    public IActionResult Crear()
    {
        return View(new NivelTriageViewModel { Activo = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(NivelTriageViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        if (await _nivelService.ExisteNivelConNombre(viewModel.Nombre))
        {
            ModelState.AddModelError("Nombre", "Ya existe un nivel con este nombre");
            return View(viewModel);
        }

        var nivel = new NivelTriage
        {
            Nombre = viewModel.Nombre,
            Color = viewModel.Color,
            Descripcion = viewModel.Descripcion,
            TiempoMaximoAtencion = viewModel.TiempoMaximoAtencion,
            Activo = viewModel.Activo
        };

        await _nivelService.CrearNivel(nivel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int id)
    {
        var nivel = await _nivelService.ObtenerNivelPorId(id);
        if (nivel == null)
            return NotFound();

        var viewModel = new NivelTriageViewModel
        {
            Id = nivel.Id,
            Nombre = nivel.Nombre,
            Color = nivel.Color,
            Descripcion = nivel.Descripcion,
            TiempoMaximoAtencion = nivel.TiempoMaximoAtencion,
            Activo = nivel.Activo,
            CantidadReglas = nivel.ReglasAsignacion?.Count ?? 0
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(NivelTriageViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var nivel = new NivelTriage
        {
            Id = viewModel.Id,
            Nombre = viewModel.Nombre,
            Color = viewModel.Color,
            Descripcion = viewModel.Descripcion,
            TiempoMaximoAtencion = viewModel.TiempoMaximoAtencion,
            Activo = viewModel.Activo
        };

        var resultado = await _nivelService.ActualizarNivel(nivel);
        if (resultado == null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        var resultado = await _nivelService.EliminarNivel(id);
        if (!resultado)
        {
            TempData["Error"] = "No se puede eliminar el nivel porque tiene reglas asociadas";
        }
        return RedirectToAction(nameof(Index));
    }
}