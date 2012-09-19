using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ColumnTitleControl : UserControl
    {
        public CoreObjects.ColumnTitle Data { get; private set; }

        public ColumnTitleControl(CoreObjects.ColumnTitle data)
        {
            InitializeComponent();
            this.Data = data;

            this.BackColor = this.Data.BackgroundColor;
            pbLogo.BackColor = this.Data.BackgroundColor;
            labelControlText.BackColor = this.Data.BackgroundColor;
            if (this.Data.EnableText && !string.IsNullOrEmpty(this.Data.Name.Trim()))
            {
                labelControlText.Visible = true;
                labelControlText.Text = this.Data.Name;
                labelControlText.Font = this.Data.HeaderFont;
                labelControlText.ForeColor = this.Data.ForeColor;
                switch (this.Data.HeaderAlignment)
                {
                    case CoreObjects.Alignment.Left:
                        labelControlText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                        break;
                    case CoreObjects.Alignment.Center:
                        labelControlText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        break;
                    case CoreObjects.Alignment.Right:
                        labelControlText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        break;
                }
                pbLogo.Dock = DockStyle.Left;
                if (this.Data.BannerProperties.Enable && this.Data.BannerProperties.Image != null)
                {
                    pbLogo.Visible = true;
                    pbLogo.Image = this.Data.BannerProperties.Image;
                    pbLogo.Width = this.Data.BannerProperties.Image.Width;
                }
                else if (this.Data.EnableWidget && this.Data.Widget != null)
                {
                    pbLogo.Visible = false;
                    labelControlText.Appearance.Image = this.Data.Widget;
                }
                else
                    pbLogo.Visible = false;
            }
            else if (this.Data.BannerProperties.Enable && this.Data.BannerProperties.Image != null)
            {
                labelControlText.Visible = false;
                pbLogo.Visible = true;
                switch (this.Data.HeaderAlignment)
                {
                    case CoreObjects.Alignment.Left:
                        pbLogo.Dock = DockStyle.Left;
                        break;
                    case CoreObjects.Alignment.Center:
                        pbLogo.Dock = DockStyle.Fill;
                        break;
                    case CoreObjects.Alignment.Right:
                        pbLogo.Dock = DockStyle.Right;
                        break;
                }
                pbLogo.Image = this.Data.BannerProperties.Image;
                pbLogo.Width = this.Data.BannerProperties.Image.Width;
            }
            else if (this.Data.EnableWidget && this.Data.Widget != null)
            {
                labelControlText.Visible = false;
                pbLogo.Visible = true;
                switch (this.Data.HeaderAlignment)
                {
                    case CoreObjects.Alignment.Left:
                        pbLogo.Dock = DockStyle.Left;
                        break;
                    case CoreObjects.Alignment.Center:
                        pbLogo.Dock = DockStyle.Fill;
                        break;
                    case CoreObjects.Alignment.Right:
                        pbLogo.Dock = DockStyle.Right;
                        break;
                }
                pbLogo.Image = this.Data.Widget;
                pbLogo.Width = this.Data.Widget.Width;
            }
        }

        public int GetHeight()
        {
            int textHeight = 0;
            int imageHeight = 0;
            if (this.Data.EnableText && !string.IsNullOrEmpty(this.Data.Name.Trim()))
                using (Graphics g = labelControlText.CreateGraphics())
                    textHeight = (int)g.MeasureString(this.Data.Name, this.Data.HeaderFont, new Size(labelControlText.Width - (this.Data.EnableWidget && this.Data.Widget != null ? this.Data.Widget.Width : 0), Int32.MaxValue)).Height;

            if (this.Data.BannerProperties.Enable && this.Data.BannerProperties.Image != null)
                imageHeight = this.Data.BannerProperties.Image.Height;
            else if (this.Data.EnableWidget && this.Data.Widget != null)
                imageHeight = this.Data.Widget.Height;

            return textHeight > imageHeight ? textHeight : imageHeight;
        }
    }
}
