namespace ScreenTime
{
    partial class SettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.settingsLbl = new System.Windows.Forms.Label();
            this.dbButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataRemover = new System.Windows.Forms.Button();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.dayUsageLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // settingsLbl
            // 
            this.settingsLbl.AutoSize = true;
            this.settingsLbl.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsLbl.Location = new System.Drawing.Point(350, 0);
            this.settingsLbl.Name = "settingsLbl";
            this.settingsLbl.Size = new System.Drawing.Size(86, 25);
            this.settingsLbl.TabIndex = 0;
            this.settingsLbl.Text = "Reports";
            this.settingsLbl.Click += new System.EventHandler(this.settingsLbl_Click);
            // 
            // dbButton
            // 
            this.dbButton.Location = new System.Drawing.Point(612, 219);
            this.dbButton.Name = "dbButton";
            this.dbButton.Size = new System.Drawing.Size(75, 23);
            this.dbButton.TabIndex = 1;
            this.dbButton.Text = "Print db";
            this.dbButton.UseVisualStyleBackColor = true;
            this.dbButton.Click += new System.EventHandler(this.dbButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(512, 248);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(279, 150);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataRemover
            // 
            this.dataRemover.Location = new System.Drawing.Point(716, 219);
            this.dataRemover.Name = "dataRemover";
            this.dataRemover.Size = new System.Drawing.Size(75, 23);
            this.dataRemover.TabIndex = 3;
            this.dataRemover.Text = "remove db";
            this.dataRemover.UseVisualStyleBackColor = true;
            this.dataRemover.Click += new System.EventHandler(this.dataRemover_Click);
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(564, 21);
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 5;
            this.calendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendar_DateChanged);
            // 
            // dayUsageLbl
            // 
            this.dayUsageLbl.AutoSize = true;
            this.dayUsageLbl.Location = new System.Drawing.Point(4, 47);
            this.dayUsageLbl.Name = "dayUsageLbl";
            this.dayUsageLbl.Size = new System.Drawing.Size(276, 13);
            this.dayUsageLbl.TabIndex = 6;
            this.dayUsageLbl.Text = "Select a date from the calender to see stats from that day";
            this.dayUsageLbl.Click += new System.EventHandler(this.dayUsageLbl_Click);
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dayUsageLbl);
            this.Controls.Add(this.calendar);
            this.Controls.Add(this.dataRemover);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dbButton);
            this.Controls.Add(this.settingsLbl);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(800, 460);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label settingsLbl;
        private System.Windows.Forms.Button dbButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button dataRemover;
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.Label dayUsageLbl;
    }
}
