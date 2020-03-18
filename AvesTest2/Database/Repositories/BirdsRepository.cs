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

        public IEnumerable<BirdDTO> Birds
        {
            get
            {
                string sql = "SELECT [Bird].Id, [Bird].Name, [Bird].SciName, [Bird].FamilyId" +
                                                " FROM [Bird] ORDER BY [Name]";
                return _connection.Query<BirdDTO>(sql);
            }
        }


        public IEnumerable<FamilyDTO> Families
        {
            get
            {
                string sql = "SELECT [Family].Id, [Family].Name, [Family].SciName" +
                                                " FROM [Family] ORDER BY [SciName]";
                return _connection.Query<FamilyDTO>(sql);
            }
        }

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

        public IEnumerable<string> GetImages(int birdId)
        {
            string sql = string.Format("SELECT [Image].FileName" +
                                            " FROM [Image]" +
                                            " WHERE [BirdId] = {0}", birdId);
            return _connection.Query<string>(sql);
        }


        public int AddBird(BirdDTO Bird)
        {
            string sql = "INSERT INTO Bird (Name, SciName, FamilyId)" +
                    " Values (@Name, @SciName, @FamilyId)";

            int rows = _connection.Execute(sql, new { Bird.Name, Bird.SciName, Bird.FamilyId });

            return rows;
        }

        public int AddImage(ImageDTO Image)
        {
            string sql = "INSERT INTO Image (BirdId, FileName, Location, Date, Country, Coordinate, KeyImage)" +
                    " Values (@BirdId, @FileName, @Location, @Date, @Country, @Coordinate, @KeyImage)";

            int rows = _connection.Execute(sql, new { Image.BirdId, Image.FileName, Image.Location, Image.Date, Image.Country, Image.Coordinate, Image.KeyImage});

            return rows;
        }
    }
}
