using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Objects.Graphics;

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
			pbImage.BackColor = DataSource.Settings.BackgroundHeaderColor;
			labelControlText.BackColor = DataSource.Settings.BackgroundHeaderColor;

			if (DataSource.Banner.Enable && DataSource.Banner.DisplayedImage != null)
			{
				pbImage.Visible = true;
				pbImage.Image = DataSource.Banner.DisplayedImage;
				if (DataSource.Banner.ShowText && !String.IsNullOrEmpty(DataSource.Banner.Text))
				{
					labelControlText.Visible = true;
					pbImage.Dock = DockStyle.Left;
					pbImage.SizeMode = PictureBoxSizeMode.Normal;
					labelControlText.Text = DataSource.Banner.Text;
					labelControlText.Font = DataSource.Banner.Font;
					labelControlText.ForeColor = DataSource.Banner.ForeColor;
					switch (DataSource.Banner.ImageAlignement)
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
				}
				else
				{
					labelControlText.Visible = false;
					switch (DataSource.Banner.ImageAlignement)
					{
						case Alignment.Left:
							pbImage.Dock = DockStyle.Left;
							pbImage.SizeMode = PictureBoxSizeMode.Normal;
							break;
						case Alignment.Center:
							pbImage.Dock = DockStyle.Fill;
							pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
							break;
						case Alignment.Right:
							pbImage.Dock = DockStyle.Right;
							pbImage.SizeMode = PictureBoxSizeMode.Normal;
							break;
					}
					pnHeaderBorder.Height = DataSource.Banner.DisplayedImage.Height;
				}
			}
			else
			{
				pbImage.Visible = false;
				labelControlText.Visible = true;
				if (DataSource.Widget.Enabled && DataSource.Widget.DisplayedImage != null)
					labelControlText.Appearance.Image = DataSource.Widget.DisplayedImage;
				else
					labelControlText.Appearance.Image = null;
				labelControlText.Text = DataSource.Name;
				labelControlText.Font = DataSource.Settings.HeaderFont;
				labelControlText.ForeColor = DataSource.Settings.ForeHeaderColor;
				switch (DataSource.Settings.HeaderAlignment)
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
			}
			base.SetupView();
		}

		public override void UpdateHeaderSize()
		{
			int textHeight;
			if (DataSource.Banner.Enable && DataSource.Banner.DisplayedImage != null)
			{
				pbImage.Width = DataSource.Banner.DisplayedImage.Width;
				if (DataSource.Banner.ShowText && !String.IsNullOrEmpty(DataSource.Banner.Text))
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
					pnHeaderBorder.Height = DataSource.Banner.DisplayedImage.Height;
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
