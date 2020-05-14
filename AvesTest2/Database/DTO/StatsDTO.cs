using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Database.DTO
{
    public struct ImagesCount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Images { get; set; }
    }

    public class StatsDTO
    {
        public int BirdCount { get; set; }
        public int HaveKeyImages { get; set; }
        public List<ImagesCount> ImagesPerBird {get; set; }
    }
}
