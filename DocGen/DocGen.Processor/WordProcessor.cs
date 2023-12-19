using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;
using WordApplication = Microsoft.Office.Interop.Word.Application;
using WordDocument = Microsoft.Office.Interop.Word.Document;

namespace DocGen.Processor
{
    public class WordProcessor
    {
        public event Action<int, string> ProgressUpdatedEvent;
        private void ProgressUpdated(double percentage, string message = null)
        {
            if (ProgressUpdatedEvent != null)
            {
                ProgressUpdatedEvent(Convert.ToInt32(Math.Round(percentage)), message);
            }
        }

        public string TemplateFilePath { get; set; }
        public string DestinationFilePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_destinationFilePath))
                {
                    var dirPath = Path.GetDirectoryName(TemplateFilePath);
                    _destinationFilePath = Path.Combine(dirPath, $"Word_res{DateTime.Now.Ticks}.doc");
                }
                return _destinationFilePath;
            }
        }
        public string _destinationFilePath;


        public static void OpenDocumnet(string filePath)
        {
            System.Diagnostics.Process.Start("winword.exe", filePath);
        }

        public void Process(List<BR> brs, List<Entry> entries)
        {
            ProgressUpdated(70, "відкриття файлу шаблона...");
            WordApplication app = new WordApplication();
            ProgressUpdated(79, "відкриття файлу шаблона...");

            ProgressUpdated(80, "розпочато генерацію рапорта...");
            GenerateReport(app, TemplateFilePath, DestinationFilePath, entries, brs);
            ProgressUpdated(91, "генерацію рапорта завершено");

            app.Quit();
            Marshal.FinalReleaseComObject(app);
            ProgressUpdated(98, "рапорта збережено на диск");
        }

        public void GenerateReport
            ( WordApplication app
            , string templatePath
            , string destinationPath
            , List<Entry> entries
            , List<BR> brs
            ) {
            WordDocument doc = null;
            try
            {
                doc = app.Documents.Open(templatePath, ReadOnly: true, Visible: false);

                Table table = doc.Tables[1];
                var rowIndex = 2;

                double progressPercent = 80;
                foreach (var entry in entries)
                {
                    int daysTotal = entry.GetDaysTotal();
                    if (daysTotal > 0)
                    {
                        PopulateTableRow(table, rowIndex, entry, daysTotal, brs);
                        table.Rows.Add();
                        ++rowIndex;
                    }
                    progressPercent += 0.1;
                    ProgressUpdated(progressPercent);
                }
                //table.Rows.Last.Delete();
                doc.SaveAs(destinationPath);
                doc.Close(true);
            }
            catch
            {
            }
            Marshal.FinalReleaseComObject(doc);
        }

        private static void PopulateTableRow
            ( Table table
            , int rowIndex
            , Entry entry
            , int daysTotal
            , IEnumerable<BR> brs
            )
        {
            Range cell;
            cell = table.Cell(rowIndex, 1).Range;
            cell.Text = $"{rowIndex - 1}.";
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 2).Range;
            cell.Text = $"N/A";
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 3).Range;
            cell.Text = entry.Name;
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 4).Range;
            cell.Text = entry.FormatTotalIntervals(days: false, full: false);
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 5).Range;
            cell.Text = $"{daysTotal}";
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            cell = table.Cell(rowIndex, 6).Range;
            cell.Text = entry.FormatTotalBRs(brs);
            cell.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        }

        #region Raw Entries Read/Write

        public static WordDocument ReadEntries(WordApplication app, string path)
        {
            WordDocument doc = app.Documents.Open(path, ReadOnly: true, Visible: false);
            try
            {
                /*object findText = "А4765";
                object replaceText = "############";

                doc.Content.Find.Execute(
                                 FindText: findText,
                                 MatchCase: false,
                                 MatchWholeWord: false,
                                 MatchWildcards: false,
                                 MatchSoundsLike: false,
                                 MatchAllWordForms: false,
                                 Forward: true, //this may be the one
                                 Wrap: false,
                                 Format: false,
                                 ReplaceWith: replaceText,
                                 Replace: WdReplace.wdReplaceAll
                                 );*/

            }
            catch { }
            return doc;
        }
        public static void WriteEntries(WordApplication app, WordDocument doc, string path)
        {
            var newDoc = app.Documents.Add();
            try
            {
                doc.ActiveWindow.Selection.WholeStory();
                doc.ActiveWindow.Selection.Copy();

                //newDoc.Content.InsertAfter(doc.Content.Text.Replace("А4765", "###########"));
                newDoc.ActiveWindow.Selection.Paste();
                newDoc.Content.Find.Execute(
                                 FindText: "А4765",
                                 MatchCase: false,
                                 MatchWholeWord: false,
                                 MatchWildcards: false,
                                 MatchSoundsLike: false,
                                 MatchAllWordForms: false,
                                 Forward: true, //this may be the one
                                 Wrap: false,
                                 Format: false,
                                 ReplaceWith: "###########",
                                 Replace: WdReplace.wdReplaceAll
                                 );

                newDoc.SaveAs(path);
                newDoc.Close(true);
                doc.Close(false);
            }
            catch { }
            Marshal.FinalReleaseComObject(doc);
            Marshal.FinalReleaseComObject(newDoc);
        }

        /// <summary>
        /// DO NOT WORK
        /// </summary>
        public static void WriteEntriesContent(WordApplication app, WordDocument doc, string path)
        {
            var newDoc = app.Documents.Add();
            try
            {
                doc.Content.Copy();
                newDoc.Content.PasteSpecial(DataType: WdPasteOptions.wdKeepSourceFormatting);

                newDoc.Content.Find.ClearFormatting();
                newDoc.Content.Find.Replacement.ClearFormatting();

                newDoc.Content.Find.Execute(
                                 FindText: "А4765",
                                 MatchCase: false,
                                 MatchWholeWord: false,
                                 MatchWildcards: false,
                                 MatchSoundsLike: false,
                                 MatchAllWordForms: false,
                                 Forward: false, //this may be the one
                                 Wrap: false,
                                 Format: false,
                                 ReplaceWith: "###########",
                                 Replace: WdReplace.wdReplaceAll
                                 );

                newDoc.SaveAs(path);
                newDoc.Close(true);
                doc.Close(false);
            }
            catch { }
            Marshal.FinalReleaseComObject(doc);
            Marshal.FinalReleaseComObject(newDoc);
        }

        #endregion Raw Entries Read/Write
    }
}
