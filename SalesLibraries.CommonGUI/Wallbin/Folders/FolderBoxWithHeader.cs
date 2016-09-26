using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Objects.Graphics;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.CommonGUI.Wallbin.Folders
{
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	public partial class FolderBoxWithHeader : BaseFolderBox
	{
		private const int MinHeaderWidth = 250;
		public FolderBoxWithHeader()
		{
			InitializeComponent();
		}

		public FolderBoxWithHeader(LibraryFolder dataSource)
			: base(dataSource) { }

		protected override void SetupView()
		{
			labelControlText.BackColor = DataSource.Settings.BackgroundHeaderColor;
			labelControlText.Appearance.Image = null;

			if (DataSource.Banner.Enable && DataSource.Banner.DisplayedImage != null)
			{
				labelControlText.Appearance.Image = DataSource.Banner.DisplayedImage;
				pnHeaderBorder.Height = DataSource.Banner.DisplayedImage.Height;

				labelControlText.Text = DataSource.Banner.Text;
				labelControlText.Font = DataSource.Banner.Font;
				labelControlText.ForeColor = DataSource.Banner.ForeColor;
				
				switch (DataSource.Banner.ImageAlignement)
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
			}
			else
			{
				if (DataSource.Widget.Enabled && DataSource.Widget.DisplayedImage != null)
					labelControlText.Appearance.Image = DataSource.Widget.DisplayedImage;

				labelControlText.Text = DataSource.Name;
				labelControlText.Font = DataSource.Settings.HeaderFont;
				labelControlText.ForeColor = DataSource.Settings.ForeHeaderColor;

				switch (DataSource.Settings.HeaderAlignment)
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
			}

			base.SetupView();
		}

		public override void UpdateHeaderSize()
		{
			int textHeight;
			if (DataSource.Banner.Enable && DataSource.Banner.DisplayedImage != null)
			{
				using (var g = labelControlText.CreateGraphics())
					textHeight = (int)g.MeasureString(
							labelControlText.Text,
							labelControlText.Font,
							new Size(labelControlText.Width < MinHeaderWidth ? MinHeaderWidth : labelControlText.Width, Int32.MaxValue))
						.Height + 10;
				pnHeaderBorder.Height = DataSource.Banner.DisplayedImage.Height > textHeight ? DataSource.Banner.DisplayedImage.Height : textHeight;
			}
			else
			{
				if (DataSource.Widget.Enabled && DataSource.Widget.DisplayedImage != null)
				{
					using (var g = labelControlText.CreateGraphics())
						textHeight = (int)g.MeasureString(
								labelControlText.Text,
								labelControlText.Font,
								new Size((labelControlText.Width < MinHeaderWidth ? MinHeaderWidth : labelControlText.Width) - DataSource.Widget.DisplayedImage.Width, Int32.MaxValue))
							.Height + 10;
					pnHeaderBorder.Height = DataSource.Widget.DisplayedImage.Height > textHeight ? DataSource.Widget.DisplayedImage.Height : (textHeight > Widget.DefaultHeight ? textHeight : Widget.DefaultHeight);
				}
				else
				{
					using (Graphics g = labelControlText.CreateGraphics())
						textHeight = (int)g.MeasureString(
								labelControlText.Text, 
								labelControlText.Font,
								new Size(labelControlText.Width < MinHeaderWidth ? MinHeaderWidth : labelControlText.Width, Int32.MaxValue))
							.Height + 10;
					pnHeaderBorder.Height = textHeight > Widget.DefaultHeight ? textHeight : Widget.DefaultHeight;
				}
			}
			base.UpdateHeaderSize();
		}
	}
}
