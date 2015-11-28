namespace SalesLibraries.FileManager.PresentationLayer.Calendars
{
	sealed partial class CalendarContainerControl
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
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnContainer
			// 
			this.pnContainer.Location = new System.Drawing.Point(127, 198);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(200, 100);
			this.pnContainer.TabIndex = 0;
			// 
			// pnEmpty
			// 
			this.pnEmpty.Location = new System.Drawing.Point(160, 387);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(200, 100);
			this.pnEmpty.TabIndex = 1;
			// 
			// CalendarContainerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnEmpty);
			this.Controls.Add(this.pnContainer);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "CalendarContainerControl";
			this.Size = new System.Drawing.Size(453, 601);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnContainer;
		private System.Windows.Forms.Panel pnEmpty;
	}
}
