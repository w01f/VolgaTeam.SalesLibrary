namespace SalesLibraries.CommonGUI.Wallbin.Folders
{
	partial class BaseFolderBox
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
        protected virtual void InitializeComponent()
        {
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.grFiles = new System.Windows.Forms.DataGridView();
			this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pnHeaderBorder = new System.Windows.Forms.Panel();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.pnBorders = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.grFiles)).BeginInit();
			this.pnHeaderBorder.SuspendLayout();
			this.pnBorders.SuspendLayout();
			this.SuspendLayout();
			// 
			// grFiles
			// 
			this.grFiles.AllowDrop = true;
			this.grFiles.AllowUserToAddRows = false;
			this.grFiles.AllowUserToDeleteRows = false;
			this.grFiles.AllowUserToResizeColumns = false;
			this.grFiles.AllowUserToResizeRows = false;
			this.grFiles.BackgroundColor = System.Drawing.Color.White;
			this.grFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.grFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.grFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grFiles.ColumnHeadersVisible = false;
			this.grFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDisplayName});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grFiles.DefaultCellStyle = dataGridViewCellStyle2;
			this.grFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.grFiles.Location = new System.Drawing.Point(1, 52);
			this.grFiles.Name = "grFiles";
			this.grFiles.ReadOnly = true;
			this.grFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.grFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.grFiles.RowHeadersVisible = false;
			this.grFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.grFiles.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.grFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grFiles.Size = new System.Drawing.Size(309, 216);
			this.grFiles.TabIndex = 3;
			// 
			// colDisplayName
			// 
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.colDisplayName.DefaultCellStyle = dataGridViewCellStyle1;
			this.colDisplayName.HeaderText = "Display Name and Note";
			this.colDisplayName.Name = "colDisplayName";
			this.colDisplayName.ReadOnly = true;
			this.colDisplayName.Width = 5;
			// 
			// pnHeaderBorder
			// 
			this.pnHeaderBorder.AllowDrop = true;
			this.pnHeaderBorder.Controls.Add(this.pnHeader);
			this.pnHeaderBorder.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeaderBorder.Location = new System.Drawing.Point(1, 1);
			this.pnHeaderBorder.Name = "pnHeaderBorder";
			this.pnHeaderBorder.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.pnHeaderBorder.Size = new System.Drawing.Size(309, 51);
			this.pnHeaderBorder.TabIndex = 5;
			// 
			// pnHeader
			// 
			this.pnHeader.AllowDrop = true;
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(309, 50);
			this.pnHeader.TabIndex = 4;
			// 
			// pnBorders
			// 
			this.pnBorders.AllowDrop = true;
			this.pnBorders.Controls.Add(this.grFiles);
			this.pnBorders.Controls.Add(this.pnHeaderBorder);
			this.pnBorders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBorders.Location = new System.Drawing.Point(0, 0);
			this.pnBorders.Name = "pnBorders";
			this.pnBorders.Padding = new System.Windows.Forms.Padding(1);
			this.pnBorders.Size = new System.Drawing.Size(311, 308);
			this.pnBorders.TabIndex = 6;
			// 
			// BaseFolderBox
			// 
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.pnBorders);
			this.Name = "BaseFolderBox";
			this.Size = new System.Drawing.Size(311, 308);
			((System.ComponentModel.ISupportInitialize)(this.grFiles)).EndInit();
			this.pnHeaderBorder.ResumeLayout(false);
			this.pnBorders.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.DataGridView grFiles;
		protected System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
		protected System.Windows.Forms.Panel pnHeader;
		protected System.Windows.Forms.Panel pnHeaderBorder;
		protected System.Windows.Forms.Panel pnBorders;
    }
}
