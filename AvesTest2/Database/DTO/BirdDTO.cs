using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvesTest2.Database.DTO
{
    public class BirdDTO
    {
        private BirdDTO bird;
        
        public BirdDTO() { }

        public BirdDTO(int id, string name, string sciName, int familyId)
        {
            this.Id = id;
            this.Name = name;
            this.SciName = sciName;
            this.FamilyId = familyId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SciName { get; set; }
        public int FamilyId { get; set; }        
    }
}
