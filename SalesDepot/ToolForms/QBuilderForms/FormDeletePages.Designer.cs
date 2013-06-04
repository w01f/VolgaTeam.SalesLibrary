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
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.simpleButtonDelete = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
			this.labelControlSiteTitle = new DevExpress.XtraEditors.LabelControl();
			this.simpleButtonSelectAll = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonClearAll = new DevExpress.XtraEditors.SimpleButton();
			this.checkedListBoxControl = new DevExpress.XtraEditors.CheckedListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
			// simpleButtonDelete
			// 
			this.simpleButtonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.simpleButtonDelete.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonDelete.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonDelete.Appearance.Options.UseFont = true;
			this.simpleButtonDelete.Appearance.Options.UseForeColor = true;
			this.simpleButtonDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.simpleButtonDelete.Location = new System.Drawing.Point(46, 343);
			this.simpleButtonDelete.Name = "simpleButtonDelete";
			this.simpleButtonDelete.Size = new System.Drawing.Size(107, 37);
			this.simpleButtonDelete.TabIndex = 1;
			this.simpleButtonDelete.Text = "Delete Selected";
			// 
			// simpleButtonCancel
			// 
			this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonCancel.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonCancel.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonCancel.Appearance.Options.UseFont = true;
			this.simpleButtonCancel.Appearance.Options.UseForeColor = true;
			this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButtonCancel.Location = new System.Drawing.Point(207, 343);
			this.simpleButtonCancel.Name = "simpleButtonCancel";
			this.simpleButtonCancel.Size = new System.Drawing.Size(107, 37);
			this.simpleButtonCancel.TabIndex = 2;
			this.simpleButtonCancel.Text = "Cancel";
			// 
			// labelControlSiteTitle
			// 
			this.labelControlSiteTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlSiteTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlSiteTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlSiteTitle.Location = new System.Drawing.Point(6, 4);
			this.labelControlSiteTitle.Name = "labelControlSiteTitle";
			this.labelControlSiteTitle.Size = new System.Drawing.Size(342, 23);
			this.labelControlSiteTitle.StyleController = this.styleController;
			this.labelControlSiteTitle.TabIndex = 3;
			this.labelControlSiteTitle.Text = "Select quickSITES you want to delete:";
			// 
			// simpleButtonSelectAll
			// 
			this.simpleButtonSelectAll.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonSelectAll.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonSelectAll.Appearance.Options.UseFont = true;
			this.simpleButtonSelectAll.Appearance.Options.UseForeColor = true;
			this.simpleButtonSelectAll.Location = new System.Drawing.Point(6, 33);
			this.simpleButtonSelectAll.Name = "simpleButtonSelectAll";
			this.simpleButtonSelectAll.Size = new System.Drawing.Size(107, 27);
			this.simpleButtonSelectAll.TabIndex = 4;
			this.simpleButtonSelectAll.Text = "Select All";
			this.simpleButtonSelectAll.Click += new System.EventHandler(this.simpleButtonSelectAll_Click);
			// 
			// simpleButtonClearAll
			// 
			this.simpleButtonClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonClearAll.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonClearAll.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonClearAll.Appearance.Options.UseFont = true;
			this.simpleButtonClearAll.Appearance.Options.UseForeColor = true;
			this.simpleButtonClearAll.Location = new System.Drawing.Point(241, 33);
			this.simpleButtonClearAll.Name = "simpleButtonClearAll";
			this.simpleButtonClearAll.Size = new System.Drawing.Size(107, 27);
			this.simpleButtonClearAll.TabIndex = 5;
			this.simpleButtonClearAll.Text = "Clear All";
			this.simpleButtonClearAll.Click += new System.EventHandler(this.simpleButtonClearAll_Click);
			// 
			// checkedListBoxControl
			// 
			this.checkedListBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxControl.Appearance.Options.UseFont = true;
			this.checkedListBoxControl.CheckOnClick = true;
			this.checkedListBoxControl.ItemHeight = 40;
			this.checkedListBoxControl.Location = new System.Drawing.Point(6, 66);
			this.checkedListBoxControl.Name = "checkedListBoxControl";
			this.checkedListBoxControl.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControl.Size = new System.Drawing.Size(342, 271);
			this.checkedListBoxControl.TabIndex = 6;
			// 
			// FormDeletePages
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(360, 392);
			this.Controls.Add(this.checkedListBoxControl);
			this.Controls.Add(this.simpleButtonClearAll);
			this.Controls.Add(this.simpleButtonSelectAll);
			this.Controls.Add(this.labelControlSiteTitle);
			this.Controls.Add(this.simpleButtonCancel);
			this.Controls.Add(this.simpleButtonDelete);
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

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SimpleButton simpleButtonDelete;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
		private DevExpress.XtraEditors.LabelControl labelControlSiteTitle;
		private DevExpress.XtraEditors.SimpleButton simpleButtonSelectAll;
		private DevExpress.XtraEditors.SimpleButton simpleButtonClearAll;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl;
	}
}