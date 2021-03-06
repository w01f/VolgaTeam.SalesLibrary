﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	[IntendForClass(typeof(LaunchScreenItem))]
	//public partial class LaunchScreenEditControl : UserControl, ILinkBundleInfoEditControl
	public partial class LaunchScreenEditControl : XtraTabPage, ILinkBundleInfoEditControl
	{
		private LaunchScreenItem _bundleItem;

		public LaunchScreenEditControl(LaunchScreenItem bundleItem)
		{
			_bundleItem = bundleItem;
			InitializeComponent();

			Text = LaunchScreenItem.ItemName;

			memoEditHeader.EnableSelectAll();
			memoEditFooter.EnableSelectAll();

			buttonEditHeaderTextFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditHeaderTextFont.Click += EditorHelper.OnFontEditClick;
			buttonEditFooterTextFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditFooterTextFont.Click += EditorHelper.OnFontEditClick;

			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLaunchScreenLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLaunchScreenLogo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLaunchScreenLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLaunchScreenLogo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void LoadData()
		{
			memoEditHeader.EditValue = _bundleItem.Header;
			memoEditFooter.EditValue = _bundleItem.Footer;

			pictureEditLaunchScreenLogo.Image = _bundleItem.Logo;
			pictureEditBanner.Image = _bundleItem.Banner;

			colorEditHeaderTextColor.Color = _bundleItem.HeaderForeColor;
			colorEditHeaderBackColor.Color = _bundleItem.HeaderBackColor;
			buttonEditHeaderTextFont.Tag = _bundleItem.HeaderFont;
			buttonEditHeaderTextFont.EditValue = Utils.FontToString(_bundleItem.HeaderFont);

			colorEditFooterTextColor.Color = _bundleItem.FooterForeColor;
			colorEditFooterBackColor.Color = _bundleItem.FooterBackColor;
			buttonEditFooterTextFont.Tag = _bundleItem.FooterFont;
			buttonEditFooterTextFont.EditValue = Utils.FontToString(_bundleItem.FooterFont);
		}

		public void SaveData()
		{
			_bundleItem.Header = memoEditHeader.EditValue as String;
			_bundleItem.Footer = memoEditFooter.EditValue as String;

			_bundleItem.Logo = pictureEditLaunchScreenLogo.Image;
			_bundleItem.Banner = pictureEditBanner.Image;

			_bundleItem.HeaderForeColor = colorEditHeaderTextColor.Color;
			_bundleItem.HeaderBackColor = colorEditHeaderBackColor.Color;
			_bundleItem.HeaderFont = buttonEditHeaderTextFont.Tag as Font;

			_bundleItem.FooterForeColor = colorEditFooterTextColor.Color;
			_bundleItem.FooterBackColor = colorEditFooterBackColor.Color;
			_bundleItem.FooterFont = buttonEditFooterTextFont.Tag as Font;
		}

		public void Release()
		{
			_bundleItem = null;
		}

		private void OnLaunchScreenLogoEditClick(object sender, EventArgs e)
		{
			using (var form = new FormImageGallery<Banner>(MainController.Instance.Lists.Banners))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				pictureEditLaunchScreenLogo.Image = (Image)form.OriginalImage.Clone();
			}
		}

		private void OnBannerEditClick(object sender, EventArgs e)
		{
			using (var form = new FormImageGallery<Banner>(MainController.Instance.Lists.Banners))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				pictureEditBanner.Image = (Image)form.OriginalImage.Clone();
			}
		}

		private void OnLaunchScreenLogoDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) &&
				e.Data.GetData(DataFormats.FileDrop, true) is String[])
			{
				var imageFilePath = (e.Data.GetData(DataFormats.FileDrop) as String[] ?? new string[] { }).FirstOrDefault();
				if (imageFilePath == null) return;
				var tempFile = Path.GetTempFileName();
				File.Copy(imageFilePath, tempFile, true);
				using (var originalImage = Image.FromFile(tempFile))
				{
					pictureEditLaunchScreenLogo.Image = originalImage.Height > 100 ? originalImage.Resize(new Size(originalImage.Width, 100)) : Image.FromFile(tempFile);
				}
			}
		}

		private void OnBannerDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) &&
				e.Data.GetData(DataFormats.FileDrop, true) is String[])
			{
				var imageFilePath = (e.Data.GetData(DataFormats.FileDrop) as String[] ?? new string[] { }).FirstOrDefault();
				if (imageFilePath == null) return;
				var tempFile = Path.GetTempFileName();
				File.Copy(imageFilePath, tempFile, true);
				pictureEditBanner.Image = Image.FromFile(tempFile);
			}
		}

		private void OnImageDragOver(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) && e.Data.GetData(DataFormats.FileDrop, true) is String[])
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void OnHeaderTextColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditHeader.ForeColor = colorEditHeaderTextColor.Color;
		}

		private void OnHeaderBackColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditHeader.BackColor = colorEditHeaderBackColor.Color;
		}

		private void OnHeaderFontEditValueChanged(object sender, EventArgs e)
		{
			memoEditHeader.Font = (Font)buttonEditHeaderTextFont.Tag;
			memoEditHeader.Properties.Appearance.Font = memoEditHeader.Font;
			memoEditHeader.Properties.AppearanceDisabled.Font = memoEditHeader.Font;
			memoEditHeader.Properties.AppearanceFocused.Font = memoEditHeader.Font;
			memoEditHeader.Properties.AppearanceReadOnly.Font = memoEditHeader.Font;
		}

		private void OnFooterTextColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditFooter.ForeColor = colorEditFooterTextColor.Color;
		}

		private void OnFooterBackColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditFooter.BackColor = colorEditFooterBackColor.Color;
		}

		private void OnFooterFontEditValueChanged(object sender, EventArgs e)
		{
			memoEditFooter.Font = (Font)buttonEditFooterTextFont.Tag;
			memoEditFooter.Properties.Appearance.Font = memoEditFooter.Font;
			memoEditFooter.Properties.AppearanceDisabled.Font = memoEditFooter.Font;
			memoEditFooter.Properties.AppearanceFocused.Font = memoEditFooter.Font;
			memoEditFooter.Properties.AppearanceReadOnly.Font = memoEditFooter.Font;
		}
	}
}
