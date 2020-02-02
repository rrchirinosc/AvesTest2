using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvesTest2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AvesTest2.Controllers
{
    public class AlphabeticalController : Controller
    {
        public async Task<IActionResult> Index()
        {
            AlphabeticalViewModel model = await AlphabeticalViewModel.Load();
            return View("Alphabetical", model);
        }
    }
}