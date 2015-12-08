using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	[ToolboxItem(false)]
	public partial class BannerSettingsControl : UserControl
	{
		private bool _loading;
		private readonly BannerSettings _data;

		public event EventHandler<CheckedChangedEventArgs> StateChanged;
		public event EventHandler<EventArgs> DoubleClicked;

		public BannerSettingsControl(BannerSettings data)
		{
			_data = data;
			InitializeComponent();
			LoadData();
		}

		public void LoadData()
		{
			_loading = true;
			xtraTabControlBanners.TabPages.Clear();
			foreach (var imageGroup in MainController.Instance.Lists.Banners.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedBannerChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlBanners.TabPages.Add(tabPage);
			}
			checkBoxEnableBanner.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			checkBoxEnableBanner.Checked = _data.Enable && MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			pbSelectedBanner.Image = _data.Enable ? _data.Image : null;
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
			checkBoxBannerShowText.Checked = _data.ImageAlignement == Alignment.Left && _data.ShowText;
			buttonEditBannerTextFont.Tag = _data.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(_data.Font);
			colorEditBannerTextColor.Color = _data.ForeColor;
			memoEditBannerText.EditValue = !String.IsNullOrEmpty(_data.Text)?_data.Text: null;
			memoEditBannerText.Font = _data.Font;
			memoEditBannerText.Properties.Appearance.Font = _data.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = _data.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = _data.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = _data.Font;
			memoEditBannerText.ForeColor = _data.ForeColor;
			_loading = false;
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
			_data.ShowText = _data.ImageAlignement == Alignment.Left && checkBoxBannerShowText.Checked;
			_data.Text = _data.ShowText ? memoEditBannerText.EditValue as String : null;
			_data.Font = buttonEditBannerTextFont.Tag as Font;
			_data.ForeColor = colorEditBannerTextColor.Color;
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
			if (!_loading && StateChanged != null)StateChanged(this, new CheckedChangedEventArgs(checkBoxEnableBanner.Checked));
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
			if (DoubleClicked != null)
				DoubleClicked(this, EventArgs.Empty);
		}
	}
}