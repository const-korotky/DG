using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace DocGen.Data.Model
{
    public class SectorItem
    {
        public Order Order { get; set; }
        public Location Location { get; set; }
        public Zone Zone { get; set; }

        public string Note { get; set; }
        public string Description { get; set; }

        public bool IsInactive { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<Person> Persons { get; }

        public SectorItem()
        {
            Persons = new List<Person>();
        }

        public override string ToString()
        {
            return string.Empty;
            //return $"Location [Name: {Name}][Color: {Color}][FontColor: {FontColor}]";
        }
    }
}
