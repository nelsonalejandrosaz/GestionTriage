using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionTriage.Models;
using GestionTriage.Services;

namespace GestionTriage.Controllers;

public class HomeController : Controller
{
    private readonly ITriageService _triageService;

    public HomeController(ITriageService triageService)
    {
        _triageService = triageService;
    }

    public IActionResult Index()
    {
        return View();
    }
}