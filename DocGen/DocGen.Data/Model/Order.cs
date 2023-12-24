using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class Order : ColorfulEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CodeName { get; set; }

        public Order() : base() { }

        public override string ToString()
        {
            return $"Order [Name: {Name}][Description: {Description}][Color: {Color}][FontColor: {FontColor}]";
        }
    }
}
