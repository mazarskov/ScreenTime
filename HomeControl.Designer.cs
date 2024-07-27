namespace ScreenTime
{
    partial class HomeControl
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
            this.appString = new System.Windows.Forms.Label();
            this.todayUsageLbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.statusLbl = new System.Windows.Forms.Label();
            this.defaultLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // appString
            // 
            this.appString.AutoSize = true;
            this.appString.Font = new System.Drawing.Font("Sans Serif Collection", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appString.Location = new System.Drawing.Point(309, 31);
            this.appString.Name = "appString";
            this.appString.Size = new System.Drawing.Size(155, 32);
            this.appString.TabIndex = 1;
            this.appString.Text = "No app running";
            this.appString.Click += new System.EventHandler(this.appString_Click);
            // 
            // todayUsageLbl
            // 
            this.todayUsageLbl.AutoSize = true;
            this.todayUsageLbl.Location = new System.Drawing.Point(3, 50);
            this.todayUsageLbl.Name = "todayUsageLbl";
            this.todayUsageLbl.Size = new System.Drawing.Size(227, 13);
            this.todayUsageLbl.TabIndex = 3;
            this.todayUsageLbl.Text = "Press the \"Reload\" button to get todays usage";
            this.todayUsageLbl.Click += new System.EventHandler(this.todayUsageLbl_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Reload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(710, 31);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(37, 13);
            this.statusLbl.TabIndex = 17;
            this.statusLbl.Text = "Offline";
            this.statusLbl.Click += new System.EventHandler(this.statusLbl_Click);
            // 
            // defaultLbl
            // 
            this.defaultLbl.AutoSize = true;
            this.defaultLbl.Font = new System.Drawing.Font("Roboto", 15.75F);
            this.defaultLbl.Location = new System.Drawing.Point(350, 0);
            this.defaultLbl.Name = "defaultLbl";
            this.defaultLbl.Size = new System.Drawing.Size(68, 25);
            this.defaultLbl.TabIndex = 18;
            this.defaultLbl.Text = "Home";
            // 
            // HomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.defaultLbl);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.todayUsageLbl);
            this.Controls.Add(this.appString);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(800, 460);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label appString;
        private System.Windows.Forms.Label todayUsageLbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.Label defaultLbl;
    }
}
