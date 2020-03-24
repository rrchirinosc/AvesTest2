﻿using AvesTest2.Database.DTO;
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
        public List<string> Images;

        public static async Task<BirdImagesViewModel> LoadSingle(SqlConnection connection, int birdId, IOptions<ApplicationOptions> appOptions)
        {
            BirdImagesViewModel model = new BirdImagesViewModel();
            BirdsRepository repo = new BirdsRepository(connection);

            string ImgRoot = appOptions.Value.Scheme + "://" + appOptions.Value.DomainName + ":" + appOptions.Value.Port + "/Images/Birds";
            List<string> fileNames = repo.GetImages(birdId).ToList();
            model.Images = new List<string>();

            foreach(var filename in fileNames)
            {
                model.Images.Add(string.Format("{0}/{1}/{2}.jpg", ImgRoot, birdId, filename));
            }
            
            return model;
        }

        public static async Task<BirdImagesViewModel> LoadFamily(SqlConnection connection, int familyId, IOptions<ApplicationOptions> appOptions)
        {
            BirdImagesViewModel model = new BirdImagesViewModel();
            BirdsRepository repo = new BirdsRepository(connection);
            List<BirdImageDTO> images = new List<BirdImageDTO>();

            string ImgRoot = appOptions.Value.Scheme + "://" + appOptions.Value.DomainName + ":" + appOptions.Value.Port + "/Images/Birds";
            images = repo.GetImagesByFamily(familyId).ToList();
            model.Images = new List<string>();

            foreach (var image in images)
            {
                model.Images.Add(string.Format("{0}/{1}/{2}.jpg", ImgRoot, image.BirdId, image.FileName));
            }

            return model;
        }

        public static async Task<BirdImagesViewModel> LoadByCountry(SqlConnection connection, int countryId, IOptions<ApplicationOptions> appOptions)
        {
            BirdImagesViewModel model = new BirdImagesViewModel();
            BirdsRepository repo = new BirdsRepository(connection);
            List<BirdImageDTO> images = new List<BirdImageDTO>();

            string ImgRoot = appOptions.Value.Scheme + "://" + appOptions.Value.DomainName + ":" + appOptions.Value.Port + "/Images/Birds";
            images = repo.GetImagesByCountry(countryId).ToList();
            model.Images = new List<string>();

            foreach (var image in images)
            {
                model.Images.Add(string.Format("{0}/{1}/{2}.jpg", ImgRoot, image.BirdId, image.FileName));
            }

            return model;
        }
    }
}
