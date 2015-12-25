namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
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
			this.edCustomNote = new System.Windows.Forms.TextBox();
			this.rbCustomNote = new System.Windows.Forms.RadioButton();
			this.rbNew = new System.Windows.Forms.RadioButton();
			this.rbUpdated = new System.Windows.Forms.RadioButton();
			this.rbAttention = new System.Windows.Forms.RadioButton();
			this.rbSell = new System.Windows.Forms.RadioButton();
			this.rbNone = new System.Windows.Forms.RadioButton();
			this.textEditLinkHoverNote = new DevExpress.XtraEditors.TextEdit();
			this.laLinkHoverNote = new System.Windows.Forms.Label();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlHoverNoteDescription = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkHoverNote.Properties)).BeginInit();
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
			// edCustomNote
			// 
			this.edCustomNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.edCustomNote.BackColor = System.Drawing.Color.White;
			this.edCustomNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.edCustomNote.ForeColor = System.Drawing.Color.Black;
			this.edCustomNote.Location = new System.Drawing.Point(8, 143);
			this.edCustomNote.Name = "edCustomNote";
			this.edCustomNote.Size = new System.Drawing.Size(520, 26);
			this.edCustomNote.TabIndex = 6;
			// 
			// rbCustomNote
			// 
			this.rbCustomNote.AutoSize = true;
			this.rbCustomNote.BackColor = System.Drawing.Color.White;
			this.rbCustomNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbCustomNote.ForeColor = System.Drawing.Color.Black;
			this.rbCustomNote.Location = new System.Drawing.Point(8, 114);
			this.rbCustomNote.Name = "rbCustomNote";
			this.rbCustomNote.Size = new System.Drawing.Size(127, 23);
			this.rbCustomNote.TabIndex = 5;
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
			this.rbNew.Location = new System.Drawing.Point(82, 45);
			this.rbNew.Name = "rbNew";
			this.rbNew.Size = new System.Drawing.Size(69, 23);
			this.rbNew.TabIndex = 0;
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
			this.rbUpdated.Location = new System.Drawing.Point(166, 45);
			this.rbUpdated.Name = "rbUpdated";
			this.rbUpdated.Size = new System.Drawing.Size(109, 23);
			this.rbUpdated.TabIndex = 1;
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
			this.rbAttention.Location = new System.Drawing.Point(411, 45);
			this.rbAttention.Name = "rbAttention";
			this.rbAttention.Size = new System.Drawing.Size(122, 23);
			this.rbAttention.TabIndex = 3;
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
			this.rbSell.Location = new System.Drawing.Point(286, 45);
			this.rbSell.Name = "rbSell";
			this.rbSell.Size = new System.Drawing.Size(114, 23);
			this.rbSell.TabIndex = 2;
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
			this.rbNone.Location = new System.Drawing.Point(8, 45);
			this.rbNone.Name = "rbNone";
			this.rbNone.Size = new System.Drawing.Size(68, 23);
			this.rbNone.TabIndex = 4;
			this.rbNone.TabStop = true;
			this.rbNone.Text = "None";
			this.rbNone.UseVisualStyleBackColor = false;
			// 
			// textEditLinkHoverNote
			// 
			this.textEditLinkHoverNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditLinkHoverNote.Location = new System.Drawing.Point(8, 238);
			this.textEditLinkHoverNote.Name = "textEditLinkHoverNote";
			this.textEditLinkHoverNote.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditLinkHoverNote.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditLinkHoverNote.Properties.Appearance.Options.UseBackColor = true;
			this.textEditLinkHoverNote.Properties.Appearance.Options.UseForeColor = true;
			this.textEditLinkHoverNote.Size = new System.Drawing.Size(520, 22);
			this.textEditLinkHoverNote.StyleController = this.styleController;
			this.textEditLinkHoverNote.TabIndex = 22;
			// 
			// laLinkHoverNote
			// 
			this.laLinkHoverNote.AutoSize = true;
			this.laLinkHoverNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laLinkHoverNote.ForeColor = System.Drawing.Color.Black;
			this.laLinkHoverNote.Location = new System.Drawing.Point(4, 216);
			this.laLinkHoverNote.Name = "laLinkHoverNote";
			this.laLinkHoverNote.Size = new System.Drawing.Size(101, 19);
			this.laLinkHoverNote.TabIndex = 21;
			this.laLinkHoverNote.Text = "Hover Note:";
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
			this.labelControlTitle.Location = new System.Drawing.Point(8, 12);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(477, 17);
			this.labelControlTitle.TabIndex = 23;
			this.labelControlTitle.Text = "Link Notes are small, bold text labels you can apply to the end of the link…";
			// 
			// labelControlHoverNoteDescription
			// 
			this.labelControlHoverNoteDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlHoverNoteDescription.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlHoverNoteDescription.Appearance.ForeColor = System.Drawing.Color.DimGray;
			this.labelControlHoverNoteDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlHoverNoteDescription.Location = new System.Drawing.Point(8, 266);
			this.labelControlHoverNoteDescription.Name = "labelControlHoverNoteDescription";
			this.labelControlHoverNoteDescription.Size = new System.Drawing.Size(520, 51);
			this.labelControlHoverNoteDescription.TabIndex = 24;
			this.labelControlHoverNoteDescription.Text = "Not the same as a “Link Note”…\r\nThe Hover Note is a custom, special little messag" +
    "e that appears when the mouse hovers over the link…";
			// 
			// LinkObjectOptions
			// 
			this.Controls.Add(this.labelControlHoverNoteDescription);
			this.Controls.Add(this.edCustomNote);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.rbCustomNote);
			this.Controls.Add(this.rbNone);
			this.Controls.Add(this.rbAttention);
			this.Controls.Add(this.rbUpdated);
			this.Controls.Add(this.rbSell);
			this.Controls.Add(this.rbNew);
			this.Controls.Add(this.textEditLinkHoverNote);
			this.Controls.Add(this.laLinkHoverNote);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LinkBaseOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkHoverNote.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox edCustomNote;
		private System.Windows.Forms.RadioButton rbNone;
		private System.Windows.Forms.RadioButton rbCustomNote;
		private System.Windows.Forms.RadioButton rbNew;
		private System.Windows.Forms.RadioButton rbUpdated;
		private System.Windows.Forms.RadioButton rbAttention;
		private System.Windows.Forms.RadioButton rbSell;
		public DevExpress.XtraEditors.TextEdit textEditLinkHoverNote;
		public System.Windows.Forms.Label laLinkHoverNote;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.LabelControl labelControlHoverNoteDescription;
	}
}
