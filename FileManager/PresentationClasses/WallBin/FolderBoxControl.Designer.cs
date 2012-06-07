namespace FileManager.PresentationClasses.WallBin
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ttCellInfo = new System.Windows.Forms.ToolTip(this.components);
            this.pnGrid = new System.Windows.Forms.Panel();
            this.grFiles = new System.Windows.Forms.DataGridView();
            this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnHeaderBorder = new System.Windows.Forms.Panel();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.labelControlText = new DevExpress.XtraEditors.LabelControl();
            this.pnGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grFiles)).BeginInit();
            this.pnHeaderBorder.SuspendLayout();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // ttCellInfo
            // 
            this.ttCellInfo.IsBalloon = true;
            this.ttCellInfo.ToolTipTitle = "Information About File";
            // 
            // pnGrid
            // 
            this.pnGrid.Controls.Add(this.grFiles);
            this.pnGrid.Controls.Add(this.pnHeaderBorder);
            this.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnGrid.Location = new System.Drawing.Point(0, 0);
            this.pnGrid.Name = "pnGrid";
            this.pnGrid.Padding = new System.Windows.Forms.Padding(1);
            this.pnGrid.Size = new System.Drawing.Size(311, 308);
            this.pnGrid.TabIndex = 3;
            this.pnGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBorders_Paint);
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
            this.grFiles.ColumnHeadersVisible = false;
            this.grFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDisplayName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grFiles.DefaultCellStyle = dataGridViewCellStyle5;
            this.grFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grFiles.Location = new System.Drawing.Point(1, 40);
            this.grFiles.MultiSelect = false;
            this.grFiles.Name = "grFiles";
            this.grFiles.ReadOnly = true;
            this.grFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grFiles.RowHeadersVisible = false;
            this.grFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grFiles.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.grFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grFiles.Size = new System.Drawing.Size(309, 267);
            this.grFiles.TabIndex = 3;
            this.grFiles.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grWindowFiles_CellBeginEdit);
            this.grFiles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grWindowFiles_CellEndEdit);
            this.grFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grFiles_CellFormatting);
            this.grFiles.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grFiles_CellMouseClick);
            this.grFiles.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grFiles_CellMouseDown);
            this.grFiles.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grFiles_CellMouseLeave);
            this.grFiles.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grFiles_CellMouseMove);
            this.grFiles.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grFiles_CellPainting);
            this.grFiles.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grFiles_RowsRemoved);
            this.grFiles.SelectionChanged += new System.EventHandler(this.grFiles_SelectionChanged);
            this.grFiles.Click += new System.EventHandler(this.grFiles_Click);
            this.grFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.grFiles_DragDrop);
            this.grFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.grFiles_DragEnter);
            this.grFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.grFiles_DragOver);
            this.grFiles.DragLeave += new System.EventHandler(this.grFiles_DragLeave);
            this.grFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grFiles_MouseDown);
            // 
            // colDisplayName
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.colDisplayName.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDisplayName.HeaderText = "Display Name and Note";
            this.colDisplayName.Name = "colDisplayName";
            this.colDisplayName.ReadOnly = true;
            this.colDisplayName.Width = 5;
            // 
            // pnHeaderBorder
            // 
            this.pnHeaderBorder.Controls.Add(this.pnHeader);
            this.pnHeaderBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeaderBorder.Location = new System.Drawing.Point(1, 1);
            this.pnHeaderBorder.Name = "pnHeaderBorder";
            this.pnHeaderBorder.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pnHeaderBorder.Size = new System.Drawing.Size(309, 39);
            this.pnHeaderBorder.TabIndex = 5;
            this.pnHeaderBorder.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBorders_Paint);
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.labelControlText);
            this.pnHeader.Controls.Add(this.pbImage);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pnHeader.Size = new System.Drawing.Size(309, 38);
            this.pnHeader.TabIndex = 4;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.SystemColors.Control;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(43, 37);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.laFolderName_Click);
            this.pbImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseDown);
            this.pbImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseMove);
            // 
            // labelControlText
            // 
            this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControlText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControlText.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControlText.Location = new System.Drawing.Point(43, 0);
            this.labelControlText.Name = "labelControlText";
            this.labelControlText.Size = new System.Drawing.Size(266, 37);
            this.labelControlText.TabIndex = 3;
            this.labelControlText.UseMnemonic = false;
            this.labelControlText.Click += new System.EventHandler(this.laFolderName_Click);
            this.labelControlText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseDown);
            this.labelControlText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.laFolderName_MouseMove);
            // 
            // FolderBoxControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnGrid);
            this.Name = "FolderBoxControl";
            this.Size = new System.Drawing.Size(311, 308);
            this.Load += new System.EventHandler(this.FileBoxControl_Load);
            this.pnGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grFiles)).EndInit();
            this.pnHeaderBorder.ResumeLayout(false);
            this.pnHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolTip ttCellInfo;
        protected System.Windows.Forms.Panel pnGrid;
        private System.Windows.Forms.DataGridView grFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pnHeaderBorder;
        private DevExpress.XtraEditors.LabelControl labelControlText;
    }
}
