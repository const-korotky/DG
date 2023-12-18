using System;
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
using DocGen.Processor;

namespace DocGen.Desktop
{
    public partial class MainForm : Form
    {
        public const string WordFileExtentionFilter = "Word files (*.docx; *.doc)|*.docx; *.doc|All files (*.*)|*.*";
        public const string ExcelFileExtentionFilter = "Excel files (*.xlsx; *.xls)|*.xls; *.xlsx|All files (*.*)|*.*";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_open_szt_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = ExcelFileExtentionFilter
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                tbx_szt.Text = dialog.FileName;
            }
        }
        private void btn_open_data_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = ExcelFileExtentionFilter
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                tbx_data.Text = dialog.FileName;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_open_templ_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = WordFileExtentionFilter
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                tbx_templ.Text = dialog.FileName;
            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            /*var excelProcessor = new ExcelProcessor
            {
                SourceDataFilePath = tbx_data.Text,
                SheetName = cmbx_month.SelectedItem.ToString()
            };
            excelProcessor.Process(populate: true);

            var wordProcessor = new WordProcessor
            {
                TemplateFilePath = tbx_templ.Text
            };
            wordProcessor.Process(excelProcessor.BRs, excelProcessor.Entries);*/

            DialogBox.ShowInfo(this, "Генерацію Рапорта завершено.", "Інформація");

            gb_result.Visible = true;
            //txb_raport_path.Text = wordProcessor.DestinationFilePath;
        }

        private void btn_raport_open_Click(object sender, EventArgs e)
        {
            WordProcessor.OpenDocumnet(txb_raport_path.Text);
        }
    }
}
