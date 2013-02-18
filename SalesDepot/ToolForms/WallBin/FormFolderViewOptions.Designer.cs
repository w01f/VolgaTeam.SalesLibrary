namespace SalesDepot.ToolForms.WallBin
{
	partial class FormFolderViewOptions
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
			this.buttonXOpen = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXOpen
			// 
			this.buttonXOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpen.Image = global::SalesDepot.Properties.Resources.OpenFile;
			this.buttonXOpen.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXOpen.Location = new System.Drawing.Point(20, 14);
			this.buttonXOpen.Name = "buttonXOpen";
			this.buttonXOpen.Size = new System.Drawing.Size(290, 56);
			this.buttonXOpen.TabIndex = 0;
			this.buttonXOpen.Text = "   Open this folder";
			this.buttonXOpen.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXOpen.TextColor = System.Drawing.Color.Black;
			this.buttonXOpen.Click += new System.EventHandler(this.buttonXOpen_Click);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(20, 100);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(290, 56);
			this.buttonXClose.TabIndex = 4;
			this.buttonXClose.Text = "   Close this Window";
			this.buttonXClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
			// 
			// FormFolderViewOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(331, 185);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXOpen);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFolderViewOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Folder Options - {0}";
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.ButtonX buttonXOpen;
        private DevComponents.DotNetBar.ButtonX buttonXClose;

    }
}