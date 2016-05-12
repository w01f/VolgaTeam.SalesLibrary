using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Common;
using SalesLibraries.Common.Helpers;
using Alignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.Alignment;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkBanner : MetroForm, ILinkSettingsEditForm
	{
		private readonly BaseLibraryLink _sourceLink;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Banner,
		};

		public FormEditLinkBanner(BaseLibraryLink sourceLink)
		{
			_sourceLink = sourceLink;
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
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}
		}

		public void InitForm(LinkSettingsType settingsType)
		{
			Width = 970;
			Height = 645;
			Text = string.Format(Text, _sourceLink.Name);
			StartPosition = FormStartPosition.CenterScreen;

			LoadData();
		}

		private void LoadData()
		{
			xtraTabControlBanners.TabPages.Clear();
			xtraTabControlBanners.TabPages.AddRange(
				MainController.Instance.Lists.Banners.Items.Select(imageGroup =>
				{
					var tabPage = BaseLinkImagesContainer.Create(imageGroup);
					tabPage.SelectedImageChanged += OnSelectedBannerChanged;
					tabPage.OnImageDoubleClick += OnImageDoubleClick;
					return (XtraTabPage)tabPage;
				}).ToArray());
			xtraTabControlBanners.SelectedPageChanged += (o, e) => ((BaseLinkImagesContainer)e.Page).Init();
			((BaseLinkImagesContainer)xtraTabControlBanners.SelectedTabPage).Init();

			checkBoxEnableBanner.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			checkBoxEnableBanner.Checked = _sourceLink.Banner.Enable && MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			checkEditInvert.Checked = _sourceLink.Banner.Inverted;
			pbSelectedBanner.Image = _sourceLink.Banner.Enable ? _sourceLink.Banner.Image : null;
			switch (_sourceLink.Banner.ImageAlignement)
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
			checkBoxBannerShowText.Checked = _sourceLink.Banner.ImageAlignement == Alignment.Left && _sourceLink.Banner.ShowText;
			buttonEditBannerTextFont.Tag = _sourceLink.Banner.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(_sourceLink.Banner.Font);
			colorEditBannerTextColor.Color = _sourceLink.Banner.ForeColor;
			memoEditBannerText.EditValue = !String.IsNullOrEmpty(_sourceLink.Banner.Text) ? _sourceLink.Banner.Text : _sourceLink.Name;
			memoEditBannerText.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.Appearance.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = _sourceLink.Banner.Font;
			memoEditBannerText.ForeColor = _sourceLink.Banner.ForeColor;
			memoEditBannerText.BackColor = _sourceLink.BannerBackColor;
		}

		private void SaveData()
		{
			_sourceLink.Banner.Enable = checkBoxEnableBanner.Checked;
			_sourceLink.Banner.Inverted = checkEditInvert.Checked;
			_sourceLink.Banner.Image = pbSelectedBanner.Image;
			if (rbBannerAligmentLeft.Checked)
				_sourceLink.Banner.ImageAlignement = Alignment.Left;
			else if (rbBannerAligmentCenter.Checked)
				_sourceLink.Banner.ImageAlignement = Alignment.Center;
			else if (rbBannerAligmentRight.Checked)
				_sourceLink.Banner.ImageAlignement = Alignment.Right;
			_sourceLink.Banner.ShowText = _sourceLink.Banner.ImageAlignement == Alignment.Left && checkBoxBannerShowText.Checked;
			_sourceLink.Banner.Text = _sourceLink.Banner.ShowText ? memoEditBannerText.EditValue as String : null;
			_sourceLink.Banner.Font = buttonEditBannerTextFont.Tag as Font;
			_sourceLink.Banner.ForeColor = colorEditBannerTextColor.Color;
		}

		private void FormEditLinkSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
		}

		private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
		{
			pnControls.Enabled = checkBoxEnableBanner.Checked;
			pnSearch.Enabled = checkBoxEnableBanner.Checked;
		}

		private void OnSelectedBannerChanged(object sender, LinkImageEventArgs e)
		{
			pbSelectedBanner.Image = e.Image;
		}

		private void OnBannerAligmentChanged(object sender, EventArgs e)
		{
			var enableText = rbBannerAligmentLeft.Checked;
			checkBoxBannerShowText.Enabled = enableText;
			checkBoxBannerShowText.Checked = checkBoxBannerShowText.Checked && enableText;
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
			DialogResult = DialogResult.OK;
			Close();
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
	}
}