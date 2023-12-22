using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Data.Model
{
    public class DateTimeInterval
    {
        public Order Order { get; set; }
        public Location Location { get; set; }
        public Zone Zone { get; set; }

        public string Note { get; set; }
        public string Description { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int Days => (End - Start).Days;

        public override string ToString()
        {
            return $"DateTimeInterval [Start: {Start}][End: {End}][{Order}][{Location}][{Zone}][Note: {Note}][Description: {Description}]";
        }
    }
}
