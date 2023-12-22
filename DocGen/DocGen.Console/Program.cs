using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using WordApplication = Microsoft.Office.Interop.Word.Application;
using WordDocument = Microsoft.Office.Interop.Word.Document;
using DocGen.Processor;
using System.IO;

namespace DocGen.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProcessExcelBRs();
            //ProcessExcelEntries(populate: true);
            //ProcessWord();

            var excelProcessor = new ExcelProcessor()
            {
                //SourceDataFilePath = @"D:\MSC\DG\examples.copy\серпень.db - Copy.xlsm",
                SourceDataFilePath = @"D:\MSC\DG\examples.copy\серпень.db - Copy (2).xlsm",
            };
            excelProcessor.ProcessDB();
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
