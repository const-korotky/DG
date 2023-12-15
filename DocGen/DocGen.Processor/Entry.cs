using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocGen.Processor
{
    public class Entry
    {
        public string Name { get; set; }

        private List<Location> _locations;
        public List<Location> Locations => _locations;

        private Dictionary<string, List<DateInterval>> _locationMap;
        public Dictionary<string, List<DateInterval>> LocationMap => _locationMap;

        public Entry() { _locations = new List<Location>(); }

        public void PopulateLocationMap()
        {
            _locationMap = new Dictionary<string, List<DateInterval>>();

            foreach (var location in _locations)
            {
                if (!_locationMap.ContainsKey(location.Name))
                {
                    _locationMap.Add(location.Name, new List<DateInterval>());
                }
                _locationMap[location.Name].Add(location.Interval);
            }
        }
    }

    public class Location
    {
        public string Name { get; set; }

        private DateInterval _interval;
        public DateInterval Interval => _interval;

        public Location()
        {
            _interval = new DateInterval();
        }
        public Location(string name, DateTime start) : this()
        {
            Name = name;
            Interval.Start = start;
        }
    }

    public class DateInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int Days => (End - Start).Days;
    }
}
