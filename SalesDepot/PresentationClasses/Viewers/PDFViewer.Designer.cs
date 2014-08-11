namespace SalesDepot.PresentationClasses.Viewers
{
    partial class PDFViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDFViewer));
			this.axAcroPDF = new AxAcroPDFLib.AxAcroPDF();
			((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).BeginInit();
			this.SuspendLayout();
			// 
			// axAcroPDF
			// 
			this.axAcroPDF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axAcroPDF.Enabled = true;
			this.axAcroPDF.Location = new System.Drawing.Point(0, 0);
			this.axAcroPDF.Name = "axAcroPDF";
			this.axAcroPDF.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF.OcxState")));
			this.axAcroPDF.Size = new System.Drawing.Size(407, 332);
			this.axAcroPDF.TabIndex = 0;
			// 
			// PDFViewer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.axAcroPDF);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PDFViewer";
			this.Size = new System.Drawing.Size(407, 332);
			((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private AxAcroPDFLib.AxAcroPDF axAcroPDF;
    }
}
