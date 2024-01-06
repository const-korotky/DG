using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;

using DocGen.Data;
using DocGen.Data.Model;


namespace DocGen.Processor
{
    public class ExcelProcessor : BaseProcessor
    {
        private const string URL_PERSON_STATUS = @"https://drive.google.com/uc?id=1R_1OYK2GY3sZDnfRLHz9Qc0nX_hPG1CZ&export=download";

        public PrintOption PrintOptions { get; set; } = PrintOption.Order;
        public int PrintScale { get; set; } = 100;

        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DateTime EndDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);

        public override void OpenDocumnet(string filePath)
        {
            System.Diagnostics.Process.Start("excel", $"\"{filePath}\"");
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

        #region Load/Update Datastore

        public Datastore LoadDatastoreOnDemand()
        {
            ExcelApplication excel = new ExcelApplication();
            Workbook workbook = null;
            try
            {
                UpdateProgress(0, "Відкриття бази даних....");
                workbook = excel.Workbooks.Open(SourceFilePath);
                UpdateProgress(20, "Бази даних відкрита.");

                UpdateProgress(20, "Розпочато завантаження бази даних....");
                var datastore = new Datastore(IncrementProgressBy);

                datastore.Load(workbook, StartDate, EndDate);
                UpdateProgress(90, "Завантаження бази даних завершено.");

                workbook.Close(SaveChanges: false);

                excel.Quit();
                UpdateProgress(100, "Операцію завершено.");

                return datastore;
            }
            catch (Exception e) { return null; }
            catch { return null; }
            finally
            {
                if (workbook != null)
                {
                    Marshal.FinalReleaseComObject(workbook);
                }
                Marshal.FinalReleaseComObject(excel);
            }
        }

        public void LoadDatastore(Workbook workbook, bool reload = false)
        {
            if (Datastore == null)
            {
                Datastore = new Datastore(IncrementProgressBy);
            }
            LoadPersonStatusRecords();
            Datastore.PopulateInactiveIntervals(workbook);
            if (!Datastore.IsLoaded)
            {
                Datastore.Load(workbook, StartDate, EndDate);
            }
            else if (reload)
            {
                Datastore.Reload(workbook, StartDate, EndDate);
            }
        }

        public void LoadPersonStatusRecords()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, $"Inactive{DateTime.Now.Ticks}.xlsx");

            var file = DownloadFile(URL_PERSON_STATUS, filePath);
            if (!file.Exists || (file.Length <= 0))
            {
                return;
            }
            ExcelApplication excel = new ExcelApplication();
            Workbook workbook = null;
            try
            {
                workbook = excel.Workbooks.Open(file.FullName);
                Datastore.LoadPersonStatusRecords(workbook);
                workbook.Close(SaveChanges: false);
                excel.Quit();
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

        private static FileInfo DownloadFile(string url, string filePath)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(url, filePath);
                    return new FileInfo(filePath);
                }
            }
            catch (Exception e) { return null; }
            catch { return null; }
        }

        public void SaveDatastoreOnDemand(Datastore datastore)
        {
            ExcelApplication excel = new ExcelApplication();
            Workbook workbook = null;
            try
            {
                workbook = excel.Workbooks.Open(SourceFilePath);
                datastore.Save(workbook);
                workbook.Close(SaveChanges: true);
                excel.Quit();
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

        #endregion Load/Update Datastore

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
                AddComment(cell, interval, date);
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
            AddComment(cell, interval, date);
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
                AddComment(cell, interval, date);
            }
            else
            {
                cell.Value = ((interval.Location == null) ? "_____" : interval.Location.CodeName);
                AddComment(cell, interval, date);
                var select = SelectPrintOptionColor(interval, printOption);
                if (select != null)
                {
                    cell.Interior.Color = select.Color;
                    cell.Font.ColorIndex = select.FontColor;
                }
            }
        }

        private static void AddComment(Range cell, DateTimeInterval interval, DateTime date)
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
            cell.AddComment(FormatComment(interval, date));
        }
        private static string FormatComment(DateTimeInterval interval, DateTime date)
        {
            var descr = interval.Description;
            var descrIsEmpty = string.IsNullOrWhiteSpace(descr);

            if (interval.IsInactive || (interval.Order == null))
            {
                return (date.ToString("dd.MM.yyyy")+Environment.NewLine+(!descrIsEmpty ? descr : "N/A"));
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
            return $"{date.ToString("dd.MM.yyyy")}{Environment.NewLine}{interval.Location?.Name}: {interval.Order.Name}{descr}";
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
