using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AvesTest2.Models;
using Microsoft.Extensions.Options;
using AvesTest2.Infraestructure.Data;

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
            LocationViewModel model = await LocationViewModel.Load(Connection);
            return View(model);
        }

        public async Task<IActionResult> Show(int birdId)
        {
            BirdImagesViewModel model = await BirdImagesViewModel.LoadAllSingle(Connection, birdId, _appOptions);
            return View(model);
        }

        public async Task<IActionResult> ShowFamily(int familyId)
        {
            BirdImagesViewModel model = await BirdImagesViewModel.LoadWholeFamily(Connection, familyId, _appOptions);
            return View("Show", model);
        }

        public async Task<IActionResult> SelectFamily(int familyId)
        {
            BirdViewModel model = await BirdViewModel.Load(Connection);
            ViewData["type"] = eBlurbDataType.Family;
            ViewData["id"] = familyId;
            return View("Selection", model);
        }

        public async Task<IActionResult> SelectLocation(int countryId)
        {
            BirdViewModel model = await BirdViewModel.Load(Connection, countryId);
            ViewData["type"] = eBlurbDataType.Location;
            ViewData["id"] = countryId;
            return View("Selection", model);
        }

        public async Task<IActionResult> ShowByLocation(int countryId)
        {
            BirdImagesViewModel model = await BirdImagesViewModel.LoadAllBirdsByLocation(Connection, countryId, _appOptions);
            return View("Show", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

