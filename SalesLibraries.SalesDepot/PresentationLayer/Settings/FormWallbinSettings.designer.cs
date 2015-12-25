namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	partial class FormWallbinSettings
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTabPages = new DevComponents.DotNetBar.ButtonX();
			this.buttonXComboboxes = new DevComponents.DotNetBar.ButtonX();
			this.buttonXColumns = new DevComponents.DotNetBar.ButtonX();
			this.buttonXAccordion = new DevComponents.DotNetBar.ButtonX();
			this.buttonXList = new DevComponents.DotNetBar.ButtonX();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(207, 273);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(119, 37);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 10;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXEmail
			// 
			this.buttonXEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmail.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXEmail.Location = new System.Drawing.Point(55, 273);
			this.buttonXEmail.Name = "buttonXEmail";
			this.buttonXEmail.Size = new System.Drawing.Size(119, 37);
			this.buttonXEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmail.TabIndex = 9;
			this.buttonXEmail.Text = "OK";
			this.buttonXEmail.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXTabPages
			// 
			this.buttonXTabPages.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTabPages.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTabPages.Image = global::SalesLibraries.SalesDepot.Properties.Resources.WallbinSettingsTabPages;
			this.buttonXTabPages.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXTabPages.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonXTabPages.Location = new System.Drawing.Point(12, 12);
			this.buttonXTabPages.Name = "buttonXTabPages";
			this.buttonXTabPages.Size = new System.Drawing.Size(88, 77);
			this.buttonXTabPages.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXTabPages, new DevComponents.DotNetBar.SuperTooltipInfo("Tabs", "", "Select library pages with tabs…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXTabPages.TabIndex = 11;
			this.buttonXTabPages.Text = "Tabs";
			this.buttonXTabPages.Click += new System.EventHandler(this.OnPageSelectorTypeButtonClick);
			// 
			// buttonXComboboxes
			// 
			this.buttonXComboboxes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXComboboxes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXComboboxes.Image = global::SalesLibraries.SalesDepot.Properties.Resources.WallbinSettingsComboboxes;
			this.buttonXComboboxes.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXComboboxes.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonXComboboxes.Location = new System.Drawing.Point(144, 12);
			this.buttonXComboboxes.Name = "buttonXComboboxes";
			this.buttonXComboboxes.Size = new System.Drawing.Size(88, 77);
			this.buttonXComboboxes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXComboboxes, new DevComponents.DotNetBar.SuperTooltipInfo("Dropdown", "", "Select library pages with dropdown selections…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXComboboxes.TabIndex = 12;
			this.buttonXComboboxes.Text = "Dropdown";
			this.buttonXComboboxes.Click += new System.EventHandler(this.OnPageSelectorTypeButtonClick);
			// 
			// buttonXColumns
			// 
			this.buttonXColumns.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXColumns.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXColumns.Image = global::SalesLibraries.SalesDepot.Properties.Resources.WallbinSettingsColumns;
			this.buttonXColumns.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXColumns.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonXColumns.Location = new System.Drawing.Point(12, 146);
			this.buttonXColumns.Name = "buttonXColumns";
			this.buttonXColumns.Size = new System.Drawing.Size(88, 77);
			this.buttonXColumns.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXColumns, new DevComponents.DotNetBar.SuperTooltipInfo("Window Columns", "", "Display windows in Three columns on the Page…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXColumns.TabIndex = 13;
			this.buttonXColumns.Text = "Columns";
			this.buttonXColumns.Click += new System.EventHandler(this.OnWallbinViewTypeSelectorClick);
			// 
			// buttonXAccordion
			// 
			this.buttonXAccordion.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAccordion.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAccordion.Image = global::SalesLibraries.SalesDepot.Properties.Resources.WallbinSettingsAccordion;
			this.buttonXAccordion.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXAccordion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonXAccordion.Location = new System.Drawing.Point(144, 146);
			this.buttonXAccordion.Name = "buttonXAccordion";
			this.buttonXAccordion.Size = new System.Drawing.Size(88, 77);
			this.buttonXAccordion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXAccordion, new DevComponents.DotNetBar.SuperTooltipInfo("Window Boxes", "", "Show windows as simple accordion dropdown boxes…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXAccordion.TabIndex = 14;
			this.buttonXAccordion.Text = "Boxes";
			this.buttonXAccordion.Click += new System.EventHandler(this.OnWallbinViewTypeSelectorClick);
			// 
			// buttonXList
			// 
			this.buttonXList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXList.Image = global::SalesLibraries.SalesDepot.Properties.Resources.WallbinSettingsList;
			this.buttonXList.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXList.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonXList.Location = new System.Drawing.Point(274, 146);
			this.buttonXList.Name = "buttonXList";
			this.buttonXList.Size = new System.Drawing.Size(88, 77);
			this.buttonXList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXList, new DevComponents.DotNetBar.SuperTooltipInfo("Window List", "", "Display single column window list…", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXList.TabIndex = 15;
			this.buttonXList.Text = "List";
			this.buttonXList.Click += new System.EventHandler(this.OnWallbinViewTypeSelectorClick);
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// FormWallbinSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(381, 322);
			this.Controls.Add(this.buttonXList);
			this.Controls.Add(this.buttonXAccordion);
			this.Controls.Add(this.buttonXColumns);
			this.Controls.Add(this.buttonXComboboxes);
			this.Controls.Add(this.buttonXTabPages);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXEmail);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormWallbinSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "User Preferences";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFileSettings_FormClosing);
			this.Load += new System.EventHandler(this.FormFileSettings_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXEmail;
		private DevComponents.DotNetBar.ButtonX buttonXTabPages;
		private DevComponents.DotNetBar.ButtonX buttonXComboboxes;
		private DevComponents.DotNetBar.ButtonX buttonXColumns;
		private DevComponents.DotNetBar.ButtonX buttonXAccordion;
		private DevComponents.DotNetBar.ButtonX buttonXList;
		public DevComponents.DotNetBar.SuperTooltip superTooltip;
	}
}