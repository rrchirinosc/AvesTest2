using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace AvesTest2.Database.DTO
{
    public class FamilyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SciName { get; set; }
        [Computed]
        public bool Available { get; set; } = false;
    }
}
