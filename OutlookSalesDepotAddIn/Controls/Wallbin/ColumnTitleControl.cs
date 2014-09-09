using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Wallbin
{
	[ToolboxItem(false)]
	public partial class ColumnTitleControl : UserControl
	{
		public ColumnTitleControl(ColumnTitle data)
		{
			InitializeComponent();
			Data = data;

			BackColor = Data.BackgroundColor;
			pbLogo.BackColor = Data.BackgroundColor;
			labelControlText.BackColor = Data.BackgroundColor;
			if (Data.EnableText && !String.IsNullOrEmpty(Data.Name))
			{
				labelControlText.Visible = true;
				labelControlText.Text = Data.Name;
				labelControlText.Font = Data.HeaderFont;
				labelControlText.ForeColor = Data.ForeColor;
				switch (Data.HeaderAlignment)
				{
					case Alignment.Left:
						labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
						break;
					case Alignment.Center:
						labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
						break;
					case Alignment.Right:
						labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
						break;
				}
				pbLogo.Dock = DockStyle.Left;
				if (Data.BannerProperties.Enable && Data.BannerProperties.Image != null)
				{
					pbLogo.Visible = true;
					pbLogo.Image = Data.BannerProperties.Image;
					pbLogo.Width = Data.BannerProperties.Image.Width;
				}
				else if (Data.EnableWidget && Data.Widget != null)
				{
					pbLogo.Visible = false;
					labelControlText.Appearance.Image = Data.Widget;
				}
				else
					pbLogo.Visible = false;
			}
			else if (Data.BannerProperties.Enable && Data.BannerProperties.Image != null)
			{
				labelControlText.Visible = false;
				pbLogo.Visible = true;
				switch (Data.HeaderAlignment)
				{
					case Alignment.Left:
						pbLogo.Dock = DockStyle.Left;
						break;
					case Alignment.Center:
						pbLogo.Dock = DockStyle.Fill;
						break;
					case Alignment.Right:
						pbLogo.Dock = DockStyle.Right;
						break;
				}
				pbLogo.Image = Data.BannerProperties.Image;
				pbLogo.Width = Data.BannerProperties.Image.Width;
			}
			else if (Data.EnableWidget && Data.Widget != null)
			{
				labelControlText.Visible = false;
				pbLogo.Visible = true;
				switch (Data.HeaderAlignment)
				{
					case Alignment.Left:
						pbLogo.Dock = DockStyle.Left;
						break;
					case Alignment.Center:
						pbLogo.Dock = DockStyle.Fill;
						break;
					case Alignment.Right:
						pbLogo.Dock = DockStyle.Right;
						break;
				}
				pbLogo.Image = Data.Widget;
				pbLogo.Width = Data.Widget.Width;
			}
		}

		public ColumnTitle Data { get; private set; }

		public int GetHeight()
		{
			var textHeight = 0;
			var imageHeight = 0;
			if (Data.EnableText && !string.IsNullOrEmpty(Data.Name))
				using (var g = labelControlText.CreateGraphics())
					textHeight = (int)g.MeasureString(Data.Name, Data.HeaderFont, new Size(labelControlText.Width - (Data.EnableWidget && Data.Widget != null ? Data.Widget.Width : 0), Int32.MaxValue)).Height;

			if (Data.BannerProperties.Enable && Data.BannerProperties.Image != null)
				imageHeight = Data.BannerProperties.Image.Height;
			else if (Data.EnableWidget && Data.Widget != null)
				imageHeight = Data.Widget.Height;

			return textHeight > imageHeight ? textHeight : imageHeight;
		}
	}
}