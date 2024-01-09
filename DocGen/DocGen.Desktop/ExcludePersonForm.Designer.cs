namespace DocGen.Desktop
{
    partial class ExcludePersonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.dt_Exclude = new System.Windows.Forms.DateTimePicker();
            this.tx_Note = new System.Windows.Forms.RichTextBox();
            this.cmbx_Reason = new System.Windows.Forms.ComboBox();
            this.lst_Active = new System.Windows.Forms.CheckedListBox();
            this.lst_Inactive = new System.Windows.Forms.CheckedListBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.lst_Inactive);
            this.groupBox1.Controls.Add(this.lst_Active);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(808, 362);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ФІО";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dt_Exclude);
            this.groupBox2.Location = new System.Drawing.Point(12, 380);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 76);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Дата";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbx_Reason);
            this.groupBox3.Location = new System.Drawing.Point(12, 462);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(343, 79);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Причина";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tx_Note);
            this.groupBox4.Location = new System.Drawing.Point(456, 389);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(364, 152);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Примітка";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(191, 556);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(164, 35);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Скасувати";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(12, 556);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(173, 35);
            this.btn_OK.TabIndex = 5;
            this.btn_OK.Text = "Вилучити";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // dt_Exclude
            // 
            this.dt_Exclude.Location = new System.Drawing.Point(6, 34);
            this.dt_Exclude.Name = "dt_Exclude";
            this.dt_Exclude.Size = new System.Drawing.Size(331, 26);
            this.dt_Exclude.TabIndex = 0;
            // 
            // tx_Note
            // 
            this.tx_Note.Location = new System.Drawing.Point(6, 25);
            this.tx_Note.MaximumSize = new System.Drawing.Size(352, 121);
            this.tx_Note.MinimumSize = new System.Drawing.Size(352, 121);
            this.tx_Note.Name = "tx_Note";
            this.tx_Note.Size = new System.Drawing.Size(352, 121);
            this.tx_Note.TabIndex = 0;
            this.tx_Note.Text = "";
            // 
            // cmbx_Reason
            // 
            this.cmbx_Reason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbx_Reason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbx_Reason.FormattingEnabled = true;
            this.cmbx_Reason.Items.AddRange(new object[] {
            "Відпустка",
            "ППД",
            "Госпіталізація",
            "ВЛК",
            "СЗЧ",
            "Інше"});
            this.cmbx_Reason.Location = new System.Drawing.Point(6, 35);
            this.cmbx_Reason.Name = "cmbx_Reason";
            this.cmbx_Reason.Size = new System.Drawing.Size(331, 28);
            this.cmbx_Reason.TabIndex = 0;
            // 
            // lst_Active
            // 
            this.lst_Active.FormattingEnabled = true;
            this.lst_Active.Location = new System.Drawing.Point(6, 30);
            this.lst_Active.Name = "lst_Active";
            this.lst_Active.Size = new System.Drawing.Size(337, 326);
            this.lst_Active.TabIndex = 0;
            // 
            // lst_Inactive
            // 
            this.lst_Inactive.FormattingEnabled = true;
            this.lst_Inactive.Location = new System.Drawing.Point(455, 30);
            this.lst_Inactive.Name = "lst_Inactive";
            this.lst_Inactive.Size = new System.Drawing.Size(347, 326);
            this.lst_Inactive.TabIndex = 1;
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.btn_add.Location = new System.Drawing.Point(349, 198);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(100, 35);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "<<";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.btn_remove.Location = new System.Drawing.Point(349, 239);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(100, 35);
            this.btn_remove.TabIndex = 3;
            this.btn_remove.Text = ">>";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // ExcludePersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 608);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(854, 664);
            this.MinimumSize = new System.Drawing.Size(854, 664);
            this.Name = "ExcludePersonForm";
            this.Text = "Вилучення";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.DateTimePicker dt_Exclude;
        private System.Windows.Forms.ComboBox cmbx_Reason;
        private System.Windows.Forms.RichTextBox tx_Note;
        private System.Windows.Forms.CheckedListBox lst_Active;
        private System.Windows.Forms.CheckedListBox lst_Inactive;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_add;
    }
}