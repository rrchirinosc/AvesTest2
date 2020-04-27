using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class StatsViewModel
    {
        public StatsDTO Stats;

        public static async Task<StatsViewModel> Load(SqlConnection connection)
        {
            StatsViewModel model = new StatsViewModel();
            BirdsRepository repo = new BirdsRepository(connection);
            model.Stats = new StatsDTO(){
                BirdCount = repo.GetBirdCount,
                HaveKeyImages = repo.GetKeyImageCount };
            return model;
        }
    }
}
