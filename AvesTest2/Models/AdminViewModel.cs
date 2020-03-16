using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using AvesTest2.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class AdminViewModel
    {
        public List<BirdDTO> Birds;
        public List<FamilyDTO> Families;
        public Dictionary<int, string> Countries;

        public static async Task<AdminViewModel> Load()
        {
            AdminViewModel model = new AdminViewModel();
            BirdsRepository repo = new BirdsRepository();
            model.Birds = repo.Birds.ToList();
            model.Families = repo.Families.ToList();
            model.Countries = new Countries().Codes;

            return model;
        }
    }    
}
