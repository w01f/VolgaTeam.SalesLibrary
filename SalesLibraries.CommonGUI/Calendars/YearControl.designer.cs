namespace SalesLibraries.CommonGUI.Calendars
{
	sealed partial class YearControl
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnMonths = new System.Windows.Forms.Panel();
			this.pnWeekDays = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Controls.Add(this.pnMonths);
			this.pnMain.Controls.Add(this.pnWeekDays);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(777, 589);
			this.pnMain.TabIndex = 0;
			// 
			// pnMonths
			// 
			this.pnMonths.BackColor = System.Drawing.Color.Transparent;
			this.pnMonths.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMonths.Location = new System.Drawing.Point(0, 35);
			this.pnMonths.Name = "pnMonths";
			this.pnMonths.Size = new System.Drawing.Size(777, 554);
			this.pnMonths.TabIndex = 1;
			// 
			// pnWeekDays
			// 
			this.pnWeekDays.BackColor = System.Drawing.Color.Azure;
			this.pnWeekDays.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnWeekDays.Location = new System.Drawing.Point(0, 0);
			this.pnWeekDays.Name = "pnWeekDays";
			this.pnWeekDays.Size = new System.Drawing.Size(777, 35);
			this.pnWeekDays.TabIndex = 0;
			// 
			// pnEmpty
			// 
			this.pnEmpty.BackColor = System.Drawing.Color.Transparent;
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(0, 0);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(777, 589);
			this.pnEmpty.TabIndex = 1;
			// 
			// YearControl
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.AliceBlue;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.pnEmpty);
			this.Controls.Add(this.pnMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Size = new System.Drawing.Size(777, 589);
			this.pnMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnEmpty;
        private System.Windows.Forms.Panel pnMonths;
        private System.Windows.Forms.Panel pnWeekDays;
    }
}
