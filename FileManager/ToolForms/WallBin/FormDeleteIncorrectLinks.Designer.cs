namespace FileManager.ToolForm.WallBin
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
            this.btCancel = new System.Windows.Forms.Button();
            this.btExpiredDate = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
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
            this.pnButtons.Controls.Add(this.btCancel);
            this.pnButtons.Controls.Add(this.btExpiredDate);
            this.pnButtons.Controls.Add(this.btOK);
            this.pnButtons.Controls.Add(this.btDelete);
            this.pnButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnButtons.Location = new System.Drawing.Point(606, 0);
            this.pnButtons.Name = "pnButtons";
            this.pnButtons.Size = new System.Drawing.Size(158, 512);
            this.pnButtons.TabIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btCancel.Location = new System.Drawing.Point(10, 457);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(136, 43);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btExpiredDate
            // 
            this.btExpiredDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btExpiredDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btExpiredDate.Location = new System.Drawing.Point(10, 62);
            this.btExpiredDate.Name = "btExpiredDate";
            this.btExpiredDate.Size = new System.Drawing.Size(136, 44);
            this.btExpiredDate.TabIndex = 3;
            this.btExpiredDate.Text = "Expired Date\r\nOptions";
            this.btExpiredDate.UseVisualStyleBackColor = true;
            this.btExpiredDate.Click += new System.EventHandler(this.btExpiredDate_Click);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btOK.Location = new System.Drawing.Point(10, 408);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(136, 43);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btDelete.Location = new System.Drawing.Point(10, 12);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(136, 44);
            this.btDelete.TabIndex = 0;
            this.btDelete.Text = "Delete\r\nthis Link";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(764, 512);
            this.Controls.Add(this.grIncorrectLinks);
            this.Controls.Add(this.pnButtons);
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
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.DataGridView grIncorrectLinks;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWindow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btExpiredDate;
    }
}