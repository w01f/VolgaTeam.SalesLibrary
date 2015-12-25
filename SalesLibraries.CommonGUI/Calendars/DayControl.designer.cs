namespace SalesLibraries.CommonGUI.Calendars
{
	sealed partial class DayControl
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
            this.SuspendLayout();
            // 
            // DayControl
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Click += new System.EventHandler(this.DayControl_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseDown);
            this.MouseEnter += new System.EventHandler(this.DayControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DayControl_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DayControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
