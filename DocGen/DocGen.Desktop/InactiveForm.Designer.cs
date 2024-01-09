namespace DocGen.Desktop
{
    partial class InactiveForm
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
            this.components = new System.ComponentModel.Container();
            this.dataGrid_inactive = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPersonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inactiveItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.btn_Include = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Exclude = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_inactive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactiveItemBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid_inactive
            // 
            this.dataGrid_inactive.AllowUserToAddRows = false;
            this.dataGrid_inactive.AllowUserToDeleteRows = false;
            this.dataGrid_inactive.AutoGenerateColumns = false;
            this.dataGrid_inactive.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_inactive.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGrid_inactive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_inactive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colPersonName,
            this.colStartDate,
            this.colEndDate,
            this.colNote});
            this.dataGrid_inactive.DataSource = this.inactiveItemBindingSource;
            this.dataGrid_inactive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_inactive.Location = new System.Drawing.Point(3, 22);
            this.dataGrid_inactive.MultiSelect = false;
            this.dataGrid_inactive.Name = "dataGrid_inactive";
            this.dataGrid_inactive.RowHeadersWidth = 62;
            this.dataGrid_inactive.RowTemplate.Height = 28;
            this.dataGrid_inactive.Size = new System.Drawing.Size(1061, 492);
            this.dataGrid_inactive.TabIndex = 0;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.MinimumWidth = 8;
            this.colID.Name = "colID";
            this.colID.Visible = false;
            this.colID.Width = 150;
            // 
            // colPersonName
            // 
            this.colPersonName.DataPropertyName = "PersonName";
            this.colPersonName.HeaderText = "ФІО";
            this.colPersonName.MinimumWidth = 8;
            this.colPersonName.Name = "colPersonName";
            this.colPersonName.Width = 150;
            // 
            // colStartDate
            // 
            this.colStartDate.DataPropertyName = "StartDate";
            this.colStartDate.HeaderText = "Вилучений";
            this.colStartDate.MinimumWidth = 8;
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.Width = 150;
            // 
            // colEndDate
            // 
            this.colEndDate.DataPropertyName = "EndDate";
            this.colEndDate.HeaderText = "Залучений";
            this.colEndDate.MinimumWidth = 8;
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.Width = 150;
            // 
            // colNote
            // 
            this.colNote.DataPropertyName = "Note";
            this.colNote.HeaderText = "Примітка";
            this.colNote.MinimumWidth = 8;
            this.colNote.Name = "colNote";
            this.colNote.Width = 150;
            // 
            // inactiveItemBindingSource
            // 
            this.inactiveItemBindingSource.DataSource = typeof(DocGen.Data.Model.InactiveItem);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Location = new System.Drawing.Point(520, 27);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(154, 36);
            this.btn_Remove.TabIndex = 2;
            this.btn_Remove.Text = "Видалити";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Location = new System.Drawing.Point(360, 27);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(154, 36);
            this.btn_Edit.TabIndex = 1;
            this.btn_Edit.Text = "Редагувати";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // btn_Include
            // 
            this.btn_Include.Location = new System.Drawing.Point(9, 27);
            this.btn_Include.Name = "btn_Include";
            this.btn_Include.Size = new System.Drawing.Size(154, 36);
            this.btn_Include.TabIndex = 0;
            this.btn_Include.Text = "Залучити";
            this.btn_Include.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1073, 598);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Exclude);
            this.groupBox1.Controls.Add(this.btn_Remove);
            this.groupBox1.Controls.Add(this.btn_Edit);
            this.groupBox1.Controls.Add(this.btn_Include);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.MaximumSize = new System.Drawing.Size(0, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1067, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дії";
            // 
            // btn_Exclude
            // 
            this.btn_Exclude.Location = new System.Drawing.Point(169, 27);
            this.btn_Exclude.Name = "btn_Exclude";
            this.btn_Exclude.Size = new System.Drawing.Size(154, 36);
            this.btn_Exclude.TabIndex = 3;
            this.btn_Exclude.Text = "Вилучити";
            this.btn_Exclude.UseVisualStyleBackColor = true;
            this.btn_Exclude.Click += new System.EventHandler(this.btn_Exclude_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGrid_inactive);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1067, 517);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Стан О/С";
            // 
            // InactiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 598);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "InactiveForm";
            this.Text = "Незалучені";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InactiveForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_inactive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactiveItemBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_inactive;
        private System.Windows.Forms.Button btn_Include;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPersonName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.BindingSource inactiveItemBindingSource;
        private System.Windows.Forms.Button btn_Exclude;
    }
}