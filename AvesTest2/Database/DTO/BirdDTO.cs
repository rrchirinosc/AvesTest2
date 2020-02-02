using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Database.DTO
{
    public class BirdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SciName { get; set; }
        public int FamilyId { get; set; }        
    }
}
