using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class BannerOptions : UserControl, ILinkProperties
	public partial class BannerOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;
		public BannerOptions(LibraryLink data)
		{
			InitializeComponent();
			_data = data;
			LoadData();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
				laAvailableBanners.Font = new Font(laAvailableBanners.Font.FontFamily, laAvailableBanners.Font.Size - 2, laAvailableBanners.Font.Style);
				laSelectedBanner.Font = new Font(laSelectedBanner.Font.FontFamily, laSelectedBanner.Font.Size - 2, laSelectedBanner.Font.Style);
				laBannerAligment.Font = new Font(laBannerAligment.Font.FontFamily, laBannerAligment.Font.Size - 2, laBannerAligment.Font.Style);
				checkBoxEnableBanner.Font = new Font(checkBoxEnableBanner.Font.FontFamily, checkBoxEnableBanner.Font.Size - 2, checkBoxEnableBanner.Font.Style);
				checkBoxBannerShowText.Font = new Font(checkBoxBannerShowText.Font.FontFamily, checkBoxBannerShowText.Font.Size - 2, checkBoxBannerShowText.Font.Style);
				rbBannerAligmentCenter.Font = new Font(rbBannerAligmentCenter.Font.FontFamily, rbBannerAligmentCenter.Font.Size - 2, rbBannerAligmentCenter.Font.Style);
				rbBannerAligmentLeft.Font = new Font(rbBannerAligmentLeft.Font.FontFamily, rbBannerAligmentLeft.Font.Size - 2, rbBannerAligmentLeft.Font.Style);
				rbBannerAligmentRight.Font = new Font(rbBannerAligmentRight.Font.FontFamily, rbBannerAligmentRight.Font.Size - 2, rbBannerAligmentRight.Font.Style);
			}
		}

		private void LoadData()
		{
			xtraTabControlBanners.TabPages.Clear();
			foreach (var imageGroup in ListManager.Instance.Banners)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedBannerChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlBanners.TabPages.Add(tabPage);
			}

			checkBoxEnableBanner.Enabled = Directory.Exists(ListManager.Instance.BannerFolder);
			checkBoxEnableBanner.Checked = _data.BannerProperties.Enable && Directory.Exists(ListManager.Instance.BannerFolder);
			pbSelectedBanner.Image = _data.BannerProperties.Enable ? _data.BannerProperties.Image : null;
			laBannerFileName.Text = string.Empty;
			switch (_data.BannerProperties.ImageAlignement)
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
			checkBoxBannerShowText.Checked = _data.BannerProperties.ShowText;
			buttonEditBannerTextFont.Tag = _data.BannerProperties.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(_data.BannerProperties.Font);
			colorEditBannerTextColor.Color = _data.BannerProperties.ForeColor;
			memoEditBannerText.EditValue = _data.BannerProperties.Text;
			memoEditBannerText.Font = _data.BannerProperties.Font;
			memoEditBannerText.Properties.Appearance.Font = _data.BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = _data.BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = _data.BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = _data.BannerProperties.Font;
			memoEditBannerText.ForeColor = _data.BannerProperties.ForeColor;
		}

		public void SaveData()
		{
			_data.BannerProperties.Enable = checkBoxEnableBanner.Checked;
			_data.EnableWidget = !_data.BannerProperties.Enable && _data.EnableWidget;
			_data.BannerProperties.Image = pbSelectedBanner.Image;
			if (rbBannerAligmentLeft.Checked)
				_data.BannerProperties.ImageAlignement = Alignment.Left;
			else if (rbBannerAligmentCenter.Checked)
				_data.BannerProperties.ImageAlignement = Alignment.Center;
			else if (rbBannerAligmentRight.Checked)
				_data.BannerProperties.ImageAlignement = Alignment.Right;
			_data.BannerProperties.ShowText = checkBoxBannerShowText.Checked;
			_data.BannerProperties.Text = memoEditBannerText.EditValue != null ? memoEditBannerText.EditValue.ToString() : string.Empty;
			_data.BannerProperties.Font = buttonEditBannerTextFont.Tag as Font;
			_data.BannerProperties.ForeColor = colorEditBannerTextColor.Color;
			_data.BannerProperties.Configured = true;
		}

		private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxBanners.Enabled = checkBoxEnableBanner.Checked;
		}

		private void xtraTabControlBanners_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			laBannerFileName.Text = String.Empty;
		}

		private void OnSelectedBannerChanged(object sender, LinkImageEventArgs e)
		{
			pbSelectedBanner.Image = e.Image;
			laBannerFileName.Text = e.Text;
		}

		private void checkBoxBannerShowText_CheckedChanged(object sender, EventArgs e)
		{
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
			if (OnForseClose != null)
				OnForseClose(this, EventArgs.Empty);
		}
	}
}
