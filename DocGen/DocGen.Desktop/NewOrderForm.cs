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
using DocGen.Data.Model;

namespace DocGen.Desktop
{
    public partial class NewOrderForm : Form
    {
        private SectorItem _sectorItem;

        private List<string> _personNames;
        private Datastore _datastore;

        public bool CreateWordDoc { get; private set; }
        public DialogResult Result { get; private set; }

        public NewOrderForm(Datastore datastore, SectorItem sectorItem)
        {
            _sectorItem = sectorItem;
            _datastore = datastore;
            InitializeComponent();

            _datastore.Location.ForEach(i => cmb_Location.Items.Add(i.Name));

            _datastore.Zone.ForEach(i => cmb_Zone.Items.Add(i.Name));
            if (cmb_Zone.Items.Count > 0)
            {
                cmb_Zone.SelectedIndex = 1;
            }

            _personNames = _datastore.Person.Select(i => i.Name).ToList();
            lst_Inactive.Items.AddRange(_personNames.ToArray());
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            _sectorItem.OrderName = txb_OrderName.Text;
            _sectorItem.Description = txb_Description.Text;
            _sectorItem.Note = txb_note.Text;
            _sectorItem.StartDate = dt_startDate.Value;
            _sectorItem.EndDate = dt_EndDate.Value;
            _sectorItem.LocationName = cmb_Location.Text;
            _sectorItem.ZoneName = cmb_Zone.Text;

            var selectedItems = lst_Active.Items.Cast<string>().ToArray();
            _sectorItem.Persons = string.Join($",{Environment.NewLine}", selectedItems);

            Result = DialogResult.OK;
            Close();
        }
        private void btn_SaveWord_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            CreateWordDoc = true;
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            var selectedItems = lst_Inactive.CheckedItems.Cast<string>().ToArray();
            foreach (var item in selectedItems)
            {
                lst_Inactive.Items.Remove(item);
            }
            lst_Active.Items.AddRange(selectedItems);
        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            var selectedItems = lst_Active.CheckedItems.Cast<string>().ToArray();
            foreach (var item in selectedItems)
            {
                lst_Active.Items.Remove(item);
            }
            var activeItems = lst_Active.Items.Cast<string>().ToArray();

            lst_Inactive.Items.Clear();
            lst_Inactive.Refresh();
            string[] items = _personNames.Where(i => !activeItems.Contains(i)).ToArray();
            lst_Inactive.Items.AddRange(items);
            lst_Inactive.Refresh();
        }
    }
}
