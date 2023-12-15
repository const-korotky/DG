using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace DocGen.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = @"D:\MSC\DG\examples.copy\Передок жовтень 2023.xlsx";
            var sheet = "жовтень";
            var range = "A2:AF124";

            Application app = new Application();

            var entries = ExtractEntries(app, path, sheet, range);
            PopulateEntries(app, entries, sheet);

            /*var entries = ReadEntries(app, path, sheet, range);
            WriteEntries(app, entries, sheet);*/

            app.Quit();

            Marshal.FinalReleaseComObject(app);
        }

        private static List<Entry> ExtractEntries(Application app, string path, string sheetName, string range)
        {
            Workbook workbook = app.Workbooks.Open(path);
            var worksheet = workbook.Sheets[sheetName];
            Range rows = worksheet.Range[range].Rows;

            List<Entry> entries = new List<Entry>();

            try
            {
                foreach (Range row in rows)
                {
                    var entry = new Entry();
                    var currLoc = new Location();
                    currLoc.Interval.Start = new DateTime(2023, 10, 1);
                    var dayCount = 1;

                    foreach (Range cell in row.Cells)
                    {
                        string value = (cell.Value as string);
                        if (1 == cell.Column)
                        {
                            entry.Name = value;
                            continue;
                        }
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            value = "<UNKNOWN>";
                        }
                        value = value.ToUpper();
                        if (2 == cell.Column)
                        {
                            currLoc.Name = value;
                            continue;
                        }
                        if (currLoc.Name == value)
                        {
                            dayCount++;
                        }
                        else
                        {
                            currLoc.Interval.End = currLoc.Interval.Start.AddDays(dayCount);
                            entry.Locations.Add(currLoc);
                            currLoc = new Location(name:value, start:currLoc.Interval.End);
                            dayCount = 1;
                        }
                        System.Console.Write(value);
                    }
                    currLoc.Interval.End = currLoc.Interval.Start.AddDays(dayCount);
                    entry.Locations.Add(currLoc);
                    entries.Add(entry);

                    System.Console.WriteLine();
                }
            }
            catch
            {
            }
            workbook.Close(false);
            Marshal.FinalReleaseComObject(worksheet);
            Marshal.FinalReleaseComObject(workbook);

            return entries;
            return entries.Where(entry => !string.IsNullOrWhiteSpace(entry.Name)).ToList();
        }
        private static List<string> MergeLocations(List<Entry> entries)
        {
            var locations = new List<string>();
            foreach (var entrie in entries)
            {
                foreach (var location in entrie.LocationMap.Keys)
                {
                    if (!locations.Contains(location))
                    {
                        locations.Add(location);
                    }
                }
            }
            return locations;
        }

        private static void PopulateEntries(Application app, List<Entry> entries, string sheetName)
        {
            entries.ForEach(entry => entry.PopulateLocationMap());
            var locations = MergeLocations(entries);

            var path = (@"D:\MSC\DG\examples.copy\res"+DateTime.Now.Ticks+".xlsx");
            Workbook workBook = app.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet workSheet = workBook.Worksheets.get_Item(1);
            workSheet.Name = sheetName;

            try
            {
                var rowIndex = 1;
                var colIndex = 1;
                workSheet.Cells[rowIndex, colIndex] = "ФІО";

                foreach (var location in locations)
                {
                    workSheet.Cells[rowIndex, ++colIndex] = location;
                }
                foreach (var entry in entries)
                {
                    colIndex = 1;
                    workSheet.Cells[++rowIndex, colIndex] = entry.Name;

                    foreach (var location in locations)
                    {
                        ++colIndex;
                        if (entry.LocationMap.ContainsKey(location))
                        {
                            workSheet.Cells[rowIndex, colIndex] = FormatLocationIntervals(entry.LocationMap[location]);
                        }
                    }
                }
            }
            catch {}

            workBook.SaveAs(path, XlFileFormat.xlOpenXMLWorkbook);
            workBook.Close(true);
            Marshal.ReleaseComObject(workSheet);
            Marshal.ReleaseComObject(workBook);
        }
        private static string FormatLocationIntervals(List<DateInterval> intervals)
        {
            string res = string.Empty;
            foreach (var interval in intervals)
            {
                res += $"{interval.Start:dd/MM/yyyy} - {interval.End.AddDays(-1):dd/MM/yyyy} ({interval.Days} днів)";
                res += Environment.NewLine;
            }
            return res.TrimEnd();
        }

        #region RAW READ/WRITE

        private static List<List<object>> ReadEntries(Application app, string path, string sheetName, string range)
        {
            Workbook workbook = app.Workbooks.Open(path);
            var worksheet = workbook.Sheets[sheetName];
            Range rows = worksheet.Range[range].Rows;

            List<List<object>> res = new List<List<object>>();

            try
            {
                foreach (Range row in rows)
                {
                    var r = new List<object>();
                    foreach (Range cell in row.Cells)
                    {
                        object value = cell.Value;
                        r.Add(value);
                        System.Console.Write(value);
                    }
                    System.Console.WriteLine();
                    res.Add(r);
                }
            } catch { }

            workbook.Close(false);
            Marshal.FinalReleaseComObject(worksheet);
            Marshal.FinalReleaseComObject(workbook);

            return res;
        }
        private static void WriteEntries(Application app, List<List<object>> entries, string sheetName)
        {
            var path = (@"D:\MSC\DG\examples.copy\res"+DateTime.Now.Ticks+".xlsx");
            Workbook workBook = app.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet workSheet = workBook.Worksheets.get_Item(1);
            workSheet.Name = sheetName;

            for (var r = 0; r < entries.Count; r++)
            {
                var cells = entries[r];
                for (var c = 0; c < cells.Count; c++)
                {
                    workSheet.Cells[r+1, c+1] = cells[c];
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

        #endregion RAW READ/WRITE
    }
}
