using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Database.DTO
{
    public class BirdFullDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SciName { get; set; }
        public string FileName { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public int Country { get; set; }
        public string Coordinate { get; set; }
        public string Comment { get; set; }
    }
}
