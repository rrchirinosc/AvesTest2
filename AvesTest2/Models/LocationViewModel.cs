using AvesTest2.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AvesTest2.Infraestructure.Data;

namespace AvesTest2.Models
{
    public class LocationViewModel
    {
        public Dictionary<int, string> AvailableCountries;

        public static async Task<LocationViewModel> Load(SqlConnection connection)
        {
            LocationViewModel model = new LocationViewModel();
            BirdsRepository repo = new BirdsRepository(connection);

            List<int>countryIds = repo.GetCountries.ToList();
            model.AvailableCountries = Countries.Codes.Where(c => countryIds.Contains(c.Key)).ToDictionary(x => x.Key, x => x.Value);
                      
            return model;
        }
    }
}
