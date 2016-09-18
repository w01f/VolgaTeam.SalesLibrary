namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Common
{
	partial class WidgetSettingsControl
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
			this.pbCustomWidget = new System.Windows.Forms.PictureBox();
			this.xtraTabControlWidgets = new DevExpress.XtraTab.XtraTabControl();
			this.radioButtonWidgetTypeCustom = new System.Windows.Forms.RadioButton();
			this.radioButtonWidgetTypeDisabled = new System.Windows.Forms.RadioButton();
			this.pnSearch = new System.Windows.Forms.Panel();
			this.labelControlSearchTitle = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonXSearch = new DevComponents.DotNetBar.ButtonX();
			this.textEditSearch = new DevExpress.XtraEditors.TextEdit();
			this.checkEditInvert = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.pbCustomWidget)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).BeginInit();
			this.pnSearch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditInvert.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pbCustomWidget
			// 
			this.pbCustomWidget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pbCustomWidget.BackColor = System.Drawing.Color.Transparent;
			this.pbCustomWidget.Enabled = false;
			this.pbCustomWidget.ForeColor = System.Drawing.Color.Black;
			this.pbCustomWidget.Location = new System.Drawing.Point(293, 497);
			this.pbCustomWidget.Name = "pbCustomWidget";
			this.pbCustomWidget.Size = new System.Drawing.Size(36, 36);
			this.pbCustomWidget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbCustomWidget.TabIndex = 8;
			this.pbCustomWidget.TabStop = false;
			// 
			// xtraTabControlWidgets
			// 
			this.xtraTabControlWidgets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControlWidgets.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraTabControlWidgets.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlWidgets.Appearance.Options.UseBackColor = true;
			this.xtraTabControlWidgets.Appearance.Options.UseForeColor = true;
			this.xtraTabControlWidgets.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlWidgets.Enabled = false;
			this.xtraTabControlWidgets.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlWidgets.Name = "xtraTabControlWidgets";
			this.xtraTabControlWidgets.Size = new System.Drawing.Size(920, 488);
			this.xtraTabControlWidgets.TabIndex = 9;
			// 
			// radioButtonWidgetTypeCustom
			// 
			this.radioButtonWidgetTypeCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonWidgetTypeCustom.AutoSize = true;
			this.radioButtonWidgetTypeCustom.BackColor = System.Drawing.Color.White;
			this.radioButtonWidgetTypeCustom.ForeColor = System.Drawing.Color.Black;
			this.radioButtonWidgetTypeCustom.Location = new System.Drawing.Point(172, 505);
			this.radioButtonWidgetTypeCustom.Name = "radioButtonWidgetTypeCustom";
			this.radioButtonWidgetTypeCustom.Size = new System.Drawing.Size(120, 20);
			this.radioButtonWidgetTypeCustom.TabIndex = 12;
			this.radioButtonWidgetTypeCustom.TabStop = true;
			this.radioButtonWidgetTypeCustom.Text = "Custom Widget:";
			this.radioButtonWidgetTypeCustom.UseVisualStyleBackColor = false;
			this.radioButtonWidgetTypeCustom.CheckedChanged += new System.EventHandler(this.OnWidgetTypeChanged);
			// 
			// radioButtonWidgetTypeDisabled
			// 
			this.radioButtonWidgetTypeDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonWidgetTypeDisabled.AutoSize = true;
			this.radioButtonWidgetTypeDisabled.BackColor = System.Drawing.Color.White;
			this.radioButtonWidgetTypeDisabled.ForeColor = System.Drawing.Color.Black;
			this.radioButtonWidgetTypeDisabled.Location = new System.Drawing.Point(6, 505);
			this.radioButtonWidgetTypeDisabled.Name = "radioButtonWidgetTypeDisabled";
			this.radioButtonWidgetTypeDisabled.Size = new System.Drawing.Size(87, 20);
			this.radioButtonWidgetTypeDisabled.TabIndex = 13;
			this.radioButtonWidgetTypeDisabled.TabStop = true;
			this.radioButtonWidgetTypeDisabled.Text = "No Widget";
			this.radioButtonWidgetTypeDisabled.UseVisualStyleBackColor = false;
			this.radioButtonWidgetTypeDisabled.CheckedChanged += new System.EventHandler(this.OnWidgetTypeChanged);
			// 
			// pnSearch
			// 
			this.pnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pnSearch.BackColor = System.Drawing.Color.Transparent;
			this.pnSearch.Controls.Add(this.labelControlSearchTitle);
			this.pnSearch.Controls.Add(this.buttonXSearch);
			this.pnSearch.Controls.Add(this.textEditSearch);
			this.pnSearch.Enabled = false;
			this.pnSearch.ForeColor = System.Drawing.Color.Black;
			this.pnSearch.Location = new System.Drawing.Point(555, 497);
			this.pnSearch.Name = "pnSearch";
			this.pnSearch.Size = new System.Drawing.Size(362, 36);
			this.pnSearch.TabIndex = 21;
			this.pnSearch.Click += new System.EventHandler(this.OnFormClick);
			// 
			// labelControlSearchTitle
			// 
			this.labelControlSearchTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSearchTitle.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSearchTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlSearchTitle.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSearchTitle.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.labelControlSearchTitle.Location = new System.Drawing.Point(7, 10);
			this.labelControlSearchTitle.Name = "labelControlSearchTitle";
			this.labelControlSearchTitle.Size = new System.Drawing.Size(60, 16);
			this.labelControlSearchTitle.StyleController = this.styleController;
			this.labelControlSearchTitle.TabIndex = 16;
			this.labelControlSearchTitle.Text = "Keyword:";
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.ForeColor = System.Drawing.Color.Black;
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.Appearance.Options.UseForeColor = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Options.UseForeColor = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// buttonXSearch
			// 
			this.buttonXSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSearch.Enabled = false;
			this.buttonXSearch.Location = new System.Drawing.Point(280, 6);
			this.buttonXSearch.Name = "buttonXSearch";
			this.buttonXSearch.Size = new System.Drawing.Size(77, 24);
			this.buttonXSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSearch.TabIndex = 18;
			this.buttonXSearch.Text = "Search";
			this.buttonXSearch.TextColor = System.Drawing.Color.Black;
			this.buttonXSearch.Click += new System.EventHandler(this.OnSearchButtonClick);
			// 
			// textEditSearch
			// 
			this.textEditSearch.Location = new System.Drawing.Point(72, 7);
			this.textEditSearch.Name = "textEditSearch";
			this.textEditSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditSearch.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditSearch.Properties.Appearance.Options.UseBackColor = true;
			this.textEditSearch.Properties.Appearance.Options.UseForeColor = true;
			this.textEditSearch.Size = new System.Drawing.Size(191, 22);
			this.textEditSearch.StyleController = this.styleController;
			this.textEditSearch.TabIndex = 17;
			this.textEditSearch.EditValueChanged += new System.EventHandler(this.OnSearchEditValueChanged);
			this.textEditSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyDown);
			// 
			// checkEditInvert
			// 
			this.checkEditInvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditInvert.Enabled = false;
			this.checkEditInvert.Location = new System.Drawing.Point(386, 505);
			this.checkEditInvert.Name = "checkEditInvert";
			this.checkEditInvert.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditInvert.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditInvert.Properties.Caption = "Invert";
			this.checkEditInvert.Size = new System.Drawing.Size(75, 20);
			this.checkEditInvert.StyleController = this.styleController;
			this.checkEditInvert.TabIndex = 49;
			// 
			// WidgetSettingsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.checkEditInvert);
			this.Controls.Add(this.pnSearch);
			this.Controls.Add(this.radioButtonWidgetTypeDisabled);
			this.Controls.Add(this.radioButtonWidgetTypeCustom);
			this.Controls.Add(this.xtraTabControlWidgets);
			this.Controls.Add(this.pbCustomWidget);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WidgetSettingsControl";
			this.Size = new System.Drawing.Size(920, 542);
			this.Click += new System.EventHandler(this.OnFormClick);
			((System.ComponentModel.ISupportInitialize)(this.pbCustomWidget)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).EndInit();
			this.pnSearch.ResumeLayout(false);
			this.pnSearch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditInvert.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pbCustomWidget;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlWidgets;
		private System.Windows.Forms.RadioButton radioButtonWidgetTypeCustom;
		private System.Windows.Forms.RadioButton radioButtonWidgetTypeDisabled;
		private System.Windows.Forms.Panel pnSearch;
		private DevExpress.XtraEditors.LabelControl labelControlSearchTitle;
		private DevComponents.DotNetBar.ButtonX buttonXSearch;
		private DevExpress.XtraEditors.TextEdit textEditSearch;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditInvert;
	}
}