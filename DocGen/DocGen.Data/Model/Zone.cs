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
        public double Color { get; set; }

        public override string ToString()
        {
            return $"Zone [Name: {Name}][Color: {Color}]";
        }
    }
}
