using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionTriage.Models;
using GestionTriage.Services;

namespace GestionTriage.Controllers;

public class HomeController : Controller
{
    private readonly IRegistroTriageService _registroTriageService;

    public HomeController(IRegistroTriageService registroTriageService)
    {
        _registroTriageService = registroTriageService;
    }

    public async Task<IActionResult> Index()
    {
        var resumenTriage = await _registroTriageService.ObtenerResumenRegistros();
        return View(resumenTriage);
    }
}