using AvesTest2.Database.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace AvesTest2.Database.Repositories
{
    public class BirdsRepository
    {
        private IDbConnection Connection { get; set; }
        private const string ConnectionString = "Server=.\\SqlExpress;Database=Aves;Trusted_Connection=Yes";

        public BirdsRepository()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

        public IEnumerable<BirdDTO> Birds
        {
            get
            {
                string sql = "SELECT [Bird].Id, [Bird].Name, [Bird].SciName, [Bird].FamilyId" +
                                                " FROM [Bird] ORDER BY [Name]";
                return Connection.Query<BirdDTO>(sql);
            }
        }


        public IEnumerable<FamilyDTO> Families
        {
            get
            {
                string sql = "SELECT [Family].Id, [Family].Name, [Family].SciName" +
                                                " FROM [Family] ORDER BY [SciName]";
                return Connection.Query<FamilyDTO>(sql);
            }
        }

       
        public int AddBird(BirdDTO Bird)
        {
            string sql = "INSERT INTO Bird (Name, SciName, FamilyId)" +
                    " Values (@Name, @SciName, @FamilyId)";

            int rows = Connection.Execute(sql, new { Bird.Name, Bird.SciName, Bird.FamilyId });

            return rows;
        }

        public int AddImage(ImageDTO Image)
        {
            string sql = "INSERT INTO Image (BirdId, FileName, Location, Date, Country, Coordinate, KeyImage)" +
                    " Values (@BirdId, @FileName, @Location, @Date, @Country, @Coordinate, @KeyImage)";

            int rows = Connection.Execute(sql, new { Image.BirdId, Image.FileName, Image.Location, Image.Date, Image.Country, Image.Coordinate, Image.KeyImage});

            return rows;
        }
    }
}
