using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class AdminViewModel
    {
        public List<FamilyDTO> Families;

        public static async Task<AdminViewModel> Load()
        {
            AdminViewModel model = new AdminViewModel();
            BirdsRepository repo = new BirdsRepository();
            model.Families = repo.Families.ToList();

            return model;
        }
    }
}
