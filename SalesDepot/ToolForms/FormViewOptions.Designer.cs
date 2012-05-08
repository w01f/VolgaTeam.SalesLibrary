namespace SalesDepot.ToolForms
{
    partial class FormViewOptions
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
            this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
            this.buttonXPrint = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
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
            this.buttonXOpen.Text = "   Open this file";
            this.buttonXOpen.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXOpen.TextColor = System.Drawing.Color.Black;
            this.buttonXOpen.Click += new System.EventHandler(this.buttonXOpen_Click);
            // 
            // buttonXSave
            // 
            this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSave.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXSave.Image = global::SalesDepot.Properties.Resources.SaveCopy;
            this.buttonXSave.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.buttonXSave.Location = new System.Drawing.Point(20, 88);
            this.buttonXSave.Name = "buttonXSave";
            this.buttonXSave.Size = new System.Drawing.Size(290, 56);
            this.buttonXSave.TabIndex = 1;
            this.buttonXSave.Text = "   Save a copy";
            this.buttonXSave.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXSave.TextColor = System.Drawing.Color.Black;
            this.buttonXSave.Click += new System.EventHandler(this.buttonXSave_Click);
            // 
            // buttonXPrint
            // 
            this.buttonXPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXPrint.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXPrint.Image = global::SalesDepot.Properties.Resources.Print;
            this.buttonXPrint.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.buttonXPrint.Location = new System.Drawing.Point(20, 162);
            this.buttonXPrint.Name = "buttonXPrint";
            this.buttonXPrint.Size = new System.Drawing.Size(290, 56);
            this.buttonXPrint.TabIndex = 2;
            this.buttonXPrint.Text = "   Send to Printer";
            this.buttonXPrint.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXPrint.TextColor = System.Drawing.Color.Black;
            this.buttonXPrint.Click += new System.EventHandler(this.buttonXPrint_Click);
            // 
            // buttonXEmail
            // 
            this.buttonXEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEmail.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXEmail.Image = global::SalesDepot.Properties.Resources.Email;
            this.buttonXEmail.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.buttonXEmail.Location = new System.Drawing.Point(20, 236);
            this.buttonXEmail.Name = "buttonXEmail";
            this.buttonXEmail.Size = new System.Drawing.Size(290, 56);
            this.buttonXEmail.TabIndex = 3;
            this.buttonXEmail.Text = "   Attach to email";
            this.buttonXEmail.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXEmail.TextColor = System.Drawing.Color.Black;
            this.buttonXEmail.Click += new System.EventHandler(this.buttonXEmail_Click);
            // 
            // buttonXClose
            // 
            this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXClose.Image = global::SalesDepot.Properties.Resources.Cancel;
            this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.buttonXClose.Location = new System.Drawing.Point(20, 310);
            this.buttonXClose.Name = "buttonXClose";
            this.buttonXClose.Size = new System.Drawing.Size(290, 56);
            this.buttonXClose.TabIndex = 4;
            this.buttonXClose.Text = "   Close this Window";
            this.buttonXClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXClose.TextColor = System.Drawing.Color.Black;
            this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
            // 
            // FormViewOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(331, 380);
            this.Controls.Add(this.buttonXClose);
            this.Controls.Add(this.buttonXEmail);
            this.Controls.Add(this.buttonXPrint);
            this.Controls.Add(this.buttonXSave);
            this.Controls.Add(this.buttonXOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormViewOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Options - {0}";
            this.Load += new System.EventHandler(this.ViewOptionsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOpen;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        private DevComponents.DotNetBar.ButtonX buttonXPrint;
        private DevComponents.DotNetBar.ButtonX buttonXEmail;
        private DevComponents.DotNetBar.ButtonX buttonXClose;

    }
}