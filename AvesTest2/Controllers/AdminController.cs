using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using AvesTest2.Models;
using Microsoft.AspNetCore.Mvc;

//TODO: Implement caching

namespace AvesTest2.Controllers
{
    public class AdminController : Controller
    {
        private AdminViewModel model;

        public AdminController()
        {
        }

        public async Task<IActionResult> Admin()
        {
            model = await AdminViewModel.Load();
            return View(model);
        }

        public async Task<IActionResult> AddBird(string Name, string SciName, int FamilyId)
        {
            BirdDTO bird = new BirdDTO();
            if (model == null)
                model = await AdminViewModel.Load();

            if (Name != null && SciName != null && FamilyId != 0)
            {
                bird.Name = Name;
                bird.SciName = SciName;
                bird.FamilyId = Convert.ToInt32(FamilyId);

                BirdsRepository repo = new BirdsRepository();
                repo.AddBird(bird);
            }
                
            return View("Admin", model);
        }
    }
}