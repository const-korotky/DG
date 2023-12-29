using System;
using System.IO;

using DocGen.Processor;

namespace DocGen.Console
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (!GetFilePath(args, 1, out string excelFilePath))
            {
                return 1;
            }
            var startDate = new DateTime(2023, 8, 1);
            var excelProcessor = new ExcelProcessor()
            {
                SourceFilePath = excelFilePath,
                PrintOptions = (PrintOption.Order|PrintOption.Location|PrintOption.Zone),

                StartDate = startDate,
                EndDate = startDate.AddMonths(1),
            };
            excelProcessor.Process();

            if (GetFilePath(args, 2, out string wordFilePath))
            {
                var wordProcessor = new WordProcessor()
                {
                    SourceFilePath = wordFilePath,
                    Datastore = excelProcessor.Datastore,
                };
                wordProcessor.Process();
            }

            System.Console.WriteLine();
            return 0;
        }

        private static bool GetFilePath(string[] args, int index, out string filePath)
        {
            filePath = null;
            if (args.Length < index)
            {
                System.Console.WriteLine("No parameters specified.");
                return false;
            }
            filePath = args[index-1];
            if (!File.Exists(filePath))
            {
                System.Console.WriteLine($"File \"{filePath}\" doesn't exist.");
                return false;
            }
            return true;
        }
    }
}
