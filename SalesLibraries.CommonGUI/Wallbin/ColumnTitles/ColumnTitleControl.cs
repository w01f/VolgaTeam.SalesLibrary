using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.CommonGUI.Wallbin.ColumnTitles
{
	[ToolboxItem(false)]
	public partial class ColumnTitleControl : UserControl
	{
		public ColumnTitle Data { get; private set; }

		public ColumnTitleControl(ColumnTitle data)
		{
			InitializeComponent();
			Data = data;

			BackColor = Data.Settings.BackgroundColor;
			pbLogo.BackColor = Data.Settings.BackgroundColor;
			labelControlText.BackColor = Data.Settings.BackgroundColor;
			if (Data.Settings.ShowText && !string.IsNullOrEmpty(Data.Settings.Text.Trim()))
			{
				labelControlText.Visible = true;
				labelControlText.Text = Data.Settings.Text;
				labelControlText.Font = Data.Settings.HeaderFont;
				labelControlText.ForeColor = Data.Settings.ForeColor;
				switch (Data.Settings.HeaderAlignment)
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
				if (Data.Banner.Enable && Data.Banner.Image != null)
				{
					pbLogo.Visible = true;
					pbLogo.Image = Data.Banner.Image;
					pbLogo.Width = Data.Banner.Image.Width;
				}
				else if (Data.Widget.Enable && Data.Widget.Image != null)
				{
					pbLogo.Visible = false;
					labelControlText.Appearance.Image = Data.Widget.Image;
				}
				else
					pbLogo.Visible = false;
			}
			else if (Data.Banner.Enable && Data.Banner.Image != null)
			{
				labelControlText.Visible = false;
				pbLogo.Visible = true;
				switch (Data.Banner.ImageAlignement)
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
				pbLogo.Image = Data.Banner.Image;
				pbLogo.Width = Data.Banner.Image.Width;
			}
			else if (Data.Widget.Enable && Data.Widget.Image != null)
			{
				labelControlText.Visible = false;
				pbLogo.Visible = true;
				switch (Data.Settings.HeaderAlignment)
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
				pbLogo.Image = Data.Widget.Image;
				pbLogo.Width = Data.Widget.Image.Width;
			}
		}

		public int GetHeight()
		{
			var textHeight = 0;
			var imageHeight = 0;
			if (Data.Settings.ShowText && !string.IsNullOrEmpty(Data.Settings.Text.Trim()))
				using (Graphics g = labelControlText.CreateGraphics())
					textHeight = (int)g.MeasureString(Data.Settings.Text, 
						Data.Settings.HeaderFont, 
						new Size(
							labelControlText.Width - (Data.Widget.Enable && Data.Widget != null ? Data.Widget.Image.Width : 0), 
							Int32.MaxValue)
						)
					.Height;

			if (Data.Banner.Enable && Data.Banner.Image != null)
				imageHeight = Data.Banner.Image.Height;
			else if (Data.Widget.Enable && Data.Widget.Image != null)
				imageHeight = Data.Widget.Image.Height;

			return textHeight > imageHeight ? textHeight : imageHeight;
		}
	}
}