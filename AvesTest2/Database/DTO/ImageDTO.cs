using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Database.DTO
{
    public class ImageDTO
    {
        public long Id { get; set; }
        public int BirdId { get; set; }
        public string FileName { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public int Country { get; set; }
        public string Coordinate { get; set; } /* comma separated latitude and longitud */
        public bool KeyImage { get; set; }
        public string Comment { get; set; } /* Image comment, may be empty */
    }
}
