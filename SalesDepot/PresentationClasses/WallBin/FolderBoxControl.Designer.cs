namespace SalesDepot.PresentationClasses.WallBin
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
			this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ttCellInfo = new System.Windows.Forms.ToolTip(this.components);
			this.pnMain = new System.Windows.Forms.Panel();
			this.xtraScrollableControlGrid = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pnHeaderBorder = new System.Windows.Forms.Panel();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.labelControlText = new DevExpress.XtraEditors.LabelControl();
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.buttonXHeader = new DevComponents.DotNetBar.ButtonX();
			this.pnRight = new System.Windows.Forms.Panel();
			this.pnIndex = new System.Windows.Forms.Panel();
			this.laIndex = new System.Windows.Forms.Label();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pnTop = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.grFiles)).BeginInit();
			this.pnMain.SuspendLayout();
			this.xtraScrollableControlGrid.SuspendLayout();
			this.pnHeaderBorder.SuspendLayout();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.pnIndex.SuspendLayout();
			this.pnBottom.SuspendLayout();
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
			this.grFiles.Size = new System.Drawing.Size(207, 279);
			this.grFiles.TabIndex = 1;
			this.grFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grFiles_CellClick);
			this.grFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.grFiles_MouseUp);
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
			this.pnMain.Location = new System.Drawing.Point(50, 20);
			this.pnMain.Name = "pnMain";
			this.pnMain.Padding = new System.Windows.Forms.Padding(1, 1, 1, 0);
			this.pnMain.Size = new System.Drawing.Size(211, 335);
			this.pnMain.TabIndex = 4;
			this.pnMain.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBorders_Paint);
			// 
			// xtraScrollableControlGrid
			// 
			this.xtraScrollableControlGrid.Controls.Add(this.grFiles);
			this.xtraScrollableControlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlGrid.Location = new System.Drawing.Point(1, 56);
			this.xtraScrollableControlGrid.Name = "xtraScrollableControlGrid";
			this.xtraScrollableControlGrid.Size = new System.Drawing.Size(209, 279);
			this.xtraScrollableControlGrid.TabIndex = 7;
			// 
			// pnHeaderBorder
			// 
			this.pnHeaderBorder.Controls.Add(this.pnHeader);
			this.pnHeaderBorder.Controls.Add(this.buttonXHeader);
			this.pnHeaderBorder.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeaderBorder.Location = new System.Drawing.Point(1, 1);
			this.pnHeaderBorder.Name = "pnHeaderBorder";
			this.pnHeaderBorder.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.pnHeaderBorder.Size = new System.Drawing.Size(209, 55);
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
			this.pnHeader.Size = new System.Drawing.Size(209, 54);
			this.pnHeader.TabIndex = 5;
			this.pnHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseUp);
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
			this.labelControlText.Size = new System.Drawing.Size(166, 54);
			this.labelControlText.TabIndex = 4;
			this.labelControlText.UseMnemonic = false;
			this.labelControlText.Click += new System.EventHandler(this.laFolderName_Click);
			this.labelControlText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseUp);
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
			this.pbImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnHeader_MouseUp);
			// 
			// buttonXHeader
			// 
			this.buttonXHeader.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXHeader.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonXHeader.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXHeader.Location = new System.Drawing.Point(0, 0);
			this.buttonXHeader.Name = "buttonXHeader";
			this.buttonXHeader.Size = new System.Drawing.Size(209, 54);
			this.buttonXHeader.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXHeader.TabIndex = 7;
			this.buttonXHeader.Text = "buttonX1 <font color=\"#595959\">(test)</font>";
			this.buttonXHeader.TextColor = System.Drawing.Color.Black;
			this.buttonXHeader.UseMnemonic = false;
			this.buttonXHeader.Click += new System.EventHandler(this.buttonXHeader_Click);
			// 
			// pnRight
			// 
			this.pnRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnRight.Location = new System.Drawing.Point(261, 20);
			this.pnRight.Name = "pnRight";
			this.pnRight.Size = new System.Drawing.Size(50, 355);
			this.pnRight.TabIndex = 6;
			this.pnRight.Visible = false;
			// 
			// pnIndex
			// 
			this.pnIndex.Controls.Add(this.laIndex);
			this.pnIndex.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnIndex.Location = new System.Drawing.Point(0, 20);
			this.pnIndex.Name = "pnIndex";
			this.pnIndex.Size = new System.Drawing.Size(50, 355);
			this.pnIndex.TabIndex = 3;
			this.pnIndex.Visible = false;
			// 
			// laIndex
			// 
			this.laIndex.Dock = System.Windows.Forms.DockStyle.Top;
			this.laIndex.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laIndex.Location = new System.Drawing.Point(0, 0);
			this.laIndex.Name = "laIndex";
			this.laIndex.Size = new System.Drawing.Size(50, 39);
			this.laIndex.TabIndex = 0;
			this.laIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.panel1);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(50, 355);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.pnBottom.Size = new System.Drawing.Size(211, 20);
			this.pnBottom.TabIndex = 5;
			this.pnBottom.Visible = false;
			this.pnBottom.Paint += new System.Windows.Forms.PaintEventHandler(this.pnBottom_Paint);
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(211, 19);
			this.panel1.TabIndex = 0;
			// 
			// pnTop
			// 
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(311, 20);
			this.pnTop.TabIndex = 7;
			this.pnTop.Visible = false;
			// 
			// FolderBoxControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnBottom);
			this.Controls.Add(this.pnIndex);
			this.Controls.Add(this.pnRight);
			this.Controls.Add(this.pnTop);
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
			this.pnIndex.ResumeLayout(false);
			this.pnBottom.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridView grFiles;
        protected System.Windows.Forms.ToolTip ttCellInfo;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnIndex;
        private System.Windows.Forms.Label laIndex;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pnHeaderBorder;
        private DevExpress.XtraEditors.LabelControl labelControlText;
        private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX buttonXHeader;
		private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlGrid;
    }
}
