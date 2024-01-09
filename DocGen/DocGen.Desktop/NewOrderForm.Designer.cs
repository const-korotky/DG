namespace DocGen.Desktop
{
    partial class NewOrderForm
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
            this.txb_Note = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txb_Description = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_OrderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dt_EndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dt_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmb_Zone = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_Location = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lst_Active = new System.Windows.Forms.CheckedListBox();
            this.lst_Inactive = new System.Windows.Forms.CheckedListBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_SaveWord = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txb_Note);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txb_Description);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txb_OrderName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(940, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Загальний Опис";
            // 
            // txb_Note
            // 
            this.txb_Note.Location = new System.Drawing.Point(661, 28);
            this.txb_Note.Name = "txb_Note";
            this.txb_Note.Size = new System.Drawing.Size(273, 104);
            this.txb_Note.TabIndex = 6;
            this.txb_Note.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Опис для Рапорта:";
            // 
            // txb_Description
            // 
            this.txb_Description.Location = new System.Drawing.Point(72, 61);
            this.txb_Description.Name = "txb_Description";
            this.txb_Description.Size = new System.Drawing.Size(389, 71);
            this.txb_Description.TabIndex = 3;
            this.txb_Description.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Опис:";
            // 
            // txb_OrderName
            // 
            this.txb_OrderName.Location = new System.Drawing.Point(72, 28);
            this.txb_OrderName.Name = "txb_OrderName";
            this.txb_OrderName.Size = new System.Drawing.Size(389, 26);
            this.txb_OrderName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Назва:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dt_EndDate);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dt_StartDate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(940, 70);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Період";
            // 
            // dt_EndDate
            // 
            this.dt_EndDate.Location = new System.Drawing.Point(604, 28);
            this.dt_EndDate.Name = "dt_EndDate";
            this.dt_EndDate.ShowCheckBox = true;
            this.dt_EndDate.Size = new System.Drawing.Size(330, 26);
            this.dt_EndDate.TabIndex = 3;
            this.dt_EndDate.ValueChanged += new System.EventHandler(this.dt_EndDate_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(502, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Дата Кінця:";
            // 
            // dt_StartDate
            // 
            this.dt_StartDate.Location = new System.Drawing.Point(131, 28);
            this.dt_StartDate.Name = "dt_StartDate";
            this.dt_StartDate.ShowCheckBox = true;
            this.dt_StartDate.Size = new System.Drawing.Size(330, 26);
            this.dt_StartDate.TabIndex = 1;
            this.dt_StartDate.ValueChanged += new System.EventHandler(this.dt_StartDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Дата Початку:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmb_Zone);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmb_Location);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(940, 72);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Локація";
            // 
            // cmb_Zone
            // 
            this.cmb_Zone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_Zone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Zone.FormattingEnabled = true;
            this.cmb_Zone.Location = new System.Drawing.Point(604, 28);
            this.cmb_Zone.Name = "cmb_Zone";
            this.cmb_Zone.Size = new System.Drawing.Size(330, 28);
            this.cmb_Zone.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(547, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Зона:";
            // 
            // cmb_Location
            // 
            this.cmb_Location.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_Location.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Location.FormattingEnabled = true;
            this.cmb_Location.Location = new System.Drawing.Point(158, 28);
            this.cmb_Location.Name = "cmb_Location";
            this.cmb_Location.Size = new System.Drawing.Size(303, 28);
            this.cmb_Location.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Населений Пункт:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lst_Active);
            this.groupBox4.Controls.Add(this.lst_Inactive);
            this.groupBox4.Controls.Add(this.btn_remove);
            this.groupBox4.Controls.Add(this.btn_add);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(12, 314);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(940, 328);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Особовий Склад";
            // 
            // lst_Active
            // 
            this.lst_Active.FormattingEnabled = true;
            this.lst_Active.Location = new System.Drawing.Point(72, 25);
            this.lst_Active.Name = "lst_Active";
            this.lst_Active.Size = new System.Drawing.Size(389, 303);
            this.lst_Active.TabIndex = 8;
            // 
            // lst_Inactive
            // 
            this.lst_Inactive.FormattingEnabled = true;
            this.lst_Inactive.Location = new System.Drawing.Point(604, 25);
            this.lst_Inactive.Name = "lst_Inactive";
            this.lst_Inactive.Size = new System.Drawing.Size(330, 303);
            this.lst_Inactive.TabIndex = 7;
            // 
            // btn_remove
            // 
            this.btn_remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.btn_remove.Location = new System.Drawing.Point(467, 176);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(131, 32);
            this.btn_remove.TabIndex = 5;
            this.btn_remove.Text = ">>";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.btn_add.Location = new System.Drawing.Point(467, 138);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(131, 32);
            this.btn_add.TabIndex = 4;
            this.btn_add.Text = "<<";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(526, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Додати:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Задіяні:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(796, 648);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(156, 33);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Скасувати";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_SaveWord
            // 
            this.btn_SaveWord.Location = new System.Drawing.Point(472, 648);
            this.btn_SaveWord.Name = "btn_SaveWord";
            this.btn_SaveWord.Size = new System.Drawing.Size(156, 33);
            this.btn_SaveWord.TabIndex = 5;
            this.btn_SaveWord.Text = "Зберегти у Word";
            this.btn_SaveWord.UseVisualStyleBackColor = true;
            this.btn_SaveWord.Click += new System.EventHandler(this.btn_SaveWord_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(634, 648);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(156, 33);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Text = "Зберегти";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // NewOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 693);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_SaveWord);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(986, 749);
            this.MinimumSize = new System.Drawing.Size(986, 749);
            this.Name = "NewOrderForm";
            this.Text = "Новий Наказ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_OrderName;
        private System.Windows.Forms.RichTextBox txb_Description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txb_Note;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dt_StartDate;
        private System.Windows.Forms.DateTimePicker dt_EndDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmb_Location;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_Zone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_SaveWord;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.CheckedListBox lst_Inactive;
        private System.Windows.Forms.CheckedListBox lst_Active;
    }
}