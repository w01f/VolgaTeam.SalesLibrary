using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Common;
using Alignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.Alignment;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkBanner : MetroForm, ILinkSettingsEditForm
	{
		private readonly BaseLibraryLink _sourceLink;

		public LinkSettingsType[] EditableSettings
		{
			get
			{
				return new[]
				{
					LinkSettingsType.Banner,
				};
			}
		}

		public bool IsForEmbedded
		{
			get { return false; }
		}

		public FormEditLinkBanner(BaseLibraryLink sourceLink)
		{
			_sourceLink = sourceLink;
			InitializeComponent();
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
			foreach (var imageGroup in MainController.Instance.Lists.Banners.Items)
			{
				var tabPage = BaseLinkImagesContainer.Create(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedBannerChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlBanners.TabPages.Add(tabPage);
			}

			checkBoxEnableBanner.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			checkBoxEnableBanner.Checked = _sourceLink.Banner.Enable && MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
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
			memoEditBannerText.EditValue = !String.IsNullOrEmpty(_sourceLink.Banner.Text)?_sourceLink.Banner.Text: _sourceLink.Name;
			memoEditBannerText.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.Appearance.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = _sourceLink.Banner.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = _sourceLink.Banner.Font;
			memoEditBannerText.ForeColor = _sourceLink.Banner.ForeColor;
		}

		private void SaveData()
		{
			_sourceLink.Banner.Enable = checkBoxEnableBanner.Checked;
			_sourceLink.Widget.WidgetType = !_sourceLink.Banner.Enable ? _sourceLink.Widget.WidgetType : _sourceLink.Widget.DefaultWidgetType;
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
	}
}