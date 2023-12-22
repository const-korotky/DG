using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DocGen.Data;
using DocGen.Data.Model;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;


namespace DocGen.Processor
{
    public class ExcelProcessor
    {
        public event Action<int, string> ProgressUpdatedEvent;
        private void ProgressUpdated(double percentage, string message = null)
        {
            if (ProgressUpdatedEvent != null)
            {
                ProgressUpdatedEvent(Convert.ToInt32(Math.Round(percentage)), message);
            }
        }

        public string SourceDataFilePath { get; set; }
        public string DestinationFilePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_destinationFilePath))
                {
                    var dirPath = Path.GetDirectoryName(SourceDataFilePath);
                    _destinationFilePath = Path.Combine(dirPath, $"Excel_res{DateTime.Now.Ticks}.xlsx");
                }
                return _destinationFilePath;
            }
        }
        public string _destinationFilePath;

        public string SheetName { get; set; }

        public List<BR> BRs { get; private set; }
        public List<Entry> Entries { get; private set; }


        public void Process(bool populate = false)
        {
            /*var sheet = "жовтень";
            var range = "A2:AF124";
            var startDate = new DateTime(2023, 10, 1);*/
            //var sheet = "серпень";
            var range = "A2:AF92";
            //var startDate = new DateTime(2023, 8, 1);
            var startDate = GetStartDate(SheetName);

            var rangeBRs = "A95:A112";

            ProgressUpdated(5, "відкриття файлу даних...");
            ExcelApplication app = new ExcelApplication();
            Workbook workbook = app.Workbooks.Open(SourceDataFilePath);
            ProgressUpdated(8, "відкриття файлу даних завершено");

            ProgressUpdated(9, "початок зчитування беерок...");
            BRs = ExtractBRs(workbook, SheetName, rangeBRs);
            ProgressUpdated(19, "зчитування беерок завершено");

            ProgressUpdated(20, "початок зчитування даних по локаціям....");
            Entries = ExtractEntries(workbook, SheetName, range, startDate);
            ProgressUpdated(39, "зчитування даних по локаціям завершено");

            ProgressUpdated(40, "формування списку локацій....");
            Entries.ForEach(entry => entry.PopulateLocationMap());
            ProgressUpdated(44, "формування списку локацій заваршено");

            if (populate)
            {
                ProgressUpdated(45, "генерація проміжного звіту...");
                PopulateEntries(app, DestinationFilePath, SheetName, Entries);
                ProgressUpdated(67, "генерація проміжного звіту завершена");
            }

            workbook.Close(false);
            app.Quit();
            Marshal.FinalReleaseComObject(workbook);
            Marshal.FinalReleaseComObject(app);
            ProgressUpdated(68, "завантаження та обробку даних завершено");
        }



        public void ProcessDB()
        {
            ExcelApplication app = new ExcelApplication();
            Workbook workbook = app.Workbooks.Open(SourceDataFilePath);

            try
            {
                Datastore db = new Datastore();
                db.Load(workbook);
                Print(workbook, db);
                workbook.SaveAs(Filename: $"D:\\MSC\\DG\\examples.copy\\Excel_res{DateTime.Now.Ticks}.xlsm");
                workbook.Close(SaveChanges: false);
                app.Quit();
            }
            catch
            {
            }

            Marshal.FinalReleaseComObject(workbook);
            Marshal.FinalReleaseComObject(app);
        }
        public void Print(Workbook workbook, Datastore db)
        {
            Worksheet worksheet = workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
            worksheet.Name = "Звіт по локаціях";

            var rowIndex = 1;
            var colIndex = 1;
            Range cell;

            cell = worksheet.Cells[rowIndex, colIndex];
            cell.Value = "ФИО";
            cell.Font.Bold = true;
            cell.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var currDate = new DateTime(2023, 8, 1);
            var endDate = currDate.AddMonths(1);
            while (currDate < endDate)
            {
                cell = worksheet.Cells[rowIndex, ++colIndex];
                cell.Value = currDate.ToString(format: "d-MMM");
                currDate = currDate.AddDays(1);
            }

            foreach(var person in db.Person)
            {
                colIndex = 1;
                cell = worksheet.Cells[++rowIndex, colIndex];
                cell.Value = person.Name;
                if ((person.Name == "ALL (КП)") || person.Name == "ALL (ТКП)")
                {
                    cell.Font.Bold = true;
                    cell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    continue;
                }

                currDate = new DateTime(2023, 8, 1);
                while (currDate < endDate)
                {
                    cell = worksheet.Cells[rowIndex, ++colIndex];

                    var interval = person.Inactive.FirstOrDefault(i => (i.StartDate <= currDate) && (currDate <= i.EndDate));
                    if (interval != null)
                    {
                        cell.Interior.Pattern = XlPattern.xlPatternDown;
                    }
                    else
                    {
                        interval = GetSectorInterval(currDate, person);
                        if ((interval != null) && (interval.Location != null))
                        {
                            cell.Value = interval.Location.Name;
                            cell.Interior.Color = interval.Location.Color;
                            cell.Font.ColorIndex = interval.Location.FontColor;
                        }
                    }
                    currDate = currDate.AddDays(1);
                }
                Console.WriteLine(person);
            }

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
        }
        private static DateTimeInterval GetSectorInterval(DateTime date, Person person)
        {
            var intervals =
                person.Sector
                .Where(i => (i.StartDate <= date) && (date <= i.EndDate))
                .OrderBy(i => i.StartDate)
                .ThenBy(i => i.Zone?.Value)
                .ToList()
                ;
            return ((intervals.Count > 0) ? intervals.Last() : null);
        }



        public static List<BR> ExtractBRs
            ( Workbook workbook
            , string sheetName
            , string range
            ) {
            Worksheet worksheet = null;
            var brs = new List<BR>();
            try
            {
                worksheet = workbook.Sheets[sheetName];
                Range rows = worksheet.Range[range].Rows;

                foreach (Range row in rows)
                {
                    var br = new BR();
                    foreach (Range cell in row.Cells)
                    {
                        br.Text = (cell.Value as string);
                        br.ColorID = cell.Interior.Color;
                        brs.Add(br);

                        Console.WriteLine($"BR[ColorID: {br.ColorID}][Text: {br.Text}]");
                    }
                }
            }
            catch
            {
            }
            Marshal.FinalReleaseComObject(worksheet);
            return brs;
        }

        public List<Entry> ExtractEntries
            ( Workbook workbook
            , string sheetName
            , string range
            , DateTime start
            ) {
            Worksheet worksheet = null;
            var entries = new List<Entry>();
            try
            {
                worksheet = workbook.Sheets[sheetName];
                Range rows = worksheet.Range[range].Rows;

                double progressPercent = 20;
                foreach (Range row in rows)
                {
                    var entry = new Entry();
                    var currLoc = new Location();
                    currLoc.Interval.Start = start;
                    var dayCount = 1;

                    foreach (Range cell in row.Cells)
                    {
                        string name = (cell.Value as string);
                        double colorID = cell.Interior.Color;

                        if (1 == cell.Column)
                        {
                            entry.Name = name;
                            continue;
                        }
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            name = "<UNKNOWN>";
                        }
                        name = name.ToUpper();
                        if (2 == cell.Column)
                        {
                            currLoc.Name = name;
                            currLoc.Interval.ColorID  = colorID;
                            continue;
                        }
                        if ((currLoc.Name == name) && (currLoc.Interval.ColorID == colorID))
                        {
                            dayCount++;
                        }
                        else
                        {
                            currLoc.Interval.End = currLoc.Interval.Start.AddDays(dayCount);
                            entry.Locations.Add(currLoc);
                            currLoc = new Location(name, colorID, currLoc.Interval.End);
                            dayCount = 1;
                        }
                        Console.Write(name);
                    }
                    currLoc.Interval.End = currLoc.Interval.Start.AddDays(dayCount);
                    entry.Locations.Add(currLoc);
                    entries.Add(entry);
                    progressPercent += 0.2;
                    ProgressUpdated(progressPercent);
                }
            }
            catch
            {
            }
            Marshal.FinalReleaseComObject(worksheet);
            return entries.Where(entry => !string.IsNullOrWhiteSpace(entry.Name)).ToList();
        }

        public void PopulateEntries
            ( ExcelApplication app
            , string path
            , string sheetName
            , List<Entry> entries
            ) {
            Workbook workBook = null;
            Worksheet workSheet = null;
            var locations = Entry.MergeLocations(entries);

            try
            {
                workBook = app.Workbooks.Add();
                workSheet = workBook.Worksheets.get_Item(1);
                workSheet.Name = sheetName;

                var rowIndex = 1;
                var colIndex = 1;
                Range cell;

                cell = workSheet.Cells[rowIndex, colIndex];
                cell.Value = null;
                SetStyle_CornerCell(cell);

                foreach (var location in locations)
                {
                    cell = workSheet.Cells[rowIndex, ++colIndex];
                    cell.Value = location;
                    SetStyle_LocationCell(cell);
                }
                double progressPercent = 45;
                foreach (var entry in entries)
                {
                    colIndex = 1;
                    cell = workSheet.Cells[++rowIndex, colIndex];
                    cell.Value = entry.Name;
                    SetStyle_NameCell(cell);

                    foreach (var location in locations)
                    {
                        ++colIndex;
                        if (entry.LocationMap.ContainsKey(location))
                        {
                            string intervalsFormat = entry.FormatLocationIntervals(location, days: true, full: true);
                            workSheet.Cells[rowIndex, colIndex] = intervalsFormat;
                        }
                    }
                    progressPercent += 0.2;
                    ProgressUpdated(progressPercent);
                }
                workSheet.Columns.AutoFit();
                workSheet.Rows.AutoFit();

                workSheet.Application.ActiveWindow.SplitRow = 1;
                workSheet.Application.ActiveWindow.SplitColumn = 1;
                workSheet.Application.ActiveWindow.FreezePanes = true;

                workBook.SaveAs(path, XlFileFormat.xlWorkbookDefault);
                workBook.Close(true);
            }
            catch
            {
            }
            Marshal.ReleaseComObject(workSheet);
            Marshal.ReleaseComObject(workBook);
        }

        private static void SetStyle_NameCell(Range cell)
        {
            cell.Font.Bold = true;
            cell.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            cell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
            cell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            cell.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            cell.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
            cell.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
        }
        private static void SetStyle_LocationCell(Range cell)
        {
            cell.Font.Bold = true;
            cell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            cell.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);
            cell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            cell.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            cell.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
            cell.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
        }
        private static void SetStyle_CornerCell(Range cell)
        {
            cell.Interior.Color = ColorTranslator.ToOle(Color.White);
            cell.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlContinuous;
            cell.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            cell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            cell.Borders[XlBordersIndex.xlDiagonalDown].Weight = XlBorderWeight.xlMedium;
            cell.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
            cell.Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
        }

        #region Raw Entries Read/Write

        public static List<List<object>> ReadEntries(ExcelApplication app, string path, string sheetName, string range)
        {
            Workbook workbook = app.Workbooks.Open(path);
            Worksheet worksheet = workbook.Sheets[sheetName];
            Range rows = worksheet.Range[range].Rows;

            List<List<object>> entries = new List<List<object>>();

            try
            {
                foreach (Range row in rows)
                {
                    var entry = new List<object>();
                    foreach (Range cell in row.Cells)
                    {
                        object value = cell.Value;
                        entry.Add(value);
                        Console.Write(value);
                    }
                    Console.WriteLine();
                    entries.Add(entry);
                }
            }
            catch { }

            workbook.Close(false);
            Marshal.FinalReleaseComObject(worksheet);
            Marshal.FinalReleaseComObject(workbook);

            return entries;
        }
        public static void WriteEntries(ExcelApplication app, string path, string sheetName, List<List<object>> entries)
        {
            Workbook workBook = app.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet workSheet = workBook.Worksheets.get_Item(1);
            workSheet.Name = sheetName;

            for (var rowIndex = 0; rowIndex < entries.Count; rowIndex++)
            {
                var cells = entries[rowIndex];
                for (var colIndex = 0; colIndex < cells.Count; colIndex++)
                {
                    workSheet.Cells[rowIndex+1, colIndex+1] = cells[colIndex];
                }
            }

            workBook.SaveAs(path,
                            XlFileFormat.xlOpenXMLWorkbook,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            XlSaveAsAccessMode.xlExclusive,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value);

            workBook.Close(true);
            Marshal.ReleaseComObject(workSheet);
            Marshal.ReleaseComObject(workBook);
        }

        #endregion Raw Entries Read/Write

        #region DateTime Processing

        private static DateTime GetStartDate(string monthName)
        {
            int month = 0;
            switch (monthName.ToUpper())
            {
                case "СІЧЕНЬ":   { month = 1; break;  }
                case "ЛЮТИЙ":    { month = 2; break;  }
                case "БЕРЕЗЕНЬ": { month = 3; break;  }
                case "КВІТЕНЬ":  { month = 4; break;  }
                case "ТРАВЕНЬ":  { month = 5; break;  }
                case "ЧЕРВЕНЬ":  { month = 6; break;  }
                case "ЛИПЕНЬ":   { month = 7; break;  }
                case "СЕРПЕНЬ":  { month = 8; break;  }
                case "ВЕРЕСЕНЬ": { month = 9; break;  }
                case "ЖОВТЕНЬ":  { month = 10; break; }
                case "ЛИСТОПАД": { month = 11; break; }
                case "ГРУДЕНЬ":  { month = 12; break; }
                default: break;
            }
            return new DateTime(DateTime.Today.Year, month, 1);
        }

        #endregion DateTime Processing
    }
}
