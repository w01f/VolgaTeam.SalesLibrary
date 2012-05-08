namespace FileManager.PresentationClasses.WallBin
{
    partial class ColumnTitleControl
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
            this.laColumnTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // laColumnTitle
            // 
            this.laColumnTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laColumnTitle.Location = new System.Drawing.Point(0, 0);
            this.laColumnTitle.Name = "laColumnTitle";
            this.laColumnTitle.Size = new System.Drawing.Size(248, 109);
            this.laColumnTitle.TabIndex = 0;
            this.laColumnTitle.Text = "label1";
            this.laColumnTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laColumnTitle.UseMnemonic = false;
            // 
            // ColumnTitleControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.laColumnTitle);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ColumnTitleControl";
            this.Size = new System.Drawing.Size(248, 109);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label laColumnTitle;
    }
}
