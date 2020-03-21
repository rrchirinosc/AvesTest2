using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AvesTest2.Models;
using Microsoft.Extensions.Options;

namespace AvesTest2.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public IOptions<ApplicationOptions> _appOptions;

        public HomeController(ILogger<HomeController> logger, 
                              IOptions<ApplicationOptions> appOptions) : base(appOptions)
        {
            _logger = logger;
            _appOptions = appOptions;
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
            BirdViewModel model = await BirdViewModel.Load(Connection);
            return View(model);
        }

        public async Task<IActionResult> Family()
        {
            BirdViewModel model = await BirdViewModel.Load(Connection);
            return View(model);
        }

        public async Task<IActionResult> Location()
        {
            //AlphabeticalViewModel model = await AlphabeticalViewModel.Load();
            return View();
        }

        public async Task<IActionResult> Show(int birdId)
        {
            BirdImagesViewModel model = await BirdImagesViewModel.LoadSingle(Connection, birdId, _appOptions);
            return View(model);
        }

        public async Task<IActionResult> ShowFamily(int familyId)
        {
            BirdImagesViewModel model = await BirdImagesViewModel.LoadFamily(Connection, familyId, _appOptions);
            return View("Show", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

