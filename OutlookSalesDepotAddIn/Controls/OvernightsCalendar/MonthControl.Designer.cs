namespace OutlookSalesDepotAddIn.Controls.OvernightsCalendar
{
    partial class MonthControl
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
            this.laMonthName = new System.Windows.Forms.Label();
            this.pnDaysExternalContainer = new System.Windows.Forms.Panel();
            this.pnDaysInternalContainer = new System.Windows.Forms.Panel();
            this.pnDaysExternalContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // laMonthName
            // 
            this.laMonthName.Dock = System.Windows.Forms.DockStyle.Top;
            this.laMonthName.Location = new System.Drawing.Point(1, 1);
            this.laMonthName.Name = "laMonthName";
            this.laMonthName.Size = new System.Drawing.Size(261, 26);
            this.laMonthName.TabIndex = 0;
            this.laMonthName.Text = "label1";
            this.laMonthName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnDaysExternalContainer
            // 
            this.pnDaysExternalContainer.Controls.Add(this.pnDaysInternalContainer);
            this.pnDaysExternalContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDaysExternalContainer.Location = new System.Drawing.Point(1, 27);
            this.pnDaysExternalContainer.Name = "pnDaysExternalContainer";
            this.pnDaysExternalContainer.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.pnDaysExternalContainer.Size = new System.Drawing.Size(261, 215);
            this.pnDaysExternalContainer.TabIndex = 1;
            this.pnDaysExternalContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.Control_Paint);
            // 
            // pnDaysInternalContainer
            // 
            this.pnDaysInternalContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDaysInternalContainer.Location = new System.Drawing.Point(0, 1);
            this.pnDaysInternalContainer.Name = "pnDaysInternalContainer";
            this.pnDaysInternalContainer.Size = new System.Drawing.Size(261, 214);
            this.pnDaysInternalContainer.TabIndex = 2;
            // 
            // MonthControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.pnDaysExternalContainer);
            this.Controls.Add(this.laMonthName);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MonthControl";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(263, 243);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Control_Paint);
            this.pnDaysExternalContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laMonthName;
        private System.Windows.Forms.Panel pnDaysExternalContainer;
        private System.Windows.Forms.Panel pnDaysInternalContainer;
    }
}
