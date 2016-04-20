namespace SalesLibraries.CommonGUI.CustomDialog
{
	partial class FormCustomDialog
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
			this.pnButtons = new System.Windows.Forms.Panel();
			this.labelControlMessage = new DevExpress.XtraEditors.LabelControl();
			this.SuspendLayout();
			// 
			// pnButtons
			// 
			this.pnButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnButtons.Location = new System.Drawing.Point(10, 136);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(377, 60);
			this.pnButtons.TabIndex = 0;
			// 
			// labelControlMessage
			// 
			this.labelControlMessage.AllowHtmlString = true;
			this.labelControlMessage.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlMessage.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControlMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlMessage.Location = new System.Drawing.Point(10, 10);
			this.labelControlMessage.Name = "labelControlMessage";
			this.labelControlMessage.Size = new System.Drawing.Size(377, 126);
			this.labelControlMessage.TabIndex = 1;
			this.labelControlMessage.Text = "labelControl1";
			// 
			// FormCustomDialog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Red;
			this.ClientSize = new System.Drawing.Size(397, 196);
			this.Controls.Add(this.labelControlMessage);
			this.Controls.Add(this.pnButtons);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCustomDialog";
			this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormCustomDialog";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnButtons;
		private DevExpress.XtraEditors.LabelControl labelControlMessage;
	}
}