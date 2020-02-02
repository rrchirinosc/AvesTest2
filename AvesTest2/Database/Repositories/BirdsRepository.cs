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
    }
}
