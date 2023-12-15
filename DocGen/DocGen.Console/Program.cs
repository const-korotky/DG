using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using DocGen.Processor;

namespace DocGen.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var srcPath = @"D:\MSC\DG\examples.copy\Передок жовтень 2023.xlsx";
            var destPath = $"D:\\MSC\\DG\\examples.copy\\res{DateTime.Now.Ticks}.xlsx";
            var sheet = "жовтень";
            var range = "A2:AF124";

            ExcelApplication app = new ExcelApplication();

            var entries = ExcelProcessor.ExtractEntries(app, srcPath, sheet, range);
            ExcelProcessor.PopulateEntries(app, destPath, sheet, entries);

            /*var data = ExcelProcessor.ReadEntries(app, srcPath, sheet, range);
            ExcelProcessor.WriteEntries(app, destPath, sheet, data);*/

            app.Quit();
            Marshal.FinalReleaseComObject(app);
        }
    }
}
