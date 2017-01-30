using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.ImageGallery;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.LinkBundles.SingleBundle
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

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}

			memoEditHeader.EnableSelectAll();
			memoEditFooter.EnableSelectAll();

			buttonEditHeaderTextFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditHeaderTextFont.Click += EditorHelper.FontEdit_Click;
			buttonEditFooterTextFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditFooterTextFont.Click += EditorHelper.FontEdit_Click;
		}

		public void LoadData()
		{
			memoEditHeader.EditValue = _bundleItem.Header;
			memoEditFooter.EditValue = _bundleItem.Footer;

			pictureEditLogo.Image = _bundleItem.Logo;
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

			_bundleItem.Logo = pictureEditLogo.Image;
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

		private void OnLogoEditClick(object sender, EventArgs e)
		{
			using (var form = new FormImageGallery(MainController.Instance.Lists.Banners.Items))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				pictureEditLogo.Image = Image.FromFile(form.SelectedImageSource.FilePath);
			}
		}

		private void OnBannerEditClick(object sender, EventArgs e)
		{
			using (var form = new FormImageGallery(MainController.Instance.Lists.Banners.Items))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				pictureEditBanner.Image = Image.FromFile(form.SelectedImageSource.FilePath);
			}
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
