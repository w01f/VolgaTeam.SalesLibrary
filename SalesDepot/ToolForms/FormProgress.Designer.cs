namespace SalesDepot.ToolForms
{
    partial class FormProgress
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
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.prbProgress = new System.Windows.Forms.ProgressBar();
            this.laProgress = new System.Windows.Forms.Label();
            this.gbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMain
            // 
            this.gbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMain.Controls.Add(this.prbProgress);
            this.gbMain.Controls.Add(this.laProgress);
            this.gbMain.Location = new System.Drawing.Point(7, 3);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(386, 83);
            this.gbMain.TabIndex = 3;
            this.gbMain.TabStop = false;
            this.gbMain.UseWaitCursor = true;
            // 
            // prbProgress
            // 
            this.prbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbProgress.Location = new System.Drawing.Point(6, 52);
            this.prbProgress.Maximum = 12;
            this.prbProgress.Name = "prbProgress";
            this.prbProgress.Size = new System.Drawing.Size(373, 23);
            this.prbProgress.Step = 1;
            this.prbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbProgress.TabIndex = 3;
            this.prbProgress.UseWaitCursor = true;
            // 
            // laProgress
            // 
            this.laProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laProgress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laProgress.Location = new System.Drawing.Point(6, 12);
            this.laProgress.Name = "laProgress";
            this.laProgress.Size = new System.Drawing.Size(373, 36);
            this.laProgress.TabIndex = 2;
            this.laProgress.Text = "Loading data...";
            this.laProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laProgress.UseWaitCursor = true;
            // 
            // FormProgress
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(400, 96);
            this.ControlBox = false;
            this.Controls.Add(this.gbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProgressForm";
            this.gbMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbMain;
        internal System.Windows.Forms.ProgressBar prbProgress;
        public System.Windows.Forms.Label laProgress;
    }
}