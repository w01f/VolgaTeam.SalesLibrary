namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	partial class SuperFilterControl
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
			this.checkedListBoxControl = new DevExpress.XtraEditors.CheckedListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).BeginInit();
			this.SuspendLayout();
			// 
			// checkedListBoxControl
			// 
			this.checkedListBoxControl.Appearance.BackColor = System.Drawing.Color.White;
			this.checkedListBoxControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxControl.Appearance.Options.UseBackColor = true;
			this.checkedListBoxControl.Appearance.Options.UseFont = true;
			this.checkedListBoxControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.checkedListBoxControl.CheckOnClick = true;
			this.checkedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxControl.ItemHeight = 23;
			this.checkedListBoxControl.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "STAR"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "SOLD"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "STAR1"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "SOLD1")});
			this.checkedListBoxControl.Location = new System.Drawing.Point(0, 0);
			this.checkedListBoxControl.MultiColumn = true;
			this.checkedListBoxControl.Name = "checkedListBoxControl";
			this.checkedListBoxControl.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControl.Size = new System.Drawing.Size(269, 47);
			this.checkedListBoxControl.TabIndex = 0;
			this.checkedListBoxControl.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControl_ItemCheck);
			// 
			// SuperFilterControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.checkedListBoxControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "SuperFilterControl";
			this.Size = new System.Drawing.Size(269, 47);
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl;
	}
}
