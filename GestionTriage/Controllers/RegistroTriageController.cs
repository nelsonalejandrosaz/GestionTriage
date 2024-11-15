using GestionTriage.Models;
using GestionTriage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionTriage.Controllers;

public class RegistroTriageController : Controller
{
    private readonly IRegistroTriageService _registroTriageService;
    private readonly IPacienteService _pacienteService;
    private readonly INivelTriageService _nivelTriageService;
    private readonly IHospitalService _hospitalService;

    public RegistroTriageController(
        IRegistroTriageService registroTriageService,
        IPacienteService pacienteService,
        INivelTriageService nivelTriageService,
        IHospitalService hospitalService)
    {
        _registroTriageService = registroTriageService;
        _pacienteService = pacienteService;
        _nivelTriageService = nivelTriageService;
        _hospitalService = hospitalService;
    }

    // GET: RegistroTriage
    public async Task<IActionResult> Index(int? filtroHospitalId, bool? filtroEstadoAtendido)
    {
        var viewModel = new ListaRegistrosTriageViewModel
        {
            FiltroHospitalId = filtroHospitalId,
            FiltroEstadoAtendido = filtroEstadoAtendido,
            Hospitales = new SelectList(await _hospitalService.ObtenerTodosHospitales(), "Id", "Nombre", filtroHospitalId)
        };

        var registros = filtroHospitalId.HasValue
            ? await _registroTriageService.ObtenerPorHospital(filtroHospitalId.Value)
            : await _registroTriageService.ObtenerTodos();

        if (filtroEstadoAtendido.HasValue)
        {
            registros = registros.Where(r => r.EstadoAtendido == filtroEstadoAtendido.Value);
        }

        viewModel.Registros = registros.Select(r => new RegistroTriageViewModel
        {
            Id = r.Id,
            CodigoAtencion = r.CodigoAtencion,
            PacienteId = r.PacienteId,
            NombrePaciente = r.Paciente.NombreCompleto,
            NivelTriageId = r.NivelTriageId,
            NivelTriageNombre = r.NivelTriage.Nombre,
            ColorNivelTriage = r.NivelTriage.Color,
            FechaHoraIngreso = r.FechaHoraIngreso,
            FechaHoraAtencion = r.FechaHoraAtencion,
            FechaHoraFinalizacion = r.FechaHoraFinalizacion,
            ObservacionesIngreso = r.ObservacionesIngreso,
            ObservacionesAtencion = r.ObservacionesAtencion,
            EstadoAtendido = r.EstadoAtendido,
            HospitalId = r.HospitalId,
            NombreHospital = r.Hospital.Nombre
        });

        return View(viewModel);
    }

    // GET: RegistroTriage/Detalles/5
    public async Task<IActionResult> Detalles(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var registro = await _registroTriageService.ObtenerPorId(id.Value);
        if (registro == null)
        {
            return NotFound();
        }

        var viewModel = new RegistroTriageViewModel
        {
            Id = registro.Id,
            CodigoAtencion = registro.CodigoAtencion,
            PacienteId = registro.PacienteId,
            NombrePaciente = registro.Paciente.NombreCompleto,
            NivelTriageId = registro.NivelTriageId,
            NivelTriageNombre = registro.NivelTriage.Nombre,
            ColorNivelTriage = registro.NivelTriage.Color,
            FechaHoraIngreso = registro.FechaHoraIngreso,
            FechaHoraAtencion = registro.FechaHoraAtencion,
            FechaHoraFinalizacion = registro.FechaHoraFinalizacion,
            ObservacionesIngreso = registro.ObservacionesIngreso,
            ObservacionesAtencion = registro.ObservacionesAtencion,
            EstadoAtendido = registro.EstadoAtendido,
            HospitalId = registro.HospitalId,
            NombreHospital = registro.Hospital.Nombre
        };

        return View(viewModel);
    }

    // GET: RegistroTriage/Crear
    public async Task<IActionResult> Crear()
    {
        var viewModel = new CrearRegistroTriageViewModel
        {
            Pacientes = new SelectList(await _pacienteService.ObtenerTodosPacientes(), "Id", "NombreCompleto"),
            NivelesTriage = new SelectList(await _nivelTriageService.ObtenerTodosNiveles(), "Id", "Nombre"),
            Hospitales = new SelectList(await _hospitalService.ObtenerTodosHospitales(), "Id", "Nombre")
        };

        return View(viewModel);
    }

    // POST: RegistroTriage/Crear
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(CrearRegistroTriageViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var registro = new RegistroTriage
            {
                PacienteId = viewModel.PacienteId,
                NivelTriageId = viewModel.NivelTriageId,
                HospitalId = viewModel.HospitalId,
                ObservacionesIngreso = viewModel.ObservacionesIngreso
            };

            await _registroTriageService.Crear(registro);
            return RedirectToAction(nameof(Index));
        }

        // Si llegamos aquí, algo falló, recargamos las listas
        viewModel.Pacientes = new SelectList(await _pacienteService.ObtenerTodosPacientes(), "Id", "NombreCompleto",
            viewModel.PacienteId);
        viewModel.NivelesTriage = new SelectList(await _nivelTriageService.ObtenerTodosNiveles(), "Id", "Nombre",
            viewModel.NivelTriageId);
        viewModel.Hospitales =
            new SelectList(await _hospitalService.ObtenerTodosHospitales(), "Id", "Nombre", viewModel.HospitalId);

        return View(viewModel);
    }

    // POST: RegistroTriage/IniciarAtencion/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IniciarAtencion(int id)
    {
        var registro = await _registroTriageService.IniciarAtencion(id);
        if (registro == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: RegistroTriage/FinalizarAtencion/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FinalizarAtencion(int id, string observaciones)
    {
        var registro = await _registroTriageService.FinalizarAtencion(id, observaciones);
        if (registro == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: RegistroTriage/Eliminar/5
    [HttpPost, ActionName("Eliminar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        var resultado = await _registroTriageService.Eliminar(id);
        if (!resultado)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}