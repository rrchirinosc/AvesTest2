using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using AvesTest2.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class AdminViewModel
    {
        public List<BirdDTO> Birds;
        public List<FamilyDTO> Families;
        public Dictionary<int, string> AllCountries;

        public static async Task<AdminViewModel> Load(SqlConnection connection)
        {
            AdminViewModel model = new AdminViewModel();
            BirdsRepository repo = new BirdsRepository(connection);
            model.Birds = repo.Birds.ToList();
            model.Families = repo.Families.ToList();
            model.AllCountries = Countries.Codes;

            return model;
        }
    }    
}
