using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DocGen.Data;
using DocGen.Data.Model;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;

namespace DocGen.Processor
{
    public class _ExcelProcessor
    {
        #region Properties and Fields

        public event Action<int, string> ProgressUpdatedEvent;
        protected void ProgressUpdated(double percentage, string message = null)
        {
            if (ProgressUpdatedEvent != null)
            {
                ProgressUpdatedEvent(Convert.ToInt32(Math.Round(percentage)), message);
            }
        }

        public string SourceDataFilePath { get; set; }

        public string DestinationFilePath { get; protected set; }
        protected string UpdateDestinationFilePath()
        {
            var dirPath = Path.GetDirectoryName(SourceDataFilePath);
            var fileName = Path.GetFileNameWithoutExtension(SourceDataFilePath);
            var extension = Path.GetExtension(SourceDataFilePath);

            string fullFileName = string.Empty;
            do {
                if (fileCount < 1)
                {
                    fullFileName = $"{fileName}.звіт{extension}";
                }
                else if (fileCount == 1)
                {
                    fullFileName = $"{fileName}.звіт1{extension}";
                }
                else
                {
                    fullFileName = fullFileName.Replace($"звіт{fileCount-1}", $"звіт{fileCount}");
                }
                DestinationFilePath = Path.Combine(dirPath, fullFileName);
                fileCount += 1;
            } while (File.Exists(DestinationFilePath));
            return DestinationFilePath;
        }
        private static byte fileCount = 0;

        #endregion Properties and Fields

        public void Process(DateTime startDate, DateTime endDate, PrintOption printOptions)
        {
            ExcelApplication excelApplication = new ExcelApplication();
            Workbook workbook = excelApplication.Workbooks.Open(SourceDataFilePath);
            try
            {
                Load(workbook);
                Print(workbook, startDate, endDate, printOptions);

                workbook.SaveAs(UpdateDestinationFilePath());
                workbook.Close(SaveChanges: false);

                excelApplication.Quit();
            }
            catch { }
            finally
            {
                Marshal.FinalReleaseComObject(workbook);
                Marshal.FinalReleaseComObject(excelApplication);
            }
        }

        #region Load Datastore

        public Datastore Datastore { get; protected set; }

        public void Load(Workbook workbook)
        {
            Datastore = new Datastore();
            Datastore.Load(workbook);
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
            PrintFooter(worksheet, rowIndex, targetPrintOption);

            Datastore.IsNormalized = true;
            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
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
            var interval = person.Inactive.FirstOrDefault(i => (i.StartDate <= date) && (date <= i.EndDate));
            if (interval != null)
            {
                cell.Interior.Pattern = XlPattern.xlPatternDown;
                person.Normalize(interval, date);
                return true;
            }
            return false;
        }
        private static void PrintSectorDate(Range cell, Person person, DateTime date, PrintOption printOption)
        {
            var interval = SelectDateTimeInterval(person.Sector, date);
            if (interval != null)
            {
                cell.Value = interval.Location?.Name;
                if (GetIntervalPrintOptionData(interval, printOption, out string name, out double color, out double fontColor))
                {
                    cell.Interior.Color = color;
                    cell.Font.ColorIndex = fontColor;
                }
                person.Normalize(interval, date);
            }
        }
        private static void PrintNormalizedDate(Range cell, Person person, DateTime date, PrintOption printOption)
        {
            var interval = SelectDateTimeInterval(person.Normalized, date);
            if (interval != null)
            {
                if (interval.IsInactive)
                {
                    cell.Interior.Pattern = XlPattern.xlPatternDown;
                }
                else
                {
                    cell.Value = interval.Location?.Name;
                    if (GetIntervalPrintOptionData(interval, printOption, out string name, out double color, out double fontColor))
                    {
                        cell.Interior.Color = color;
                        cell.Font.ColorIndex = fontColor;
                    }
                }
            }
        }
        private static void PrintFooter(Worksheet worksheet, int rowIndex, PrintOption printOption)
        {
            worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[2, 1]].AutoFilter();
        }

        private static DateTimeInterval SelectDateTimeInterval(IEnumerable<DateTimeInterval> intervals, DateTime date)
        {
            var select =
                intervals
                .Where(i => (i.StartDate <= date) && (date <= i.EndDate))
                .OrderBy(i => i.StartDate)
                .ThenBy(i => i.Zone?.Value)
                .ToList()
                ;
            return ((select.Count > 0) ? select.Last() : null);
        }
        private static bool GetIntervalPrintOptionData
            ( DateTimeInterval interval
            , PrintOption printOption
            , out string name
            , out double color
            , out double fontColor 
            ) {
            name = null;
            color = 0;
            fontColor = 0;
            switch (printOption)
            {
                case PrintOption.Order:
                {
                    if (interval.Order != null)
                    {
                        name = interval.Order.Name;
                        color = interval.Order.Color;
                        fontColor = interval.Order.FontColor;
                        return true;
                    }
                    return false;
                }
                case PrintOption.Location:
                {
                    if (interval.Location != null)
                    {
                        name = interval.Location.Name;
                        color = interval.Location.Color;
                        fontColor = interval.Location.FontColor;
                        return true;
                    }
                    return false;
                }
                case PrintOption.Zone:
                {
                    if (interval.Zone != null)
                    {
                        name = interval.Zone.Name;
                        color = interval.Zone.Color;
                        fontColor = interval.Zone.FontColor;
                        return true;
                    }
                    return false;
                }
                default: return false;
            }
        }

        #endregion Print
    }
}
