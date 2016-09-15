namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkObjectOptions
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.rbCustomNote = new System.Windows.Forms.RadioButton();
			this.rbNew = new System.Windows.Forms.RadioButton();
			this.rbUpdated = new System.Windows.Forms.RadioButton();
			this.rbAttention = new System.Windows.Forms.RadioButton();
			this.rbSell = new System.Windows.Forms.RadioButton();
			this.rbNone = new System.Windows.Forms.RadioButton();
			this.labelControlBasicNotes = new DevExpress.XtraEditors.LabelControl();
			this.rbSold = new System.Windows.Forms.RadioButton();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.textEditCustomNote = new DevExpress.XtraEditors.TextEdit();
			this.labelControlCustomNote = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditCustomNote.Properties)).BeginInit();
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
			// rbCustomNote
			// 
			this.rbCustomNote.AutoSize = true;
			this.rbCustomNote.BackColor = System.Drawing.Color.White;
			this.rbCustomNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbCustomNote.ForeColor = System.Drawing.Color.Black;
			this.rbCustomNote.Location = new System.Drawing.Point(30, 370);
			this.rbCustomNote.Name = "rbCustomNote";
			this.rbCustomNote.Size = new System.Drawing.Size(127, 23);
			this.rbCustomNote.TabIndex = 6;
			this.rbCustomNote.TabStop = true;
			this.rbCustomNote.Text = "Custom Note";
			this.rbCustomNote.UseVisualStyleBackColor = false;
			this.rbCustomNote.CheckedChanged += new System.EventHandler(this.rbCustomNote_CheckedChanged);
			// 
			// rbNew
			// 
			this.rbNew.AutoSize = true;
			this.rbNew.BackColor = System.Drawing.Color.White;
			this.rbNew.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbNew.ForeColor = System.Drawing.Color.Black;
			this.rbNew.Location = new System.Drawing.Point(152, 168);
			this.rbNew.Name = "rbNew";
			this.rbNew.Size = new System.Drawing.Size(69, 23);
			this.rbNew.TabIndex = 1;
			this.rbNew.TabStop = true;
			this.rbNew.Text = "NEW!";
			this.rbNew.UseVisualStyleBackColor = false;
			// 
			// rbUpdated
			// 
			this.rbUpdated.AutoSize = true;
			this.rbUpdated.BackColor = System.Drawing.Color.White;
			this.rbUpdated.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbUpdated.ForeColor = System.Drawing.Color.Black;
			this.rbUpdated.Location = new System.Drawing.Point(401, 168);
			this.rbUpdated.Name = "rbUpdated";
			this.rbUpdated.Size = new System.Drawing.Size(109, 23);
			this.rbUpdated.TabIndex = 3;
			this.rbUpdated.TabStop = true;
			this.rbUpdated.Text = "UPDATED!";
			this.rbUpdated.UseVisualStyleBackColor = false;
			// 
			// rbAttention
			// 
			this.rbAttention.AutoSize = true;
			this.rbAttention.BackColor = System.Drawing.Color.White;
			this.rbAttention.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbAttention.ForeColor = System.Drawing.Color.Black;
			this.rbAttention.Location = new System.Drawing.Point(204, 227);
			this.rbAttention.Name = "rbAttention";
			this.rbAttention.Size = new System.Drawing.Size(122, 23);
			this.rbAttention.TabIndex = 5;
			this.rbAttention.TabStop = true;
			this.rbAttention.Text = "ATTENTION!";
			this.rbAttention.UseVisualStyleBackColor = false;
			// 
			// rbSell
			// 
			this.rbSell.AutoSize = true;
			this.rbSell.BackColor = System.Drawing.Color.White;
			this.rbSell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSell.ForeColor = System.Drawing.Color.Black;
			this.rbSell.Location = new System.Drawing.Point(30, 227);
			this.rbSell.Name = "rbSell";
			this.rbSell.Size = new System.Drawing.Size(114, 23);
			this.rbSell.TabIndex = 4;
			this.rbSell.TabStop = true;
			this.rbSell.Text = "SELL THIS!";
			this.rbSell.UseVisualStyleBackColor = false;
			// 
			// rbNone
			// 
			this.rbNone.AutoSize = true;
			this.rbNone.BackColor = System.Drawing.Color.White;
			this.rbNone.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbNone.ForeColor = System.Drawing.Color.Black;
			this.rbNone.Location = new System.Drawing.Point(30, 168);
			this.rbNone.Name = "rbNone";
			this.rbNone.Size = new System.Drawing.Size(68, 23);
			this.rbNone.TabIndex = 0;
			this.rbNone.TabStop = true;
			this.rbNone.Text = "None";
			this.rbNone.UseVisualStyleBackColor = false;
			// 
			// labelControlBasicNotes
			// 
			this.labelControlBasicNotes.AllowHtmlString = true;
			this.labelControlBasicNotes.Location = new System.Drawing.Point(17, 134);
			this.labelControlBasicNotes.Name = "labelControlBasicNotes";
			this.labelControlBasicNotes.Size = new System.Drawing.Size(122, 18);
			this.labelControlBasicNotes.StyleController = this.styleController;
			this.labelControlBasicNotes.TabIndex = 23;
			this.labelControlBasicNotes.Text = "<size=+2>Basic Link Notes:</size>";
			// 
			// rbSold
			// 
			this.rbSold.AutoSize = true;
			this.rbSold.BackColor = System.Drawing.Color.White;
			this.rbSold.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSold.ForeColor = System.Drawing.Color.Black;
			this.rbSold.Location = new System.Drawing.Point(272, 168);
			this.rbSold.Name = "rbSold";
			this.rbSold.Size = new System.Drawing.Size(76, 23);
			this.rbSold.TabIndex = 2;
			this.rbSold.TabStop = true;
			this.rbSold.Text = "SOLD!";
			this.rbSold.UseVisualStyleBackColor = false;
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
			this.labelControlTitle.TabIndex = 25;
			this.labelControlTitle.Text = "<size=+2>Link Notes are small, <b>BOLD</b> text labels you can assign to a specif" +
    "ic link. These are useful if you need to bring attention to a link on your site…" +
    "</size>";
			// 
			// textEditCustomNote
			// 
			this.textEditCustomNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditCustomNote.Enabled = false;
			this.textEditCustomNote.Location = new System.Drawing.Point(30, 399);
			this.textEditCustomNote.Name = "textEditCustomNote";
			this.textEditCustomNote.Size = new System.Drawing.Size(571, 22);
			this.textEditCustomNote.StyleController = this.styleController;
			this.textEditCustomNote.TabIndex = 26;
			// 
			// labelControlCustomNote
			// 
			this.labelControlCustomNote.AllowHtmlString = true;
			this.labelControlCustomNote.Location = new System.Drawing.Point(17, 330);
			this.labelControlCustomNote.Name = "labelControlCustomNote";
			this.labelControlCustomNote.Size = new System.Drawing.Size(168, 18);
			this.labelControlCustomNote.StyleController = this.styleController;
			this.labelControlCustomNote.TabIndex = 27;
			this.labelControlCustomNote.Text = "<size=+2>Add your own Link Note:</size>";
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.LinkSettingsTextNote;
			this.pictureBoxLogo.Location = new System.Drawing.Point(17, 18);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
			this.pictureBoxLogo.TabIndex = 28;
			this.pictureBoxLogo.TabStop = false;
			// 
			// LinkObjectOptions
			// 
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.labelControlCustomNote);
			this.Controls.Add(this.textEditCustomNote);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.rbSold);
			this.Controls.Add(this.labelControlBasicNotes);
			this.Controls.Add(this.rbCustomNote);
			this.Controls.Add(this.rbNone);
			this.Controls.Add(this.rbAttention);
			this.Controls.Add(this.rbUpdated);
			this.Controls.Add(this.rbSell);
			this.Controls.Add(this.rbNew);
			this.Name = "LinkObjectOptions";
			this.Size = new System.Drawing.Size(616, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditCustomNote.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.RadioButton rbNone;
		private System.Windows.Forms.RadioButton rbCustomNote;
		private System.Windows.Forms.RadioButton rbNew;
		private System.Windows.Forms.RadioButton rbUpdated;
		private System.Windows.Forms.RadioButton rbAttention;
		private System.Windows.Forms.RadioButton rbSell;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlBasicNotes;
		private System.Windows.Forms.RadioButton rbSold;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.TextEdit textEditCustomNote;
		private DevExpress.XtraEditors.LabelControl labelControlCustomNote;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
	}
}
