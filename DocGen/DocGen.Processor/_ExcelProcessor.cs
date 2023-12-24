using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;

using DocGen.Data;
using DocGen.Data.Model;

namespace DocGen.Processor
{
    public class _ExcelProcessor : BaseProcessor
    {
        public override void OpenDocumnet(string filePath)
        {
            System.Diagnostics.Process.Start("excel", filePath);
        }

        public void Process(DateTime startDate, DateTime endDate, PrintOption printOptions)
        {
            ExcelApplication excel = new ExcelApplication();
            Workbook workbook = excel.Workbooks.Open(SourceFilePath);
            try
            {
                Load(workbook, startDate, endDate);
                Print(workbook, startDate, endDate, printOptions);

                //workbook.SaveAs(UpdateDestinationFilePath());
                workbook.Close(SaveChanges: false);

                excel.Quit();
            }
            catch { }
            finally
            {
                Marshal.FinalReleaseComObject(workbook);
                Marshal.FinalReleaseComObject(excel);
            }
        }

        #region Load Datastore

        public void Load(Workbook workbook, DateTime startDate, DateTime endDate)
        {
            Datastore = new Datastore();
            Datastore.Load(workbook, startDate, endDate);
        }

        #endregion Load Datastore

        #region Print

        public void Print
            ( Workbook workbook
            , DateTime startDate
            , DateTime endDate
            , PrintOption printOptions
            ) {
            PrintByOption(workbook, startDate, endDate, printOptions, PrintOption.Order);
            PrintByOption(workbook, startDate, endDate, printOptions, PrintOption.Location);
            PrintByOption(workbook, startDate, endDate, printOptions, PrintOption.Zone);
        }
        private void PrintByOption
            ( Workbook workbook
            , DateTime startDate
            , DateTime endDate
            , PrintOption printOptions
            , PrintOption targetPrintOption
            ) {
            if ((printOptions & targetPrintOption) == targetPrintOption)
            {
                PrintByOption(workbook, startDate, endDate, targetPrintOption);
            }
        }

        public void PrintByOption
            ( Workbook workbook
            , DateTime startDate
            , DateTime endDate
            , PrintOption targetPrintOption
            ) {
            string worksheetName = GetWorksheetName(targetPrintOption);
            Worksheet worksheet = AddWorksheet(workbook, worksheetName);
            PrintHeader(worksheet, startDate, endDate);

            int rowIndex = 2;
            int colIndex = 1;
            Range cell;

            foreach (var person in Datastore.Person)
            {
                cell = worksheet.Cells[rowIndex, colIndex];
                cell.Value = person.Name;
                if (!IsCompanyHeader(cell))
                {
                    var currDate = startDate;
                    while (currDate < endDate)
                    {
                        cell = worksheet.Cells[rowIndex, ++colIndex];
                        if (Datastore.IsNormalized)
                        {
                            PrintNormalizedDate(cell, person, currDate, targetPrintOption);
                        }
                        else
                        {
                            if (!IsInactiveDate(cell, person, currDate))
                            {
                                PrintSectorDate(cell, person, currDate, targetPrintOption);
                            }
                        }
                        currDate = currDate.AddDays(1);
                    }
                }
                Console.WriteLine(person);
                colIndex = 1;
                rowIndex += 1;
            }
            Datastore.IsNormalized = true;
            PrintFooter(worksheet);
        }

        private static string GetWorksheetName(PrintOption printOption)
        {
            switch (printOption)
            {
                case PrintOption.Order:    return "Звіт по Наказам";
                case PrintOption.Location: return "Звіт по Локаціях";
                case PrintOption.Zone:     return "Звіт по Зонах";
                default: return "Звіт";
            }
        }
        private static Worksheet AddWorksheet(Workbook workbook, string worksheetName)
        {
            var lastSheet = workbook.Sheets[workbook.Sheets.Count];
            Worksheet worksheet = workbook.Sheets.Add(After: lastSheet);
            worksheet.Name = worksheetName;
            return worksheet;
        }

        private static void PrintHeader(Worksheet worksheet, DateTime startDate, DateTime endDate)
        {
            var colIndex = 1;
            Range cell;

            cell = worksheet.Cells[1, colIndex];
            cell.Value = "ФИО";
            cell.Font.Bold = true;
            cell.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var currDate = startDate;
            while (currDate < endDate)
            {
                cell = worksheet.Cells[1, ++colIndex];
                cell.Value = currDate.ToString(format: "d-MMM");
                currDate = currDate.AddDays(1);
            }
        }

        private static bool IsCompanyHeader(Range cell)
        {
            var name = cell.Value;
            if ((name == "ALL (КП)") || (name == "ALL (ТКП)"))
            {
                cell.Font.Bold = true;
                cell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                return true;
            }
            return false;
        }
        private static bool IsInactiveDate(Range cell, Person person, DateTime date)
        {
            var interval = person.Inactive.FirstOrDefault(i => (i.StartDate <= date) && (date < i.EndDate));
            if (interval != null)
            {
                cell.Interior.Pattern = XlPattern.xlPatternDown;
                AddComment(cell, interval);
                person.Normalize(interval, date);
                return true;
            }
            return false;
        }

        private static void PrintSectorDate(Range cell, Person person, DateTime date, PrintOption printOption)
        {
            var interval = SelectDateTimeInterval(person.Sector, date);
            if (interval == null)
            {
                return;
            }
            cell.Value = interval.Location?.CodeName;
            AddComment(cell, interval);
            var select = SelectPrintOptionColor(interval, printOption);
            if (select != null)
            {
                cell.Interior.Color = select.Color;
                cell.Font.ColorIndex = select.FontColor;
            }
            person.Normalize(interval, date);
        }
        private static void PrintNormalizedDate(Range cell, Person person, DateTime date, PrintOption printOption)
        {
            var interval = SelectDateTimeInterval(person.Normalized, date);
            if (interval == null)
            {
                return;
            }
            if (interval.IsInactive)
            {
                cell.Interior.Pattern = XlPattern.xlPatternDown;
                AddComment(cell, interval);
            }
            else
            {
                cell.Value = interval.Location?.CodeName;
                AddComment(cell, interval);
                var select = SelectPrintOptionColor(interval, printOption);
                if (select != null)
                {
                    cell.Interior.Color = select.Color;
                    cell.Font.ColorIndex = select.FontColor;
                }
            }
        }

        private static void AddComment(Range cell, DateTimeInterval interval)
        {
            int row = (interval.ID + 1);
            if(interval.IsInactive)
            {
                row -= 10000;
                cell.Worksheet.Hyperlinks.Add(cell, $"#НЕЗАДІЯНІ!A{row}:D{row}", TextToDisplay: "______");
                cell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }
            else
            {
                cell.Worksheet.Hyperlinks.Add(cell, $"#СЕКТОР!A{row}:H{row}");
            }
            cell.AddComment(FormatComment(interval));
        }
        private static string FormatComment(DateTimeInterval interval)
        {
            var descr = interval.Description;
            var descrIsEmpty = string.IsNullOrWhiteSpace(descr);

            if (interval.IsInactive || (interval.Order == null))
            {
                return (!descrIsEmpty ? descr : "N/A");
            }
            if (descrIsEmpty)
            {
                descr = interval.Order.Description;
                descrIsEmpty = string.IsNullOrWhiteSpace(descr);
            }
            if (!descrIsEmpty)
            {
                descr = $" - {descr}";
            }
            return $"{interval.Location?.Name}: {interval.Order.Name}{descr}";
        }

        private static void PrintFooter(Worksheet worksheet)
        {
            var leftCornerCell = worksheet.Cells[1, 1];
            var nextRowCell = worksheet.Cells[2, 1];
            worksheet.Range[leftCornerCell, nextRowCell].AutoFilter();

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();

            worksheet.Application.ActiveWindow.SplitRow = 1;
            worksheet.Application.ActiveWindow.SplitColumn = 1;
            worksheet.Application.ActiveWindow.FreezePanes = true;
        }

        private static DateTimeInterval SelectDateTimeInterval(IEnumerable<DateTimeInterval> intervals, DateTime date)
        {
            var select =
                intervals
                .Where(i => (i.StartDate <= date) && (date < i.EndDate))
                .OrderBy(i => i.StartDate)
                .ThenBy(i => i.Zone?.Value)
                .ToList()
                ;
            return ((select.Count > 0) ? select.Last() : null);
        }
        private static ColorfulEntity SelectPrintOptionColor(DateTimeInterval interval, PrintOption printOption)
        {
            switch (printOption)
            {
                case PrintOption.Order: return interval.Order;
                case PrintOption.Location: return interval.Location;
                case PrintOption.Zone: return interval.Zone;
                default: return null;
            }
        }

        #endregion Print
    }
}
