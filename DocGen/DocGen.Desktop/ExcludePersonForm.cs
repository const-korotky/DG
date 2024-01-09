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
    public partial class ExcludePersonForm : Form
    {
        public DialogResult Result { get; private set; }
        public List<InactiveItem> ResultItems { get; private set; }
        public string Reason { get; private set; }

        Datastore _datastore;
        private List<string> _personNames;

        public ExcludePersonForm(Datastore datastore)
        {
            _datastore = datastore;
            ResultItems = new List<InactiveItem>();
            InitializeComponent();
            InitializeComponentData();
        }

        private void InitializeComponentData()
        {
            _personNames = _datastore.Person.Select(i => i.Name).ToList();
            lst_Inactive.Items.AddRange(_personNames.ToArray());
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            var note = (!string.IsNullOrWhiteSpace(tx_Note.Text) ? tx_Note.Text : cmbx_Reason.Text);
            foreach (var item in lst_Active.Items.Cast<string>())
            {
                ResultItems.Add(
                    new InactiveItem()
                    {
                        IsNew = true,
                        PersonName = item,
                        StartDate = dt_Exclude.Value.Date,
                        Note = note,
                    });
            }
            Reason = cmbx_Reason.Text;
            Result = DialogResult.OK;
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
