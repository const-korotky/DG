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
    public partial class EditInactiveForm : Form
    {
        private readonly InactiveItem _inactiveItem;

        public DialogResult Result { get; private set; }

        public EditInactiveForm(InactiveItem inactiveItem)
        {
            _inactiveItem = inactiveItem;

            InitializeComponent();
            InitializeComponentData();
        }

        private void InitializeComponentData()
        {
            lb_personName.Text = _inactiveItem.PersonName;
            txb_Note.Text = _inactiveItem.Note;

            if (!_inactiveItem.StartDate.HasValue)
            {
                dt_StartDate.Checked = false;
                dt_StartDate.Value = DateTime.Today;
            }
            else
            {
                dt_StartDate.Checked = true;
                dt_StartDate.Value = _inactiveItem.StartDate.Value;
            }
            if (!_inactiveItem.EndDate.HasValue)
            {
                dt_EndDate.Checked = false;
                dt_EndDate.Value = DateTime.Today;
            }
            else
            {
                dt_EndDate.Checked = true;
                dt_EndDate.Value = _inactiveItem.EndDate.Value;
            }
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
        private void dt_StartDate_ValueChanged(object sender, EventArgs e)
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            PoplateDataToInactiveItem();
            Result = DialogResult.OK;
            Close();
        }

        private void PoplateDataToInactiveItem()
        {
            _inactiveItem.Note = txb_Note.Text;

            if (!dt_StartDate.Checked) { _inactiveItem.StartDate = null; }
            else { _inactiveItem.StartDate = dt_StartDate.Value; }
            if (!dt_EndDate.Checked) { _inactiveItem.EndDate = null; }
            else { _inactiveItem.EndDate = dt_EndDate.Value; }

            if (!_inactiveItem.IsNew)
            {
                _inactiveItem.IsDirty = true;
            }
        }
    }
}
