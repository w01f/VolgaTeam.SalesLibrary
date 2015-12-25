namespace SalesLibraries.CommonGUI.RetractableBar
{
	partial class RetractableBarRight
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
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).BeginInit();
			this.pnClosed.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).BeginInit();
			this.pnOpened.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnClosed
			// 
			this.pnClosed.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnClosed.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnClosed.Location = new System.Drawing.Point(304, 0);
			this.pnClosed.Size = new System.Drawing.Size(55, 388);
			// 
			// pnOpened
			// 
			this.pnOpened.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pnOpened.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnOpened.Location = new System.Drawing.Point(0, 0);
			this.pnOpened.Size = new System.Drawing.Size(304, 388);
			// 
			// simpleButtonExpand
			// 
			this.simpleButtonExpand.Image = global::SalesLibraries.CommonGUI.Properties.Resources.RetractableBarCollapse;
			this.simpleButtonExpand.Size = new System.Drawing.Size(47, 36);
			// 
			// pnTop
			// 
			this.pnTop.Size = new System.Drawing.Size(300, 40);
			// 
			// pnContent
			// 
			this.pnContent.Size = new System.Drawing.Size(300, 344);
			// 
			// simpleButtonCollapse
			// 
			this.simpleButtonCollapse.Dock = System.Windows.Forms.DockStyle.Right;
			this.simpleButtonCollapse.Image = global::SalesLibraries.CommonGUI.Properties.Resources.RetractableBarExpand;
			this.simpleButtonCollapse.Location = new System.Drawing.Point(251, 2);
			// 
			// pnHeaderContent
			// 
			this.pnHeaderContent.Location = new System.Drawing.Point(2, 2);
			this.pnHeaderContent.Size = new System.Drawing.Size(249, 36);
			// 
			// RetractableBarRight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "RetractableBarRight";
			((System.ComponentModel.ISupportInitialize)(this.pnClosed)).EndInit();
			this.pnClosed.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnOpened)).EndInit();
			this.pnOpened.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
