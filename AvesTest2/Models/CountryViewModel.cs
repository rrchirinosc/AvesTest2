using AvesTest2.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AvesTest2.Infraestructure.Data;

namespace AvesTest2.Models
{
    public class CountryViewModel
    {
        public Dictionary<int, string> AvailableCountries;

        public static async Task<CountryViewModel> Load(SqlConnection connection)
        {
            CountryViewModel model = new CountryViewModel();
            BirdsRepository repo = new BirdsRepository(connection);

            List<int>countryIds = repo.GetCountries.ToList();
            Countries countries = new Countries();

            model.AvailableCountries = countries.Codes.Where(c => countryIds.Contains(c.Key)).ToDictionary(x => x.Key, x => x.Value);
                      
            return model;
        }
    }
}
