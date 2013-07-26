namespace FileManager.PresentationClasses.Tags
{
	partial class SuperFiltersEditor
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
			this.laHeader = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlRestrictionInfo = new DevExpress.XtraEditors.LabelControl();
			this.checkedListBoxControl = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).BeginInit();
			this.SuspendLayout();
			// 
			// laHeader
			// 
			this.laHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.laHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.laHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laHeader.Location = new System.Drawing.Point(0, 0);
			this.laHeader.Name = "laHeader";
			this.laHeader.Size = new System.Drawing.Size(350, 24);
			this.laHeader.TabIndex = 0;
			this.laHeader.Text = "Manage Super Filters";
			this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Controls.Add(this.pnButtons);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 24);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(350, 380);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.checkedListBoxControl);
			this.pnData.Controls.Add(this.labelControlRestrictionInfo);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 47);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(346, 329);
			this.pnData.TabIndex = 1;
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.buttonXReset);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(346, 47);
			this.pnButtons.TabIndex = 0;
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Location = new System.Drawing.Point(5, 8);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(336, 30);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 0;
			this.buttonXReset.Text = "RESET ALL Super Filters for the Selected Links";
			this.buttonXReset.TextColor = System.Drawing.Color.Black;
			this.buttonXReset.Click += new System.EventHandler(this.buttonXReset_Click);
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
			// labelControlRestrictionInfo
			// 
			this.labelControlRestrictionInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlRestrictionInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlRestrictionInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlRestrictionInfo.Location = new System.Drawing.Point(0, 0);
			this.labelControlRestrictionInfo.Name = "labelControlRestrictionInfo";
			this.labelControlRestrictionInfo.Size = new System.Drawing.Size(346, 28);
			this.labelControlRestrictionInfo.StyleController = this.styleController;
			this.labelControlRestrictionInfo.TabIndex = 0;
			this.labelControlRestrictionInfo.Text = "Max 4 Super Filters are allowed…";
			// 
			// checkedListBoxControl
			// 
			this.checkedListBoxControl.CheckOnClick = true;
			this.checkedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxControl.ItemHeight = 30;
			this.checkedListBoxControl.Location = new System.Drawing.Point(0, 28);
			this.checkedListBoxControl.Name = "checkedListBoxControl";
			this.checkedListBoxControl.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControl.Size = new System.Drawing.Size(346, 301);
			this.checkedListBoxControl.StyleController = this.styleController;
			this.checkedListBoxControl.TabIndex = 1;
			// 
			// SuperFiltersEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.laHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SuperFiltersEditor";
			this.Size = new System.Drawing.Size(350, 404);
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label laHeader;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlRestrictionInfo;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl;
	}
}
