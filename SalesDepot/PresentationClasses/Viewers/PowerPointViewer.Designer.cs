namespace SalesDepot.PresentationClasses.Viewers
{
    partial class PowerPointViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerPointViewer));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnNavigationArea = new System.Windows.Forms.Panel();
            this.laSlideSize = new System.Windows.Forms.Label();
            this.laFileInfo = new System.Windows.Forms.Label();
            this.laSlideNumber = new System.Windows.Forms.Label();
            this.comboBoxEditSlides = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.pnNavigationArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlides.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pnNavigationArea
            // 
            this.pnNavigationArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnNavigationArea.Controls.Add(this.laSlideSize);
            this.pnNavigationArea.Controls.Add(this.laFileInfo);
            this.pnNavigationArea.Controls.Add(this.laSlideNumber);
            this.pnNavigationArea.Controls.Add(this.comboBoxEditSlides);
            this.pnNavigationArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnNavigationArea.Location = new System.Drawing.Point(0, 352);
            this.pnNavigationArea.Name = "pnNavigationArea";
            this.pnNavigationArea.Size = new System.Drawing.Size(705, 67);
            this.pnNavigationArea.TabIndex = 1;
            // 
            // laSlideSize
            // 
            this.laSlideSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laSlideSize.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSlideSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laSlideSize.Location = new System.Drawing.Point(456, 0);
            this.laSlideSize.Name = "laSlideSize";
            this.laSlideSize.Size = new System.Drawing.Size(242, 31);
            this.laSlideSize.TabIndex = 8;
            this.laSlideSize.Text = "label1";
            this.laSlideSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laFileInfo
            // 
            this.laFileInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.laFileInfo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laFileInfo.ForeColor = System.Drawing.Color.White;
            this.laFileInfo.Location = new System.Drawing.Point(0, 0);
            this.laFileInfo.Name = "laFileInfo";
            this.laFileInfo.Size = new System.Drawing.Size(244, 63);
            this.laFileInfo.TabIndex = 7;
            this.laFileInfo.Text = "label1";
            this.laFileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laSlideNumber
            // 
            this.laSlideNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.laSlideNumber.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laSlideNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.laSlideNumber.Location = new System.Drawing.Point(456, 32);
            this.laSlideNumber.Name = "laSlideNumber";
            this.laSlideNumber.Size = new System.Drawing.Size(242, 31);
            this.laSlideNumber.TabIndex = 6;
            this.laSlideNumber.Text = "label1";
            this.laSlideNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEditSlides
            // 
            this.comboBoxEditSlides.EditValue = "";
            this.comboBoxEditSlides.Location = new System.Drawing.Point(260, 6);
            this.comboBoxEditSlides.Name = "comboBoxEditSlides";
            this.comboBoxEditSlides.Properties.Appearance.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEditSlides.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditSlides.Properties.Appearance.Options.UseTextOptions = true;
            this.comboBoxEditSlides.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.comboBoxEditSlides.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEditSlides.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditSlides.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.comboBoxEditSlides.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.comboBoxEditSlides.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("comboBoxEditSlides.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("comboBoxEditSlides.Properties.Buttons1"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.comboBoxEditSlides.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditSlides.Size = new System.Drawing.Size(184, 54);
            this.comboBoxEditSlides.TabIndex = 5;
            this.comboBoxEditSlides.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditSlides_SelectedIndexChanged);
            this.comboBoxEditSlides.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.comboBoxEditSlides_ButtonClick);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(705, 352);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 5;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.DoubleClick += new System.EventHandler(this.pictureBoxPreview_DoubleClick);
            // 
            // PowerPointViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.pnNavigationArea);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PowerPointViewer";
            this.Size = new System.Drawing.Size(705, 419);
            this.Resize += new System.EventHandler(this.pnNavigationArea_Resize);
            this.pnNavigationArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlides.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnNavigationArea;
        private System.Windows.Forms.Label laSlideSize;
        private System.Windows.Forms.Label laFileInfo;
        private System.Windows.Forms.Label laSlideNumber;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlides;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
    }
}
