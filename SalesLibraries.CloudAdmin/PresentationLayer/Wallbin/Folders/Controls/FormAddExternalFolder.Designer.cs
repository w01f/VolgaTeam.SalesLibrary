namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Controls
{
	partial class FormAddExternalFolder
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.checkedListBoxControl = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.buttonXSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClearAll = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).BeginInit();
			this.SuspendLayout();
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
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
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(363, 457);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(111, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(217, 457);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(111, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 1;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.AllowHtmlString = true;
			this.labelControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTitle.Location = new System.Drawing.Point(78, 12);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(396, 51);
			this.labelControlTitle.StyleController = this.styleController;
			this.labelControlTitle.TabIndex = 5;
			this.labelControlTitle.Text = "Folder Info";
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.White;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.LinkAddExternalFolder;
			this.pbLogo.Location = new System.Drawing.Point(12, 12);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(49, 51);
			this.pbLogo.TabIndex = 4;
			this.pbLogo.TabStop = false;
			// 
			// checkedListBoxControl
			// 
			this.checkedListBoxControl.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkedListBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxControl.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxControl.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkedListBoxControl.Appearance.Options.UseBackColor = true;
			this.checkedListBoxControl.Appearance.Options.UseForeColor = true;
			this.checkedListBoxControl.CheckOnClick = true;
			this.checkedListBoxControl.HorizontalScrollbar = true;
			this.checkedListBoxControl.ItemHeight = 25;
			this.checkedListBoxControl.Location = new System.Drawing.Point(12, 125);
			this.checkedListBoxControl.Name = "checkedListBoxControl";
			this.checkedListBoxControl.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControl.ShowFocusRect = false;
			this.checkedListBoxControl.Size = new System.Drawing.Size(462, 314);
			this.checkedListBoxControl.StyleController = this.styleController;
			this.checkedListBoxControl.TabIndex = 6;
			this.checkedListBoxControl.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControl_ItemCheck);
			// 
			// buttonXSelectAll
			// 
			this.buttonXSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSelectAll.Location = new System.Drawing.Point(12, 87);
			this.buttonXSelectAll.Name = "buttonXSelectAll";
			this.buttonXSelectAll.Size = new System.Drawing.Size(111, 32);
			this.buttonXSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSelectAll.TabIndex = 7;
			this.buttonXSelectAll.Text = "Select All";
			this.buttonXSelectAll.TextColor = System.Drawing.Color.Black;
			this.buttonXSelectAll.Click += new System.EventHandler(this.buttonXSelectAll_Click);
			// 
			// buttonXClearAll
			// 
			this.buttonXClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClearAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClearAll.Location = new System.Drawing.Point(363, 87);
			this.buttonXClearAll.Name = "buttonXClearAll";
			this.buttonXClearAll.Size = new System.Drawing.Size(111, 32);
			this.buttonXClearAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClearAll.TabIndex = 8;
			this.buttonXClearAll.Text = "Clear All";
			this.buttonXClearAll.TextColor = System.Drawing.Color.Black;
			this.buttonXClearAll.Click += new System.EventHandler(this.buttonXClearAll_Click);
			// 
			// FormAddExternalFolder
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(486, 501);
			this.Controls.Add(this.buttonXClearAll);
			this.Controls.Add(this.buttonXSelectAll);
			this.Controls.Add(this.checkedListBoxControl);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.pbLogo);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddExternalFolder";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Folder";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private System.Windows.Forms.PictureBox pbLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl;
		private DevComponents.DotNetBar.ButtonX buttonXSelectAll;
		private DevComponents.DotNetBar.ButtonX buttonXClearAll;
	}
}