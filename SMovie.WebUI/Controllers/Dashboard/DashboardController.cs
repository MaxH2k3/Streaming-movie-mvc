﻿using Microsoft.AspNetCore.Mvc;

namespace SMovie.WebUI.Controllers.Dashboard;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}