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
using AvesTest2.Infrastructure.Options;

namespace AvesTest2.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public IOptions<ApplicationOptions> _appOptions;

        public HomeController(ILogger<HomeController> logger, 
                              IOptions<ApplicationOptions> appOptions,
                              IOptions<MailOptions> mailOptions) : base(appOptions, mailOptions)
        {
            _logger = logger;
            _appOptions = appOptions;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Alphabetical()
        {
            BirdViewModel model = await BirdViewModel.Load(Connection);
            ViewData["Title"] = "Birds A-Z";
            return View(model);
        }

        public async Task<IActionResult> Family()
        {
            BirdViewModel model = await BirdViewModel.Load(Connection);
            ViewData["Title"] = "Bird Families";
            return View(model);
        }

        public async Task<IActionResult> Location()
        {
            LocationViewModel model = await LocationViewModel.Load(Connection);
            ViewData["Title"] = "Bird Locations";
            return View(model);
        }

        //TODO: refactor
        public async Task<IActionResult> Show(int birdId, int location = 0)
        {
            BirdImagesViewModel model;
            if (location != 0)
                model = await BirdImagesViewModel.LoadAllSingleByCountry(Connection, birdId, location, _appOptions);
            else
                model = await BirdImagesViewModel.LoadAllSingle(Connection, birdId, _appOptions);
            
            int index = model.Birds.FindIndex(x => x.Id == birdId);
            if(index >= 0)
                ViewData["Title"] = string.Format("{0} ({1})", model.Birds[index].Name, model.Birds[index].SciName);

            return View(model);
        }

        public async Task<IActionResult> ShowFamily(int familyId)
        {
            BirdImagesViewModel model = await BirdImagesViewModel.LoadWholeFamily(Connection, familyId, _appOptions);
            //int index = model.Families.FindIndex(x => x.Id == familyId);
            //ViewData["Title"] = string.Format("{0} ({1})", model.Families[index].SciName, model.Families[index].Name);
            return View("Show", model);
        }

        public async Task<IActionResult> SelectFamily(int familyId)
        {
            BirdViewModel model = await BirdViewModel.Load(Connection);
            ViewData["type"] = eBlurbDataType.Family;
            ViewData["id"] = familyId;
            int index = model.Families.FindIndex(x => x.Id == familyId);
            ViewData["Title"] = string.Format("{0} ( {1} )", model.Families[index].SciName, model.Families[index].Name);
            return View("Selection", model);
        }

        public async Task<IActionResult> SelectLocation(int countryId)
        {
            BirdViewModel model = await BirdViewModel.Load(Connection, countryId);

            ViewData["type"] = eBlurbDataType.Location;
            ViewData["id"] = countryId;
            
            string countryName;
            Countries.Codes.TryGetValue(countryId, out countryName);
            ViewData["Title"] = countryName;
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

