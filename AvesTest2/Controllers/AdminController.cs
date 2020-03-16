﻿using System;
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
#if DEBUG
            model = await AdminViewModel.Load();
            return View(model);
#else
            return RedirectToAction("Index", "Home");
#endif
        }

        public async Task<IActionResult> AddBird(string Name, string SciName, int FamilyId)
        {
            BirdDTO bird = new BirdDTO();
            if (model == null)
                model = await AdminViewModel.Load();

            if (Name != null && 
                SciName != null && 
                FamilyId != 0)
            {
                bird.Name = Name;
                bird.SciName = SciName;
                bird.FamilyId = Convert.ToInt32(FamilyId);

                BirdsRepository repo = new BirdsRepository();
                repo.AddBird(bird);
            }
                
            return View("Admin", model);
        }

        public async Task<IActionResult> AddImage(int BirdId, string FileName, string Location, 
            DateTime Date, int Country, string Coordinate, bool IsActive = false)
        {
            ImageDTO image = new ImageDTO();
            if (model == null)
                model = await AdminViewModel.Load();

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

                BirdsRepository repo = new BirdsRepository();
                repo.AddImage(image);
            }

            return View("Admin", model);
        }
    }    
 }