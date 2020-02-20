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
                                                " FROM [Bird] ORDER BY [Id]";
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

        public int AddBird(BirdDTO NewBird) 
        {
            string sql = "INSERT INTO Bird (Id, Name, SciName, FamilyId)" +
                    " Values (@Id, @Name, @SciName, @FamilyId)";

            BirdDTO bird = new BirdDTO(NewBird.Id, NewBird.Name, NewBird.SciName, NewBird.FamilyId);

            int rows = Connection.Execute(sql, bird);

            return rows;
        }
    }
}
