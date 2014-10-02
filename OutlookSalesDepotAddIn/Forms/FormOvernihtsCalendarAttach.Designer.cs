namespace OutlookSalesDepotAddIn.Forms
{
	partial class FormOvernihtsCalendarAttach
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
			this.labelDescription = new System.Windows.Forms.Label();
			this.buttonXAttach = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// labelDescription
			// 
			this.labelDescription.BackColor = System.Drawing.Color.White;
			this.labelDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelDescription.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelDescription.ForeColor = System.Drawing.Color.Black;
			this.labelDescription.Location = new System.Drawing.Point(0, 0);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(327, 37);
			this.labelDescription.TabIndex = 0;
			this.labelDescription.Text = "What do you want to do now?";
			this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonXAttach
			// 
			this.buttonXAttach.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXAttach.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAttach.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXAttach.Location = new System.Drawing.Point(12, 52);
			this.buttonXAttach.Name = "buttonXAttach";
			this.buttonXAttach.Size = new System.Drawing.Size(143, 37);
			this.buttonXAttach.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAttach.TabIndex = 1;
			this.buttonXAttach.Text = "Attach another file";
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXClose.Location = new System.Drawing.Point(172, 52);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(143, 37);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 2;
			this.buttonXClose.Text = "Close Library";
			// 
			// FormOvernihtsCalendarAttach
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(327, 101);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXAttach);
			this.Controls.Add(this.labelDescription);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOvernihtsCalendarAttach";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Your file is successfully added to the email:";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelDescription;
		private DevComponents.DotNetBar.ButtonX buttonXAttach;
		private DevComponents.DotNetBar.ButtonX buttonXClose;
	}
}