namespace SalesDepot.PresentationClasses.Viewers
{
    partial class EmptyViewer
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
            this.laMessage = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // laMessage
            // 
            this.laMessage.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laMessage.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.laMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.laMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.laMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laMessage.Location = new System.Drawing.Point(0, 0);
            this.laMessage.Name = "laMessage";
            this.laMessage.Size = new System.Drawing.Size(407, 332);
            this.laMessage.TabIndex = 0;
            this.laMessage.Text = "Select file to preview";
            // 
            // EmptyViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.laMessage);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EmptyViewer";
            this.Size = new System.Drawing.Size(407, 332);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl laMessage;
    }
}
