﻿using System;
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
        public PrintOption PrintOptions { get; set; } = PrintOption.Order;
        public int PrintScale { get; set; } = 100;

        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DateTime EndDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);

        public override void OpenDocumnet(string filePath)
        {
            System.Diagnostics.Process.Start("excel", filePath);
        }

        public override void Process(bool reloadDatastore = false)
        {
            UpdateProgress(0, "Відкриття бази даних....");
            ExcelApplication excel = new ExcelApplication();
            Workbook workbook = null;
            try
            {
                workbook = excel.Workbooks.Open(SourceFilePath);
                UpdateProgress(1, "База даних відкрита.");

                UpdateProgress(1, "Завантаження бази даних....");
                LoadDatastore(workbook, reloadDatastore);
                UpdateProgress(15, "Завантаження бази даних завершено.");

                UpdateProgress(15, "Генерація діаграми....");
                Print(workbook);
                UpdateProgress(70, "Генерацію діаграми завершено.");

                UpdateProgress(70, "Збереження діаграми.....");
                workbook.SaveAs(UpdateDestinationFilePath());
                workbook.Close(SaveChanges: false);

                excel.Quit();
                UpdateProgress(71, "Діаграму збережено.");
            }
            catch (Exception e) { }
            catch { }
            finally
            {
                if (workbook != null)
                {
                    Marshal.FinalReleaseComObject(workbook);
                }
                Marshal.FinalReleaseComObject(excel);
            }
        }

        #region Load Datastore

        public void LoadDatastore(Workbook workbook, bool reload = false)
        {
            if ((Datastore == null) || !Datastore.IsLoaded || reload)
            {
                Datastore = new Datastore(IncrementProgressBy);
                Datastore.Load(workbook, StartDate, EndDate);
            }
        }

        #endregion Load Datastore

        #region Print

        public void Print(Workbook workbook)
        {
            TryPrint(workbook, PrintOption.Order);
            TryPrint(workbook, PrintOption.Location);
            TryPrint(workbook, PrintOption.Zone);
        }
        protected void TryPrint(Workbook workbook, PrintOption targetPrintOption)
        {
            if ((PrintOptions & targetPrintOption) == targetPrintOption)
            {
                PrintByOption(workbook, targetPrintOption);
            }
        }

        public void PrintByOption(Workbook workbook, PrintOption printOption)
        {
            string worksheetName = GetWorksheetName(printOption);
            Worksheet worksheet = AddWorksheet(workbook, worksheetName);
            PrintHeader(worksheet, StartDate, EndDate);

            int rowIndex = 2;
            int colIndex = 1;
            Range cell;

            foreach (var person in Datastore.Person)
            {
                cell = worksheet.Cells[rowIndex, colIndex];
                cell.Value = person.Name;
                if (!IsCompanyHeader(cell))
                {
                    var currDate = StartDate;
                    while (currDate < EndDate)
                    {
                        cell = worksheet.Cells[rowIndex, ++colIndex];
                        if (Datastore.IsNormalized)
                        {
                            PrintNormalizedDate(cell, person, currDate, printOption);
                        }
                        else
                        {
                            if (!IsInactiveDate(cell, person, currDate))
                            {
                                PrintSectorDate(cell, person, currDate, printOption);
                            }
                        }
                        currDate = currDate.AddDays(1);
                    }
                }
                colIndex = 1;
                rowIndex += 1;

                IncrementProgressBy(0.135);
                Console.WriteLine(person);
            }
            Datastore.IsNormalized = true;
            PrintFooter(worksheet, PrintScale);
        }

        private static string GetWorksheetName(PrintOption printOption)
        {
            switch (printOption)
            {
                case PrintOption.Order:    return "Звіт по Наказах";
                case PrintOption.Location: return "Звіт по Локаціях";
                case PrintOption.Zone:     return "Звіт по Зонах";
                default: return "N/A";
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
            person.Normalize(interval, date);
            if (interval.IsGeneral == true)
            {
                return;
            }
            cell.Value = ((interval.Location == null) ? "_____" : interval.Location.CodeName);
            AddComment(cell, interval);
            var select = SelectPrintOptionColor(interval, printOption);
            if (select != null)
            {
                cell.Interior.Color = select.Color;
                cell.Font.ColorIndex = select.FontColor;
            }
        }
        private static void PrintNormalizedDate(Range cell, Person person, DateTime date, PrintOption printOption)
        {
            var interval = SelectDateTimeInterval(person.Normalized, date);
            if (( interval == null )||( interval.IsGeneral == true ))
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
                cell.Value = ((interval.Location == null) ? "_____" : interval.Location.CodeName);
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

        private static void PrintFooter(Worksheet worksheet, double scale)
        {
            var leftCornerCell = worksheet.Cells[1, 1];
            var nextRowCell = worksheet.Cells[2, 1];
            worksheet.Range[leftCornerCell, nextRowCell].AutoFilter();

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();

            worksheet.Application.ActiveWindow.SplitRow = 1;
            worksheet.Application.ActiveWindow.SplitColumn = 1;
            worksheet.Application.ActiveWindow.FreezePanes = true;
            worksheet.Application.ActiveWindow.Zoom = scale;
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
