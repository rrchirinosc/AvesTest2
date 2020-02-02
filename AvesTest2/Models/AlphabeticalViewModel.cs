using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class AlphabeticalViewModel
    {
        public List<BirdDTO> Birds;
        public char[] Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public static async Task<AlphabeticalViewModel> Load()
        {
            AlphabeticalViewModel model = new AlphabeticalViewModel();
            BirdsRepository repo = new BirdsRepository();
            model.Birds = repo.Birds.ToList();

            return model;
        }
    }
}
