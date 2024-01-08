namespace DocGen.Desktop
{
    partial class MainForm
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
            this.btn_loadDb = new System.Windows.Forms.Button();
            this.btn_open_data = new System.Windows.Forms.Button();
            this.txb_data = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_open_templ = new System.Windows.Forms.Button();
            this.txb_templ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_generate = new System.Windows.Forms.Button();
            this.gb_result = new System.Windows.Forms.GroupBox();
            this.btn_diagram_open = new System.Windows.Forms.Button();
            this.txb_diagram_path = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_raport_open = new System.Windows.Forms.Button();
            this.txb_raport_path = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gb_progress = new System.Windows.Forms.GroupBox();
            this.lb_progress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chbx_zone = new System.Windows.Forms.CheckBox();
            this.chbx_location = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chbx_order = new System.Windows.Forms.CheckBox();
            this.chbx_reloadDb = new System.Windows.Forms.CheckBox();
            this.lb_reloadDb = new System.Windows.Forms.Label();
            this.nmb_zoom = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.dt_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dt_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItem_Order = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Inactive = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gb_result.SuspendLayout();
            this.gb_progress.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_zoom)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_loadDb);
            this.groupBox1.Controls.Add(this.btn_open_data);
            this.groupBox1.Controls.Add(this.txb_data);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(899, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Зазначте шлях до даних";
            // 
            // btn_loadDb
            // 
            this.btn_loadDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_loadDb.Location = new System.Drawing.Point(753, 27);
            this.btn_loadDb.Name = "btn_loadDb";
            this.btn_loadDb.Size = new System.Drawing.Size(135, 35);
            this.btn_loadDb.TabIndex = 4;
            this.btn_loadDb.Text = "Завантажити";
            this.btn_loadDb.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_loadDb.UseVisualStyleBackColor = true;
            this.btn_loadDb.Click += new System.EventHandler(this.btn_loadDb_Click);
            // 
            // btn_open_data
            // 
            this.btn_open_data.Font = new System.Drawing.Font("Impact", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_open_data.Location = new System.Drawing.Point(711, 27);
            this.btn_open_data.Name = "btn_open_data";
            this.btn_open_data.Size = new System.Drawing.Size(36, 35);
            this.btn_open_data.TabIndex = 2;
            this.btn_open_data.Text = "...";
            this.btn_open_data.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_open_data.UseVisualStyleBackColor = true;
            this.btn_open_data.Click += new System.EventHandler(this.btn_open_data_Click);
            // 
            // txb_data
            // 
            this.txb_data.Location = new System.Drawing.Point(152, 29);
            this.txb_data.Name = "txb_data";
            this.txb_data.Size = new System.Drawing.Size(553, 26);
            this.txb_data.TabIndex = 3;
            this.txb_data.TextChanged += new System.EventHandler(this.txb_data_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "База Даних:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_open_templ);
            this.groupBox2.Controls.Add(this.txb_templ);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(899, 78);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Зазначте шлях до шаблону";
            // 
            // btn_open_templ
            // 
            this.btn_open_templ.Font = new System.Drawing.Font("Impact", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_open_templ.Location = new System.Drawing.Point(852, 31);
            this.btn_open_templ.Name = "btn_open_templ";
            this.btn_open_templ.Size = new System.Drawing.Size(36, 35);
            this.btn_open_templ.TabIndex = 6;
            this.btn_open_templ.Text = "...";
            this.btn_open_templ.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_open_templ.UseVisualStyleBackColor = true;
            this.btn_open_templ.Click += new System.EventHandler(this.btn_open_templ_Click);
            // 
            // txb_templ
            // 
            this.txb_templ.Location = new System.Drawing.Point(152, 33);
            this.txb_templ.Name = "txb_templ";
            this.txb_templ.Size = new System.Drawing.Size(694, 26);
            this.txb_templ.TabIndex = 1;
            this.txb_templ.TextChanged += new System.EventHandler(this.txb_templ_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Шаблон Рапорта:";
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(12, 369);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(135, 35);
            this.btn_generate.TabIndex = 2;
            this.btn_generate.Text = "Згенерувати";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // gb_result
            // 
            this.gb_result.Controls.Add(this.btn_diagram_open);
            this.gb_result.Controls.Add(this.txb_diagram_path);
            this.gb_result.Controls.Add(this.label6);
            this.gb_result.Controls.Add(this.btn_raport_open);
            this.gb_result.Controls.Add(this.txb_raport_path);
            this.gb_result.Controls.Add(this.label5);
            this.gb_result.Location = new System.Drawing.Point(12, 410);
            this.gb_result.Name = "gb_result";
            this.gb_result.Size = new System.Drawing.Size(899, 121);
            this.gb_result.TabIndex = 3;
            this.gb_result.TabStop = false;
            this.gb_result.Text = "Результат";
            this.gb_result.Visible = false;
            // 
            // btn_diagram_open
            // 
            this.btn_diagram_open.Location = new System.Drawing.Point(753, 68);
            this.btn_diagram_open.Name = "btn_diagram_open";
            this.btn_diagram_open.Size = new System.Drawing.Size(135, 35);
            this.btn_diagram_open.TabIndex = 6;
            this.btn_diagram_open.Text = "Відкрити";
            this.btn_diagram_open.UseVisualStyleBackColor = true;
            this.btn_diagram_open.Click += new System.EventHandler(this.btn_diagram_open_Click);
            // 
            // txb_diagram_path
            // 
            this.txb_diagram_path.Location = new System.Drawing.Point(152, 72);
            this.txb_diagram_path.Name = "txb_diagram_path";
            this.txb_diagram_path.Size = new System.Drawing.Size(586, 26);
            this.txb_diagram_path.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Діаграма:";
            // 
            // btn_raport_open
            // 
            this.btn_raport_open.Location = new System.Drawing.Point(753, 29);
            this.btn_raport_open.Name = "btn_raport_open";
            this.btn_raport_open.Size = new System.Drawing.Size(135, 35);
            this.btn_raport_open.TabIndex = 3;
            this.btn_raport_open.Text = "Відкрити";
            this.btn_raport_open.UseVisualStyleBackColor = true;
            this.btn_raport_open.Click += new System.EventHandler(this.btn_open_raport_Click);
            // 
            // txb_raport_path
            // 
            this.txb_raport_path.Location = new System.Drawing.Point(152, 33);
            this.txb_raport_path.Name = "txb_raport_path";
            this.txb_raport_path.Size = new System.Drawing.Size(586, 26);
            this.txb_raport_path.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Рапорт:";
            // 
            // gb_progress
            // 
            this.gb_progress.Controls.Add(this.lb_progress);
            this.gb_progress.Controls.Add(this.progressBar);
            this.gb_progress.Location = new System.Drawing.Point(12, 607);
            this.gb_progress.Name = "gb_progress";
            this.gb_progress.Size = new System.Drawing.Size(899, 80);
            this.gb_progress.TabIndex = 4;
            this.gb_progress.TabStop = false;
            this.gb_progress.Text = "Прогрес";
            this.gb_progress.Visible = false;
            // 
            // lb_progress
            // 
            this.lb_progress.AutoSize = true;
            this.lb_progress.Location = new System.Drawing.Point(6, 22);
            this.lb_progress.Name = "lb_progress";
            this.lb_progress.Size = new System.Drawing.Size(25, 20);
            this.lb_progress.TabIndex = 4;
            this.lb_progress.Text = "....";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 45);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(882, 23);
            this.progressBar.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbx_zone);
            this.groupBox3.Controls.Add(this.chbx_location);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.chbx_order);
            this.groupBox3.Controls.Add(this.chbx_reloadDb);
            this.groupBox3.Controls.Add(this.lb_reloadDb);
            this.groupBox3.Controls.Add(this.nmb_zoom);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dt_EndDate);
            this.groupBox3.Controls.Add(this.dt_StartDate);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(12, 212);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(899, 151);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметри";
            // 
            // chbx_zone
            // 
            this.chbx_zone.AutoSize = true;
            this.chbx_zone.Location = new System.Drawing.Point(406, 112);
            this.chbx_zone.Name = "chbx_zone";
            this.chbx_zone.Size = new System.Drawing.Size(102, 24);
            this.chbx_zone.TabIndex = 19;
            this.chbx_zone.Text = "по Зонах";
            this.chbx_zone.UseVisualStyleBackColor = true;
            this.chbx_zone.CheckedChanged += new System.EventHandler(this.chbx_zone_CheckedChanged);
            // 
            // chbx_location
            // 
            this.chbx_location.AutoSize = true;
            this.chbx_location.Location = new System.Drawing.Point(277, 112);
            this.chbx_location.Name = "chbx_location";
            this.chbx_location.Size = new System.Drawing.Size(123, 24);
            this.chbx_location.TabIndex = 18;
            this.chbx_location.Text = "по Локаціях";
            this.chbx_location.UseVisualStyleBackColor = true;
            this.chbx_location.CheckedChanged += new System.EventHandler(this.chbx_location_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Звіт-діаграми:";
            // 
            // chbx_order
            // 
            this.chbx_order.AutoSize = true;
            this.chbx_order.Checked = true;
            this.chbx_order.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_order.Enabled = false;
            this.chbx_order.Location = new System.Drawing.Point(152, 112);
            this.chbx_order.Name = "chbx_order";
            this.chbx_order.Size = new System.Drawing.Size(119, 24);
            this.chbx_order.TabIndex = 16;
            this.chbx_order.Text = "по Наказах";
            this.chbx_order.UseVisualStyleBackColor = true;
            // 
            // chbx_reloadDb
            // 
            this.chbx_reloadDb.AutoSize = true;
            this.chbx_reloadDb.Location = new System.Drawing.Point(753, 65);
            this.chbx_reloadDb.Name = "chbx_reloadDb";
            this.chbx_reloadDb.Size = new System.Drawing.Size(22, 21);
            this.chbx_reloadDb.TabIndex = 15;
            this.chbx_reloadDb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbx_reloadDb.UseVisualStyleBackColor = true;
            this.chbx_reloadDb.Visible = false;
            // 
            // lb_reloadDb
            // 
            this.lb_reloadDb.AutoSize = true;
            this.lb_reloadDb.Location = new System.Drawing.Point(559, 66);
            this.lb_reloadDb.Name = "lb_reloadDb";
            this.lb_reloadDb.Size = new System.Drawing.Size(179, 20);
            this.lb_reloadDb.TabIndex = 14;
            this.lb_reloadDb.Text = "Перезавантажити БД:";
            this.lb_reloadDb.Visible = false;
            // 
            // nmb_zoom
            // 
            this.nmb_zoom.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmb_zoom.Location = new System.Drawing.Point(753, 29);
            this.nmb_zoom.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmb_zoom.Name = "nmb_zoom";
            this.nmb_zoom.Size = new System.Drawing.Size(135, 26);
            this.nmb_zoom.TabIndex = 13;
            this.nmb_zoom.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nmb_zoom.ValueChanged += new System.EventHandler(this.nmb_zoom_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(555, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Масштаб діаграми (%):";
            // 
            // dt_EndDate
            // 
            this.dt_EndDate.Location = new System.Drawing.Point(152, 61);
            this.dt_EndDate.Name = "dt_EndDate";
            this.dt_EndDate.Size = new System.Drawing.Size(356, 26);
            this.dt_EndDate.TabIndex = 11;
            this.dt_EndDate.ValueChanged += new System.EventHandler(this.dt_EndDate_ValueChanged);
            // 
            // dt_StartDate
            // 
            this.dt_StartDate.Location = new System.Drawing.Point(152, 26);
            this.dt_StartDate.Name = "dt_StartDate";
            this.dt_StartDate.Size = new System.Drawing.Size(356, 26);
            this.dt_StartDate.TabIndex = 10;
            this.dt_StartDate.ValueChanged += new System.EventHandler(this.dt_StartDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Дата Кінця:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Дата Початку:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Order,
            this.menuItem_Inactive});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(923, 33);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItem_Order
            // 
            this.menuItem_Order.Name = "menuItem_Order";
            this.menuItem_Order.Size = new System.Drawing.Size(86, 29);
            this.menuItem_Order.Text = "Накази";
            this.menuItem_Order.Click += new System.EventHandler(this.menuItem_Order_Click);
            // 
            // menuItem_Inactive
            // 
            this.menuItem_Inactive.Name = "menuItem_Inactive";
            this.menuItem_Inactive.Size = new System.Drawing.Size(118, 29);
            this.menuItem_Inactive.Text = "Незалучені";
            this.menuItem_Inactive.Click += new System.EventHandler(this.menuItem_Inactive_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 694);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gb_progress);
            this.Controls.Add(this.gb_result);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(945, 750);
            this.MinimumSize = new System.Drawing.Size(945, 750);
            this.Name = "MainForm";
            this.Text = "Рапорт Генератор";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gb_result.ResumeLayout(false);
            this.gb_result.PerformLayout();
            this.gb_progress.ResumeLayout(false);
            this.gb_progress.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_zoom)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_data;
        private System.Windows.Forms.Button btn_open_data;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_open_templ;
        private System.Windows.Forms.TextBox txb_templ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.GroupBox gb_result;
        private System.Windows.Forms.Button btn_raport_open;
        private System.Windows.Forms.TextBox txb_raport_path;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gb_progress;
        private System.Windows.Forms.Label lb_progress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btn_diagram_open;
        private System.Windows.Forms.TextBox txb_diagram_path;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dt_EndDate;
        private System.Windows.Forms.DateTimePicker dt_StartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nmb_zoom;
        private System.Windows.Forms.CheckBox chbx_reloadDb;
        private System.Windows.Forms.Label lb_reloadDb;
        private System.Windows.Forms.CheckBox chbx_order;
        private System.Windows.Forms.CheckBox chbx_zone;
        private System.Windows.Forms.CheckBox chbx_location;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Order;
        private System.Windows.Forms.Button btn_loadDb;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Inactive;
    }
}

