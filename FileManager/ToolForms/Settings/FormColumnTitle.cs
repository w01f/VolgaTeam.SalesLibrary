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
	public partial class FormColumnTitle : MetroForm
	{
		private readonly ColumnTitle _columnTitle;

		public FormColumnTitle(ColumnTitle columnTitle)
		{
			InitializeComponent();
			_columnTitle = columnTitle;
			LoadData();

			xtraTabPageWindowPropertiesBanner.PageEnabled = Directory.Exists(ListManager.Instance.Banners.BannerFolder);
			xtraTabPageWindowPropertiesWidget.PageEnabled = Directory.Exists(ListManager.Instance.Widgets.WidgetFolder);

			if ((base.CreateGraphics()).DpiX > 96)
			{
				laColumn1BackColor.Font = new Font(laColumn1BackColor.Font.FontFamily, laColumn1BackColor.Font.Size - 2, laColumn1BackColor.Font.Style);
				laForeColor.Font = new Font(laForeColor.Font.FontFamily, laForeColor.Font.Size - 2, laForeColor.Font.Style);
				laFont.Font = new Font(laFont.Font.FontFamily, laFont.Font.Size - 2, laFont.Font.Style);
				laColumn1Alignment.Font = new Font(laColumn1Alignment.Font.FontFamily, laColumn1Alignment.Font.Size - 2, laColumn1Alignment.Font.Style);
				ckEnableText.Font = new Font(ckEnableText.Font.FontFamily, ckEnableText.Font.Size - 2, ckEnableText.Font.Style);
				rbAlignmentCenter.Font = new Font(rbAlignmentCenter.Font.FontFamily, rbAlignmentCenter.Font.Size - 2, rbAlignmentCenter.Font.Style);
				rbAlignmentLeft.Font = new Font(rbAlignmentLeft.Font.FontFamily, rbAlignmentLeft.Font.Size - 2, rbAlignmentLeft.Font.Style);
				rbAlignmentRight.Font = new Font(rbAlignmentRight.Font.FontFamily, rbAlignmentRight.Font.Size - 2, rbAlignmentRight.Font.Style);
				laAvailableBanners.Font = new Font(laAvailableBanners.Font.FontFamily, laAvailableBanners.Font.Size - 2, laAvailableBanners.Font.Style);
				laSelectedBanner.Font = new Font(laSelectedBanner.Font.FontFamily, laSelectedBanner.Font.Size - 2, laSelectedBanner.Font.Style);
				laAvailableWidgets.Font = new Font(laAvailableWidgets.Font.FontFamily, laAvailableWidgets.Font.Size - 2, laAvailableWidgets.Font.Style);
				laSelectedWidget.Font = new Font(laSelectedWidget.Font.FontFamily, laSelectedWidget.Font.Size - 2, laSelectedWidget.Font.Style);
				ckApplyForAllColumnTitles.Font = new Font(ckApplyForAllColumnTitles.Font.FontFamily, ckApplyForAllColumnTitles.Font.Size - 2, ckApplyForAllColumnTitles.Font.Style);
				ckEnableBanner.Font = new Font(ckEnableBanner.Font.FontFamily, ckEnableBanner.Font.Size - 2, ckEnableBanner.Font.Style);
				ckEnableWidget.Font = new Font(ckEnableWidget.Font.FontFamily, ckEnableWidget.Font.Size - 2, ckEnableWidget.Font.Style);
				xtraTabControlWindowProperties.Appearance.Font = new Font(xtraTabControlWindowProperties.Appearance.Font.FontFamily, xtraTabControlWindowProperties.Appearance.Font.Size - 2, xtraTabControlWindowProperties.Appearance.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.Header.Font = new Font(xtraTabControlWindowProperties.AppearancePage.Header.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.Header.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.Header.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Style);
			}
		}

		private void LoadData()
		{
			laLocation.Text = String.Format("Title or column {0}", _columnTitle.ColumnOrder + 1);

			ckApplyForAllColumnTitles.Checked = _columnTitle.Parent.ApplyForAllColumnTitles;
			colorEditBackColor.Color = _columnTitle.BackgroundColor;
			switch (_columnTitle.HeaderAlignment)
			{
				case Alignment.Left:
					rbAlignmentLeft.Checked = true;
					break;
				case Alignment.Center:
					rbAlignmentCenter.Checked = true;
					break;
				case Alignment.Right:
					rbAlignmentRight.Checked = true;
					break;
			}
			ckEnableText.Checked = _columnTitle.EnableText;
			memoEditTitle.EditValue = _columnTitle.Name;
			colorEditForeColor.Color = _columnTitle.ForeColor;
			buttonEditFont.Tag = _columnTitle.HeaderFont;
			buttonEditFont.EditValue = Utils.FontToString(_columnTitle.HeaderFont);
			memoEditTitle.ForeColor = colorEditForeColor.Color;
			memoEditTitle.BackColor = colorEditBackColor.Color;
			memoEditTitle.Font = (Font)buttonEditFont.Tag;
			memoEditTitle.Properties.Appearance.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceDisabled.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceFocused.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceReadOnly.Font = memoEditTitle.Font;

			xtraTabControlWidgets.TabPages.Clear();
			foreach (var imageGroup in ListManager.Instance.Widgets.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
				xtraTabControlWidgets.TabPages.Add(tabPage);
			}
			pbSelectedWidget.Image = _columnTitle.EnableWidget ? _columnTitle.Widget : null;
			ckEnableWidget.Checked = _columnTitle.EnableWidget;

			xtraTabControlBanners.TabPages.Clear();
			foreach (var imageGroup in ListManager.Instance.Banners.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedBannerChanged;
				xtraTabControlBanners.TabPages.Add(tabPage);
			}
			ckEnableBanner.Checked = _columnTitle.BannerProperties.Enable;
			pbSelectedBanner.Image = _columnTitle.BannerProperties.Enable ? _columnTitle.BannerProperties.Image : null;
		}

		private void SaveData()
		{
			_columnTitle.Parent.ApplyForAllColumnTitles = ckApplyForAllColumnTitles.Checked;
			_columnTitle.BackgroundColor = colorEditBackColor.Color;
			if (rbAlignmentLeft.Checked)
				_columnTitle.HeaderAlignment = Alignment.Left;
			else if (rbAlignmentCenter.Checked)
				_columnTitle.HeaderAlignment = Alignment.Center;
			else if (rbAlignmentRight.Checked)
				_columnTitle.HeaderAlignment = Alignment.Right;
			_columnTitle.Name = ckEnableText.Checked & memoEditTitle.EditValue != null ? memoEditTitle.EditValue.ToString() : string.Empty;
			_columnTitle.EnableText = ckEnableText.Checked & !String.IsNullOrEmpty(_columnTitle.Name);
			_columnTitle.ForeColor = colorEditForeColor.Color;
			_columnTitle.HeaderFont = (Font)buttonEditFont.Tag;

			_columnTitle.EnableWidget = ckEnableWidget.Checked;
			_columnTitle.Widget = pbSelectedWidget.Image;

			_columnTitle.BannerProperties.Enable = ckEnableBanner.Checked;
			_columnTitle.BannerProperties.Image = pbSelectedBanner.Image;
			_columnTitle.BannerProperties.Configured = true;

			foreach (var columnTitle in _columnTitle.Parent.ColumnTitles.Where(columnTitle => columnTitle != _columnTitle))
			{
				if (_columnTitle.Parent.ApplyForAllColumnTitles)
				{
					columnTitle.BackgroundColor = _columnTitle.BackgroundColor;
					columnTitle.HeaderAlignment = _columnTitle.HeaderAlignment;
					columnTitle.Name = _columnTitle.Name;
					columnTitle.EnableText = _columnTitle.EnableText;
					columnTitle.ForeColor = _columnTitle.ForeColor;
					columnTitle.HeaderFont = _columnTitle.HeaderFont != null ? (Font)_columnTitle.HeaderFont.Clone() : null;
					columnTitle.EnableWidget = _columnTitle.EnableWidget;
					columnTitle.Widget = _columnTitle.Widget != null ? (Image)_columnTitle.Widget.Clone() : null;
					columnTitle.BannerProperties = _columnTitle.BannerProperties.Clone(columnTitle);
					_columnTitle.BannerProperties.Image = pbSelectedBanner.Image;
					_columnTitle.BannerProperties.Configured = true;
				}
			}
		}

		#region Appearance
		private void ckEnableText_CheckedChanged(object sender, EventArgs e)
		{
			laFont.Enabled = ckEnableText.Checked;
			laForeColor.Enabled = ckEnableText.Checked;
			memoEditTitle.Enabled = ckEnableText.Checked;
			colorEditForeColor.Enabled = ckEnableText.Checked;
			buttonEditFont.Enabled = ckEnableText.Checked;
		}

		private void colorEditBackColor_EditValueChanged(object sender, EventArgs e)
		{
			memoEditTitle.BackColor = colorEditBackColor.Color;
		}

		private void colorEditForeColor_EditValueChanged(object sender, EventArgs e)
		{
			memoEditTitle.ForeColor = colorEditForeColor.Color;
		}

		private void buttonEditFont_EditValueChanged(object sender, EventArgs e)
		{
			memoEditTitle.Font = (Font)buttonEditFont.Tag;
			memoEditTitle.Properties.Appearance.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceDisabled.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceFocused.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceReadOnly.Font = memoEditTitle.Font;
		}

		private void rbAlignmentLeft_CheckedChanged(object sender, EventArgs e)
		{
			if (!rbAlignmentLeft.Checked) return;
			memoEditTitle.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
			memoEditTitle.Properties.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Near;
			memoEditTitle.Properties.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Near;
			memoEditTitle.Properties.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Near;
		}

		private void rbAlignmentCenter_CheckedChanged(object sender, EventArgs e)
		{
			if (!rbAlignmentCenter.Checked) return;
			memoEditTitle.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
			memoEditTitle.Properties.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Center;
			memoEditTitle.Properties.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Center;
			memoEditTitle.Properties.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Center;
		}

		private void rbAlignmentRight_CheckedChanged(object sender, EventArgs e)
		{
			if (!rbAlignmentRight.Checked) return;
			memoEditTitle.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
			memoEditTitle.Properties.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
			memoEditTitle.Properties.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Far;
			memoEditTitle.Properties.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Far;
		}
		#endregion

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
