using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.CommonGUI.Wallbin.ColumnTitles
{
	[ToolboxItem(false)]
	public partial class ColumnTitleControl : UserControl
	{
		public ColumnTitle Data { get; }

		public ColumnTitleControl(ColumnTitle data)
		{
			InitializeComponent();
			Data = data;

			BackColor = Data.Settings.BackgroundColor;
			labelControlText.BackColor = Data.Settings.BackgroundColor;

			if (Data.Banner.Enable && Data.Banner.DisplayedImage != null)
			{
				labelControlText.Appearance.Image = Data.Banner.DisplayedImage;
			}
			else if (Data.Widget.Enabled && Data.Widget.DisplayedImage != null)
			{
				labelControlText.Appearance.Image = Data.Widget.DisplayedImage;
			}
			else
				labelControlText.Appearance.Image = null;

			var alignment = Data.Banner.Enable && Data.Banner.DisplayedImage != null
				? Data.Banner.ImageAlignement
				: Data.Settings.HeaderAlignment;
			switch (alignment)
			{
				case HorizontalAlignment.Left:
					labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
					break;
				case HorizontalAlignment.Center:
					labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
					break;
				case HorizontalAlignment.Right:
					labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
					break;
			}

			if (Data.Banner.Enable && Data.Banner.TextEnabled && !String.IsNullOrEmpty(Data.Banner.Text.Trim()))
			{
				labelControlText.Text = Data.Banner.Text;
				labelControlText.Font = Data.Banner.Font;
				labelControlText.ForeColor = Data.Banner.ForeColor;
			}
			else if (Data.Settings.ShowText && !String.IsNullOrEmpty(Data.Settings.Text.Trim()))
			{
				labelControlText.Text = Data.Settings.Text;
				labelControlText.Font = Data.Settings.HeaderFont;
				labelControlText.ForeColor = Data.Settings.ForeColor;
			}
		}

		public int GetHeight()
		{
			var textHeight = 0;
			var imageHeight = 0;
			if (Data.Settings.ShowText && !string.IsNullOrEmpty(Data.Settings.Text.Trim()))
				using (var g = labelControlText.CreateGraphics())
					textHeight = (int)g.MeasureString(Data.Settings.Text,
						Data.Settings.HeaderFont,
						new Size(
							labelControlText.Width - (Data.Widget.Enabled && Data.Widget != null ? Data.Widget.DisplayedImage.Width : 0),
							Int32.MaxValue)
						)
					.Height;

			if (Data.Banner.Enable && Data.Banner.DisplayedImage != null)
				imageHeight = Data.Banner.DisplayedImage.Height;
			else if (Data.Widget.Enabled && Data.Widget.Image != null)
				imageHeight = Data.Widget.DisplayedImage.Height;

			return textHeight > imageHeight ? textHeight : imageHeight;
		}
	}
}