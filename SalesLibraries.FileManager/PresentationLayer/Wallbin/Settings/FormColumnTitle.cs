using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraLayout;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
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

			layoutControlGroupBanner.PageEnabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			layoutControlGroupWidget.PageEnabled = MainController.Instance.Lists.Widgets.MainFolder.ExistsLocal();

			buttonEditFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditFont.Click += EditorHelper.FontEdit_Click;

			Load += OnFormLoad;

			layoutControlItemSave.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSave.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSave.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			labelControlLocation.Text = String.Format("Title for column {0}", (_columnTitle.ColumnOrder + 1).ToString("#,##0"));

			checkEditApplyForAllColumnTitles.Checked = _columnTitle.Page.Settings.ApplyForAllColumnTitles;
			colorEditBackColor.Color = _columnTitle.Settings.BackgroundColor;
			switch (_columnTitle.Settings.HeaderAlignment)
			{
				case HorizontalAlignment.Left:
					checkEditAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditAlignmentRight.Checked = true;
					break;
			}
			checkEditEnableText.Checked = _columnTitle.Settings.ShowText;
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
			_columnTitle.Page.Settings.ApplyForAllColumnTitles = checkEditApplyForAllColumnTitles.Checked;
			_columnTitle.Settings.BackgroundColor = colorEditBackColor.Color;
			if (checkEditAlignmentLeft.Checked)
				_columnTitle.Settings.HeaderAlignment = HorizontalAlignment.Left;
			else if (checkEditAlignmentCenter.Checked)
				_columnTitle.Settings.HeaderAlignment = HorizontalAlignment.Center;
			else if (checkEditAlignmentRight.Checked)
				_columnTitle.Settings.HeaderAlignment = HorizontalAlignment.Right;
			_columnTitle.Settings.Text = checkEditEnableText.Checked & memoEditTitle.EditValue != null ? memoEditTitle.EditValue.ToString() : string.Empty;
			_columnTitle.Settings.ShowText = checkEditEnableText.Checked & !String.IsNullOrEmpty(_columnTitle.Settings.Text);
			_columnTitle.Settings.ForeColor = colorEditForeColor.Color;
			_columnTitle.Settings.HeaderFont = (Font)buttonEditFont.Tag;

			_widgetControl?.SaveData();
			if (_bannerControl == null && _columnTitle.Widget.Enabled)
				_columnTitle.Banner.Enable = false;

			_bannerControl?.SaveData();
			if (_widgetControl == null && _columnTitle.Banner.Enable)
				_columnTitle.Widget.WidgetType = _columnTitle.Widget.DefaultWidgetType;

			_columnTitle.Page.ApplyColumnTitleSettings(_columnTitle);
		}

		private void OnSelectedPageChanging(object sender, LayoutTabPageChangedEventArgs pageArgs)
		{
			if (pageArgs.Page == layoutControlGroupWidget && _widgetControl == null)
			{
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				_widgetControl = new WidgetSettingsControl(_columnTitle.Widget);
				pnWidgetContainer.Controls.Add(_widgetControl);
				_widgetControl.Dock = DockStyle.Fill;
				_widgetControl.LoadData();
				_widgetControl.StateChanged += (o, e) =>
				{
					if (e.IsChecked)
						_bannerControl?.ChangeState(false);
				};
				_widgetControl.ControlClicked += OnFormClick;
				Cursor = Cursors.Default;
			}
			else if (pageArgs.Page == layoutControlGroupBanner && _bannerControl == null)
			{
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				_bannerControl = new BannerSettingsControl(_columnTitle);
				pnBannerContainer.Controls.Add(_bannerControl);
				_bannerControl.Dock = DockStyle.Fill;
				_bannerControl.LoadData();
				_bannerControl.StateChanged += (o, e) =>
				{
					if (e.IsChecked)
						_widgetControl?.ChangeState(false);
				};
				_bannerControl.ControlClicked += OnFormClick;
				Cursor = Cursors.Default;
			}
		}

		#region Appearance
		private void ckEnableText_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTitle.Enabled = checkEditEnableText.Checked;
			layoutControlItemForeColor.Enabled = checkEditEnableText.Checked;
			layoutControlItemFont.Enabled = checkEditEnableText.Checked;
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
			if (!checkEditAlignmentLeft.Checked) return;
			memoEditTitle.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
			memoEditTitle.Properties.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Near;
			memoEditTitle.Properties.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Near;
			memoEditTitle.Properties.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Near;
		}

		private void rbAlignmentCenter_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkEditAlignmentCenter.Checked) return;
			memoEditTitle.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
			memoEditTitle.Properties.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Center;
			memoEditTitle.Properties.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Center;
			memoEditTitle.Properties.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Center;
		}

		private void rbAlignmentRight_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkEditAlignmentRight.Checked) return;
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

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXSave.Focus();
		}
	}
}
