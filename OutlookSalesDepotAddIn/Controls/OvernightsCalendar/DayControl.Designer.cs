namespace OutlookSalesDepotAddIn.Controls.OvernightsCalendar
{
	sealed partial class DayControl
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemAttach = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAttach,
            this.toolStripSeparator,
            this.toolStripMenuItemOpen});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(250, 86);
			// 
			// toolStripMenuItemAttach
			// 
			this.toolStripMenuItemAttach.Image = global::OutlookSalesDepotAddIn.Properties.Resources.AttachSmall;
			this.toolStripMenuItemAttach.Name = "toolStripMenuItemAttach";
			this.toolStripMenuItemAttach.Size = new System.Drawing.Size(249, 38);
			this.toolStripMenuItemAttach.Text = "Attach this Overnight to email";
			this.toolStripMenuItemAttach.Click += new System.EventHandler(this.toolStripMenuItemAttach_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(246, 6);
			// 
			// toolStripMenuItemOpen
			// 
			this.toolStripMenuItemOpen.Image = global::OutlookSalesDepotAddIn.Properties.Resources.OpenCalendarSmall;
			this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			this.toolStripMenuItemOpen.Size = new System.Drawing.Size(249, 38);
			this.toolStripMenuItemOpen.Text = "Open this Overnight";
			this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
			// 
			// DayControl
			// 
			this.BackColor = System.Drawing.Color.AliceBlue;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ForeColor = System.Drawing.Color.Black;
			this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
			this.MouseEnter += new System.EventHandler(this.DayControl_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.DayControl_MouseLeave);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAttach;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
    }
}
