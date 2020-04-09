using AvesTest2.Database.DTO;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;

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

        /* Fetch all bird display info, including images orderd by keyimage*/
        public IEnumerable<BirdFullDTO> GetAllBirdInfo(int birdId)
        {
            string sql = string.Format("SELECT [Bird].Id, [Bird].Name, [Bird].SciName," +
                            "[Image].FileName, [Image].Location, [Image].Date, [Image].Country, [Image].Coordinate" +
                                            " FROM [Bird] INNER JOIN [Image]" +
                                            " ON [Image].BirdId = [Bird].Id" +
                                            " WHERE [Image].BirdId = {0}" +
                                            " ORDER BY [Image].KeyImage DESC", birdId);
            return _connection.Query<BirdFullDTO>(sql);
        }

        /* Fetch all bird file info belonging to a particular family */
        public IEnumerable<BirdFullDTO> GetAllBirdInfoByFamily(int familyId)
        {
            string sql = string.Format("SELECT [Bird].Id, [Bird].Name, [Bird].SciName," +
                            "[Image].FileName, [Image].Location, [Image].Date, [Image].Country, [Image].Coordinate" +
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
            string sql = "INSERT INTO Bird (Name, SciName, FamilyId)" +
                    " Values (@Name, @SciName, @FamilyId)";

            int rows = _connection.Execute(sql, new { Bird.Name, Bird.SciName, Bird.FamilyId });

            return rows;
        }

        /* Add a new image to the [Image] table */
        public int AddImage(ImageDTO Image)
        {
            string sql = "INSERT INTO Image (BirdId, FileName, Location, Date, Country, Coordinate, KeyImage)" +
                    " Values (@BirdId, @FileName, @Location, @Date, @Country, @Coordinate, @KeyImage)";

            int rows = _connection.Execute(sql, new { Image.BirdId, Image.FileName, Image.Location, Image.Date, Image.Country, Image.Coordinate, Image.KeyImage});

            return rows;
        }
    }
}
