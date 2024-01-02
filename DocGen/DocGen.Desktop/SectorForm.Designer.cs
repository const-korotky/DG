namespace DocGen.Desktop
{
    partial class SectorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGrid_sector = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZoneName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_RemoveOrder = new System.Windows.Forms.Button();
            this.btn_EditOrder = new System.Windows.Forms.Button();
            this.btn_AddOrder = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.colOrderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPersons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sectorGridDataSourceItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_sector)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sectorGridDataSourceItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid_sector
            // 
            this.dataGrid_sector.AllowUserToAddRows = false;
            this.dataGrid_sector.AllowUserToDeleteRows = false;
            this.dataGrid_sector.AutoGenerateColumns = false;
            this.dataGrid_sector.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_sector.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGrid_sector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_sector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colOrderName,
            this.colStartDate,
            this.colEndDate,
            this.colPersons,
            this.colDescription,
            this.colNote,
            this.colZoneName,
            this.colLocationName});
            this.dataGrid_sector.DataSource = this.sectorGridDataSourceItemBindingSource;
            this.dataGrid_sector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_sector.Location = new System.Drawing.Point(3, 22);
            this.dataGrid_sector.MultiSelect = false;
            this.dataGrid_sector.Name = "dataGrid_sector";
            this.dataGrid_sector.RowHeadersWidth = 62;
            this.dataGrid_sector.RowTemplate.Height = 28;
            this.dataGrid_sector.Size = new System.Drawing.Size(1061, 492);
            this.dataGrid_sector.TabIndex = 0;
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
            // colZoneName
            // 
            this.colZoneName.DataPropertyName = "ZoneName";
            this.colZoneName.HeaderText = "Зона";
            this.colZoneName.MinimumWidth = 8;
            this.colZoneName.Name = "colZoneName";
            this.colZoneName.Width = 150;
            // 
            // colLocationName
            // 
            this.colLocationName.DataPropertyName = "LocationName";
            this.colLocationName.HeaderText = "Локація";
            this.colLocationName.MinimumWidth = 8;
            this.colLocationName.Name = "colLocationName";
            this.colLocationName.Width = 150;
            // 
            // btn_RemoveOrder
            // 
            this.btn_RemoveOrder.Location = new System.Drawing.Point(329, 27);
            this.btn_RemoveOrder.Name = "btn_RemoveOrder";
            this.btn_RemoveOrder.Size = new System.Drawing.Size(154, 36);
            this.btn_RemoveOrder.TabIndex = 2;
            this.btn_RemoveOrder.Text = "Видалити";
            this.btn_RemoveOrder.UseVisualStyleBackColor = true;
            this.btn_RemoveOrder.Click += new System.EventHandler(this.btn_RemoveOrder_Click);
            // 
            // btn_EditOrder
            // 
            this.btn_EditOrder.Location = new System.Drawing.Point(169, 27);
            this.btn_EditOrder.Name = "btn_EditOrder";
            this.btn_EditOrder.Size = new System.Drawing.Size(154, 36);
            this.btn_EditOrder.TabIndex = 1;
            this.btn_EditOrder.Text = "Редагувати";
            this.btn_EditOrder.UseVisualStyleBackColor = true;
            // 
            // btn_AddOrder
            // 
            this.btn_AddOrder.Location = new System.Drawing.Point(9, 27);
            this.btn_AddOrder.Name = "btn_AddOrder";
            this.btn_AddOrder.Size = new System.Drawing.Size(154, 36);
            this.btn_AddOrder.TabIndex = 0;
            this.btn_AddOrder.Text = "Новий Наказ";
            this.btn_AddOrder.UseVisualStyleBackColor = true;
            this.btn_AddOrder.Click += new System.EventHandler(this.btn_AddOrder_Click);
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
            this.groupBox1.Controls.Add(this.btn_RemoveOrder);
            this.groupBox1.Controls.Add(this.btn_EditOrder);
            this.groupBox1.Controls.Add(this.btn_AddOrder);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.MaximumSize = new System.Drawing.Size(0, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1067, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дії";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGrid_sector);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1067, 517);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Накази";
            // 
            // colOrderName
            // 
            this.colOrderName.DataPropertyName = "OrderName";
            this.colOrderName.HeaderText = "Наказ";
            this.colOrderName.MinimumWidth = 8;
            this.colOrderName.Name = "colOrderName";
            this.colOrderName.ReadOnly = true;
            this.colOrderName.Width = 150;
            // 
            // colStartDate
            // 
            this.colStartDate.DataPropertyName = "StartDate";
            this.colStartDate.HeaderText = "Дата Початку";
            this.colStartDate.MinimumWidth = 8;
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.ReadOnly = true;
            this.colStartDate.Width = 150;
            // 
            // colEndDate
            // 
            this.colEndDate.DataPropertyName = "EndDate";
            this.colEndDate.HeaderText = "Дата Кінця";
            this.colEndDate.MinimumWidth = 8;
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.ReadOnly = true;
            this.colEndDate.Width = 150;
            // 
            // colPersons
            // 
            this.colPersons.DataPropertyName = "Persons";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colPersons.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPersons.HeaderText = "Особовий Скад";
            this.colPersons.MinimumWidth = 8;
            this.colPersons.Name = "colPersons";
            this.colPersons.ReadOnly = true;
            this.colPersons.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Опис";
            this.colDescription.MinimumWidth = 8;
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 150;
            // 
            // colNote
            // 
            this.colNote.DataPropertyName = "Note";
            this.colNote.HeaderText = "Опис для Рапорта";
            this.colNote.MinimumWidth = 8;
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            this.colNote.Width = 150;
            // 
            // sectorGridDataSourceItemBindingSource
            // 
            this.sectorGridDataSourceItemBindingSource.DataSource = typeof(DocGen.Desktop.SectorGridDataSourceItem);
            // 
            // SectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 598);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SectorForm";
            this.Text = "Наказ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_sector)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sectorGridDataSourceItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_sector;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sectorGridDataSourceItemBindingSource;
        private System.Windows.Forms.Button btn_AddOrder;
        private System.Windows.Forms.Button btn_RemoveOrder;
        private System.Windows.Forms.Button btn_EditOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPersons;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZoneName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocationName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}