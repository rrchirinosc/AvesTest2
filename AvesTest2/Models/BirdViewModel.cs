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
        public List<FamilyDTO> Families;

        public static async Task<BirdViewModel> Load()
        {
            BirdViewModel model = new BirdViewModel();
            BirdsRepository repo = new BirdsRepository();
            model.Birds = repo.Birds.ToList();
            model.Families = repo.Families.ToList();

            List<int> AvailableFamilies = new List<int>();
            foreach(var bird in model.Birds)
            {
                AvailableFamilies.Add(bird.FamilyId);
            }
            IEnumerable<int> availableFamilies = AvailableFamilies.Distinct();

            foreach(var family in model.Families)
            {
                if(availableFamilies.Contains(family.Id)) {
                    family.Available = true;
                }
            }
            return model;
        }
    }   
}
