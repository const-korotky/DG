using System;
using System.Drawing;
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

        private readonly List<Location> _locations;
        public List<Location> Locations => _locations;

        private Dictionary<string, List<DateInterval>> _locationMap;
        public Dictionary<string, List<DateInterval>> LocationMap => _locationMap;

        public IEnumerable<KeyValuePair<string, List<DateInterval>>>
            ValidLocations => _locationMap.Where(i => !string.IsNullOrWhiteSpace(i.Key) && i.Key != "<UNKNOWN>");

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
        public static List<string> MergeLocations(List<Entry> entries)
        {
            var locations = new List<string>();
            foreach (var entry in entries)
            {
                foreach (var location in entry.LocationMap.Keys)
                {
                    if (!locations.Contains(location))
                    {
                        locations.Add(location);
                    }
                }
            }
            return locations;
        }

        #region Format BRs

        public string FormatLocationBRs(string location, IEnumerable<BR> brs)
        {
            return FormatBRs(_locationMap[location], brs);
        }
        public string FormatTotalBRs(IEnumerable<BR> brs)
        {
            return FormatBRs(ValidLocations.SelectMany(i => i.Value), brs);
        }
        public static string FormatBRs
            ( IEnumerable<DateInterval> intervals
            , IEnumerable<BR> brs
            ) {
            var resBRs = new List<BR>();

            foreach (var interval in intervals)
            {
                var br = brs.FirstOrDefault(i => i.ColorID == interval.ColorID);
                if ((br != null) && !resBRs.Exists(i => i.ColorID == br.ColorID))
                {
                    resBRs.Add(br);
                }
            }
            return string.Join($",{Environment.NewLine}", resBRs.Select(i => i.Text)).Trim();
        }

        #endregion Format BRs

        #region Format Intervals

        public string FormatLocationIntervals
            ( string location
            , bool days = false
            , bool full = true
            ) {
            return FormatIntervals(_locationMap[location], days, full);
        }
        public string FormatTotalIntervals
            ( bool days = false
            , bool full = true
            ) {
            return FormatIntervals(ValidLocations.SelectMany(i => i.Value), days, full);
        }
        public static string FormatIntervals
            ( IEnumerable<DateInterval> intervals
            , bool days = false
            , bool full = true
            ) {
            string res = string.Empty;
            int intervalDays;

            foreach (var interval in intervals)
            {
                intervalDays = interval.Days;
                if (full)
                {
                    res += $"{interval.Start:dd/MM/yyyy} - {interval.End.AddDays(-1):dd/MM/yyyy}";
                }
                else
                {
                    if (intervalDays < 2)
                    {
                        res += $"{interval.Start:dd.MM.yyyy}";
                    }
                    else
                    {
                        res += $"{interval.Start:dd}-{interval.End.AddDays(-1):dd.MM.yyyy}";
                    }
                }
                if (days)
                {
                    res += $" ({intervalDays} днів)";
                }
                res += Environment.NewLine;
            }
            return res.TrimEnd();
        }

        #endregion Format Intervals

        public int GetDaysOnLocation(string location)
        {
            return _locationMap.ContainsKey(location) ? _locationMap[location].Sum(i => i.Days) : 0;
        }
        public int GetDaysTotal()
        {
            return ValidLocations.SelectMany(i => i.Value).Sum(interval => interval.Days);
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
        public Location(string name, double colorID, DateTime start) : this()
        {
            Name = name;
            Interval.ColorID = colorID;
            Interval.Start = start;
        }
    }

    public class DateInterval
    {
        public double ColorID { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int Days => (End - Start).Days;
    }

    public class BR
    {
        public double ColorID { get; set; }
        public string Text { get; set; }
    }
}
