namespace SalesLibraries.CommonGUI.Wallbin.ColumnTitles
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
			this.labelControlText = new DevExpress.XtraEditors.LabelControl();
			this.SuspendLayout();
			// 
			// labelControlText
			// 
			this.labelControlText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlText.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.labelControlText.Location = new System.Drawing.Point(0, 0);
			this.labelControlText.Name = "labelControlText";
			this.labelControlText.Size = new System.Drawing.Size(248, 109);
			this.labelControlText.TabIndex = 2;
			this.labelControlText.UseMnemonic = false;
			// 
			// ColumnTitleControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.labelControlText);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ColumnTitleControl";
			this.Size = new System.Drawing.Size(248, 109);
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControlText;
    }
}
