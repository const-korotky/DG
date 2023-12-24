using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Location : ColorfulEntity
    {
        public string Name { get; set; }
        public string CodeName { get; set; }

        public Location() : base() { }

        public override string ToString()
        {
            return $"Location [Name: {Name}][Color: {Color}][FontColor: {FontColor}]";
        }
    }
}
