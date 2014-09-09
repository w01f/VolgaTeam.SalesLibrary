namespace OutlookSalesDepotAddIn.Controls.Wallbin
{
    partial class FolderBoxControl
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.grFiles = new System.Windows.Forms.DataGridView();
			this.ttCellInfo = new System.Windows.Forms.ToolTip(this.components);
			this.pnMain = new System.Windows.Forms.Panel();
			this.xtraScrollableControlGrid = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pnHeaderBorder = new System.Windows.Forms.Panel();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.labelControlText = new DevExpress.XtraEditors.LabelControl();
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.grFiles)).BeginInit();
			this.pnMain.SuspendLayout();
			this.xtraScrollableControlGrid.SuspendLayout();
			this.pnHeaderBorder.SuspendLayout();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.SuspendLayout();
			// 
			// grFiles
			// 
			this.grFiles.AllowUserToAddRows = false;
			this.grFiles.AllowUserToDeleteRows = false;
			this.grFiles.AllowUserToResizeColumns = false;
			this.grFiles.AllowUserToResizeRows = false;
			this.grFiles.BackgroundColor = System.Drawing.Color.White;
			this.grFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.grFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.grFiles.ColumnHeadersVisible = false;
			this.grFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelected,
            this.colDisplayName});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grFiles.DefaultCellStyle = dataGridViewCellStyle2;
			this.grFiles.Dock = System.Windows.Forms.DockStyle.Left;
			this.grFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.grFiles.GridColor = System.Drawing.Color.White;
			this.grFiles.Location = new System.Drawing.Point(0, 0);
			this.grFiles.MultiSelect = false;
			this.grFiles.Name = "grFiles";
			this.grFiles.ReadOnly = true;
			this.grFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.grFiles.RowHeadersVisible = false;
			this.grFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.grFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grFiles.Size = new System.Drawing.Size(207, 319);
			this.grFiles.TabIndex = 1;
			this.grFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grFiles_CellClick);
			// 
			// ttCellInfo
			// 
			this.ttCellInfo.IsBalloon = true;
			this.ttCellInfo.ToolTipTitle = "Information About File";
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.xtraScrollableControlGrid);
			this.pnMain.Controls.Add(this.pnHeaderBorder);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Padding = new System.Windows.Forms.Padding(1, 1, 1, 0);
			this.pnMain.Size = new System.Drawing.Size(311, 375);
			this.pnMain.TabIndex = 4;
			this.pnMain.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBorders_Paint);
			// 
			// xtraScrollableControlGrid
			// 
			this.xtraScrollableControlGrid.Controls.Add(this.grFiles);
			this.xtraScrollableControlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlGrid.Location = new System.Drawing.Point(1, 56);
			this.xtraScrollableControlGrid.Name = "xtraScrollableControlGrid";
			this.xtraScrollableControlGrid.Size = new System.Drawing.Size(309, 319);
			this.xtraScrollableControlGrid.TabIndex = 7;
			// 
			// pnHeaderBorder
			// 
			this.pnHeaderBorder.Controls.Add(this.pnHeader);
			this.pnHeaderBorder.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeaderBorder.Location = new System.Drawing.Point(1, 1);
			this.pnHeaderBorder.Name = "pnHeaderBorder";
			this.pnHeaderBorder.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.pnHeaderBorder.Size = new System.Drawing.Size(309, 55);
			this.pnHeaderBorder.TabIndex = 6;
			this.pnHeaderBorder.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBorders_Paint);
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.labelControlText);
			this.pnHeader.Controls.Add(this.pbImage);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(309, 54);
			this.pnHeader.TabIndex = 5;
			// 
			// labelControlText
			// 
			this.labelControlText.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlText.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.labelControlText.Location = new System.Drawing.Point(43, 0);
			this.labelControlText.Name = "labelControlText";
			this.labelControlText.Size = new System.Drawing.Size(266, 54);
			this.labelControlText.TabIndex = 4;
			this.labelControlText.UseMnemonic = false;
			this.labelControlText.Click += new System.EventHandler(this.laFolderName_Click);
			// 
			// pbImage
			// 
			this.pbImage.BackColor = System.Drawing.SystemColors.Control;
			this.pbImage.Dock = System.Windows.Forms.DockStyle.Left;
			this.pbImage.Location = new System.Drawing.Point(0, 0);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(43, 54);
			this.pbImage.TabIndex = 0;
			this.pbImage.TabStop = false;
			this.pbImage.Click += new System.EventHandler(this.laFolderName_Click);
			// 
			// colSelected
			// 
			this.colSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colSelected.HeaderText = "Selected";
			this.colSelected.MinimumWidth = 32;
			this.colSelected.Name = "colSelected";
			this.colSelected.ReadOnly = true;
			this.colSelected.Width = 32;
			// 
			// colDisplayName
			// 
			this.colDisplayName.DataPropertyName = "DisplayName";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.colDisplayName.DefaultCellStyle = dataGridViewCellStyle1;
			this.colDisplayName.HeaderText = "DisplayName";
			this.colDisplayName.Name = "colDisplayName";
			this.colDisplayName.ReadOnly = true;
			this.colDisplayName.Width = 5;
			// 
			// FolderBoxControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FolderBoxControl";
			this.Size = new System.Drawing.Size(311, 375);
			this.Load += new System.EventHandler(this.FileBoxControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.grFiles)).EndInit();
			this.pnMain.ResumeLayout(false);
			this.xtraScrollableControlGrid.ResumeLayout(false);
			this.pnHeaderBorder.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridView grFiles;
        protected System.Windows.Forms.ToolTip ttCellInfo;
		private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pnHeaderBorder;
		private DevExpress.XtraEditors.LabelControl labelControlText;
		private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlGrid;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
    }
}
