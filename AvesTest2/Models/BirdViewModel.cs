using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class BirdViewModel
    {
        public List<BirdDTO> Birds;

        public static async Task<BirdViewModel> Load()
        {
            BirdViewModel model = new BirdViewModel();
            BirdsRepository repo = new BirdsRepository();
            model.Birds = repo.Birds.ToList();

            return model;
        }
    }
}
