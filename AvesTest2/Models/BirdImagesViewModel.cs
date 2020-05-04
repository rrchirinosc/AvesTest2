using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Models
{
    public class BirdImagesViewModel
    {
        public List<BirdFullDTO> Birds;

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public static async Task<BirdImagesViewModel> LoadAllSingle(SqlConnection connection, int birdId, IOptions<ApplicationOptions> appOptions)
        {
            BirdImagesViewModel model = new BirdImagesViewModel();
            BirdsRepository repo = new BirdsRepository(connection);

            string ImgRoot = "/Images/Birds";
            model.Birds = repo.GetAllBirdInfo(birdId).ToList();
   
            foreach (var bird in model.Birds)
            {
                bird.FileName = string.Format("{0}/{1}/{2}.jpg", ImgRoot, birdId, bird.FileName);
            }

            return model;
        }

        public static async Task<BirdImagesViewModel> LoadAllSingleByCountry(SqlConnection connection, int birdId, int countryId, IOptions<ApplicationOptions> appOptions)
        {
            BirdImagesViewModel model = new BirdImagesViewModel();
            BirdsRepository repo = new BirdsRepository(connection);

            string ImgRoot = "/Images/Birds";
            model.Birds = repo.GetAllBirdInfoByCountry(birdId, countryId).ToList();

            foreach (var bird in model.Birds)
            {
                bird.FileName = string.Format("{0}/{1}/{2}.jpg", ImgRoot, birdId, bird.FileName);
            }

            return model;
        }

        public static async Task<BirdImagesViewModel> LoadWholeFamily(SqlConnection connection, int familyId, IOptions<ApplicationOptions> appOptions)
        {
            BirdImagesViewModel model = new BirdImagesViewModel();
            BirdsRepository repo = new BirdsRepository(connection);

            string ImgRoot = "/Images/Birds";
            model.Birds = repo.GetAllBirdInfoByFamily(familyId).ToList();
            foreach (var bird in model.Birds)
            {
                bird.FileName = string.Format("{0}/{1}/{2}.jpg", ImgRoot, bird.Id, bird.FileName);
            }

            return model;
        }

        public static async Task<BirdImagesViewModel> LoadAllBirdsByLocation(SqlConnection connection, int countryId, IOptions<ApplicationOptions> appOptions)
        {
            BirdImagesViewModel model = new BirdImagesViewModel();
            BirdsRepository repo = new BirdsRepository(connection);

            string ImgRoot = "/Images/Birds";
            model.Birds = repo.GetAllBirdInfoByCountry(countryId).ToList();

            foreach (var bird in model.Birds)
            {
                bird.FileName = string.Format("{0}/{1}/{2}.jpg", ImgRoot, bird.Id, bird.FileName);
            }

            return model;
        }
    }
}
