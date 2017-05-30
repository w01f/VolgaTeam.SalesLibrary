namespace SalesLibraries.CommonGUI.RetractableBar
{
	partial class RetractableBarControl
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
			this.pnClosed = new DevExpress.XtraEditors.PanelControl();
			this.pnAdditionalButtons = new System.Windows.Forms.Panel();
			this.simpleButtonExpand = new DevExpress.XtraEditors.SimpleButton();
			this.pnOpened = new DevExpress.XtraEditors.PanelControl();
			this.pnContent = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.pnHeaderContent = new System.Windows.Forms.Panel();
			this.simpleButtonCollapse = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).BeginInit();
			this.pnClosed.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).BeginInit();
			this.pnOpened.SuspendLayout();
			this.pnTop.SuspendLayout();
			this.pnHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnClosed
			// 
			this.pnClosed.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnClosed.Controls.Add(this.pnAdditionalButtons);
			this.pnClosed.Controls.Add(this.simpleButtonExpand);
			this.pnClosed.Location = new System.Drawing.Point(6, 3);
			this.pnClosed.Name = "pnClosed";
			this.pnClosed.Padding = new System.Windows.Forms.Padding(2);
			this.pnClosed.Size = new System.Drawing.Size(55, 151);
			this.pnClosed.TabIndex = 0;
			this.pnClosed.Visible = false;
			// 
			// pnAdditionalButtons
			// 
			this.pnAdditionalButtons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnAdditionalButtons.Location = new System.Drawing.Point(4, 40);
			this.pnAdditionalButtons.Name = "pnAdditionalButtons";
			this.pnAdditionalButtons.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.pnAdditionalButtons.Size = new System.Drawing.Size(47, 107);
			this.pnAdditionalButtons.TabIndex = 1;
			this.pnAdditionalButtons.Resize += new System.EventHandler(this.pnAdditionalButtons_Resize);
			// 
			// simpleButtonExpand
			// 
			this.simpleButtonExpand.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonExpand.Dock = System.Windows.Forms.DockStyle.Top;
			this.simpleButtonExpand.Image = global::SalesLibraries.CommonGUI.Properties.Resources.RetractableBarExpand;
			this.simpleButtonExpand.Location = new System.Drawing.Point(4, 4);
			this.simpleButtonExpand.Name = "simpleButtonExpand";
			this.simpleButtonExpand.Size = new System.Drawing.Size(47, 36);
			this.simpleButtonExpand.TabIndex = 0;
			this.simpleButtonExpand.ToolTip = "Expand bar";
			this.simpleButtonExpand.Click += new System.EventHandler(this.simpleButtonExpand_Click);
			// 
			// pnOpened
			// 
			this.pnOpened.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnOpened.Controls.Add(this.pnContent);
			this.pnOpened.Controls.Add(this.pnTop);
			this.pnOpened.Location = new System.Drawing.Point(6, 160);
			this.pnOpened.Name = "pnOpened";
			this.pnOpened.Size = new System.Drawing.Size(219, 160);
			this.pnOpened.TabIndex = 1;
			// 
			// pnContent
			// 
			this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContent.Location = new System.Drawing.Point(2, 42);
			this.pnContent.Name = "pnContent";
			this.pnContent.Size = new System.Drawing.Size(215, 116);
			this.pnContent.TabIndex = 1;
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.pnHeader);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(2, 2);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(215, 40);
			this.pnTop.TabIndex = 0;
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.pnHeaderContent);
			this.pnHeader.Controls.Add(this.simpleButtonCollapse);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Margin = new System.Windows.Forms.Padding(0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Padding = new System.Windows.Forms.Padding(2);
			this.pnHeader.Size = new System.Drawing.Size(215, 40);
			this.pnHeader.TabIndex = 2;
			// 
			// pnHeaderContent
			// 
			this.pnHeaderContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnHeaderContent.Location = new System.Drawing.Point(49, 2);
			this.pnHeaderContent.Name = "pnHeaderContent";
			this.pnHeaderContent.Size = new System.Drawing.Size(164, 36);
			this.pnHeaderContent.TabIndex = 2;
			// 
			// simpleButtonCollapse
			// 
			this.simpleButtonCollapse.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonCollapse.Dock = System.Windows.Forms.DockStyle.Left;
			this.simpleButtonCollapse.Image = global::SalesLibraries.CommonGUI.Properties.Resources.RetractableBarCollapse;
			this.simpleButtonCollapse.Location = new System.Drawing.Point(2, 2);
			this.simpleButtonCollapse.Name = "simpleButtonCollapse";
			this.simpleButtonCollapse.Size = new System.Drawing.Size(47, 36);
			this.simpleButtonCollapse.TabIndex = 1;
			this.simpleButtonCollapse.ToolTip = "Collapse bar";
			this.simpleButtonCollapse.Click += new System.EventHandler(this.simpleButtonCollapse_Click);
			// 
			// RetractableBarControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnOpened);
			this.Controls.Add(this.pnClosed);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "RetractableBarControl";
			this.Size = new System.Drawing.Size(359, 388);
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).EndInit();
			this.pnClosed.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).EndInit();
			this.pnOpened.ResumeLayout(false);
			this.pnTop.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.PanelControl pnClosed;
		protected DevExpress.XtraEditors.PanelControl pnOpened;
		protected DevExpress.XtraEditors.SimpleButton simpleButtonExpand;
		protected System.Windows.Forms.Panel pnTop;
		protected System.Windows.Forms.Panel pnContent;
		protected DevExpress.XtraEditors.SimpleButton simpleButtonCollapse;
		private System.Windows.Forms.Panel pnAdditionalButtons;
		private System.Windows.Forms.Panel pnHeader;
		protected System.Windows.Forms.Panel pnHeaderContent;

	}
}
