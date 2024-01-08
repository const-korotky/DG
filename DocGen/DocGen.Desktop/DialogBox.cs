using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocGen.Desktop
{
    public static class DialogBox
    {
        public static string ShowPrompt(IWin32Window owner, string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top=20, Text=text };
            TextBox textBox = new TextBox() { Left = 50, Top=50, Width=400 };
            Button confirmation = new Button() { Text = "Ok", Left=350, Width=100, Top=70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog(owner) == DialogResult.OK ? textBox.Text : "";
        }

        public static Form ShowInfoWithoutConfirmation(IWin32Window owner, string text)
        {
            Form form = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Інформація",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top=20, Width = 400, Text=text };
            form.Controls.Add(textLabel);
            return form;
        }


        public static DialogResult ShowInfo(IWin32Window owner, string text)
        {
            return MessageBox.Show
                ( owner
                , text
                , "Повідомлення"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Information
                , MessageBoxDefaultButton.Button1
                );
        }
        public static DialogResult ShowWarning(IWin32Window owner, string text)
        {
            return MessageBox.Show
                ( owner
                , text
                , "Увага!"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Warning
                , MessageBoxDefaultButton.Button1
                );
        }
        public static DialogResult ShowError(IWin32Window owner, string text)
        {
            return MessageBox.Show
                ( owner
                , text
                , "Помилка!"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Error
                , MessageBoxDefaultButton.Button1
                );
        }
    }
}
