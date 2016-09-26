using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Common;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormWindow : MetroForm
	{
		private readonly LibraryFolder _folder;
		private readonly WindowSettingsEditFormParams _formParameters;
		private WidgetSettingsControl _widgetControl;
		private BannerSettingsControl _bannerControl;

		public FormWindow(LibraryFolder folder, WindowSettingsEditFormParams formParameters)
		{
			InitializeComponent();
			_folder = folder;
			_formParameters = formParameters;
			Text = String.Format(_formParameters.Title, folder.Name);
			Width = _formParameters.Width;
			Height = _formParameters.Height;
			LoadData();

			ckApllyForAllWindowsAppearance.Checked = _folder.Page.Library.Settings.ApplyAppearanceForAllWindows;
			ckApllyForAllWindowsWidget.Checked = _folder.Page.Library.Settings.ApplyWidgetForAllWindows;
			ckApllyForAllWindowsBanner.Checked = _folder.Page.Library.Settings.ApplyBannerForAllWindows;
			xtraTabPageBanner.PageEnabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			xtraTabPageWidget.PageEnabled = MainController.Instance.Lists.Widgets.MainFolder.ExistsLocal();
			buttonEditWindowHeaderFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditWindowHeaderFont.Click += EditorHelper.FontEdit_Click;
			textEditName.MouseDown += EditorHelper.EditorMouseDown;
			textEditName.MouseUp += EditorHelper.EditorMouseUp;
			textEditName.Enter += EditorHelper.EditorEnter;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laHeaderBackColor.Font = new Font(laHeaderBackColor.Font.FontFamily, laHeaderBackColor.Font.Size - 2, laHeaderBackColor.Font.Style);
				laHeaderFont.Font = new Font(laHeaderFont.Font.FontFamily, laHeaderFont.Font.Size - 2, laHeaderFont.Font.Style);
				laHeaderForeColor.Font = new Font(laHeaderForeColor.Font.FontFamily, laHeaderForeColor.Font.Size - 2, laHeaderForeColor.Font.Style);
				laBorderColor.Font = new Font(laBorderColor.Font.FontFamily, laBorderColor.Font.Size - 2, laBorderColor.Font.Style);
				laBackColor.Font = new Font(laBackColor.Font.FontFamily, laBackColor.Font.Size - 2, laBackColor.Font.Style);
				laForeColor.Font = new Font(laForeColor.Font.FontFamily, laForeColor.Font.Size - 2, laForeColor.Font.Style);
				laHeaderAlignment.Font = new Font(laHeaderAlignment.Font.FontFamily, laHeaderAlignment.Font.Size - 2, laHeaderAlignment.Font.Style);
				ckApllyForAllWindowsAppearance.Font = new Font(ckApllyForAllWindowsAppearance.Font.FontFamily, ckApllyForAllWindowsAppearance.Font.Size - 2, ckApllyForAllWindowsAppearance.Font.Style);
				ckApllyForAllWindowsBanner.Font = new Font(ckApllyForAllWindowsBanner.Font.FontFamily, ckApllyForAllWindowsBanner.Font.Size - 2, ckApllyForAllWindowsBanner.Font.Style);
				ckApllyForAllWindowsWidget.Font = new Font(ckApllyForAllWindowsWidget.Font.FontFamily, ckApllyForAllWindowsWidget.Font.Size - 2, ckApllyForAllWindowsWidget.Font.Style);
				rbHeaderAlignmentCenter.Font = new Font(rbHeaderAlignmentCenter.Font.FontFamily, rbHeaderAlignmentCenter.Font.Size - 2, rbHeaderAlignmentCenter.Font.Style);
				rbHeaderAlignmentLeft.Font = new Font(rbHeaderAlignmentLeft.Font.FontFamily, rbHeaderAlignmentLeft.Font.Size - 2, rbHeaderAlignmentLeft.Font.Style);
				rbWindowHeaderAlignmentRight.Font = new Font(rbWindowHeaderAlignmentRight.Font.FontFamily, rbWindowHeaderAlignmentRight.Font.Size - 2, rbWindowHeaderAlignmentRight.Font.Style);
				xtraTabControlWindowProperties.Appearance.Font = new Font(xtraTabControlWindowProperties.Appearance.Font.FontFamily, xtraTabControlWindowProperties.Appearance.Font.Size - 2, xtraTabControlWindowProperties.Appearance.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.Header.Font = new Font(xtraTabControlWindowProperties.AppearancePage.Header.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.Header.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.Header.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Style);
			}
		}

		private void LoadData()
		{
			switch (_formParameters.Type)
			{
				case WindowPropertiesType.None:
					xtraTabPageAppearance.PageVisible = true;
					xtraTabPageWidget.PageVisible = true;
					xtraTabPageBanner.PageVisible = true;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.True;
					xtraTabControlWindowProperties.BorderStyle = BorderStyles.Default;
					xtraTabControlWindowProperties.BorderStylePage = BorderStyles.Default;
					break;
				case WindowPropertiesType.Appearnce:
					xtraTabPageAppearance.PageVisible = true;
					xtraTabPageWidget.PageVisible = false;
					xtraTabPageBanner.PageVisible = false;
					xtraTabPageBanner.BorderStyle = BorderStyle.None;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.False;
					xtraTabControlWindowProperties.BorderStyle = BorderStyles.NoBorder;
					xtraTabControlWindowProperties.BorderStylePage = BorderStyles.NoBorder;
					break;
				case WindowPropertiesType.Widget:
					xtraTabPageAppearance.PageVisible = false;
					xtraTabPageWidget.PageVisible = true;
					xtraTabPageBanner.PageVisible = false;
					xtraTabPageWidget.BorderStyle = BorderStyle.None;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.False;
					xtraTabControlWindowProperties.BorderStyle = BorderStyles.NoBorder;
					xtraTabControlWindowProperties.BorderStylePage = BorderStyles.NoBorder;
					OnSelectedPageChanging(xtraTabControlWindowProperties, new TabPageChangingEventArgs(null, xtraTabPageWidget));
					break;
				case WindowPropertiesType.Banner:
					xtraTabPageAppearance.PageVisible = false;
					xtraTabPageWidget.PageVisible = false;
					xtraTabPageBanner.PageVisible = true;
					xtraTabControlWindowProperties.ShowTabHeader = DefaultBoolean.False;
					xtraTabControlWindowProperties.BorderStyle = BorderStyles.NoBorder;
					xtraTabControlWindowProperties.BorderStylePage = BorderStyles.NoBorder;
					OnSelectedPageChanging(xtraTabControlWindowProperties, new TabPageChangingEventArgs(null, xtraTabPageBanner));
					break;
			}

			laLocation.Text = String.Format("Location: {0}", "Window " + (_folder.RowOrder + 1).ToString("#,##0") + " - Column " + (_folder.ColumnOrder + 1).ToString("#,##0"));
			textEditName.EditValue = _folder.Name;
			colorEditWindowHeaderBackColor.Color = _folder.Settings.BackgroundHeaderColor;
			colorEditWindowHeaderForeColor.Color = _folder.Settings.ForeHeaderColor;
			colorEditWindowBackColor.Color = _folder.Settings.BackgroundWindowColor;
			colorEditWindowForeColor.Color = _folder.Settings.ForeWindowColor;
			colorEditWindowBorderColor.Color = _folder.Settings.BorderColor;
			if (_folder.Settings.HeaderFont != null)
			{
				buttonEditWindowHeaderFont.Tag = _folder.Settings.HeaderFont;
				buttonEditWindowHeaderFont.EditValue = Utils.FontToString(_folder.Settings.HeaderFont);
			}
			else
				buttonEditWindowHeaderFont.EditValue = string.Empty;
			switch (_folder.Settings.HeaderAlignment)
			{
				case HorizontalAlignment.Left:
					rbHeaderAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					rbHeaderAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					rbWindowHeaderAlignmentRight.Checked = true;
					break;
			}
			ckApllyForAllWindowsAppearance.Visible = _formParameters.Type == WindowPropertiesType.None;
			ckApllyForAllWindowsAppearance.Checked = _formParameters.Type == WindowPropertiesType.None && _folder.Page.Library.Settings.ApplyAppearanceForAllWindows;
		}

		private void SaveData()
		{
			_folder.Name = textEditName.EditValue as String;
			_folder.Settings.BackgroundHeaderColor = colorEditWindowHeaderBackColor.Color;
			_folder.Settings.ForeHeaderColor = colorEditWindowHeaderForeColor.Color;
			_folder.Settings.BackgroundWindowColor = colorEditWindowBackColor.Color;
			_folder.Settings.ForeWindowColor = colorEditWindowForeColor.Color;
			_folder.Settings.BorderColor = colorEditWindowBorderColor.Color;
			_folder.Settings.HeaderFont = buttonEditWindowHeaderFont.Tag as Font;
			if (rbHeaderAlignmentLeft.Checked)
				_folder.Settings.HeaderAlignment = HorizontalAlignment.Left;
			else if (rbHeaderAlignmentCenter.Checked)
				_folder.Settings.HeaderAlignment = HorizontalAlignment.Center;
			else if (rbWindowHeaderAlignmentRight.Checked)
				_folder.Settings.HeaderAlignment = HorizontalAlignment.Right;
			if (_formParameters.Type == WindowPropertiesType.None)
				_folder.Page.Library.Settings.ApplyAppearanceForAllWindows = ckApllyForAllWindowsAppearance.Checked;

			_widgetControl?.SaveData();
			if (_bannerControl == null && _folder.Widget.Enabled)
				_folder.Banner.Enable = false;

			if (_formParameters.Type == WindowPropertiesType.None)
				_folder.Page.Library.Settings.ApplyWidgetForAllWindows = ckApllyForAllWindowsWidget.Checked;

			_bannerControl?.SaveData();
			if (_widgetControl == null && _folder.Banner.Enable)
				_folder.Widget.WidgetType = WidgetType.NoWidget;
			if (_formParameters.Type == WindowPropertiesType.None)
				_folder.Page.Library.Settings.ApplyBannerForAllWindows = ckApllyForAllWindowsBanner.Checked;

			if (_formParameters.Type == WindowPropertiesType.None)
				_folder.Page.ApplyFolderSettings(_folder);
			_folder.BeforeSave();
		}

		private void FormWindowSettings_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveData();
		}

		private void OnSelectedPageChanging(object sender, TabPageChangingEventArgs pageArgs)
		{
			if (pageArgs.Page == xtraTabPageWidget && _widgetControl == null)
			{
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				_widgetControl = new WidgetSettingsControl(_folder.Widget);
				_widgetControl.Dock = DockStyle.Fill;
				pnWidgetContainer.Controls.Add(_widgetControl);
				_widgetControl.LoadData();
				_widgetControl.StateChanged += (o, e) =>
				{
					if (e.IsChecked)
						_bannerControl?.ChangeState(false);
				};
				_widgetControl.ControlClicked += OnFormClick;
				pnApllyForAllWindowsWidget.Visible = _formParameters.Type == WindowPropertiesType.None;
				ckApllyForAllWindowsWidget.Checked = _formParameters.Type == WindowPropertiesType.None && _folder.Page.Library.Settings.ApplyWidgetForAllWindows;
				Cursor = Cursors.Default;
			}
			else if (pageArgs.Page == xtraTabPageBanner && _bannerControl == null)
			{
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				_bannerControl = new BannerSettingsControl(_folder);
				_bannerControl.Dock = DockStyle.Fill;
				pnBannerContainer.Controls.Add(_bannerControl);
				_bannerControl.LoadData();
				_bannerControl.StateChanged += (o, e) =>
				{
					if (e.IsChecked)
						_widgetControl?.ChangeState(false);
				};
				_bannerControl.ControlClicked += OnFormClick;
				pnApllyForAllWindowsBanner.Visible = _formParameters.Type == WindowPropertiesType.None;
				ckApllyForAllWindowsBanner.Checked = _formParameters.Type == WindowPropertiesType.None && _folder.Page.Library.Settings.ApplyBannerForAllWindows;
				Cursor = Cursors.Default;
			}
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXSave.Focus();
		}
	}

	public enum WindowPropertiesType
	{
		None,
		Appearnce,
		Widget,
		Banner
	}

	public class WindowSettingsEditFormParams
	{
		public WindowPropertiesType Type { get; protected set; }
		public string Title { get; protected set; }
		public int Height { get; protected set; }
		public int Width { get; protected set; }
	}

	class BaseEditFormParams : WindowSettingsEditFormParams
	{
		public BaseEditFormParams()
		{
			Title = "Window Settings";
			Width = 980;
			Height = 670;
		}
	}

	class TitleFormParams : WindowSettingsEditFormParams
	{
		public TitleFormParams()
		{
			Type = WindowPropertiesType.Appearnce;
			Title = "Window Settings";
			Width = 580;
			Height = 430;
		}
	}

	class WidgetFormParams : WindowSettingsEditFormParams
	{
		public WidgetFormParams()
		{
			Type = WindowPropertiesType.Widget;
			Title = "Widget Gallery ({0})";
			Width = 980;
			Height = 670;
		}
	}

	class BannerFormParams : WindowSettingsEditFormParams
	{
		public BannerFormParams()
		{
			Type = WindowPropertiesType.Banner;
			Title = "Banner Gallery ({0})";
			Width = 980;
			Height = 670;
		}
	}
}
