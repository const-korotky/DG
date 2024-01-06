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
            PoplateDataToSectorItem();
            Result = DialogResult.OK;
            Close();
        }
        private void btn_SaveWord_Click(object sender, EventArgs e)
        {
            PoplateDataToSectorItem();
            Result = DialogResult.OK;
            CreateWordDoc = true;
            Close();
        }
        private void PoplateDataToSectorItem()
        {
            _sectorItem.OrderName = txb_OrderName.Text;
            _sectorItem.Description = txb_Description.Text;
            _sectorItem.Note = txb_note.Text;

            if (!dt_StartDate.Checked) { _sectorItem.StartDate = null; } else { _sectorItem.StartDate = dt_StartDate.Value; }
            if (!dt_EndDate.Checked) { _sectorItem.EndDate = null; } else { _sectorItem.EndDate = dt_EndDate.Value; }

            _sectorItem.LocationName = cmb_Location.Text;
            _sectorItem.ZoneName = cmb_Zone.Text;

            var selectedItems = lst_Active.Items.Cast<string>().ToArray();
            _sectorItem.Persons = string.Join($",{Environment.NewLine}", selectedItems);

            var locationName = _datastore.Location.FirstOrDefault(i => (i.Name == _sectorItem.LocationName));
            if (locationName == null)
            {
                _datastore.Location.Add(new Location()
                {
                    IsNew = true,
                    Name = _sectorItem.LocationName,
                });
            }

            var orderName = _datastore.Order.FirstOrDefault(i => (i.Name == _sectorItem.OrderName));
            if (orderName == null)
            {
                _datastore.Order.Add(new Order()
                {
                    IsNew = true,
                    Name = _sectorItem.OrderName,
                    Description = _sectorItem.Description,
                    CodeName = _sectorItem.Note,
                });
            }
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

        private void dt_EndDate_ValueChanged(object sender, EventArgs e)
        {
            if (!dt_EndDate.Checked)
            {
                dt_EndDate.CustomFormat = " ";
                dt_EndDate.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dt_EndDate.CustomFormat = null;
                dt_EndDate.Format = DateTimePickerFormat.Long;
            }
        }
        private void dt_startDate_ValueChanged(object sender, EventArgs e)
        {
            if (!dt_StartDate.Checked)
            {
                dt_StartDate.CustomFormat = " ";
                dt_StartDate.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dt_StartDate.CustomFormat = null;
                dt_StartDate.Format = DateTimePickerFormat.Long;
            }
        }
    }
}
