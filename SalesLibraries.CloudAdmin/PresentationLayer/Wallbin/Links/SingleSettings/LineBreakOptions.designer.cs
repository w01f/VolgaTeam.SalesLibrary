namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	partial class LineBreakOptions
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
			this.memoEditNote = new DevExpress.XtraEditors.MemoEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laNote = new System.Windows.Forms.Label();
			this.laFont = new System.Windows.Forms.Label();
			this.buttonEditLineBreakFont = new DevExpress.XtraEditors.ButtonEdit();
			this.colorEditLineBreakFontColor = new DevExpress.XtraEditors.ColorEdit();
			this.laFontColor = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.memoEditNote.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLineBreakFont.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLineBreakFontColor.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// memoEditNote
			// 
			this.memoEditNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditNote.Location = new System.Drawing.Point(94, 87);
			this.memoEditNote.Name = "memoEditNote";
			this.memoEditNote.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditNote.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.memoEditNote.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditNote.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditNote.Size = new System.Drawing.Size(434, 451);
			this.memoEditNote.StyleController = this.styleController;
			this.memoEditNote.TabIndex = 39;
			this.memoEditNote.UseOptimizedRendering = true;
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
			// laNote
			// 
			this.laNote.AutoSize = true;
			this.laNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNote.ForeColor = System.Drawing.Color.Black;
			this.laNote.Location = new System.Drawing.Point(3, 88);
			this.laNote.Name = "laNote";
			this.laNote.Size = new System.Drawing.Size(37, 16);
			this.laNote.TabIndex = 38;
			this.laNote.Text = "Note";
			// 
			// laFont
			// 
			this.laFont.AutoSize = true;
			this.laFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laFont.ForeColor = System.Drawing.Color.Black;
			this.laFont.Location = new System.Drawing.Point(3, 9);
			this.laFont.Name = "laFont";
			this.laFont.Size = new System.Drawing.Size(34, 16);
			this.laFont.TabIndex = 37;
			this.laFont.Text = "Font";
			// 
			// buttonEditLineBreakFont
			// 
			this.buttonEditLineBreakFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditLineBreakFont.Location = new System.Drawing.Point(94, 8);
			this.buttonEditLineBreakFont.Name = "buttonEditLineBreakFont";
			this.buttonEditLineBreakFont.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditLineBreakFont.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditLineBreakFont.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditLineBreakFont.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditLineBreakFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditLineBreakFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.buttonEditLineBreakFont.Size = new System.Drawing.Size(434, 22);
			this.buttonEditLineBreakFont.StyleController = this.styleController;
			this.buttonEditLineBreakFont.TabIndex = 36;
			this.buttonEditLineBreakFont.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FontEdit_ButtonClick);
			this.buttonEditLineBreakFont.Click += new System.EventHandler(this.FontEdit_Click);
			// 
			// colorEditLineBreakFontColor
			// 
			this.colorEditLineBreakFontColor.EditValue = System.Drawing.Color.Empty;
			this.colorEditLineBreakFontColor.Location = new System.Drawing.Point(94, 47);
			this.colorEditLineBreakFontColor.Name = "colorEditLineBreakFontColor";
			this.colorEditLineBreakFontColor.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.colorEditLineBreakFontColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditLineBreakFontColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditLineBreakFontColor.Properties.Appearance.Options.UseForeColor = true;
			this.colorEditLineBreakFontColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.colorEditLineBreakFontColor.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colorEditLineBreakFontColor.Properties.ShowSystemColors = false;
			this.colorEditLineBreakFontColor.Properties.ShowWebColors = true;
			this.colorEditLineBreakFontColor.Size = new System.Drawing.Size(105, 22);
			this.colorEditLineBreakFontColor.StyleController = this.styleController;
			this.colorEditLineBreakFontColor.TabIndex = 35;
			// 
			// laFontColor
			// 
			this.laFontColor.AutoSize = true;
			this.laFontColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laFontColor.ForeColor = System.Drawing.Color.Black;
			this.laFontColor.Location = new System.Drawing.Point(3, 50);
			this.laFontColor.Name = "laFontColor";
			this.laFontColor.Size = new System.Drawing.Size(69, 16);
			this.laFontColor.TabIndex = 34;
			this.laFontColor.Text = "Font Color";
			// 
			// LineBreakOptions
			// 
			this.Controls.Add(this.memoEditNote);
			this.Controls.Add(this.laNote);
			this.Controls.Add(this.laFont);
			this.Controls.Add(this.buttonEditLineBreakFont);
			this.Controls.Add(this.colorEditLineBreakFontColor);
			this.Controls.Add(this.laFontColor);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Size = new System.Drawing.Size(531, 541);
			this.Text = "Info";
			((System.ComponentModel.ISupportInitialize)(this.memoEditNote.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLineBreakFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLineBreakFontColor.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.MemoEdit memoEditNote;
		private System.Windows.Forms.Label laNote;
		private System.Windows.Forms.Label laFont;
		private DevExpress.XtraEditors.ButtonEdit buttonEditLineBreakFont;
		private DevExpress.XtraEditors.ColorEdit colorEditLineBreakFontColor;
		private System.Windows.Forms.Label laFontColor;
		private DevExpress.XtraEditors.StyleController styleController;
	}
}
