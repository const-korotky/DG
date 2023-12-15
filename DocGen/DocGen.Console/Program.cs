using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
            //ProcessExcel();
            ProcessWord();
        }

        private static void ProcessExcel()
        {
            var srcPath = @"D:\MSC\DG\examples.copy\Передок жовтень 2023.xlsx";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\Excel_res{DateTime.Now.Ticks}.xlsx";
            var sheet = "жовтень";
            var range = "A2:AF124";

            ExcelApplication app = new ExcelApplication();

            var entries = ExcelProcessor.ExtractEntries(app, srcPath, sheet, range);
            ExcelProcessor.PopulateEntries(app, destPath, sheet, entries);

            /*var entries = ExcelProcessor.ReadEntries(app, srcPath, sheet, range);
            ExcelProcessor.WriteEntries(app, destPath, sheet, entries);*/

            app.Quit();
            Marshal.FinalReleaseComObject(app);
        }
        private static void ProcessWord()
        {
            var srcPath = @"D:\MSC\DG\examples.copy\БН_НАЧПВЗ_37_041023.docx";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\Word_res{DateTime.Now.Ticks}.docx";

            WordApplication app = new WordApplication();

            var doc = WordProcessor.ReadEntries(app, srcPath);
            WordProcessor.WriteEntries(app, doc, destPath);

            app.Quit();
            Marshal.FinalReleaseComObject(app);
        }
    }
}
