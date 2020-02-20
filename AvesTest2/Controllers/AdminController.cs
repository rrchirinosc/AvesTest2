using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AvesTest2.Controllers
{
    public class AdminController : Controller
    {
        private BirdsRepository repo;

        public AdminController()
        {
            repo = new BirdsRepository();
        }

        public IActionResult Admin()
        {
     
            return View();
        }

        public async Task<IActionResult> AddBird(string Name, string SciName, int FamilyId)
        {
            BirdDTO bird = new BirdDTO();
            bird.Name = Name;
            bird.SciName = SciName;
            bird.FamilyId = FamilyId;
            //repo.AddBird(bird);
            return View("Admin");
        }
    }
}