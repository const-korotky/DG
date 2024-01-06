using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using DocGen.Data.Model;

namespace DocGen.Data
{
    public class Datastore
    {
        #region Properties and Fields

        protected readonly Action<double> ProgressIncrementor = null;
        protected void IncrementProgressBy(double value) { ProgressIncrementor?.Invoke(value); }

        public bool IsLoaded { get; protected set; }
        public bool IsNormalized { get; set; }

        protected readonly List<PersonStatusRecord> PersonStatusRecords;
        public readonly List<SectorItem> SectorItems;

        public readonly List<Person> Person;
        public readonly List<Location> Location;
        public readonly List<Zone> Zone;
        public readonly List<Order> Order;

        #endregion Properties and Fields

        public Datastore(Action<double> progressIncrementor = null)
        {
            ProgressIncrementor = progressIncrementor;

            Person = new List<Person>();
            Location = new List<Location>();
            Zone = new List<Zone>();
            Order = new List<Order>();

            PersonStatusRecords = new List<PersonStatusRecord>();
            SectorItems = new List<SectorItem>();
        }

        #region Load

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
        public void Reload(Workbook workbook, DateTime? startDate = null, DateTime? endDate = null)
        {
            SectorItems.Clear();

            Person.Clear();
            Location.Clear();
            Zone.Clear();
            Order.Clear();

            IsNormalized = false;
            IsLoaded = false;

            Load(workbook, startDate, endDate);
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
                    location.CodeName = FormatLocationCodeName(location.Name);
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

                SectorItem sectorItem = new SectorItem();
                sectorItem.StartDate = cells[1].Value;
                sectorItem.EndDate = cells[2].Value;

                DateTimeInterval interval = ComposeInterval(cells[1].Value, cells[2].Value, startDate, endDate);
                if (interval == null)
                {
                    continue;
                }
                interval.Description = TrimDataString(cells[5].Value);
                interval.Note = TrimDataString(cells[6].Value);

                sectorItem.Description = interval.Description;
                sectorItem.Note = interval.Note;

                var orderName = TrimDataString(cells[0].Value);
                var order = Order.FirstOrDefault(i => i.Name == orderName);
                if (order != null)
                {
                    interval.Order = order;
                }
                sectorItem.OrderName = orderName;

                var locationName = TrimDataString(cells[4].Value);
                var location = Location.FirstOrDefault(i => i.Name == locationName);
                if (location != null)
                {
                    interval.Location = location;
                }
                sectorItem.LocationName = locationName;

                var zoneName = TrimDataString(cells[7].Value);
                var zone = Zone.FirstOrDefault(i => i.Name == zoneName);
                if (zone != null)
                {
                    interval.Zone = zone;
                }
                sectorItem.ZoneName = zoneName;

                var persons = TrimDataString(cells[3].Value);
                sectorItem.Persons = persons;
                var personNames = (persons as string).Split(',');
                if (( personNames.Length == 1 )&&( personNames[0] == "ALL (КП)"))
                {
                    interval.IsGeneral = true;
                    foreach(var person in Person.Where(i => i.Company == "КП"))
                    {
                        person.Sector.Add(interval);
                    }

                    var allPerson = Person.FirstOrDefault(i => i.Name == "ALL (КП)");
                    if (allPerson != null)
                    {
                        allPerson.Sector.Add(interval);
                    }
                }
                else if (( personNames.Length == 1 )&&( personNames[0] == "ALL (ТКП)"))
                {
                    interval.IsGeneral = true;
                    foreach (var person in Person.Where(i => i.Company == "ТКП"))
                    {
                        person.Sector.Add(interval);
                    }

                    var allPerson = Person.FirstOrDefault(i => i.Name == "ALL (ТКП)");
                    if (allPerson != null)
                    {
                        allPerson.Sector.Add(interval);
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

                SectorItems.Add(sectorItem);
            }
        }

        #endregion Load

        #region Utility Methods

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
        public static DateTime? GetNullableDateTime(DateTime data, DateTime check)
        {
            if (data == check) {
                return null;
            } return data;
        }

        private static string FormatLocationCodeName(string locationName)
        {
            int length = ((locationName.Length < 5) ? locationName.Length : 5);
            var locationCodeName = locationName.Substring(0, length);
            return locationCodeName;
        }

        #endregion Utility Methods

        #region PersonStatusRecords

        public void LoadPersonStatusRecords(Workbook workbook)
        {
            Worksheet worksheet = workbook.Sheets["СТАТУС"];

            int emptyRowCount = 0;
            int rowIndex = 2;
            Range cell;

            do
            {
                var record = new PersonStatusRecord();

                cell = worksheet.Cells[rowIndex, 1];
                var name = TrimDataString(cell.Value);
                if (string.IsNullOrWhiteSpace(name))
                {
                    ++emptyRowCount;
                    ++rowIndex;
                    continue;
                }
                else
                {
                    emptyRowCount = 0;
                }
                record.PersonName = name;

                cell = worksheet.Cells[rowIndex, 2];
                var status = TrimDataString(cell.Value);
                if (status == "Заведений")
                {
                    record.PersonStatus = PersonStatus.Attached;
                }
                else if (status == "Виведений")
                {
                    record.PersonStatus = PersonStatus.Detached;
                }

                cell = worksheet.Cells[rowIndex, 3];
                try
                {
                    DateTime? startDate = cell.Value;
                    record.StartDate = startDate ?? DateTime.Today;
                }
                catch
                {
                    try
                    {
                        string value = TrimDataString(cell.Value);
                        var startDate = DateTime.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        record.StartDate = startDate;
                    }
                    catch
                    {
                        ++rowIndex;
                        continue;
                    }
                }

                cell = worksheet.Cells[rowIndex, 4];
                record.Note = TrimDataString(cell.Value);

                PersonStatusRecords.Add(record);
                ++rowIndex;
            }
            while (emptyRowCount < 4);
        }

        public void PopulateInactiveIntervals(Workbook workbook)
        {
            GroupRecordsByPerson
                ( out Dictionary<string, List<DateTimeInterval>> inactiveIntervals
                , out Dictionary<string, List<PersonStatusRecord>> recordsDictionary
                );
            ComposePersonInactiveIntervals
                ( recordsDictionary
                , inactiveIntervals
                );
            PopulatePersonInactiveIntervals(workbook, inactiveIntervals);
            PersonStatusRecords.Clear();
        }

        private void PopulatePersonInactiveIntervals
            ( Workbook workbook
            , Dictionary<string, List<DateTimeInterval>> inactiveIntervals
            ) {
            Range row = GetTable(workbook, "НЕЗАДІЯНІ", "INACTIVE").Rows.Cast<Range>().FirstOrDefault();
            if (row == null)
            {
                return;
            }
            foreach (var entry in inactiveIntervals)
            {
                var personName = entry.Key;

                foreach (var interval in entry.Value)
                {
                    var cells = row.Cells.Cast<Range>().ToList();

                    cells[0].Value = personName;
                    cells[1].Value = GetNullableDateTime(interval.StartDate, DateTime.MinValue);
                    cells[2].Value = GetNullableDateTime(interval.EndDate, DateTime.MaxValue);
                    cells[3].Value = interval.Note;

                    row.Insert(XlInsertShiftDirection.xlShiftDown);
                    row = GetTable(workbook, "НЕЗАДІЯНІ", "INACTIVE").Rows.Cast<Range>().First();
                }
            }
        }

        private static void ComposePersonInactiveIntervals
            ( Dictionary<string, List<PersonStatusRecord>> recordsDictionary
            , Dictionary<string, List<DateTimeInterval>> inactiveIntervals
            ) {
            foreach (var entry in recordsDictionary)
            {
                var records = entry.Value.OrderBy(i => i.StartDate).ThenBy(i => i.PersonStatus).ToList();
                foreach (var record in records)
                {
                    if (record.PersonStatus == PersonStatus.Attached)
                    {
                        var intervals = inactiveIntervals[record.PersonName];
                        if (intervals.Count < 1)
                        {
                            intervals.Add(
                                new DateTimeInterval()
                                {
                                    StartDate = DateTime.MinValue,
                                    EndDate = record.StartDate.AddDays(-1),
                                });
                        }
                        else
                        {
                            intervals.Last().EndDate = record.StartDate.AddDays(-1);
                        }
                    }
                    else if (record.PersonStatus == PersonStatus.Detached)
                    {
                        var intervals = inactiveIntervals[record.PersonName];
                        intervals.Add(
                            new DateTimeInterval()
                            {
                                Note = record.Note,
                                StartDate = record.StartDate,
                                EndDate = DateTime.MaxValue
                            });
                    }
                }
            }
        }

        private void GroupRecordsByPerson
            ( out Dictionary<string, List<DateTimeInterval>> inactiveIntervals
            , out Dictionary<string, List<PersonStatusRecord>> dictionary
            ) {
            inactiveIntervals = new Dictionary<string, List<DateTimeInterval>>();
            dictionary = new Dictionary<string, List<PersonStatusRecord>>();
            foreach (var record in PersonStatusRecords)
            {
                if (!dictionary.ContainsKey(record.PersonName))
                {
                    dictionary.Add(record.PersonName, new List<PersonStatusRecord>() { record });
                    inactiveIntervals.Add(record.PersonName, new List<DateTimeInterval>());
                }
                else
                {
                    dictionary[record.PersonName].Add(record);
                }
            }
        }

        #endregion PersonStatusRecords

        #region Save

        public void Save(Workbook workbook)
        {
            SaveSectorItems(workbook);
            SaveLocations(workbook);
            SaveOrders(workbook);
        }

        private void SaveSectorItems(Workbook workbook)
        {
            Range table = GetTable(workbook, "СЕКТОР", "SECTOR");
            table.Rows.Delete();

            Range row = GetTable(workbook, "СЕКТОР", "SECTOR").Rows.Cast<Range>().FirstOrDefault();
            if (row == null)
            {
                return;
            }
            var items = (SectorItems as IEnumerable<SectorItem>).Reverse();
            foreach (var item in items)
            {
                var cells = row.Cells.Cast<Range>().ToList();

                cells[0].Value = item.OrderName;
                cells[1].Value = item.StartDate;
                cells[2].Value = item.EndDate;
                cells[3].Value = item.Persons;
                cells[4].Value = item.LocationName;
                cells[5].Value = item.Description;
                cells[6].Value = item.Note;
                cells[7].Value = item.ZoneName;

                row.Insert(XlInsertShiftDirection.xlShiftDown);
                row = GetTable(workbook, "СЕКТОР", "SECTOR").Rows.Cast<Range>().First();
            }

            row = GetTable(workbook, "СЕКТОР", "SECTOR").Rows.Cast<Range>().FirstOrDefault();
            if (row != null)
            {
                row.Delete();
            }
        }
        private void SaveLocations(Workbook workbook)
        {
            var newLocations = Location.Where(i => i.IsNew).ToList();
            var newLocationsCount = newLocations.Count;
            if (newLocationsCount < 1)
            {
                return;
            }
            var index = 0;

            Range table = GetTable(workbook, "ЛОКАЦІЯ", "LOCATION");
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                var locationName = TrimDataString(cells[0].Value);
                if (!string.IsNullOrWhiteSpace(locationName))
                {
                    continue;
                }
                var newLocationName = newLocations[index].Name;
                cells[0].Value = newLocationName;
                cells[1].Value = FormatLocationCodeName(newLocationName);
                if (++index == newLocationsCount)
                {
                    break;
                }
            }
            while (index < newLocationsCount)
            {
                Range row = GetTable(workbook, "ЛОКАЦІЯ", "LOCATION").Rows.Cast<Range>().First();
                row.Insert(XlInsertShiftDirection.xlShiftDown);
                row = GetTable(workbook, "ЛОКАЦІЯ", "LOCATION").Rows.Cast<Range>().First();

                var cells = row.Cells.Cast<Range>().ToList();

                var newLocationName = newLocations[index].Name;
                cells[0].Value = newLocationName;
                cells[1].Value = FormatLocationCodeName(newLocationName);

                ++index;
            }
        }
        private void SaveOrders(Workbook workbook)
        {
            var newOrders = Order.Where(i => i.IsNew).ToList();
            var newOrdersCount = newOrders.Count;
            if (newOrdersCount < 1)
            {
                return;
            }
            var index = 0;

            Range table = GetTable(workbook, "НАКАЗ", "ORDER");
            foreach (Range row in table.Rows)
            {
                var cells = row.Cells.Cast<Range>().ToList();
                var orderName = TrimDataString(cells[0].Value);
                if (!string.IsNullOrWhiteSpace(orderName))
                {
                    continue;
                }
                cells[0].Value = newOrders[index].Name;
                cells[1].Value = newOrders[index].Description;
                cells[2].Value = newOrders[index].CodeName;
                if (++index == newOrdersCount)
                {
                    break;
                }
            }
            while (index < newOrdersCount)
            {
                Range row = GetTable(workbook, "НАКАЗ", "ORDER").Rows.Cast<Range>().First();
                row.Insert(XlInsertShiftDirection.xlShiftDown);
                row = GetTable(workbook, "НАКАЗ", "ORDER").Rows.Cast<Range>().First();

                var cells = row.Cells.Cast<Range>().ToList();

                cells[0].Value = newOrders[index].Name;
                cells[1].Value = newOrders[index].Description;
                cells[2].Value = newOrders[index].CodeName;

                ++index;
            }
        }

        #endregion Save
    }
}
