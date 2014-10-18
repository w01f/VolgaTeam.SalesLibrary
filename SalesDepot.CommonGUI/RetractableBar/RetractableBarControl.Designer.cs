namespace SalesDepot.CommonGUI.RetractableBar
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
			this.simpleButtonCollapse = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).BeginInit();
			this.pnClosed.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).BeginInit();
			this.pnOpened.SuspendLayout();
			this.pnTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnClosed
			// 
			this.pnClosed.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnClosed.Appearance.Options.UseBackColor = true;
			this.pnClosed.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnClosed.Controls.Add(this.pnAdditionalButtons);
			this.pnClosed.Controls.Add(this.simpleButtonExpand);
			this.pnClosed.Location = new System.Drawing.Point(0, 149);
			this.pnClosed.Name = "pnClosed";
			this.pnClosed.Padding = new System.Windows.Forms.Padding(2);
			this.pnClosed.Size = new System.Drawing.Size(55, 485);
			this.pnClosed.TabIndex = 0;
			this.pnClosed.Visible = false;
			// 
			// pnAdditionalButtons
			// 
			this.pnAdditionalButtons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnAdditionalButtons.Location = new System.Drawing.Point(4, 36);
			this.pnAdditionalButtons.Name = "pnAdditionalButtons";
			this.pnAdditionalButtons.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.pnAdditionalButtons.Size = new System.Drawing.Size(47, 445);
			this.pnAdditionalButtons.TabIndex = 1;
			this.pnAdditionalButtons.Resize += new System.EventHandler(this.pnAdditionalButtons_Resize);
			// 
			// simpleButtonExpand
			// 
			this.simpleButtonExpand.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonExpand.Dock = System.Windows.Forms.DockStyle.Top;
			this.simpleButtonExpand.Image = global::SalesDepot.CommonGUI.Properties.Resources.RetractableBarExpand;
			this.simpleButtonExpand.Location = new System.Drawing.Point(4, 4);
			this.simpleButtonExpand.Name = "simpleButtonExpand";
			this.simpleButtonExpand.Size = new System.Drawing.Size(47, 32);
			this.simpleButtonExpand.TabIndex = 0;
			this.simpleButtonExpand.ToolTip = "Expand bar";
			this.simpleButtonExpand.Click += new System.EventHandler(this.simpleButtonExpand_Click);
			// 
			// pnOpened
			// 
			this.pnOpened.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnOpened.Appearance.Options.UseBackColor = true;
			this.pnOpened.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.pnOpened.Controls.Add(this.pnContent);
			this.pnOpened.Controls.Add(this.pnTop);
			this.pnOpened.Location = new System.Drawing.Point(140, 84);
			this.pnOpened.Name = "pnOpened";
			this.pnOpened.Size = new System.Drawing.Size(219, 347);
			this.pnOpened.TabIndex = 1;
			// 
			// pnContent
			// 
			this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContent.Location = new System.Drawing.Point(2, 42);
			this.pnContent.Name = "pnContent";
			this.pnContent.Size = new System.Drawing.Size(215, 303);
			this.pnContent.TabIndex = 1;
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.simpleButtonCollapse);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(2, 2);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(215, 40);
			this.pnTop.TabIndex = 0;
			// 
			// simpleButtonCollapse
			// 
			this.simpleButtonCollapse.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonCollapse.Image = global::SalesDepot.CommonGUI.Properties.Resources.RetractableBarCollapse;
			this.simpleButtonCollapse.Location = new System.Drawing.Point(4, 4);
			this.simpleButtonCollapse.Name = "simpleButtonCollapse";
			this.simpleButtonCollapse.Size = new System.Drawing.Size(45, 32);
			this.simpleButtonCollapse.TabIndex = 1;
			this.simpleButtonCollapse.ToolTip = "Collapse bar";
			this.simpleButtonCollapse.Click += new System.EventHandler(this.simpleButtonCollapse_Click);
			// 
			// RetractableBarControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pnOpened);
			this.Controls.Add(this.pnClosed);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "RetractableBarControl";
			this.Size = new System.Drawing.Size(359, 655);
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).EndInit();
			this.pnClosed.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).EndInit();
			this.pnOpened.ResumeLayout(false);
			this.pnTop.ResumeLayout(false);
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

	}
}
