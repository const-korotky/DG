﻿using System;
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
    public class WordProcessor : BaseProcessor
    {
        public override void OpenDocumnet(string filePath)
        {
            System.Diagnostics.Process.Start("winword", $"\"{filePath}\"");
        }

        public override void Process(bool reloadDatastore = false)
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
                    try { doc.Close(SaveChanges: false); } catch { }
                    Marshal.FinalReleaseComObject(doc);
                }
                Marshal.FinalReleaseComObject(word);
            }
        }

        public void GenerateReport(WordDocument doc)
        {
            Table table;
            if (doc.Tables.Count > 0)
            {
                table = doc.Tables[1];
                PopulateZone(table, 30);
            }
            if (doc.Tables.Count > 1)
            {
                table = doc.Tables[2];
                PopulateZone(table, 100);
            }
        }

        private void PopulateZone(Table table, double zone)
        {
            List<DateTimeInterval> intervals;
            var rowIndex = 2;

            foreach (var person in Datastore.Person)
            {
                intervals = GetZoneIntervals(person, zone);
                int totalDays = GetTotalDays(intervals);
                if (totalDays > 0)
                {
                    PopulateTableRow(table, rowIndex, person, intervals, totalDays);
                    table.Rows.Add();
                    ++rowIndex;
                }
                IncrementProgressBy(0.15);
                Console.WriteLine($"WORD: {person}");
            }
            //table.Rows.Last.Delete();
        }

        protected static void PopulateTableRow
            ( Table table
            , int rowIndex
            , Person person
            , List<DateTimeInterval> intervals
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
            cell.Text = FormatTotalIntervals(intervals);
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 5).Range;
            cell.Text = $"{totalDays}";
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 6).Range;
            cell.Text = FormatTotalOrders(intervals);
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        }

        protected static int GetTotalDays(List<DateTimeInterval> intervals)
        {
            return intervals.Sum(interval => interval.Days);
        }
        protected static string FormatTotalIntervals(List<DateTimeInterval> intervals, bool full = false)
        {
            string res = string.Empty;
            int intervalDays;

            foreach (var interval in intervals)
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
        protected static string FormatTotalOrders(List<DateTimeInterval> intervals)
        {
            var orders = new Dictionary<string, List<string>>();
            foreach (var interval in intervals)
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

        private static List<DateTimeInterval> GetZoneIntervals(Person person, double zone)
        {
            return person
                   .Normalized
                   .Where(interval => !interval.IsInactive)
                   .Where(interval => ( interval.Zone != null )&&( interval.Zone.Value == zone ))
                   .ToList();
        }


        public string CreateOrderDocument(Datastore datastore, SectorItem sectorItem)
        {
            WordApplication word = new WordApplication();
            WordDocument doc = null;
            try
            {
                doc = word.Documents.Open(@"D:\MSC\DG\work\БН_НАЧПВЗ.docx", ReadOnly: true, Visible: false);

                FormatOrderDocument(datastore, sectorItem, doc);

                var filePath = $"D:\\MSC\\DG\\work\\{sectorItem.OrderName}.docx";
                doc.SaveAs(filePath);
                doc.Close(SaveChanges: false);

                word.Quit();

                return filePath;
            }
            catch (Exception e) { return null; }
            catch { return null; }
            finally
            {
                if (doc != null)
                {
                    try { doc.Close(SaveChanges: false); } catch { }
                    Marshal.FinalReleaseComObject(doc);
                }
                Marshal.FinalReleaseComObject(word);
            }
        }
        private void FormatOrderDocument(Datastore datastore, SectorItem sectorItem, WordDocument doc)
        {
            doc.Content.Find.Execute(FindText: "$location$",
                                    MatchCase: false,
                                    MatchWholeWord: false,
                                    MatchWildcards: false,
                                    MatchSoundsLike: false,
                                    MatchAllWordForms: false,
                                    Forward: true, //this may be the one
                                    Wrap: false,
                                    Format: false,
                                    ReplaceWith: sectorItem.LocationName,
                                    Replace: WdReplace.wdReplaceAll
                                    );
            doc.Content.Find.Execute(FindText: "$DateStart$",
                                    MatchCase: false,
                                    MatchWholeWord: false,
                                    MatchWildcards: false,
                                    MatchSoundsLike: false,
                                    MatchAllWordForms: false,
                                    Forward: true, //this may be the one
                                    Wrap: false,
                                    Format: false,
                                    ReplaceWith: sectorItem.StartDate,
                                    Replace: WdReplace.wdReplaceAll
                                    );
            doc.Content.Find.Execute(FindText: "$DateEnd$",
                                    MatchCase: false,
                                    MatchWholeWord: false,
                                    MatchWildcards: false,
                                    MatchSoundsLike: false,
                                    MatchAllWordForms: false,
                                    Forward: true, //this may be the one
                                    Wrap: false,
                                    Format: false,
                                    ReplaceWith: sectorItem.EndDate,
                                    Replace: WdReplace.wdReplaceAll
                                    );
            doc.Content.Find.Execute(FindText: "$personnel$",
                                    MatchCase: false,
                                    MatchWholeWord: false,
                                    MatchWildcards: false,
                                    MatchSoundsLike: false,
                                    MatchAllWordForms: false,
                                    Forward: true, //this may be the one
                                    Wrap: false,
                                    Format: false,
                                    ReplaceWith: FormatPersons(datastore, sectorItem),
                                    Replace: WdReplace.wdReplaceAll
                                    );
        }


        private string FormatPersons(Datastore datastore, SectorItem sectorItem)
        {
            var persons = new List<string>();

            var personNames = sectorItem.Persons.Split(',');
            foreach (var personName in personNames)
            {
                var person = datastore.Person.FirstOrDefault(i => i.Name == personName.Trim());
                if (person != null)
                {
                    persons.Add($"{person.Rank} {person.Name}");
                }
            }
            return string.Join(Environment.NewLine, persons);
        }
        private string FormatPersonName(Datastore datastore, InactiveItem inactiveItem)
        {
            var person = datastore.Person.FirstOrDefault(i => i.Name == inactiveItem.PersonName);
            if (person != null)
            {
                string rank = person.Rank;
                rank = $"{char.ToUpper(rank[0])}{rank.Substring(1)}";
                return $"{rank} {person.Name}";
            }
            return string.Empty;
        }


        public string CreateExcludeDocument(Datastore datastore, List<InactiveItem> inactiveItems)
        {
            WordApplication word = new WordApplication();
            WordDocument doc = null;
            try
            {
                doc = word.Documents.Open(@"D:\MSC\DG\work\Рапорт Виведення з УМП.шаблон.docx", ReadOnly: true, Visible: false);

                FormatExcludeDocument(datastore, inactiveItems, doc);

                var filePath = $"D:\\MSC\\DG\\work\\Рапорт Виведення з УМП.{DateTime.Now:dd.MM.yyyy_HH.mm.ss.ffff}.docx";
                doc.SaveAs(filePath);
                doc.Close(SaveChanges: false);

                word.Quit();

                return filePath;
            }
            catch (Exception e) { return null; }
            catch { return null; }
            finally
            {
                if (doc != null)
                {
                    try { doc.Close(SaveChanges: false); }
                    catch { }
                    Marshal.FinalReleaseComObject(doc);
                }
                Marshal.FinalReleaseComObject(word);
            }
        }
        private void FormatExcludeDocument(Datastore datastore, List<InactiveItem> inactiveItems, WordDocument doc)
        {
            for (int i = 0; i < inactiveItems.Count; i++)
            {
                doc.Content.Find.Execute(FindText: $"$_RN_{i+1}_$",
                                        MatchCase: false,
                                        MatchWholeWord: false,
                                        MatchWildcards: false,
                                        MatchSoundsLike: false,
                                        MatchAllWordForms: false,
                                        Forward: true, //this may be the one
                                        Wrap: false,
                                        Format: false,
                                        ReplaceWith: FormatPersonName(datastore, inactiveItems[i]),
                                        Replace: WdReplace.wdReplaceAll
                                        );
            }

            string date = (( (inactiveItems.Count > 0) && inactiveItems[0].StartDate.HasValue )
                          ? inactiveItems[0].StartDate.Value
                          : DateTime.Today
                          ).ToString("dd.MM.yyyy");

            doc.Content.Find.Execute(FindText: "$_DT_$",
                                    MatchCase: false,
                                    MatchWholeWord: false,
                                    MatchWildcards: false,
                                    MatchSoundsLike: false,
                                    MatchAllWordForms: false,
                                    Forward: true, //this may be the one
                                    Wrap: false,
                                    Format: false,
                                    ReplaceWith: date,
                                    Replace: WdReplace.wdReplaceAll
                                    );
        }
    }
}
