namespace SalesDepot.ToolForms.QBuilderForms
{
	partial class FormDeletePages
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlSiteTitle = new DevExpress.XtraEditors.LabelControl();
			this.checkedListBoxControl = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDeletePage = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).BeginInit();
			this.SuspendLayout();
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
			// labelControlSiteTitle
			// 
			this.labelControlSiteTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlSiteTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSiteTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlSiteTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlSiteTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlSiteTitle.Location = new System.Drawing.Point(6, 4);
			this.labelControlSiteTitle.Name = "labelControlSiteTitle";
			this.labelControlSiteTitle.Size = new System.Drawing.Size(342, 23);
			this.labelControlSiteTitle.StyleController = this.styleController;
			this.labelControlSiteTitle.TabIndex = 3;
			this.labelControlSiteTitle.Text = "Select quickSITES you want to delete:";
			// 
			// checkedListBoxControl
			// 
			this.checkedListBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxControl.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxControl.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkedListBoxControl.Appearance.Options.UseBackColor = true;
			this.checkedListBoxControl.Appearance.Options.UseFont = true;
			this.checkedListBoxControl.Appearance.Options.UseForeColor = true;
			this.checkedListBoxControl.CheckOnClick = true;
			this.checkedListBoxControl.ItemHeight = 40;
			this.checkedListBoxControl.Location = new System.Drawing.Point(6, 66);
			this.checkedListBoxControl.Name = "checkedListBoxControl";
			this.checkedListBoxControl.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControl.Size = new System.Drawing.Size(342, 271);
			this.checkedListBoxControl.TabIndex = 6;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(188, 348);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(107, 37);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 14;
			this.buttonXCancel.Text = "Cancel";
			// 
			// buttonXDeletePage
			// 
			this.buttonXDeletePage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDeletePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXDeletePage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDeletePage.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXDeletePage.Location = new System.Drawing.Point(65, 348);
			this.buttonXDeletePage.Name = "buttonXDeletePage";
			this.buttonXDeletePage.Size = new System.Drawing.Size(107, 37);
			this.buttonXDeletePage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDeletePage.TabIndex = 15;
			this.buttonXDeletePage.Text = "Delete Selected";
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Location = new System.Drawing.Point(6, 33);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(107, 27);
			this.buttonXSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectAll.TabIndex = 16;
			this.buttonXSelectAll.Text = "Select All";
			this.buttonXSelectAll.Click += new System.EventHandler(this.simpleButtonSelectAll_Click);
			// 
			// buttonXClearAll
			// 
			this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClearAll.Location = new System.Drawing.Point(241, 33);
			this.buttonXClearAll.Name = "buttonXClearAll";
			this.buttonXClearAll.Size = new System.Drawing.Size(107, 27);
			this.buttonXClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClearAll.TabIndex = 17;
			this.buttonXClearAll.Text = "Select All";
			this.buttonXClearAll.Click += new System.EventHandler(this.simpleButtonClearAll_Click);
			// 
			// FormDeletePages
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(360, 392);
			this.Controls.Add(this.buttonXClearAll);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXDeletePage);
			this.Controls.Add(this.checkedListBoxControl);
			this.Controls.Add(this.labelControlSiteTitle);
			this.Controls.Add(this.buttonXSelectAll);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDeletePages";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Clean up my quickSITES";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlSiteTitle;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXDeletePage;
		private DevComponents.DotNetBar.ButtonX buttonXSelectAll;
		private DevComponents.DotNetBar.ButtonX buttonXClearAll;
	}
}