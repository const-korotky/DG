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
    public partial class SectorForm : Form
    {
        private readonly Datastore _datastore;
        private readonly ExcelProcessor _excelProcessor;
        private readonly WordProcessor _wordProcessor;

        public SectorForm(ExcelProcessor excelProcessor, WordProcessor wordProcessor, Datastore datastore)
        {
            InitializeComponent();

            _datastore = datastore;
            _excelProcessor = excelProcessor;
            _wordProcessor = wordProcessor;

            dataGrid_sector.DataSource = new BindingList<SectorItem>(datastore.SectorItems);
        }

        /*private void btn_AddOrder_Click(object sender, EventArgs e)
        {
            (dataGrid_sector.DataSource as BindingList<SectorGridDataSourceItem>)
                .Add(new SectorGridDataSourceItem
                    {
                        OrderName = "OrderName",
                        StartDate = DateTime.Today,
                        EndDate = DateTime.Today,
                        Persons = "<empty>",
                        LocationName = "Location",
                        Description = "Description",
                        Note = "Note",
                        ZoneName = "Zone",
                        IsNew = true,
                });
        }*/

        private void btn_RemoveOrder_Click(object sender, EventArgs e)
        {
            var data = (dataGrid_sector.DataSource as BindingList<SectorItem>);

            foreach (DataGridViewRow row in dataGrid_sector.SelectedRows)
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
        private void btn_AddOrder_Click(object sender, EventArgs e)
        {
            var sectorItem = new SectorItem() { IsNew = true };
            var newOrderForm = new NewOrderForm(_datastore, sectorItem);
            newOrderForm.ShowDialog(this);

            if (newOrderForm.Result == DialogResult.OK)
            {
                var data = (dataGrid_sector.DataSource as BindingList<SectorItem>);
                data.Add(sectorItem);

                if (newOrderForm.CreateWordDoc)
                {
                    _wordProcessor.OpenDocumnet(_wordProcessor.CreateOrderDocument(_datastore, sectorItem));
                    Close();
                }
            }
        }
        private void btn_EditOrder_Click(object sender, EventArgs e)
        {
            var data = (dataGrid_sector.DataSource as BindingList<SectorItem>);
            var selecteRow = dataGrid_sector.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selecteRow == null)
            {
                return;
            }
            var id = (selecteRow.Cells["colID"].Value as int?);
            if (!id.HasValue)
            {
                return;
            }
            var sectorItem = data.FirstOrDefault(i => (i.ID == id.Value));

            var newOrderForm = new NewOrderForm(_datastore, sectorItem, isEdit: true);
            newOrderForm.ShowDialog(this);

            if ((newOrderForm.Result == DialogResult.OK) && newOrderForm.CreateWordDoc)
            {
                _wordProcessor.OpenDocumnet(_wordProcessor.CreateOrderDocument(_datastore, sectorItem));
                Close();
            }
        }

        private void SectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dialog = DialogBox.ShowInfoWithoutConfirmation(this, "Збереження даних....");
            dialog.Shown += Dialog_Shown;
            dialog.ShowDialog(this);
        }

        private void Dialog_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            //_excelProcessor.SaveDatastoreOnDemand(_datastore);
            (sender as Form).Close();
        }
    }
}
