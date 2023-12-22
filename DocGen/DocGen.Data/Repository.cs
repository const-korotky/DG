using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DocGen.Data.Model;
using Microsoft.Office.Interop.Excel;

namespace DocGen.Data
{
    public class Datastore
    {
        public readonly List<Person> Person;
        public readonly List<Location> Location;
        public readonly List<Zone> Zone;
        public readonly List<Order> Order;

        public Datastore()
        {
            Person = new List<Person>();
            Location = new List<Location>();
            Zone = new List<Zone>();
            Order = new List<Order>();
        }

        public void Load(Workbook workbook)
        {
            LoadPerson(GetTable(workbook, "ОС", "PERSON"));
            LoadLocation(GetTable(workbook, "ЛОКАЦІЯ", "LOCATION"));
            LoadZone(GetTable(workbook, "ЛОКАЦІЯ", "ZONE"));
            LoadOrder(GetTable(workbook, "НАКАЗ", "ORDER"));
            LoadAbsent(GetTable(workbook, "НЕ В СЕКТОРІ", "ABSENT"));
            LoadSector(GetTable(workbook, "СЕКТОР", "SEKTOR"));
        }

        public static Range GetTable(Workbook workbook, string schemeName, string tableName)
        {
            return workbook.Sheets[schemeName].Range[tableName];
        }

        public void LoadPerson(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                var person = new Person
                {
                    Name = TrimDataString(cells[0].Value),
                    Rank = TrimDataString(cells[1].Value),
                    Company = TrimDataString(cells[2].Value),
                };
                Person.Add(person);
                Console.WriteLine(person);
            }
        }
        public void LoadLocation(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                var location = new Location
                {
                    Name = TrimDataString(cells[0].Value),
                    Color = cells[1].Interior.Color,
                };
                Location.Add(location);
                Console.WriteLine(location);
            }
        }
        public void LoadZone(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                var zone = new Zone
                {
                    Name = TrimDataString(cells[0].Value),
                    Color = cells[1].Interior.Color,
                };
                Zone.Add(zone);
                Console.WriteLine(zone);
            }
        }
        public void LoadOrder(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                var order = new Order
                {
                    Name = TrimDataString(cells[0].Value),
                    Description = TrimDataString(cells[1].Value),
                    Color = cells[2].Interior.Color,
                };
                Order.Add(order);
                Console.WriteLine(order);
            }
        }

        public void LoadAbsent(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();

                DateTime? start = cells[1].Value;
                if (!start.HasValue)
                {
                    continue;
                }
                DateTime? end = cells[2].Value;
                if (!end.HasValue)
                {
                    end = DateTime.Today;
                }
                var interval = new DateTimeInterval
                {
                    Start = start.Value,
                    End = end.Value,
                };
                interval.End.AddDays(1);

                var name = TrimDataString(cells[0].Value);
                var person = Person.FirstOrDefault(i => i.Name == name);
                if (person != null)
                {
                    person.Absent.Add(interval);
                }
                Console.WriteLine($"{person} {interval}");
            }
        }
        public void LoadSector(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();

                DateTime? start = cells[1].Value;
                if (!start.HasValue)
                {
                    continue;
                }
                DateTime? end = cells[2].Value;
                if (!end.HasValue)
                {
                    end = DateTime.Today;
                }
                var interval = new DateTimeInterval
                {
                    Start = start.Value,
                    End = end.Value,
                    Note = cells[5].Value,
                    Description = cells[6].Value,
                };
                interval.End.AddDays(1);

                var orderName = TrimDataString(cells[0].Value);
                var order = Order.FirstOrDefault(i => i.Name == orderName);
                if (order != null)
                {
                    interval.Order = order;
                }

                var locationName = TrimDataString(cells[4].Value);
                var location = Location.FirstOrDefault(i => i.Name == locationName);
                if (location != null)
                {
                    interval.Location = location;
                }

                var zoneName = TrimDataString(cells[7].Value);
                var zone = Zone.FirstOrDefault(i => i.Name == zoneName);
                if (zone != null)
                {
                    interval.Zone = zone;
                }

                var personNames = (TrimDataString(cells[3].Value) as string).Split(',');
                foreach(var personName in personNames)
                {
                    var person = Person.FirstOrDefault(i => i.Name == personName.Trim());
                    if (person != null)
                    {
                        person.Sector.Add(interval);
                    }
                }

                Console.WriteLine($"{interval}");
            }
        }

        public static string TrimDataString(string data)
        {
            return string.IsNullOrEmpty(data) ? string.Empty : data.Trim();
        }
    }
}
