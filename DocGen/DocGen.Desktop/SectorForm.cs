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
        private Datastore _datastore { get; set; }
        private ExcelProcessor _excelProcessor { get; set; }

        public SectorForm(ExcelProcessor excelProcessor, Datastore datastore)
        {
            InitializeComponent();

            _datastore = datastore;
            _excelProcessor = excelProcessor;

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
