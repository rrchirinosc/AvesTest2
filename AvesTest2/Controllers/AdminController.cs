using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using AvesTest2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

//TODO: Implement caching

namespace AvesTest2.Controllers
{
    public class AdminController : BaseController
    {
        private AdminViewModel model;

        public AdminController(IOptions<ApplicationOptions> appOptions):base(appOptions)
        {
        }

        public async Task<IActionResult> Admin()
        {
#if DEBUG
            model = await AdminViewModel.Load(Connection);
            return View(model);
#else
            return RedirectToAction("Index", "Home");
#endif
        }

        public async Task<IActionResult> AddBird(string Name, string SciName, int FamilyId)
        {
            BirdDTO bird = new BirdDTO();
            if (model == null)
                model = await AdminViewModel.Load(Connection);

            if (Name != null && 
                SciName != null && 
                FamilyId != 0)
            {
                bird.Name = Name;
                bird.SciName = SciName;
                bird.FamilyId = Convert.ToInt32(FamilyId);

                BirdsRepository repo = new BirdsRepository(Connection);
                repo.AddBird(bird);
            }
                
            return View("Admin", model);
        }

        public async Task<IActionResult> AddImage(int BirdId, string FileName, string Location, 
            DateTime Date, int Country, string Coordinate, bool IsActive = false)
        {
            ImageDTO image = new ImageDTO();
            if (model == null)
                model = await AdminViewModel.Load(Connection);

            // check for missing fields that make entry invalid
            if (BirdId != 0 &&
                FileName != null &&
                Location != null &&
                Date.Year != 1 &&
                Country != 0)
            {
                image.BirdId = BirdId;
                image.FileName = FileName;
                image.Location = Location;
                image.Date = Date.ToShortDateString();
                image.Country = Country;
                image.Coordinate = (Coordinate == null) ? "" : Coordinate;
                image.KeyImage = IsActive;

                BirdsRepository repo = new BirdsRepository(Connection);

                // check whether or not the bird has already an image set as key and
                // if so remove the flag for that entry
                if (image.KeyImage == true)
                {
                    repo.ResetKeyImage(BirdId);
                }

                repo.AddImage(image);
            }

            return View("Admin", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetBirdTable()
        {
            BirdsRepository repo = new BirdsRepository(Connection);
            List<BirdDTO>  model = repo.Birds.ToList();
            
            return Json(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetFamilyTable()
        {
            BirdsRepository repo = new BirdsRepository(Connection);
            List<FamilyDTO> model = repo.GetFamilyTable.ToList();

            return Json(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetImageTable()
        {
            BirdsRepository repo = new BirdsRepository(Connection);
            List<ImageDTO> model = repo.GetImageTable.ToList();

            return Json(model);
        }
    }    
 }