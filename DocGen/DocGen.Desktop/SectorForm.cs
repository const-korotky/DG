using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocGen.Data;

namespace DocGen.Desktop
{
    public partial class SectorForm : Form
    {
        private Datastore Datastore { get; set; }

        public SectorForm(Datastore datastore)
        {
            InitializeComponent();

            Datastore = datastore;

            dataGrid_sector.DataSource
                = new BindingList<SectorGridDataSourceItem>(datastore.SectorItems.Select(
                    i => new SectorGridDataSourceItem
                    {
                        OrderName = i.Order?.Name,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        Persons = string.Join($",{Environment.NewLine}", i.Persons.Select(p => p.Name).ToArray()),
                        LocationName = i.Location?.Name,
                        Description = i.Description,
                        Note = i.Note,
                        ZoneName = i.Zone?.Name
                    }).ToList());
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
            var data = (dataGrid_sector.DataSource as BindingList<SectorGridDataSourceItem>);

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
    }



    public class SectorGridDataSourceItem
    {
        protected static int count = 0;

        public SectorGridDataSourceItem() { ID = ++count; }

        public bool IsNew { get; set; } = false;
        public bool IsDirty { get; set; } = false;

        public int ID { get; set; }

        public string OrderName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Persons { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string ZoneName { get; set; }
    }
}
