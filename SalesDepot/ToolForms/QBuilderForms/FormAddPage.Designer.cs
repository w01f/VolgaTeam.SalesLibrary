namespace SalesDepot.ToolForms.QBuilderForms
{
	partial class FormAddPage
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
			this.simpleButtonAddPage = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
			this.labelControlSiteTitle = new DevExpress.XtraEditors.LabelControl();
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
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
			// simpleButtonAddPage
			// 
			this.simpleButtonAddPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.simpleButtonAddPage.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonAddPage.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonAddPage.Appearance.Options.UseFont = true;
			this.simpleButtonAddPage.Appearance.Options.UseForeColor = true;
			this.simpleButtonAddPage.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.simpleButtonAddPage.Location = new System.Drawing.Point(46, 76);
			this.simpleButtonAddPage.Name = "simpleButtonAddPage";
			this.simpleButtonAddPage.Size = new System.Drawing.Size(107, 37);
			this.simpleButtonAddPage.TabIndex = 1;
			this.simpleButtonAddPage.Text = "Add quickSITE";
			// 
			// simpleButtonCancel
			// 
			this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonCancel.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonCancel.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonCancel.Appearance.Options.UseFont = true;
			this.simpleButtonCancel.Appearance.Options.UseForeColor = true;
			this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButtonCancel.Location = new System.Drawing.Point(169, 76);
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
			this.labelControlSiteTitle.Size = new System.Drawing.Size(304, 23);
			this.labelControlSiteTitle.StyleController = this.styleController;
			this.labelControlSiteTitle.TabIndex = 3;
			this.labelControlSiteTitle.Text = "Name of new quickSITE:";
			// 
			// textEditName
			// 
			this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditName.Location = new System.Drawing.Point(6, 33);
			this.textEditName.Name = "textEditName";
			this.textEditName.Size = new System.Drawing.Size(304, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 4;
			// 
			// FormAddPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(322, 125);
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.labelControlSiteTitle);
			this.Controls.Add(this.simpleButtonCancel);
			this.Controls.Add(this.simpleButtonAddPage);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddPage";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add quickSITE";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddPage_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SimpleButton simpleButtonAddPage;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
		private DevExpress.XtraEditors.LabelControl labelControlSiteTitle;
		private DevExpress.XtraEditors.TextEdit textEditName;
	}
}