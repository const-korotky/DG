using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;


namespace DocGen.Processor
{
    public class ExcelProcessor
    {
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

        public static List<Entry> ExtractEntries
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
                }
            }
            catch
            {
            }
            Marshal.FinalReleaseComObject(worksheet);
            return entries.Where(entry => !string.IsNullOrWhiteSpace(entry.Name)).ToList();
        }

        public static void PopulateEntries
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
    }
}
