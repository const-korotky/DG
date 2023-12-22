using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Zone
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public double Color { get; set; }
        public double FontColor { get; set; }

        public override string ToString()
        {
            return $"Zone [Name: {Name}][Color: {Color}][FontColor: {FontColor}]";
        }
    }
}
