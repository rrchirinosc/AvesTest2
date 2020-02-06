﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AvesTest2.Models;

namespace AvesTest2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Alphabetical()
        {
            BirdViewModel model = await BirdViewModel.Load();
            return View(model);
        }

        public async Task<IActionResult> Family()
        {
            BirdViewModel model = await BirdViewModel.Load();
            return View(model);
        }

        public async Task<IActionResult> Location()
        {
            //AlphabeticalViewModel model = await AlphabeticalViewModel.Load();
            return View();
        }

        public async Task<IActionResult> Show()
        {
            //AlphabeticalViewModel model = await AlphabeticalViewModel.Load();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
