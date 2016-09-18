using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;
using Alignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.Alignment;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	[ToolboxItem(false)]
	public partial class BannerSettingsControl : UserControl
	{
		private bool _loading;
		private readonly IBannerSettingsHolder _bannerHolder;

		public event EventHandler<CheckedChangedEventArgs> StateChanged;
		public event EventHandler<EventArgs> DoubleClicked;
		public event EventHandler<EventArgs> ControlClicked;

		public BannerSettingsControl(IBannerSettingsHolder bannerHolder)
		{
			_bannerHolder = bannerHolder;
			InitializeComponent();
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

				checkBoxEnableBanner.Font = new Font(checkBoxEnableBanner.Font.FontFamily, checkBoxEnableBanner.Font.Size - 2,
					checkBoxEnableBanner.Font.Style);
				checkBoxBannerShowText.Font = new Font(checkBoxBannerShowText.Font.FontFamily, checkBoxBannerShowText.Font.Size - 2,
					checkBoxBannerShowText.Font.Style);
				rbBannerAligmentCenter.Font = new Font(rbBannerAligmentCenter.Font.FontFamily, rbBannerAligmentCenter.Font.Size - 2,
					rbBannerAligmentCenter.Font.Style);
				rbBannerAligmentLeft.Font = new Font(rbBannerAligmentLeft.Font.FontFamily, rbBannerAligmentLeft.Font.Size - 2,
					rbBannerAligmentLeft.Font.Style);
				rbBannerAligmentRight.Font = new Font(rbBannerAligmentRight.Font.FontFamily, rbBannerAligmentRight.Font.Size - 2,
					rbBannerAligmentRight.Font.Style);
				laBannerAligment.Font = new Font(laBannerAligment.Font.FontFamily, laBannerAligment.Font.Size - 2, laBannerAligment.Font.Style);
				laTextFormat.Font = new Font(laTextFormat.Font.FontFamily, laTextFormat.Font.Size - 2, laTextFormat.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}
		}

		public void LoadData()
		{
			_loading = true;
			xtraTabControlBanners.TabPages.Clear();
			xtraTabControlBanners.TabPages.AddRange(
				MainController.Instance.Lists.Banners.Items.Select(imageGroup =>
				{
					var tabPage = BaseLinkImagesContainer.Create(imageGroup);
					tabPage.SelectedImageChanged += OnSelectedBannerChanged;
					tabPage.OnImageDoubleClick += OnImageDoubleClick;
					return (XtraTabPage)tabPage;
				}).ToArray());
			xtraTabControlBanners.SelectedPageChanged += (o, e) =>
			{
				((BaseLinkImagesContainer)e.Page).Init();
			};
			((BaseLinkImagesContainer)xtraTabControlBanners.SelectedTabPage).Init();

			checkBoxEnableBanner.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			checkBoxEnableBanner.Checked = _bannerHolder.Banner.Enable && MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			checkEditInvert.Checked = _bannerHolder.Banner.Inverted;
			pbSelectedBanner.Image = _bannerHolder.Banner.Enable ? _bannerHolder.Banner.Image : null;
			switch (_bannerHolder.Banner.ImageAlignement)
			{
				case Alignment.Left:
					rbBannerAligmentLeft.Checked = true;
					rbBannerAligmentCenter.Checked = false;
					rbBannerAligmentRight.Checked = false;
					break;
				case Alignment.Center:
					rbBannerAligmentLeft.Checked = false;
					rbBannerAligmentCenter.Checked = true;
					rbBannerAligmentRight.Checked = false;
					break;
				case Alignment.Right:
					rbBannerAligmentLeft.Checked = false;
					rbBannerAligmentCenter.Checked = false;
					rbBannerAligmentRight.Checked = true;
					break;
			}
			checkBoxBannerShowText.Checked = _bannerHolder.Banner.ShowText;
			buttonEditBannerTextFont.Tag = _bannerHolder.Banner.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(_bannerHolder.Banner.Font);
			colorEditBannerTextColor.Color = _bannerHolder.Banner.ForeColor;
			memoEditBannerText.EditValue = !String.IsNullOrEmpty(_bannerHolder.Banner.Text) ? _bannerHolder.Banner.Text : null;
			memoEditBannerText.Font = _bannerHolder.Banner.Font;
			memoEditBannerText.Properties.Appearance.Font = _bannerHolder.Banner.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = _bannerHolder.Banner.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = _bannerHolder.Banner.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = _bannerHolder.Banner.Font;
			memoEditBannerText.ForeColor = _bannerHolder.Banner.ForeColor;
			memoEditBannerText.BackColor = _bannerHolder.BannerBackColor;
			_loading = false;
		}

		public void SaveData()
		{
			_bannerHolder.Banner.Enable = checkBoxEnableBanner.Checked;
			_bannerHolder.Banner.Inverted = checkEditInvert.Checked;
			_bannerHolder.Banner.Image = pbSelectedBanner.Image;
			if (rbBannerAligmentLeft.Checked)
				_bannerHolder.Banner.ImageAlignement = Alignment.Left;
			else if (rbBannerAligmentCenter.Checked)
				_bannerHolder.Banner.ImageAlignement = Alignment.Center;
			else if (rbBannerAligmentRight.Checked)
				_bannerHolder.Banner.ImageAlignement = Alignment.Right;
			_bannerHolder.Banner.ShowText = checkBoxBannerShowText.Checked;
			_bannerHolder.Banner.Text = _bannerHolder.Banner.ShowText ? memoEditBannerText.EditValue as String : null;
			_bannerHolder.Banner.Font = buttonEditBannerTextFont.Tag as Font;
			_bannerHolder.Banner.ForeColor = colorEditBannerTextColor.Color;
		}

		public void ChangeState(bool enable)
		{
			_loading = true;
			checkBoxEnableBanner.Checked = enable;
			_loading = false;
		}

		private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkBoxEnableBanner.Checked;
			pnSearch.Enabled = checkBoxEnableBanner.Checked;
			if (!_loading)
				StateChanged?.Invoke(this, new CheckedChangedEventArgs(checkBoxEnableBanner.Checked));
		}

		private void OnSelectedBannerChanged(object sender, LinkImageEventArgs e)
		{
			pbSelectedBanner.Image = e.Image;
		}

		private void checkBoxBannerShowText_CheckedChanged(object sender, EventArgs e)
		{
			laTextFormat.Enabled = checkBoxBannerShowText.Checked;
			memoEditBannerText.Enabled = checkBoxBannerShowText.Checked;
			buttonEditBannerTextFont.Enabled = checkBoxBannerShowText.Checked;
			colorEditBannerTextColor.Enabled = checkBoxBannerShowText.Checked;
		}

		private void colorEditBannerTextColor_EditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.ForeColor = colorEditBannerTextColor.Color;
		}

		private void buttonEditBannerTextFont_EditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.Font = (Font)buttonEditBannerTextFont.Tag;
			memoEditBannerText.Properties.Appearance.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = memoEditBannerText.Font;
		}

		private void FontEdit_Click(object sender, EventArgs e)
		{
			var fontEdit = sender as ButtonEdit;
			if (fontEdit == null) return;
			using (var dlgFont = new FontDialog())
			{
				dlgFont.Font = fontEdit.Tag as Font;
				if (dlgFont.ShowDialog() != DialogResult.OK) return;
				fontEdit.Tag = dlgFont.Font;
				fontEdit.EditValue = Utils.FontToString(dlgFont.Font);
			}
		}

		private void FontEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			FontEdit_Click(sender, null);
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			DoubleClicked?.Invoke(this, EventArgs.Empty);
		}

		private void OnSearchButtonClick(object sender, EventArgs e)
		{
			var keyword = textEditSearch.EditValue as String;
			if (String.IsNullOrEmpty(keyword)) return;
			MainController.Instance.Lists.Banners.SearchResults.LoadImages(keyword);
		}

		private void OnSearchEditValueChanged(object sender, EventArgs e)
		{
			buttonXSearch.Enabled = !String.IsNullOrEmpty(textEditSearch.EditValue as String);
		}

		private void OnSearchKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OnSearchButtonClick(sender, EventArgs.Empty);
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			ControlClicked?.Invoke(sender, e);
		}
	}
}