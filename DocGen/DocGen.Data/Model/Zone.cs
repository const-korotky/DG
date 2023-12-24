using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Zone : ColorfulEntity
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public Zone() : base() { }

        public override string ToString()
        {
            return $"Zone [Name: {Name}][Color: {Color}][FontColor: {FontColor}]";
        }
    }
}
