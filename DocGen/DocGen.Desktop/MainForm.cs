﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocGen.Data;
using DocGen.Processor;


namespace DocGen.Desktop
{
    public partial class MainForm : Form
    {
        public const string WordFileExtentionFilter = "Word files (*.doc; *.docx)|*.doc; *.docx|All files (*.*)|*.*";
        public const string ExcelFileExtentionFilter = "Excel files (*.xls; *.xlsx; *.xlsm)|*.xls; *.xlsx; *.xlsm|All files (*.*)|*.*";

        private Datastore _datastore;

        private readonly ExcelProcessor ExcelProcessor;
        private readonly WordProcessor WordProcessor;

        public MainForm()
        {
            InitializeComponent();

            ExcelProcessor = new ExcelProcessor();
            WordProcessor = new WordProcessor();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dt_StartDate.Value = new DateTime(DateTime.Now.Year, now.Month, 1);
            dt_EndDate.Value = dt_StartDate.Value.AddMonths(1).AddDays(-1);
        }

        private void btn_open_data_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = ExcelFileExtentionFilter
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                txb_data.Text = dialog.FileName;
            }
        }
        private void btn_open_templ_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = WordFileExtentionFilter
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                txb_templ.Text = dialog.FileName;
            }
        }

        private void btn_open_raport_Click(object sender, EventArgs e)
        {
            WordProcessor.OpenDocumnet(txb_raport_path.Text);
        }
        private void btn_diagram_open_Click(object sender, EventArgs e)
        {
            ExcelProcessor.OpenDocumnet(txb_diagram_path.Text);
        }


        private void btn_generate_Click(object sender, EventArgs e)
        {
            if (!CheckFilePaths())
            {
                return;
            }

            gb_progress.Visible = true;
            btn_generate.Enabled = false;
            gb_result.Visible = false;

            ExcelProcessor.ProgressUpdatedEvent += progressUpdated_EventHandler;
            ExcelProcessor.Process(chbx_reloadDb.Checked);
            ExcelProcessor.ProgressUpdatedEvent -= progressUpdated_EventHandler;

            WordProcessor.ProgressUpdatedEvent += progressUpdated_EventHandler;
            WordProcessor.Datastore = ExcelProcessor.Datastore;
            WordProcessor.Process();
            WordProcessor.ProgressUpdatedEvent -= progressUpdated_EventHandler;

            DialogBox.ShowInfo(this, "Генерацію Рапорта завершено.");

            btn_generate.Enabled = true;
            progressBar.Value = 100;
            lb_progress.Text = "100% - операцію завершено";

            gb_result.Visible = true;
            txb_raport_path.Text = WordProcessor.DestinationFilePath;
            txb_diagram_path.Text = ExcelProcessor.DestinationFilePath;

            lb_reloadDb.Visible = ExcelProcessor.Datastore.IsLoaded;
            chbx_reloadDb.Visible = ExcelProcessor.Datastore.IsLoaded;
        }
        private void progressUpdated_EventHandler(int percentage, string message = null)
        {
            progressBar.Value = (( percentage > 100 ) ? 100 : percentage);
            if (!string.IsNullOrWhiteSpace(message))
            {
                lb_progress.Text = $"{percentage}% - {message}";
            }
            else
            {
                var text = lb_progress.Text;
                if (!string.IsNullOrEmpty(text) && text.Contains("-"))
                {
                    var split = text.Split('-');
                    lb_progress.Text = $"{percentage}% -{split[1]}";
                }
            }
        }


        private void dt_StartDate_ValueChanged(object sender, EventArgs e)
        {
            ExcelProcessor.StartDate = dt_StartDate.Value;
        }
        private void dt_EndDate_ValueChanged(object sender, EventArgs e)
        {
            ExcelProcessor.EndDate = dt_EndDate.Value.AddDays(1);
        }

        private void txb_data_TextChanged(object sender, EventArgs e)
        {
            ExcelProcessor.SourceFilePath = txb_data.Text;
        }
        private void txb_templ_TextChanged(object sender, EventArgs e)
        {
            WordProcessor.SourceFilePath = txb_templ.Text;
        }

        private void nmb_zoom_ValueChanged(object sender, EventArgs e)
        {
            ExcelProcessor.PrintScale = Convert.ToInt32(nmb_zoom.Value);
        }

        private void chbx_location_CheckedChanged(object sender, EventArgs e)
        {
            SetPrintOptions();
        }
        private void chbx_zone_CheckedChanged(object sender, EventArgs e)
        {
            SetPrintOptions();
        }
        private void SetPrintOptions()
        {
            PrintOption options = PrintOption.Order;
            if (chbx_location.Checked)
            {
                options |= PrintOption.Location;
            }
            if (chbx_zone.Checked)
            {
                options |= PrintOption.Zone;
            }
            ExcelProcessor.PrintOptions = options;
        }

        private bool CheckFilePaths()
        {
            if (!File.Exists(ExcelProcessor.SourceFilePath))
            {
                var fileName =
                    string.IsNullOrWhiteSpace(ExcelProcessor.SourceFilePath)
                        ? "Бази Даних"
                        : $"\"{ExcelProcessor.SourceFilePath}\"";
                DialogBox.ShowWarning(this, $"Файл {fileName} не існує.");
                return false;
            }
            if (!File.Exists(WordProcessor.SourceFilePath))
            {
                var fileName =
                    string.IsNullOrWhiteSpace(WordProcessor.SourceFilePath)
                        ? "Шаблона Рапорта"
                        : $"\"{WordProcessor.SourceFilePath}\"";
                DialogBox.ShowWarning(this, $"Файл {fileName} не існує.");
                return false;
            }
            return true;
        }
        private bool CheckDataStoreLoaded()
        {
            if (_datastore == null)
            {
                DialogBox.ShowWarning(this, $"Завантажте файл Бази Даних.");
                return false;
            }
            return true;
        }

        private void menuItem_Order_Click(object sender, EventArgs e)
        {
            if (CheckDataStoreLoaded())
            {
                (new SectorForm(ExcelProcessor, WordProcessor, _datastore)).ShowDialog(this);
            }
        }
        private void menuItem_Inactive_Click(object sender, EventArgs e)
        {
            if (CheckDataStoreLoaded())
            {
                (new InactiveForm(ExcelProcessor, WordProcessor, _datastore)).ShowDialog(this);
            }
        }


        private void btn_loadDb_Click(object sender, EventArgs e)
        {
            if (!File.Exists(ExcelProcessor.SourceFilePath))
            {
                var text =
                    string.IsNullOrWhiteSpace(ExcelProcessor.SourceFilePath)
                        ? "Оберіть файл Бази Даних."
                        : $"Файл \"{ExcelProcessor.SourceFilePath}\" не існує.";
                DialogBox.ShowWarning(this, text);
                return;
            }
            gb_progress.Visible = true;
            ExcelProcessor.ProgressUpdatedEvent += progressUpdated_EventHandler;
            _datastore = ExcelProcessor.LoadDatastoreOnDemand();
            ExcelProcessor.ProgressUpdatedEvent -= progressUpdated_EventHandler;
        }
    }
}
