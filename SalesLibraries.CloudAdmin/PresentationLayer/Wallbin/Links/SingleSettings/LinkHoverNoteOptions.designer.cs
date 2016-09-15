namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkHoverNoteOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkHoverNoteOptions));
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.checkEditNote = new DevExpress.XtraEditors.CheckEdit();
			this.memoEditNote = new DevExpress.XtraEditors.MemoEdit();
			this.checkEditShowOnlyCustomNote = new DevExpress.XtraEditors.CheckEdit();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditNote.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditNote.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowOnlyCustomNote.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
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
			// checkEditNote
			// 
			this.checkEditNote.Location = new System.Drawing.Point(17, 136);
			this.checkEditNote.Name = "checkEditNote";
			this.checkEditNote.Properties.AutoWidth = true;
			this.checkEditNote.Properties.Caption = "Apply a “Hover Note”";
			this.checkEditNote.Size = new System.Drawing.Size(141, 20);
			this.checkEditNote.StyleController = this.styleController;
			this.checkEditNote.TabIndex = 46;
			this.checkEditNote.CheckedChanged += new System.EventHandler(this.checkEditNote_CheckedChanged);
			// 
			// memoEditNote
			// 
			this.memoEditNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditNote.Enabled = false;
			this.memoEditNote.Location = new System.Drawing.Point(17, 162);
			this.memoEditNote.Name = "memoEditNote";
			this.memoEditNote.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditNote.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.memoEditNote.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditNote.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditNote.Size = new System.Drawing.Size(582, 331);
			this.memoEditNote.StyleController = this.styleController;
			this.memoEditNote.TabIndex = 45;
			// 
			// checkEditShowOnlyCustomNote
			// 
			this.checkEditShowOnlyCustomNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowOnlyCustomNote.Enabled = false;
			this.checkEditShowOnlyCustomNote.Location = new System.Drawing.Point(17, 509);
			this.checkEditShowOnlyCustomNote.Name = "checkEditShowOnlyCustomNote";
			this.checkEditShowOnlyCustomNote.Properties.AutoWidth = true;
			this.checkEditShowOnlyCustomNote.Properties.Caption = "ONLY SHOW HOVER NOTE (no other info will be seen)";
			this.checkEditShowOnlyCustomNote.Size = new System.Drawing.Size(346, 20);
			this.checkEditShowOnlyCustomNote.StyleController = this.styleController;
			this.checkEditShowOnlyCustomNote.TabIndex = 47;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkSettingsHoverNote;
			this.pictureBoxLogo.Location = new System.Drawing.Point(17, 18);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
			this.pictureBoxLogo.TabIndex = 49;
			this.pictureBoxLogo.TabStop = false;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.AllowHtmlString = true;
			this.labelControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTitle.Location = new System.Drawing.Point(103, 18);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(478, 83);
			this.labelControlTitle.StyleController = this.styleController;
			this.labelControlTitle.TabIndex = 48;
			this.labelControlTitle.Text = resources.GetString("labelControlTitle.Text");
			// 
			// LinkHoverNoteOptions
			// 
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.checkEditShowOnlyCustomNote);
			this.Controls.Add(this.checkEditNote);
			this.Controls.Add(this.memoEditNote);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LinkHoverNoteOptions";
			this.Size = new System.Drawing.Size(616, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditNote.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditNote.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowOnlyCustomNote.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditNote;
		private DevExpress.XtraEditors.MemoEdit memoEditNote;
		private DevExpress.XtraEditors.CheckEdit checkEditShowOnlyCustomNote;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
	}
}
