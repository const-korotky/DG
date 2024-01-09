using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocGen.Data;
using DocGen.Data.Model;
using DocGen.Processor;

namespace DocGen.Desktop
{
    public partial class InactiveForm : Form
    {
        private readonly Datastore _datastore;
        private readonly ExcelProcessor _excelProcessor;
        private readonly WordProcessor _wordProcessor;

        public InactiveForm(ExcelProcessor excelProcessor, WordProcessor wordProcessor, Datastore datastore)
        {
            InitializeComponent();

            _datastore = datastore;
            _excelProcessor = excelProcessor;
            _wordProcessor = wordProcessor;

            dataGrid_inactive.DataSource = new BindingList<InactiveItem>(datastore.InactiveItems);
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            var data = (dataGrid_inactive.DataSource as BindingList<InactiveItem>);

            foreach (DataGridViewRow row in dataGrid_inactive.SelectedRows)
            {
                var id = (row.Cells["colID"].Value as int?);
                if (id.HasValue)
                {
                    var items = data.Where(i => i.ID == id.Value).ToList();
                    foreach (var item in items)
                    {
                        data.Remove(item);
                    }
                }
            }
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            var data = (dataGrid_inactive.DataSource as BindingList<InactiveItem>);
            var selecteRow = dataGrid_inactive.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selecteRow == null)
            {
                return;
            }
            var id = (selecteRow.Cells["colID"].Value as int?);
            if (!id.HasValue)
            {
                return;
            }
            var inactiveItem = data.FirstOrDefault(i => (i.ID == id.Value));

            var editInactiveForm = new EditInactiveForm(inactiveItem);
            editInactiveForm.ShowDialog(this);
        }

        private void InactiveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var saveDataDialog = DialogBox.ShowInfoWithoutConfirmation(this, "Збереження даних....");
            saveDataDialog.Shown += SaveDataDialog_Shown;
            saveDataDialog.ShowDialog(this);
        }
        private void SaveDataDialog_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            //_excelProcessor.SaveDatastoreOnDemand(_datastore);
            (sender as Form).Close();
        }

        private void btn_Exclude_Click(object sender, EventArgs e)
        {
            var excludePersonForm = new ExcludePersonForm(_datastore);
            excludePersonForm.ShowDialog(this);

            if (excludePersonForm.Result != DialogResult.OK)
            {
                return;
            }
            var data = (dataGrid_inactive.DataSource as BindingList<InactiveItem>);
            foreach (var item in excludePersonForm.ResultItems)
            {
                data.Insert(0, item);
            }

            _wordProcessor.OpenDocumnet(_wordProcessor.CreateExcludeDocument(_datastore, excludePersonForm.ResultItems));
        }
    }
}
