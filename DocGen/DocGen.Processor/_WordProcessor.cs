using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.Office.Interop.Word;
using WordDocument = Microsoft.Office.Interop.Word.Document;
using WordApplication = Microsoft.Office.Interop.Word.Application;

using DocGen.Data;
using DocGen.Data.Model;

namespace DocGen.Processor
{
    public class _WordProcessor : BaseProcessor
    {
        public override void OpenDocumnet(string filePath)
        {
            System.Diagnostics.Process.Start("winword", filePath);
        }

        public override void Process()
        {
            UpdateProgress(70, "Відкриття шаблону рапорта....");
            WordApplication word = new WordApplication();
            WordDocument doc = null;
            try
            {
                doc = word.Documents.Open(SourceFilePath, ReadOnly: true, Visible: false);
                UpdateProgress(71, "Шаблон рапорта відкрито.");

                UpdateProgress(71, "Генерація рапорта....");
                GenerateReport(doc);
                UpdateProgress(98, "Генерацію рапорта завершено.");

                UpdateProgress(98, "Збереження рапорта....");
                doc.SaveAs(UpdateDestinationFilePath());
                doc.Close(SaveChanges: false);

                word.Quit();
                UpdateProgress(99, "Рапорт збережено.");
            }
            catch (Exception e) { }
            catch { }
            finally
            {
                if (doc != null)
                {
                    Marshal.FinalReleaseComObject(doc);
                }
                Marshal.FinalReleaseComObject(word);
            }
        }

        public void GenerateReport(WordDocument doc)
        {
            double percent = 71F;

            Table table = doc.Tables[1];
            var rowIndex = 2;

            foreach (var person in Datastore.Person)
            {
                int totalDays = GetTotalDays(person);
                if (totalDays > 0)
                {
                    PopulateTableRow(table, rowIndex, person, totalDays);
                    table.Rows.Add();
                    ++rowIndex;
                }
                percent += 0.15F;
                UpdateProgress(percent);
                Console.WriteLine($"WORD: {person}");
            }
            //table.Rows.Last.Delete();
        }

        protected static void PopulateTableRow
            ( Table table
            , int rowIndex
            , Person person
            , int totalDays
            ) {
            Range cell;
            cell = table.Cell(rowIndex, 1).Range;
            cell.Text = $"{rowIndex - 1}.";
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 2).Range;
            cell.Text = person.Rank;
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 3).Range;
            cell.Text = person.Name;
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 4).Range;
            cell.Text = FormatTotalIntervals(person);
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 5).Range;
            cell.Text = $"{totalDays}";
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 6).Range;
            cell.Text = FormatTotalOrders(person);
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        }

        protected static int GetTotalDays(Person person)
        {
            return person
                   .Normalized
                   .Where(interval => !interval.IsInactive)
                   .Sum(interval => interval.Days);
        }
        protected static string FormatTotalIntervals(Person person, bool full = false)
        {
            string res = string.Empty;
            int intervalDays;

            foreach (var interval in person.Normalized.Where(i => !i.IsInactive))
            {
                intervalDays = interval.Days;
                if (full)
                {
                    res += $"{interval.StartDate:dd/MM/yyyy} - {interval.EndDate.AddDays(-1):dd/MM/yyyy}";
                }
                else
                {
                    if (intervalDays < 2)
                    {
                        res += $"{interval.StartDate:dd.MM.yyyy}";
                    }
                    else
                    {
                        res += $"{interval.StartDate:dd}-{interval.EndDate.AddDays(-1):dd.MM.yyyy}";
                    }
                }
                res += Environment.NewLine;
            }
            return res.TrimEnd();
        }
        protected static string FormatTotalOrders(Person person)
        {
            var orders = new Dictionary<string, List<string>>();
            foreach (var interval in person.Normalized.Where(i => !i.IsInactive))
            {
                AddOrder(interval, orders);
            }
            return FormatOrdersDictionary(orders);
        }
        protected static void AddOrder(DateTimeInterval interval, Dictionary<string, List<string>> orders)
        {
            var key = interval.Order.CodeName;
            if (string.IsNullOrWhiteSpace(key))
            {
                key = interval.Order.Name;
            }
            if (!orders.ContainsKey(key))
            {
                orders.Add(key, new List<string>());
            }
            var note = interval.Note;
            if (!string.IsNullOrWhiteSpace(note))
            {
                var notes = orders[key];
                if (!notes.Contains(note))
                {
                    notes.Add(note);
                }
            }
        }
        private static string FormatOrdersDictionary(Dictionary<string, List<string>> orders)
        {
            return string.Join(
                Environment.NewLine,
                orders.Select(
                        i => $"{i.Key}, {string.Join(", ", i.Value)}".TrimEnd(',', ' ')
                        )
                );
        }
    }
}
