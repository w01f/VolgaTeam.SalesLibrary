namespace FileManager.ToolForms.WallBin
{
    partial class FormDeleteIncorrectLinks
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExpiredDate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDelete = new DevComponents.DotNetBar.ButtonX();
			this.grIncorrectLinks = new System.Windows.Forms.DataGridView();
			this.colIdentifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colWindow = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pnButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grIncorrectLinks)).BeginInit();
			this.SuspendLayout();
			// 
			// pnButtons
			// 
			this.pnButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnButtons.Controls.Add(this.buttonXCancel);
			this.pnButtons.Controls.Add(this.buttonXOK);
			this.pnButtons.Controls.Add(this.buttonXExpiredDate);
			this.pnButtons.Controls.Add(this.buttonXDelete);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnButtons.ForeColor = System.Drawing.Color.Black;
			this.pnButtons.Location = new System.Drawing.Point(606, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(158, 512);
			this.pnButtons.TabIndex = 0;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(10, 468);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(136, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 11;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(10, 430);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(136, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 10;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXExpiredDate
			// 
			this.buttonXExpiredDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExpiredDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXExpiredDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExpiredDate.Location = new System.Drawing.Point(10, 62);
			this.buttonXExpiredDate.Name = "buttonXExpiredDate";
			this.buttonXExpiredDate.Size = new System.Drawing.Size(136, 44);
			this.buttonXExpiredDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExpiredDate.TabIndex = 6;
			this.buttonXExpiredDate.Text = "Expired Date\r\nOptions";
			this.buttonXExpiredDate.Click += new System.EventHandler(this.btExpiredDate_Click);
			// 
			// buttonXDelete
			// 
			this.buttonXDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDelete.Location = new System.Drawing.Point(10, 12);
			this.buttonXDelete.Name = "buttonXDelete";
			this.buttonXDelete.Size = new System.Drawing.Size(136, 44);
			this.buttonXDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDelete.TabIndex = 5;
			this.buttonXDelete.Text = "Delete\r\nthis Link";
			this.buttonXDelete.Click += new System.EventHandler(this.btDelete_Click);
			// 
			// grIncorrectLinks
			// 
			this.grIncorrectLinks.AllowUserToAddRows = false;
			this.grIncorrectLinks.AllowUserToDeleteRows = false;
			this.grIncorrectLinks.AllowUserToResizeRows = false;
			this.grIncorrectLinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.grIncorrectLinks.BackgroundColor = System.Drawing.Color.White;
			this.grIncorrectLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grIncorrectLinks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdentifier,
            this.colWindow,
            this.colLink,
            this.colPath});
			this.grIncorrectLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grIncorrectLinks.Location = new System.Drawing.Point(0, 0);
			this.grIncorrectLinks.MultiSelect = false;
			this.grIncorrectLinks.Name = "grIncorrectLinks";
			this.grIncorrectLinks.ReadOnly = true;
			this.grIncorrectLinks.RowHeadersVisible = false;
			this.grIncorrectLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grIncorrectLinks.Size = new System.Drawing.Size(606, 512);
			this.grIncorrectLinks.TabIndex = 1;
			// 
			// colIdentifier
			// 
			this.colIdentifier.HeaderText = "Identifier";
			this.colIdentifier.Name = "colIdentifier";
			this.colIdentifier.ReadOnly = true;
			this.colIdentifier.Visible = false;
			// 
			// colWindow
			// 
			this.colWindow.HeaderText = "Window";
			this.colWindow.Name = "colWindow";
			this.colWindow.ReadOnly = true;
			this.colWindow.Width = 79;
			// 
			// colLink
			// 
			this.colLink.HeaderText = "Link";
			this.colLink.Name = "colLink";
			this.colLink.ReadOnly = true;
			this.colLink.Width = 57;
			// 
			// colPath
			// 
			this.colPath.HeaderText = "Path";
			this.colPath.Name = "colPath";
			this.colPath.ReadOnly = true;
			this.colPath.Width = 60;
			// 
			// FormDeleteIncorrectLinks
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(764, 512);
			this.Controls.Add(this.grIncorrectLinks);
			this.Controls.Add(this.pnButtons);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FormDeleteIncorrectLinks";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "{0} Links";
			this.Load += new System.EventHandler(this.DeadLinksForm_Load);
			this.pnButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grIncorrectLinks)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnButtons;
		private System.Windows.Forms.DataGridView grIncorrectLinks;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWindow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLink;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
		private DevComponents.DotNetBar.ButtonX buttonXExpiredDate;
		private DevComponents.DotNetBar.ButtonX buttonXDelete;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
    }
}