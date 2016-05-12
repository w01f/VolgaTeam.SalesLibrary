using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Common;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	public partial class FormColumnTitle : MetroForm
	{
		private readonly ColumnTitle _columnTitle;
		private WidgetSettingsControl _widgetControl;
		private BannerSettingsControl _bannerControl;

		public FormColumnTitle(ColumnTitle columnTitle)
		{
			InitializeComponent();
			_columnTitle = columnTitle;

			xtraTabPageBanner.PageEnabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			xtraTabPageWidget.PageEnabled = MainController.Instance.Lists.Widgets.MainFolder.ExistsLocal();

			buttonEditFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditFont.Click += EditorHelper.FontEdit_Click;

			Load += OnFormLoad;

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
				ckApplyForAllColumnTitles.Font = new Font(ckApplyForAllColumnTitles.Font.FontFamily, ckApplyForAllColumnTitles.Font.Size - 2, ckApplyForAllColumnTitles.Font.Style);
				xtraTabControlWindowProperties.Appearance.Font = new Font(xtraTabControlWindowProperties.Appearance.Font.FontFamily, xtraTabControlWindowProperties.Appearance.Font.Size - 2, xtraTabControlWindowProperties.Appearance.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.Header.Font = new Font(xtraTabControlWindowProperties.AppearancePage.Header.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.Header.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.Header.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Style);
			}
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			laLocation.Text = String.Format("Title for column {0}", (_columnTitle.ColumnOrder + 1).ToString("#,##0"));

			ckApplyForAllColumnTitles.Checked = _columnTitle.Page.Settings.ApplyForAllColumnTitles;
			colorEditBackColor.Color = _columnTitle.Settings.BackgroundColor;
			switch (_columnTitle.Settings.HeaderAlignment)
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
			ckEnableText.Checked = _columnTitle.Settings.ShowText;
			memoEditTitle.EditValue = _columnTitle.Settings.Text;
			colorEditForeColor.Color = _columnTitle.Settings.ForeColor;
			buttonEditFont.Tag = _columnTitle.Settings.HeaderFont;
			buttonEditFont.EditValue = Utils.FontToString(_columnTitle.Settings.HeaderFont);
			memoEditTitle.ForeColor = colorEditForeColor.Color;
			memoEditTitle.BackColor = colorEditBackColor.Color;
			memoEditTitle.Font = (Font)buttonEditFont.Tag;
			memoEditTitle.Properties.Appearance.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceDisabled.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceFocused.Font = memoEditTitle.Font;
			memoEditTitle.Properties.AppearanceReadOnly.Font = memoEditTitle.Font;
		}

		private void SaveData()
		{
			_columnTitle.Page.Settings.ApplyForAllColumnTitles = ckApplyForAllColumnTitles.Checked;
			_columnTitle.Settings.BackgroundColor = colorEditBackColor.Color;
			if (rbAlignmentLeft.Checked)
				_columnTitle.Settings.HeaderAlignment = Alignment.Left;
			else if (rbAlignmentCenter.Checked)
				_columnTitle.Settings.HeaderAlignment = Alignment.Center;
			else if (rbAlignmentRight.Checked)
				_columnTitle.Settings.HeaderAlignment = Alignment.Right;
			_columnTitle.Settings.Text = ckEnableText.Checked & memoEditTitle.EditValue != null ? memoEditTitle.EditValue.ToString() : string.Empty;
			_columnTitle.Settings.ShowText = ckEnableText.Checked & !String.IsNullOrEmpty(_columnTitle.Settings.Text);
			_columnTitle.Settings.ForeColor = colorEditForeColor.Color;
			_columnTitle.Settings.HeaderFont = (Font)buttonEditFont.Tag;
			
			_widgetControl?.SaveData();
			_bannerControl?.SaveData();

			_columnTitle.Page.ApplyColumnTitleSettings(_columnTitle);
		}

		private void OnSelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs pageArgs)
		{
			if (pageArgs.Page == xtraTabPageWidget && _widgetControl == null)
			{
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				_widgetControl = new WidgetSettingsControl(_columnTitle.Widget);
				xtraTabPageWidget.Controls.Add(_widgetControl);
				_widgetControl.Dock = DockStyle.Fill;
				_widgetControl.LoadData();
				_widgetControl.StateChanged += (o, e) =>
				{
					if (e.IsChecked)
					{
						if (_bannerControl != null)
							_bannerControl.ChangeState(false);
						else
							_columnTitle.Banner.Enable = false;
					}
				};
				Cursor = Cursors.Default;
			}
			else if (pageArgs.Page == xtraTabPageBanner && _bannerControl == null)
			{
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				_bannerControl = new BannerSettingsControl(_columnTitle);
				xtraTabPageBanner.Controls.Add(_bannerControl);
				_bannerControl.Dock = DockStyle.Fill;
				_bannerControl.LoadData();
				_bannerControl.StateChanged += (o, e) =>
				{
					if (e.IsChecked)
					{
						if (_widgetControl != null)
							_widgetControl.ChangeState(false);
						else
							_columnTitle.Widget.WidgetType = _columnTitle.Widget.DefaultWidgetType;
					}
				};
				Cursor = Cursors.Default;
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

		private void FormWindowSettings_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveData();
		}
	}
}
