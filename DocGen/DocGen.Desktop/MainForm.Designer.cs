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
            this.cmbx_month = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_open_data = new System.Windows.Forms.Button();
            this.btn_open_szt = new System.Windows.Forms.Button();
            this.tbx_data = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_szt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_open_templ = new System.Windows.Forms.Button();
            this.tbx_templ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_generate = new System.Windows.Forms.Button();
            this.gb_result = new System.Windows.Forms.GroupBox();
            this.btn_raport_open = new System.Windows.Forms.Button();
            this.txb_raport_path = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gb_progress = new System.Windows.Forms.GroupBox();
            this.lb_progress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gb_result.SuspendLayout();
            this.gb_progress.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbx_month);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_open_data);
            this.groupBox1.Controls.Add(this.btn_open_szt);
            this.groupBox1.Controls.Add(this.tbx_data);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbx_szt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(899, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Зазначте шлях до даних";
            // 
            // cmbx_month
            // 
            this.cmbx_month.FormattingEnabled = true;
            this.cmbx_month.Items.AddRange(new object[] {
            "Січень",
            "Лютий",
            "Березень",
            "Квітень",
            "Травень",
            "Червень",
            "Липень",
            "Серпень",
            "Вересень",
            "Жовтень",
            "Листопад",
            "Грудень"});
            this.cmbx_month.Location = new System.Drawing.Point(152, 111);
            this.cmbx_month.Name = "cmbx_month";
            this.cmbx_month.Size = new System.Drawing.Size(173, 28);
            this.cmbx_month.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Місяць:";
            // 
            // btn_open_data
            // 
            this.btn_open_data.Font = new System.Drawing.Font("Impact", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_open_data.Location = new System.Drawing.Point(852, 72);
            this.btn_open_data.Name = "btn_open_data";
            this.btn_open_data.Size = new System.Drawing.Size(36, 35);
            this.btn_open_data.TabIndex = 2;
            this.btn_open_data.Text = "...";
            this.btn_open_data.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_open_data.UseVisualStyleBackColor = true;
            this.btn_open_data.Click += new System.EventHandler(this.btn_open_data_Click);
            // 
            // btn_open_szt
            // 
            this.btn_open_szt.Font = new System.Drawing.Font("Impact", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_open_szt.Location = new System.Drawing.Point(852, 35);
            this.btn_open_szt.Name = "btn_open_szt";
            this.btn_open_szt.Size = new System.Drawing.Size(36, 35);
            this.btn_open_szt.TabIndex = 1;
            this.btn_open_szt.Text = "...";
            this.btn_open_szt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_open_szt.UseVisualStyleBackColor = true;
            this.btn_open_szt.Click += new System.EventHandler(this.btn_open_szt_Click);
            // 
            // tbx_data
            // 
            this.tbx_data.Location = new System.Drawing.Point(152, 74);
            this.tbx_data.Name = "tbx_data";
            this.tbx_data.Size = new System.Drawing.Size(694, 26);
            this.tbx_data.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дані по локаціям:";
            // 
            // tbx_szt
            // 
            this.tbx_szt.Location = new System.Drawing.Point(152, 38);
            this.tbx_szt.Name = "tbx_szt";
            this.tbx_szt.Size = new System.Drawing.Size(694, 26);
            this.tbx_szt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Штатка:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_open_templ);
            this.groupBox2.Controls.Add(this.tbx_templ);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(899, 129);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Зазначте шлях до шаблону";
            // 
            // btn_open_templ
            // 
            this.btn_open_templ.Font = new System.Drawing.Font("Impact", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_open_templ.Location = new System.Drawing.Point(852, 36);
            this.btn_open_templ.Name = "btn_open_templ";
            this.btn_open_templ.Size = new System.Drawing.Size(36, 35);
            this.btn_open_templ.TabIndex = 6;
            this.btn_open_templ.Text = "...";
            this.btn_open_templ.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_open_templ.UseVisualStyleBackColor = true;
            this.btn_open_templ.Click += new System.EventHandler(this.btn_open_templ_Click);
            // 
            // tbx_templ
            // 
            this.tbx_templ.Location = new System.Drawing.Point(152, 38);
            this.tbx_templ.Name = "tbx_templ";
            this.tbx_templ.Size = new System.Drawing.Size(694, 26);
            this.tbx_templ.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Шаблон Рапорта:";
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(12, 322);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(135, 35);
            this.btn_generate.TabIndex = 2;
            this.btn_generate.Text = "Згенерувати";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // gb_result
            // 
            this.gb_result.Controls.Add(this.btn_raport_open);
            this.gb_result.Controls.Add(this.txb_raport_path);
            this.gb_result.Controls.Add(this.label5);
            this.gb_result.Location = new System.Drawing.Point(12, 363);
            this.gb_result.Name = "gb_result";
            this.gb_result.Size = new System.Drawing.Size(899, 84);
            this.gb_result.TabIndex = 3;
            this.gb_result.TabStop = false;
            this.gb_result.Text = "Результат";
            this.gb_result.Visible = false;
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
            this.gb_progress.Location = new System.Drawing.Point(12, 572);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 664);
            this.Controls.Add(this.gb_progress);
            this.Controls.Add(this.gb_result);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(945, 720);
            this.MinimumSize = new System.Drawing.Size(945, 720);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_szt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_open_szt;
        private System.Windows.Forms.TextBox tbx_data;
        private System.Windows.Forms.Button btn_open_data;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbx_month;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_open_templ;
        private System.Windows.Forms.TextBox tbx_templ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.GroupBox gb_result;
        private System.Windows.Forms.Button btn_raport_open;
        private System.Windows.Forms.TextBox txb_raport_path;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gb_progress;
        private System.Windows.Forms.Label lb_progress;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

