using AvesTest2.Database.DTO;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Routing.Constraints;

namespace AvesTest2.Database.Repositories
{
    public class BirdsRepository
    {
        private SqlConnection _connection;

        public BirdsRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        /* Fetch all available birds */
        public IEnumerable<BirdDTO> Birds
        {
            get
            {
                string sql = "SELECT [Bird].Id, [Bird].Name, [Bird].SciName, [Bird].FamilyId" +
                                                " FROM [Bird] ORDER BY [Name]";
                return _connection.Query<BirdDTO>(sql);
            }
        }

        public IEnumerable<BirdDTO> BirdsByCountry(int countryId)
        {
                string sql = string.Format("SELECT DISTINCT [Bird].Id, [Bird].Name, [Bird].SciName, [Bird].FamilyId" +
                                        " FROM [Bird] INNER JOIN [Image]" +
                                        " ON [Image].BirdId = [Bird].Id" +
                                        " WHERE [Image].Country = {0}" +
                                        " ORDER BY [Name]", countryId);

                return _connection.Query<BirdDTO>(sql);
        }

        /* Fetch all available bird family names */
        public IEnumerable<FamilyDTO> Families
        {
            get
            {
                string sql = "SELECT [Family].Id, [Family].Name, [Family].SciName" +
                                                " FROM [Family] ORDER BY [SciName]";
                return _connection.Query<FamilyDTO>(sql);
            }
        }

        /* Fetch all file names that are key images */
        public IEnumerable<BirdImageDTO> KeyImages
        {
            get
            {
                string sql = "SELECT [Image].BirdId, [Image].FileName" +
                                                " FROM [Image]" +
                                                " WHERE [KeyImage] = 1" +
                                                " ORDER BY [BirdId]";
                return _connection.Query<BirdImageDTO>(sql);
            }
        }

        /* Fetch images for a particular bird, sorted with key image first */
        public IEnumerable<string> GetImages(int birdId)
        {
            string sql = string.Format("SELECT [Image].FileName" +
                                            " FROM [Image]" +
                                            " WHERE [BirdId] = {0}" +
                                            " ORDER BY [Image].KeyImage DESC", birdId);
            return _connection.Query<string>(sql);
        }

        /* Fetch all bird file names belonging to a particular family */
        public IEnumerable<BirdImageDTO> GetImagesByFamily(int familyId)
        {
            string sql = string.Format("SELECT [Image].BirdId, [Image].FileName" +
                                            " FROM [Image]" +
                                            " WHERE [BirdId] in (" +
                                            " SELECT [Bird].Id " +
                                            " FROM [Bird]" +
                                            " WHERE [Bird].FamilyId = {0} )" +
                                            " ORDER BY [Image].BirdId", familyId);
            return _connection.Query<BirdImageDTO>(sql);
        }

        /* Fetch all bird file names belonging to a particular country */
        public IEnumerable<BirdImageDTO> GetImagesByCountry(int countryId)
        {
            string sql = string.Format("SELECT [Image].BirdId, [Image].FileName" +
                                            " FROM [Image]" +
                                            " WHERE [Image].Country = {0}" +
                                            " ORDER BY [Image].BirdId", countryId);
            return _connection.Query<BirdImageDTO>(sql);
        }

        /* Fetch all bird display info, including images ordered by keyimage */
        public IEnumerable<BirdFullDTO> GetAllBirdInfo(int birdId)
        {
            string sql = string.Format("SELECT [Bird].Id, [Bird].Name, [Bird].SciName," +
                            "[Image].FileName, [Image].Location, [Image].Date, [Image].Country, [Image].Coordinate, [Image].Comment" +
                                            " FROM [Bird] INNER JOIN [Image]" +
                                            " ON [Image].BirdId = [Bird].Id" +
                                            " WHERE [Image].BirdId = {0}" +
                                            " ORDER BY [Image].KeyImage DESC", birdId);
            return _connection.Query<BirdFullDTO>(sql);
        }

        public IEnumerable<BirdFullDTO> GetAllBirdInfoByCountry(int birdId, int countryId)
        {
            string sql = string.Format("SELECT [Bird].Id, [Bird].Name, [Bird].SciName," +
                            "[Image].FileName, [Image].Location, [Image].Date, [Image].Country, [Image].Coordinate, [Image].Comment" +
                                            " FROM [Bird] INNER JOIN [Image]" +
                                            " ON [Image].BirdId = [Bird].Id" +
                                            " WHERE ([Image].BirdId = {0} AND [Image].Country = {1})" +
                                            " ORDER BY [Image].KeyImage DESC", birdId, countryId);
            return _connection.Query<BirdFullDTO>(sql);
        }


        /* Fetch all bird file info belonging to a particular family */
        public IEnumerable<BirdFullDTO> GetAllBirdInfoByFamily(int familyId)
        {
            string sql = string.Format("SELECT [Bird].Id, [Bird].Name, [Bird].SciName," +
                            "[Image].FileName, [Image].Location, [Image].Date, [Image].Country, [Image].Coordinate, [Image].Comment" +
                                            " FROM [Bird] INNER JOIN [Image]" +
                                            " ON [Image].BirdId = [Bird].Id" +
                                            " WHERE [BirdId] in (" +
                                            " SELECT [Bird].Id " +
                                            " FROM [Bird]" +
                                            " WHERE [Bird].FamilyId = {0} )" +
                                            " ORDER BY [Image].BirdId", familyId);

            return _connection.Query<BirdFullDTO>(sql);
        }

        /* Fetch all bird file info belonging to a particular country */
        public IEnumerable<BirdFullDTO> GetAllBirdInfoByCountry(int countryId)
        {
            string sql = string.Format("SELECT [Bird].Id, [Bird].Name, [Bird].SciName," +
                            "[Image].FileName, [Image].Location, [Image].Date, [Image].Country, [Image].Coordinate" +
                                            " FROM [Bird] INNER JOIN [Image]" +
                                            " ON [Image].BirdId = [Bird].Id" +
                                            " WHERE [Image].Country = {0}", countryId);

            return _connection.Query<BirdFullDTO>(sql);
        }

        public IEnumerable<BirdCountryDTO> GetBirdByCountry(int countryId)
        {
                string sql = string.Format("SELECT DISTINCT [Bird].Id, [Image].Country AS CountryId" +
                                                " FROM [Bird] INNER JOIN [Image]" +
                                                " ON [Image].BirdId = [Bird].Id" +
                                                " WHERE [Image].Country = {0}", countryId);

                return _connection.Query<BirdCountryDTO>(sql);
        }

        /* Fetch all country existing Ids */
        public IEnumerable<int> GetCountries
        {
            get
            {
                string sql = string.Format("SELECT DISTINCT [Image].Country" +
                                            " FROM [Image]");
                return _connection.Query<int>(sql);
            }
        }

        /* Add a new bird to the [Bird] table */
        public int AddBird(BirdDTO Bird)
        {
            string sql = "INSERT INTO [Bird] (Name, SciName, FamilyId)" +
                    " Values (@Name, @SciName, @FamilyId)";

            int rows = _connection.Execute(sql, new { Bird.Name, Bird.SciName, Bird.FamilyId });

            return rows;
        }

        /* Add a new image to the [Image] table */
        public int AddImage(ImageDTO Image)
        {
            string sql = "INSERT INTO [Image] (BirdId, FileName, Location, Date, Country, Coordinate, Comment, KeyImage)" +
                    " Values (@BirdId, @FileName, @Location, @Date, @Country, @Coordinate, @Comment, @KeyImage)";

            int rows = _connection.Execute(sql, new { Image.BirdId, Image.FileName, Image.Location, Image.Date, Image.Country, Image.Coordinate, Image.Comment, Image.KeyImage});

            return rows;
        }

        /* Update image data in the [Image] table */
        public int UpdateImage(ImageDTO UpdatedImage)
        {
            // Read in imaga data to be modified
            List<ImageDTO> image = (List<ImageDTO>)GetImage(UpdatedImage.Id);

            if (image == null)
                return 0;

            // Check what parameters are to be changed and setup query accordingly
            if (UpdatedImage.FileName != null)
                image[0].FileName = UpdatedImage.FileName;
            if (UpdatedImage.Location != null)
                image[0].Location = UpdatedImage.Location;
            if (UpdatedImage.Date != null)
                image[0].Date = UpdatedImage.Date;
            if (UpdatedImage.Country != 0)
                image[0].Country = UpdatedImage.Country;
            if (UpdatedImage.Coordinate != "")
                image[0].Coordinate = UpdatedImage.Coordinate;
            if (UpdatedImage.Comment != "")
                image[0].Comment = UpdatedImage.Comment;

            string sql = "UPDATE [Image] SET [FileName] = @filename," +
                                            " [Location] = @location," +
                                            " [Date] = @date," +
                                            " [Country] = @country," +
                                            " [Coordinate] = @coordinate," +
                                            " [Comment] = @comment" +
                                            " WHERE [Id] = @id";

            return _connection.Execute(sql, new { filename = image[0].FileName, 
                                                location = image[0].Location, 
                                                date = image[0].Date,
                                                country = image[0].Country,
                                                coordinate = image[0].Coordinate,
                                                comment = image[0].Comment,
                                                id = image[0].Id });
        }

        /* Add a new image to the [Image] table */
        public int RemoveImage(int ImageId)
        {
            string sql = string.Format("DELETE FROM [Image] WHERE Id = {0}", ImageId);

            return _connection.Execute(sql);
        }

        /* Get table methods -- used for Admin purposes */
        public IEnumerable<FamilyDTO> GetFamilyTable        
        {
            get
            {
                string sql = "SELECT * FROM [Family] ORDER BY [Id]";
                return _connection.Query<FamilyDTO>(sql);
            }
        }

        public IEnumerable<ImageDTO> GetImageTable
        {
            get
            {
                string sql = "SELECT * FROM [Image] ORDER BY [BirdId]";               
                return _connection.Query<ImageDTO>(sql);
            }
        }

        public IEnumerable<string> ResetKeyImage(int birdId)
        {
            string sql = string.Format("UPDATE [Image]" +
                                            " SET [KeyImage]= 0" +
                                            " WHERE [BirdId] = {0}", birdId);
            return _connection.Query<string>(sql);
        }

        public int GetBirdCount
        {
            get
            {
                string sql = string.Format("SELECT COUNT(*) FROM [Bird]");

                return _connection.Query<int>(sql).First();
            }
        }

        public int GetKeyImageCount
        {
            get
            {
                string sql = string.Format("SELECT COUNT(DISTINCT [BirdId])" +
                                        " FROM [Image]" +
                                        " WHERE [KeyImage] = 1");

                return _connection.Query<int>(sql).First();
            }
        }

        public IEnumerable<ImagesCount> GetImagesPerBird()
        {
            string sql = "SELECT BirdsTable.Id, BirdsTable.Name," +
                " (CASE WHEN ImagesTable.Images is NULL THEN 0 ELSE ImagesTable.Images end) AS Images" +
                " FROM " +
                " (SELECT [Id], [Name] FROM [Bird]) AS BirdsTable" +
                " LEFT JOIN" +
                " (SELECT [BirdId], COUNT([BirdId]) AS Images" +
                " FROM [Image] GROUP BY [BirdId]) AS ImagesTable" +
                " ON BirdsTable.[Id] = ImagesTable.[BirdId]" +
                " ORDER BY BirdsTable.[Name]";

            return _connection.Query<ImagesCount>(sql);

    }

        private IEnumerable<ImageDTO> GetImage(long ImageId)
        {
            string sql = string.Format("SELECT * FROM [Image] WHERE [Id]= {0}", ImageId);
            return _connection.Query<ImageDTO>(sql);
        }
    }
}
