using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ColumnTitleControl : UserControl
    {
        public BusinessClasses.ColumnTitle Data { get; private set; }

        public ColumnTitleControl(BusinessClasses.ColumnTitle data)
        {
            InitializeComponent();
            this.Data = data;

            this.BackColor = this.Data.BackgroundColor;
            pbLogo.BackColor = this.Data.BackgroundColor;
            laColumnTitle.BackColor = this.Data.BackgroundColor;
            if (this.Data.EnableText && !string.IsNullOrEmpty(this.Data.Name.Trim()))
            {
                laColumnTitle.Visible = true;
                laColumnTitle.Text = this.Data.Name;
                laColumnTitle.Font = this.Data.HeaderFont;
                laColumnTitle.ForeColor = this.Data.ForeColor;
                switch (this.Data.HeaderAlignment)
                {
                    case BusinessClasses.Alignment.Left:
                        laColumnTitle.TextAlign = ContentAlignment.MiddleLeft;
                        break;
                    case BusinessClasses.Alignment.Center:
                        laColumnTitle.TextAlign = ContentAlignment.MiddleCenter;
                        break;
                    case BusinessClasses.Alignment.Right:
                        laColumnTitle.TextAlign = ContentAlignment.MiddleRight;
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
                    pbLogo.Visible = true;
                    pbLogo.Image = this.Data.Widget;
                    pbLogo.Width = this.Data.Widget.Width;
                }
            }
            else if (this.Data.BannerProperties.Enable && this.Data.BannerProperties.Image != null)
            {
                laColumnTitle.Visible = false;
                pbLogo.Visible = true;
                switch (this.Data.HeaderAlignment)
                {
                    case BusinessClasses.Alignment.Left:
                        pbLogo.Dock = DockStyle.Left;
                        break;
                    case BusinessClasses.Alignment.Center:
                        pbLogo.Dock = DockStyle.Fill;
                        break;
                    case BusinessClasses.Alignment.Right:
                        pbLogo.Dock = DockStyle.Right;
                        break;
                }
                pbLogo.Image = this.Data.BannerProperties.Image;
                pbLogo.Width = this.Data.BannerProperties.Image.Width;
            }
            else if (this.Data.EnableWidget && this.Data.Widget != null)
            {
                laColumnTitle.Visible = false;
                pbLogo.Visible = true;
                switch (this.Data.HeaderAlignment)
                {
                    case BusinessClasses.Alignment.Left:
                        pbLogo.Dock = DockStyle.Left;
                        break;
                    case BusinessClasses.Alignment.Center:
                        pbLogo.Dock = DockStyle.Fill;
                        break;
                    case BusinessClasses.Alignment.Right:
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
            {
                Size labelSize = new Size(this.Width, Int32.MaxValue);
                textHeight = TextRenderer.MeasureText(this.Data.Name, this.Data.HeaderFont, labelSize, TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix).Height;
            }

            if (this.Data.BannerProperties.Enable && this.Data.BannerProperties.Image != null)
                imageHeight = this.Data.BannerProperties.Image.Height;
            else if (this.Data.EnableWidget && this.Data.Widget != null)
                imageHeight = this.Data.Widget.Height;

            return textHeight > imageHeight ? textHeight : imageHeight;
        }
    }
}
