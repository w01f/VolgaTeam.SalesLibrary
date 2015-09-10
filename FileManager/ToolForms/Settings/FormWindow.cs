using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using FileManager.BusinessClasses;
using FileManager.PresentationClasses.WallBin;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.ToolForms.Settings
{
	public enum WindowPropertiesType
	{
		None,
		Appearnce,
		Widget,
		Banner
	}

	public partial class FormWindow : MetroForm
	{
		private readonly LibraryFolder _folder;
		private readonly WindowPropertiesType _propertiesType;

		public FormWindow(LibraryFolder folder, WindowPropertiesType propertiesType = WindowPropertiesType.None)
		{
			InitializeComponent();
			_folder = folder;
			_propertiesType = propertiesType;
			LoadData();

			ckApllyForAllWindowsAppearance.Checked = _folder.Parent.Parent.ApplyAppearanceForAllWindows;
			ckApllyForAllWindowsWidget.Checked = _folder.Parent.Parent.ApplyWidgetForAllWindows;
			ckApllyForAllWindowsBanner.Checked = _folder.Parent.Parent.ApplyBannerForAllWindows;
			xtraTabPageWindowPropertiesBanner.PageEnabled = Directory.Exists(ListManager.Instance.Banners.BannerFolder);
			xtraTabPageWindowPropertiesWidget.PageEnabled = Directory.Exists(ListManager.Instance.Widgets.WidgetFolder);
			textEditName.MouseDown += FormMain.Instance.EditorMouseDown;
			textEditName.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditName.Enter += FormMain.Instance.EditorEnter;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laHeaderBackColor.Font = new Font(laHeaderBackColor.Font.FontFamily, laHeaderBackColor.Font.Size - 2, laHeaderBackColor.Font.Style);
				laHeaderFont.Font = new Font(laHeaderFont.Font.FontFamily, laHeaderFont.Font.Size - 2, laHeaderFont.Font.Style);
				laHeaderForeColor.Font = new Font(laHeaderForeColor.Font.FontFamily, laHeaderForeColor.Font.Size - 2, laHeaderForeColor.Font.Style);
				laBorderColor.Font = new Font(laBorderColor.Font.FontFamily, laBorderColor.Font.Size - 2, laBorderColor.Font.Style);
				laBackColor.Font = new Font(laBackColor.Font.FontFamily, laBackColor.Font.Size - 2, laBackColor.Font.Style);
				laForeColor.Font = new Font(laForeColor.Font.FontFamily, laForeColor.Font.Size - 2, laForeColor.Font.Style);
				laHeaderAlignment.Font = new Font(laHeaderAlignment.Font.FontFamily, laHeaderAlignment.Font.Size - 2, laHeaderAlignment.Font.Style);
				laAvailableBanners.Font = new Font(laAvailableBanners.Font.FontFamily, laAvailableBanners.Font.Size - 2, laAvailableBanners.Font.Style);
				laBannerAligment.Font = new Font(laBannerAligment.Font.FontFamily, laBannerAligment.Font.Size - 2, laBannerAligment.Font.Style);
				laSelectedBanner.Font = new Font(laSelectedBanner.Font.FontFamily, laSelectedBanner.Font.Size - 2, laSelectedBanner.Font.Style);
				laAvailableWidgets.Font = new Font(laAvailableWidgets.Font.FontFamily, laAvailableWidgets.Font.Size - 2, laAvailableWidgets.Font.Style);
				laSelectedWidget.Font = new Font(laSelectedWidget.Font.FontFamily, laSelectedWidget.Font.Size - 2, laSelectedWidget.Font.Style);
				ckApllyForAllWindowsAppearance.Font = new Font(ckApllyForAllWindowsAppearance.Font.FontFamily, ckApllyForAllWindowsAppearance.Font.Size - 2, ckApllyForAllWindowsAppearance.Font.Style);
				ckApllyForAllWindowsBanner.Font = new Font(ckApllyForAllWindowsBanner.Font.FontFamily, ckApllyForAllWindowsBanner.Font.Size - 2, ckApllyForAllWindowsBanner.Font.Style);
				ckEnableBanner.Font = new Font(ckEnableBanner.Font.FontFamily, ckEnableBanner.Font.Size - 2, ckEnableBanner.Font.Style);
				checkBoxBannerShowText.Font = new Font(checkBoxBannerShowText.Font.FontFamily, checkBoxBannerShowText.Font.Size - 2, checkBoxBannerShowText.Font.Style);
				ckApllyForAllWindowsWidget.Font = new Font(ckApllyForAllWindowsWidget.Font.FontFamily, ckApllyForAllWindowsWidget.Font.Size - 2, ckApllyForAllWindowsWidget.Font.Style);
				ckEnableWidget.Font = new Font(ckEnableWidget.Font.FontFamily, ckEnableWidget.Font.Size - 2, ckEnableWidget.Font.Style);
				rbHeaderAlignmentCenter.Font = new Font(rbHeaderAlignmentCenter.Font.FontFamily, rbHeaderAlignmentCenter.Font.Size - 2, rbHeaderAlignmentCenter.Font.Style);
				rbHeaderAlignmentLeft.Font = new Font(rbHeaderAlignmentLeft.Font.FontFamily, rbHeaderAlignmentLeft.Font.Size - 2, rbHeaderAlignmentLeft.Font.Style);
				rbWindowHeaderAlignmentRight.Font = new Font(rbWindowHeaderAlignmentRight.Font.FontFamily, rbWindowHeaderAlignmentRight.Font.Size - 2, rbWindowHeaderAlignmentRight.Font.Style);
				rbBannerAlignmentCenter.Font = new Font(rbBannerAlignmentCenter.Font.FontFamily, rbBannerAlignmentCenter.Font.Size - 2, rbBannerAlignmentCenter.Font.Style);
				rbBannerAlignmentLeft.Font = new Font(rbBannerAlignmentLeft.Font.FontFamily, rbBannerAlignmentLeft.Font.Size - 2, rbBannerAlignmentLeft.Font.Style);
				rbBannerAlignmentRight.Font = new Font(rbBannerAlignmentRight.Font.FontFamily, rbBannerAlignmentRight.Font.Size - 2, rbBannerAlignmentRight.Font.Style);
				xtraTabControlWindowProperties.Appearance.Font = new Font(xtraTabControlWindowProperties.Appearance.Font.FontFamily, xtraTabControlWindowProperties.Appearance.Font.Size - 2, xtraTabControlWindowProperties.Appearance.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.Header.Font = new Font(xtraTabControlWindowProperties.AppearancePage.Header.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.Header.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.Header.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Style);
			}
		}

		private void LoadData()
		{
			switch (_propertiesType)
			{
				case WindowPropertiesType.None:
					xtraTabPageWindowPropertiesAppearance.PageVisible = true;
					xtraTabPageWindowPropertiesWidget.PageVisible = true;
					xtraTabPageWindowPropertiesBanner.PageVisible = true;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.True;
					break;
				case WindowPropertiesType.Appearnce:
					xtraTabPageWindowPropertiesAppearance.PageVisible = true;
					xtraTabPageWindowPropertiesWidget.PageVisible = false;
					xtraTabPageWindowPropertiesBanner.PageVisible = false;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.False;
					break;
				case WindowPropertiesType.Widget:
					xtraTabPageWindowPropertiesAppearance.PageVisible = false;
					xtraTabPageWindowPropertiesWidget.PageVisible = true;
					xtraTabPageWindowPropertiesBanner.PageVisible = false;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.False;
					break;
				case WindowPropertiesType.Banner:
					xtraTabPageWindowPropertiesAppearance.PageVisible = false;
					xtraTabPageWindowPropertiesWidget.PageVisible = false;
					xtraTabPageWindowPropertiesBanner.PageVisible = true;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.False;
					break;
			}

			laLocation.Text = String.Format("Location: {0}", "Window " + (_folder.RowOrder + 1) + " - Column " + (_folder.ColumnOrder + 1));
			textEditName.EditValue = _folder.Name;
			colorEditWindowHeaderBackColor.Color = _folder.BackgroundHeaderColor;
			colorEditWindowHeaderForeColor.Color = _folder.ForeHeaderColor;
			colorEditWindowBackColor.Color = _folder.BackgroundWindowColor;
			colorEditWindowForeColor.Color = _folder.ForeWindowColor;
			colorEditWindowBorderColor.Color = _folder.BorderColor;
			if (_folder.HeaderFont != null)
			{
				buttonEditWindowHeaderFont.Tag = _folder.HeaderFont;
				buttonEditWindowHeaderFont.EditValue = Utils.FontToString(_folder.HeaderFont);
			}
			else
				buttonEditWindowHeaderFont.EditValue = string.Empty;
			switch (_folder.HeaderAlignment)
			{
				case Alignment.Left:
					rbHeaderAlignmentLeft.Checked = true;
					break;
				case Alignment.Center:
					rbHeaderAlignmentCenter.Checked = true;
					break;
				case Alignment.Right:
					rbWindowHeaderAlignmentRight.Checked = true;
					break;
			}
			ckApllyForAllWindowsAppearance.Visible = _propertiesType == WindowPropertiesType.None;
			ckApllyForAllWindowsAppearance.Checked = _propertiesType == WindowPropertiesType.None && _folder.Parent.Parent.ApplyAppearanceForAllWindows;

			xtraTabControlWidgets.TabPages.Clear();
			foreach (var imageGroup in ListManager.Instance.Widgets.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
				xtraTabControlWidgets.TabPages.Add(tabPage);
			}
			pbSelectedWidget.Image = _folder.EnableWidget ? _folder.Widget : null;
			ckEnableWidget.Checked = _folder.EnableWidget;
			ckApllyForAllWindowsWidget.Visible = _propertiesType == WindowPropertiesType.None;
			ckApllyForAllWindowsWidget.Checked = _propertiesType == WindowPropertiesType.None && _folder.Parent.Parent.ApplyWidgetForAllWindows;

			xtraTabControlBanners.TabPages.Clear();
			foreach (var imageGroup in ListManager.Instance.Banners.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedBannerChanged;
				xtraTabControlBanners.TabPages.Add(tabPage);
			}
			ckEnableBanner.Checked = _folder.BannerProperties.Enable;
			pbSelectedBanner.Image = _folder.BannerProperties.Enable ? _folder.BannerProperties.Image : null;
			switch (_folder.BannerProperties.ImageAlignement)
			{
				case Alignment.Left:
					rbBannerAlignmentLeft.Checked = true;
					break;
				case Alignment.Center:
					rbBannerAlignmentCenter.Checked = true;
					break;
				case Alignment.Right:
					rbBannerAlignmentRight.Checked = true;
					break;
			}
			checkBoxBannerShowText.Checked = _folder.BannerProperties.ShowText;
			memoEditBannerText.EditValue = _folder.BannerProperties.Text;
			buttonEditBannerTextFont.Tag = _folder.BannerProperties.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(_folder.BannerProperties.Font);
			colorEditBannerTextColor.Color = _folder.BannerProperties.ForeColor;
			memoEditBannerText.ForeColor = colorEditBannerTextColor.Color;
			memoEditBannerText.Font = _folder.BannerProperties.Font;
			memoEditBannerText.Properties.Appearance.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = memoEditBannerText.Font;
			ckApllyForAllWindowsBanner.Visible = _propertiesType == WindowPropertiesType.None;
			ckApllyForAllWindowsBanner.Checked = _propertiesType == WindowPropertiesType.None && _folder.Parent.Parent.ApplyBannerForAllWindows;
		}

		private void SaveData()
		{
			_folder.Name = textEditName.EditValue as String;
			_folder.BackgroundHeaderColor = colorEditWindowHeaderBackColor.Color;
			_folder.ForeHeaderColor = colorEditWindowHeaderForeColor.Color;
			_folder.BackgroundWindowColor = colorEditWindowBackColor.Color;
			_folder.ForeWindowColor = colorEditWindowForeColor.Color;
			_folder.BorderColor = colorEditWindowBorderColor.Color;
			_folder.HeaderFont = buttonEditWindowHeaderFont.Tag as Font;
			if (rbHeaderAlignmentLeft.Checked)
				_folder.HeaderAlignment = Alignment.Left;
			else if (rbHeaderAlignmentCenter.Checked)
				_folder.HeaderAlignment = Alignment.Center;
			else if (rbWindowHeaderAlignmentRight.Checked)
				_folder.HeaderAlignment = Alignment.Right;
			if (_propertiesType == WindowPropertiesType.None)
				_folder.Parent.Parent.ApplyAppearanceForAllWindows = ckApllyForAllWindowsAppearance.Checked;

			_folder.EnableWidget = ckEnableWidget.Checked;
			_folder.Widget = _folder.EnableWidget ? pbSelectedWidget.Image : null;
			if (_propertiesType == WindowPropertiesType.None)
				_folder.Parent.Parent.ApplyWidgetForAllWindows = ckApllyForAllWindowsWidget.Checked;

			_folder.BannerProperties.Enable = ckEnableBanner.Checked;
			_folder.BannerProperties.Image = _folder.BannerProperties.Enable ? pbSelectedBanner.Image : null;
			if (rbBannerAlignmentLeft.Checked)
				_folder.BannerProperties.ImageAlignement = Alignment.Left;
			else if (rbBannerAlignmentCenter.Checked)
				_folder.BannerProperties.ImageAlignement = Alignment.Center;
			else if (rbBannerAlignmentRight.Checked)
				_folder.BannerProperties.ImageAlignement = Alignment.Right;
			_folder.BannerProperties.ShowText = checkBoxBannerShowText.Checked;
			_folder.BannerProperties.Text = memoEditBannerText.EditValue != null ? memoEditBannerText.EditValue.ToString() : string.Empty;
			_folder.BannerProperties.Font = buttonEditBannerTextFont.Tag as Font;
			_folder.BannerProperties.ForeColor = colorEditBannerTextColor.Color;
			_folder.BannerProperties.Configured = true;
			if (_propertiesType == WindowPropertiesType.None)
				_folder.Parent.Parent.ApplyBannerForAllWindows = ckApllyForAllWindowsBanner.Checked;
			if (_propertiesType == WindowPropertiesType.None)
				foreach (var libraryFolder in _folder.Parent.Folders.Where(f => f != _folder))
					_folder.Parent.ApplyFolderSettings(libraryFolder, _folder);
		}

		#region Widgets
		private void checkBoxEnableWidget_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxWidgets.Enabled = ckEnableWidget.Checked;
			if (ckEnableWidget.Checked)
				ckEnableBanner.Checked = false;
		}

		private void xtraTabControlWidgets_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			laWidgetFileName.Text = string.Empty;
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			pbSelectedWidget.Image = e.Image;
			laWidgetFileName.Text = string.Empty;
		}
		#endregion

		#region Banners
		private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxBanners.Enabled = ckEnableBanner.Checked;
			if (ckEnableBanner.Checked)
				ckEnableWidget.Checked = false;
		}

		private void xtraTabControlBanners_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
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
		#endregion

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

		private void FormWindowSettings_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveData();
		}
	}
}
