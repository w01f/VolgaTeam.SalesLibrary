namespace SalesLibraries.BatchTagger
{
	partial class FormMain
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
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnCustomFilter = new System.Windows.Forms.Panel();
			this.pnFilterButtons = new System.Windows.Forms.Panel();
			this.buttonXLoadData = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabControlLibraries = new DevExpress.XtraTab.XtraTabControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.pnFilterButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlLibraries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.splitContainerControl.Appearance.ForeColor = System.Drawing.Color.Black;
			this.splitContainerControl.Appearance.Options.UseBackColor = true;
			this.splitContainerControl.Appearance.Options.UseForeColor = true;
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.splitContainerControl.Panel1.Appearance.ForeColor = System.Drawing.Color.Black;
			this.splitContainerControl.Panel1.Appearance.Options.UseBackColor = true;
			this.splitContainerControl.Panel1.Appearance.Options.UseForeColor = true;
			this.splitContainerControl.Panel1.Controls.Add(this.pnCustomFilter);
			this.splitContainerControl.Panel1.Controls.Add(this.pnFilterButtons);
			this.splitContainerControl.Panel1.MinSize = 300;
			this.splitContainerControl.Panel1.Padding = new System.Windows.Forms.Padding(5);
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.splitContainerControl.Panel2.Appearance.ForeColor = System.Drawing.Color.Black;
			this.splitContainerControl.Panel2.Appearance.Options.UseBackColor = true;
			this.splitContainerControl.Panel2.Appearance.Options.UseForeColor = true;
			this.splitContainerControl.Panel2.Controls.Add(this.xtraTabControlLibraries);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(870, 505);
			this.splitContainerControl.SplitterPosition = 250;
			this.splitContainerControl.TabIndex = 18;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// pnCustomFilter
			// 
			this.pnCustomFilter.BackColor = System.Drawing.Color.Transparent;
			this.pnCustomFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnCustomFilter.ForeColor = System.Drawing.Color.Black;
			this.pnCustomFilter.Location = new System.Drawing.Point(5, 44);
			this.pnCustomFilter.Name = "pnCustomFilter";
			this.pnCustomFilter.Size = new System.Drawing.Size(290, 456);
			this.pnCustomFilter.TabIndex = 18;
			// 
			// pnFilterButtons
			// 
			this.pnFilterButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnFilterButtons.Controls.Add(this.buttonXLoadData);
			this.pnFilterButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnFilterButtons.ForeColor = System.Drawing.Color.Black;
			this.pnFilterButtons.Location = new System.Drawing.Point(5, 5);
			this.pnFilterButtons.Name = "pnFilterButtons";
			this.pnFilterButtons.Size = new System.Drawing.Size(290, 39);
			this.pnFilterButtons.TabIndex = 19;
			// 
			// buttonXLoadData
			// 
			this.buttonXLoadData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLoadData.CausesValidation = false;
			this.buttonXLoadData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLoadData.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXLoadData.Location = new System.Drawing.Point(7, 5);
			this.buttonXLoadData.Name = "buttonXLoadData";
			this.buttonXLoadData.Size = new System.Drawing.Size(276, 27);
			this.buttonXLoadData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLoadData.TabIndex = 16;
			this.buttonXLoadData.Text = "Load Data";
			this.buttonXLoadData.TextColor = System.Drawing.Color.Black;
			this.buttonXLoadData.Click += new System.EventHandler(this.buttonXLoadData_Click);
			// 
			// xtraTabControlLibraries
			// 
			this.xtraTabControlLibraries.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.xtraTabControlLibraries.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlLibraries.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlLibraries.Appearance.Options.UseBackColor = true;
			this.xtraTabControlLibraries.Appearance.Options.UseFont = true;
			this.xtraTabControlLibraries.Appearance.Options.UseForeColor = true;
			this.xtraTabControlLibraries.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlLibraries.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlLibraries.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlLibraries.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlLibraries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlLibraries.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlLibraries.Name = "xtraTabControlLibraries";
			this.xtraTabControlLibraries.Size = new System.Drawing.Size(558, 505);
			this.xtraTabControlLibraries.TabIndex = 3;
			this.xtraTabControlLibraries.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.OnSelectedPageChanged);
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(870, 505);
			this.Controls.Add(this.splitContainerControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Batch Tagger";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Shown += new System.EventHandler(this.OnFormShown);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.pnFilterButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlLibraries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private System.Windows.Forms.Panel pnCustomFilter;
		private System.Windows.Forms.Panel pnFilterButtons;
		private DevComponents.DotNetBar.ButtonX buttonXLoadData;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlLibraries;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
	}
}

