using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using WordApplication = Microsoft.Office.Interop.Word.Application;

using DocGen.Processor;

namespace DocGen.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProcessExcelBRs();
            //ProcessExcelEntries(populate: true);
            //ProcessWord();

            DateTime startDate = new DateTime(2023, 8, 1);
            DateTime endDate = startDate.AddMonths(1);
            //PrintOption printOptions = (PrintOption.Order|PrintOption.Location|PrintOption.Zone);
            PrintOption printOptions = PrintOption.Order;

            var excelProcessor = new _ExcelProcessor()
            {
                SourceFilePath = @"D:\MSC\DG\examples.copy\серпень.db.xlsm",
            };
            excelProcessor.Process(startDate, endDate, printOptions);

            var wordProcessor = new _WordProcessor()
            {
                SourceFilePath = @"D:\MSC\DG\examples.copy\РАПОРТ.docx",
                Datastore = excelProcessor.Datastore,
            };
            wordProcessor.Process(PrintZone._100);
        }

        private static List<BR> ProcessExcelBRs()
        {
            var srcPath = @"D:\MSC\DG\examples.copy\Передок жовтень 2023.xlsx";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\Excel_res{DateTime.Now.Ticks}.xlsx";
            var sheet = "серпень";
            var range = "A95:A112";

            ExcelApplication app = new ExcelApplication();
            Workbook workbook = app.Workbooks.Open(srcPath);

            var brs = ExcelProcessor.ExtractBRs(workbook, sheet, range);

            workbook.Close(false);
            app.Quit();
            Marshal.FinalReleaseComObject(workbook);
            Marshal.FinalReleaseComObject(app);

            return brs;
        }
        private static List<Entry> ProcessExcelEntries(out List<BR> brs, bool populate = false)
        {
            var srcPath = @"D:\MSC\DG\examples.copy\Передок жовтень 2023.xlsx";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\Excel_res{DateTime.Now.Ticks}.xlsx";

            /*var sheet = "жовтень";
            var range = "A2:AF124";
            var startDate = new DateTime(2023, 10, 1);*/
            var sheet = "серпень";
            var range = "A2:AF92";
            var startDate = new DateTime(2023, 8, 1);

            var rangeBRs = "A95:A112";

            ExcelApplication app = new ExcelApplication();
            Workbook workbook = app.Workbooks.Open(srcPath);

            var excelProcessor = new ExcelProcessor();

            brs = ExcelProcessor.ExtractBRs(workbook, sheet, rangeBRs);
            var entries = excelProcessor.ExtractEntries(workbook, sheet, range, startDate);

            entries.ForEach(entry => entry.PopulateLocationMap());
            if (populate)
            {
                excelProcessor.PopulateEntries(app, destPath, sheet, entries);
            }

            workbook.Close(false);
            app.Quit();
            Marshal.FinalReleaseComObject(workbook);
            Marshal.FinalReleaseComObject(app);

            return entries;
        }

        private static void ProcessWord()
        {
            var templPath = @"D:\MSC\DG\examples.copy\РАПОРТ_template_full.doc";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\Word_res{DateTime.Now.Ticks}.doc";

            List<BR> brs;
            var entries = ProcessExcelEntries(out brs, populate: true);

            WordApplication app = new WordApplication();

            var wordProcessor = new WordProcessor();
            wordProcessor.GenerateReport(app, templPath, destPath, entries, brs);

            app.Quit();
            Marshal.FinalReleaseComObject(app);
        }
    }
}
