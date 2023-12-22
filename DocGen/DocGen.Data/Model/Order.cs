using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Order
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double Color { get; set; }
        public double FontColor { get; set; }

        public override string ToString()
        {
            return $"Order [Name: {Name}][Description: {Description}][Color: {Color}][FontColor: {FontColor}]";
        }
    }
}
