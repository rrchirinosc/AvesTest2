using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class BirdViewModel
    {
        public List<BirdDTO> Birds;
        public List<FamilyDTO> Families;
        public Dictionary<int, string> KeyImages;
        public List<BirdCountryDTO> InCountry;
        
        //TODO: Make loading more specific

        public static async Task<BirdViewModel> Load(SqlConnection connection)
        {
            BirdViewModel model = new BirdViewModel();
            BirdsRepository repo = new BirdsRepository(connection);
            model.Birds = repo.Birds.ToList();
            model.Families = repo.Families.ToList();
            model.KeyImages = repo.KeyImages.ToDictionary(x => x.BirdId, x => x.FileName);

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

        public static async Task<BirdViewModel> Load(SqlConnection connection, int countryId)
        {
            BirdViewModel model = new BirdViewModel();
            BirdsRepository repo = new BirdsRepository(connection);
            model.Birds = repo.BirdsByCountry(countryId).ToList();
            model.Families = repo.Families.ToList();
            model.KeyImages = repo.KeyImages.ToDictionary(x => x.BirdId, x => x.FileName);
            model.InCountry = repo.GetBirdByCountry(countryId).ToList();

            List<int> AvailableFamilies = new List<int>();
            foreach (var bird in model.Birds)
            {
                AvailableFamilies.Add(bird.FamilyId);
            }
            IEnumerable<int> availableFamilies = AvailableFamilies.Distinct();

            foreach (var family in model.Families)
            {
                if (availableFamilies.Contains(family.Id))
                {
                    family.Available = true;
                }
            }
            return model;
        }
    }   
}
