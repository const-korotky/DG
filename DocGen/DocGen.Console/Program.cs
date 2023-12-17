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
            //ProcessExcel(populate: true);
            ProcessWord();
        }

        private static List<Entry> ProcessExcel(bool populate = false)
        {
            var srcPath = @"D:\MSC\DG\examples.copy\Передок жовтень 2023.xlsx";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\Excel_res{DateTime.Now.Ticks}.xlsx";
            var sheet = "жовтень";
            var range = "A2:AF124";

            ExcelApplication app = new ExcelApplication();

            var entries = ExcelProcessor.ExtractEntries(app, srcPath, sheet, range);
            entries.ForEach(entry => entry.PopulateLocationMap());
            if (populate)
            {
                ExcelProcessor.PopulateEntries(app, destPath, sheet, entries);
            }

            app.Quit();
            Marshal.FinalReleaseComObject(app);

            return entries;
        }

        private static void ProcessWord()
        {
            var templPath = @"D:\MSC\DG\examples.copy\РАПОРТ_template.doc";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\Word_res{DateTime.Now.Ticks}.doc";

            WordApplication app = new WordApplication();

            var entries = ProcessExcel(populate: true);
            WordProcessor.GenerateReport(app, templPath, destPath, entries);

            app.Quit();
            Marshal.FinalReleaseComObject(app);
        }
    }
}
