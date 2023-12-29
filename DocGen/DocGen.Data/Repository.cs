using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Office.Interop.Excel;

using DocGen.Data.Model;

namespace DocGen.Data
{
    public class Datastore
    {
        protected readonly Action<double> ProgressIncrementor = null;
        protected void IncrementProgressBy(double value) { ProgressIncrementor?.Invoke(value); }

        public bool IsLoaded { get; protected set; }
        public bool IsNormalized { get; set; }

        public readonly List<Person> Person;
        public readonly List<Location> Location;
        public readonly List<Zone> Zone;
        public readonly List<Order> Order;

        public Datastore(Action<double> progressIncrementor = null)
        {
            ProgressIncrementor = progressIncrementor;

            Person = new List<Person>();
            Location = new List<Location>();
            Zone = new List<Zone>();
            Order = new List<Order>();
        }

        public void Load(Workbook workbook, DateTime? startDate = null, DateTime? endDate = null)
        {
            LoadPerson(GetTable(workbook, "ОС", "PERSON"));
            LoadLocation(GetTable(workbook, "ЛОКАЦІЯ", "LOCATION"));
            LoadZone(GetTable(workbook, "ЛОКАЦІЯ", "ZONE"));
            LoadOrder(GetTable(workbook, "НАКАЗ", "ORDER"));
            LoadInactive(GetTable(workbook, "НЕЗАДІЯНІ", "INACTIVE"), startDate, endDate);
            LoadSector(GetTable(workbook, "СЕКТОР", "SECTOR"), startDate, endDate);
            IsLoaded = true;
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
                string name = TrimDataString(cells[0].Value);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var person = new Person
                    {
                        Name = name,
                        Rank = TrimDataString(cells[1].Value),
                        Company = TrimDataString(cells[2].Value),
                    };
                    Person.Add(person);
                    Console.WriteLine(person);
                }
                IncrementProgressBy(0.07);
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
                    CodeName = TrimDataString(cells[1].Value),
                    Color = cells[2].Interior.Color,
                    FontColor = cells[2].Font.ColorIndex,
                };
                if (string.IsNullOrWhiteSpace(location.Name))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(location.CodeName))
                {
                    var name = location.Name;
                    int length = ((name.Length < 5) ? name.Length : 5);
                    location.CodeName = name.Substring(0, length);
                }
                Location.Add(location);
                Console.WriteLine(location);
            }
        }
        public void LoadZone(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                string name = TrimDataString(cells[0].Value);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var zone = new Zone
                    {
                        Name = name,
                        Value = (cells[1].Value ?? 0),
                        Color = cells[2].Interior.Color,
                        FontColor = cells[2].Font.ColorIndex,
                    };
                    Zone.Add(zone);
                    Console.WriteLine(zone);
                }
            }
        }
        public void LoadOrder(Range table)
        {
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                string name = TrimDataString(cells[0].Value);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var order = new Order
                    {
                        Name = name,
                        Description = TrimDataString(cells[1].Value),
                        CodeName = TrimDataString(cells[2].Value),
                        Color = cells[3].Interior.Color,
                        FontColor = cells[3].Font.ColorIndex,
                    };
                    Order.Add(order);
                    Console.WriteLine(order);
                }
            }
        }

        public void LoadInactive(Range table, DateTime? startDate = null, DateTime? endDate = null)
        {
            Entity.ResetID(@base: 10000);
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();

                DateTimeInterval interval = ComposeInterval(cells[1].Value, cells[2].Value, startDate, endDate);
                if (interval == null)
                {
                    continue;
                }

                // NOTE: HAS TO BE CLARIFIED
                if (!endDate.HasValue || (endDate.Value != interval.EndDate))
                {
                    interval.EndDate = interval.EndDate.AddDays(-1);
                }
                // NOTE: HAS TO BE CLARIFIED

                interval.IsInactive = true;
                interval.Description = TrimDataString(cells[3].Value);

                var name = TrimDataString(cells[0].Value);
                var person = Person.FirstOrDefault(i => i.Name == name);
                if (person != null)
                {
                    person.Inactive.Add(interval);
                }
                Console.WriteLine($"{person} {interval}");
                IncrementProgressBy(0.03);
            }
        }
        public void LoadSector(Range table, DateTime? startDate = null, DateTime? endDate = null)
        {
            Entity.ResetID();
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();

                DateTimeInterval interval = ComposeInterval(cells[1].Value, cells[2].Value, startDate, endDate);
                if (interval == null)
                {
                    continue;
                }
                interval.Description = TrimDataString(cells[5].Value);
                interval.Note = TrimDataString(cells[6].Value);

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
                if (( personNames.Length == 1 )&&( personNames[0] == "ALL (КП)"))
                {
                    interval.IsGeneral = true;
                    foreach(var person in Person.Where(i => i.Company == "КП"))
                    {
                        person.Sector.Add(interval);
                    }
                }
                else if (( personNames.Length == 1 )&&( personNames[0] == "ALL (ТКП)"))
                {
                    interval.IsGeneral = true;
                    foreach (var person in Person.Where(i => i.Company == "ТКП"))
                    {
                        person.Sector.Add(interval);
                    }
                }
                else
                {
                    foreach (var personName in personNames)
                    {
                        var person = Person.FirstOrDefault(i => i.Name == personName.Trim());
                        if (person != null)
                        {
                            person.Sector.Add(interval);
                        }
                    }
                }
                IncrementProgressBy(0.05);
                Console.WriteLine($"{interval}");
            }
        }

        protected static DateTimeInterval ComposeInterval
            ( DateTime? readStartDate
            , DateTime? readEndDate
            , DateTime? startDate
            , DateTime? endDate
            ) {
            DateTime intervalStartDate;
            DateTime intervalEndDate;

            if (startDate.HasValue)
            {
                intervalStartDate =
                    !readStartDate.HasValue
                    ? startDate.Value
                    : (readStartDate.Value < startDate.Value)
                      ? startDate.Value
                      : readStartDate.Value;
            }
            else if (readStartDate.HasValue) { intervalStartDate = readStartDate.Value; }
            else return null;

            intervalEndDate =
                endDate.HasValue
                    ? !readEndDate.HasValue
                        ? endDate.Value
                        : (readEndDate.Value < endDate.Value)
                            ? readEndDate.Value.AddDays(1)
                            : endDate.Value
                    : !readEndDate.HasValue
                        ? DateTime.Today.AddDays(1)
                        : readEndDate.Value.AddDays(1);

            return new DateTimeInterval
            {
                StartDate = intervalStartDate,
                EndDate = intervalEndDate,
            };
        }

        public static string TrimDataString(string data)
        {
            return (string.IsNullOrWhiteSpace(data) ? string.Empty : data.Trim());
        }

        /*private static void UploadData(Workbook workbook)
        {
            Range table = GetTable(workbook, "НЕЗАДІЯНІ", "INACTIVE");
            Range firstRow = table.Rows.Cast<Range>().FirstOrDefault();
            if (firstRow != null)
            {
                firstRow.Insert(XlInsertShiftDirection.xlShiftDown);
                firstRow.Insert(XlInsertShiftDirection.xlShiftDown);

                var cells = GetTable(workbook, "НЕЗАДІЯНІ", "INACTIVE").Rows.Cast<Range>().First().Cells.Cast<Range>().ToList();

                cells[0].Value = "ЦИГАНОВА Людмила Федорівна";
                cells[1].Value = DateTime.Today;
                cells[2].Value = DateTime.Today.AddDays(1);
                cells[3].Value = "Відпустка FFFF";
            }
        }*/

        public void UpdateInactive(Workbook workbook)
        {
            Worksheet worksheet = workbook.Sheets["СТАТУС"];
            worksheet.Activate();

            int rowIndex = 2;
            Range cell;

            var records = new List<PersonStatusRecord>();
            do
            {
                var record = new PersonStatusRecord();

                cell = worksheet.Cells[rowIndex, 1];
                var name = TrimDataString(cell.Value);
                if (string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                record.PersonName = name;

                cell = worksheet.Cells[rowIndex, 2];
                var status = TrimDataString(cell.Value);
                if (status == "Заведений")
                {
                    record.PersonStatus = PersonStatus.Active;
                }
                else if (status == "Виведений")
                {
                    record.PersonStatus = PersonStatus.Inactive;
                }
                else break;

                cell = worksheet.Cells[rowIndex, 3];
                DateTime? startDate = cell.Value;
                if (!startDate.HasValue)
                {
                    break;
                }
                record.StartDate = startDate.Value;

                records.Add(record);
            }
            while (++rowIndex < 10000);
        }
    }
}
