using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	[ToolboxItem(false)]
	public partial class BannerSettingsControl : UserControl
	{
		private readonly BannerSettings _data;

		public event EventHandler<CheckedChangedEventArgs> StateChanged;
		public event EventHandler<EventArgs> DoubleClicked;
		public BannerSettingsControl(BannerSettings data)
		{
			InitializeComponent();
			_data = data;

			xtraTabControlBanners.TabPages.Clear();
			foreach (var imageGroup in MainController.Instance.Lists.Banners.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedBannerChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlBanners.TabPages.Add(tabPage);
			}
			
			LoadData();

			buttonEditBannerTextFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditBannerTextFont.Click += EditorHelper.FontEdit_Click;

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

		public void LoadData()
		{
			checkBoxEnableBanner.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			checkBoxEnableBanner.Checked = _data.Enable && MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			pbSelectedBanner.Image = _data.Enable ? _data.Image : null;
			laBannerFileName.Text = string.Empty;
			switch (_data.ImageAlignement)
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
			checkBoxBannerShowText.Checked = _data.ShowText;
			buttonEditBannerTextFont.Tag = _data.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(_data.Font);
			colorEditBannerTextColor.Color = _data.ForeColor;
			memoEditBannerText.EditValue = _data.Text;
			memoEditBannerText.Font = _data.Font;
			memoEditBannerText.Properties.Appearance.Font = _data.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = _data.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = _data.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = _data.Font;
			memoEditBannerText.ForeColor = _data.ForeColor;
		}

		public void SaveData()
		{
			_data.Enable = checkBoxEnableBanner.Checked;
			_data.Image = pbSelectedBanner.Image;
			if (rbBannerAligmentLeft.Checked)
				_data.ImageAlignement = Alignment.Left;
			else if (rbBannerAligmentCenter.Checked)
				_data.ImageAlignement = Alignment.Center;
			else if (rbBannerAligmentRight.Checked)
				_data.ImageAlignement = Alignment.Right;
			_data.ShowText = checkBoxBannerShowText.Checked;
			_data.Text = memoEditBannerText.EditValue != null ? memoEditBannerText.EditValue.ToString() : string.Empty;
			_data.Font = buttonEditBannerTextFont.Tag as Font;
			_data.ForeColor = colorEditBannerTextColor.Color;
		}

		public void ChangeState(bool enable)
		{
			checkBoxEnableBanner.Checked = enable;
		}

		private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxBanners.Enabled = checkBoxEnableBanner.Checked;
			if (StateChanged != null)
				StateChanged(this, new CheckedChangedEventArgs(checkBoxEnableBanner.Checked));
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

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			if (DoubleClicked != null)
				DoubleClicked(this, EventArgs.Empty);
		}
	}
}
