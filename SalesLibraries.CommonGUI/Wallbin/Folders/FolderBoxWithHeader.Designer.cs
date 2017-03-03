namespace SalesLibraries.CommonGUI.Wallbin.Folders
{
	partial class FolderBoxWithHeader
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
		protected override void InitializeComponent()
		{
			base.InitializeComponent();
			this.labelControlText = new DevExpress.XtraEditors.LabelControl();
			this.pnHeader.SuspendLayout();
			this.pnHeaderBorder.SuspendLayout();
			this.pnBorders.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.labelControlText);
			// 
			// labelControlText
			// 
			this.labelControlText.AllowDrop = true;
			this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlText.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.labelControlText.Location = new System.Drawing.Point(0, 0);
			this.labelControlText.Name = "labelControlText";
			this.labelControlText.Size = new System.Drawing.Size(309, 50);
			this.labelControlText.TabIndex = 5;
			this.labelControlText.UseMnemonic = false;
			// 
			// FolderBoxWithHeader
			// 
			this.Name = "FolderBoxWithHeader";
			this.pnHeader.ResumeLayout(false);
			this.pnHeaderBorder.ResumeLayout(false);
			this.pnBorders.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public DevExpress.XtraEditors.LabelControl labelControlText;
	}
}
