namespace SalesDepot.CustomControls
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
            this.laFolderName = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnRight = new System.Windows.Forms.Panel();
            this.pnIndex = new System.Windows.Forms.Panel();
            this.laIndex = new System.Windows.Forms.Label();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.pnTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grFiles)).BeginInit();
            this.pnMain.SuspendLayout();
            this.pnIndex.SuspendLayout();
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grFiles.DefaultCellStyle = dataGridViewCellStyle2;
            this.grFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grFiles.GridColor = System.Drawing.Color.White;
            this.grFiles.Location = new System.Drawing.Point(0, 39);
            this.grFiles.MultiSelect = false;
            this.grFiles.Name = "grFiles";
            this.grFiles.ReadOnly = true;
            this.grFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grFiles.RowHeadersVisible = false;
            this.grFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grFiles.Size = new System.Drawing.Size(209, 227);
            this.grFiles.TabIndex = 1;
            this.grFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grFiles_CellClick);
            this.grFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grFiles_MouseDown);
            this.grFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grFiles_MouseMove);
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
            // laFolderName
            // 
            this.laFolderName.BackColor = System.Drawing.Color.Black;
            this.laFolderName.Dock = System.Windows.Forms.DockStyle.Top;
            this.laFolderName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laFolderName.ForeColor = System.Drawing.Color.White;
            this.laFolderName.Location = new System.Drawing.Point(0, 0);
            this.laFolderName.Name = "laFolderName";
            this.laFolderName.Size = new System.Drawing.Size(209, 39);
            this.laFolderName.TabIndex = 1;
            this.laFolderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laFolderName.UseMnemonic = false;
            this.laFolderName.Click += new System.EventHandler(this.laFolderName_Click);
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnMain.Controls.Add(this.grFiles);
            this.pnMain.Controls.Add(this.laFolderName);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(50, 20);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(211, 268);
            this.pnMain.TabIndex = 4;
            // 
            // pnRight
            // 
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnRight.Location = new System.Drawing.Point(261, 20);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(50, 268);
            this.pnRight.TabIndex = 6;
            this.pnRight.Visible = false;
            // 
            // pnIndex
            // 
            this.pnIndex.Controls.Add(this.laIndex);
            this.pnIndex.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnIndex.Location = new System.Drawing.Point(0, 20);
            this.pnIndex.Name = "pnIndex";
            this.pnIndex.Size = new System.Drawing.Size(50, 268);
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
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 288);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(311, 20);
            this.pnBottom.TabIndex = 5;
            this.pnBottom.Visible = false;
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnIndex);
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pnTop);
            this.Controls.Add(this.pnBottom);
            this.Name = "FolderBoxControl";
            this.Size = new System.Drawing.Size(311, 308);
            this.Load += new System.EventHandler(this.FileBoxControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grFiles)).EndInit();
            this.pnMain.ResumeLayout(false);
            this.pnIndex.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridView grFiles;
        protected System.Windows.Forms.ToolTip ttCellInfo;
        protected System.Windows.Forms.Label laFolderName;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnIndex;
        private System.Windows.Forms.Label laIndex;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
    }
}
