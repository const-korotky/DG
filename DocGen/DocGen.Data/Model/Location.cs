using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Location : Entity
    {
        public string Name { get; set; }

        public double Color { get; set; }
        public double FontColor { get; set; }

        public Location() : base() { }

        public override string ToString()
        {
            return $"Location [Name: {Name}][Color: {Color}][FontColor: {FontColor}]";
        }
    }
}
