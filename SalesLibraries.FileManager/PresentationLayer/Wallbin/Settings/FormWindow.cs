using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
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
			Width = (Int32)(_formParameters.Width * Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
			Height = (Int32)(_formParameters.Height * Utils.GetScaleFactor(CreateGraphics().DpiX).Height);
			Load += (o, e) => LoadData();
			Shown += (o, e) => textEditName.Focus();
			checkEditApllyForAllWindowsAppearance.Checked = _folder.Page.Library.Settings.ApplyAppearanceForAllWindows;
			checkEditApllyForAllWindowsWidget.Checked = _folder.Page.Library.Settings.ApplyWidgetForAllWindows;
			checkEditApllyForAllWindowsBanner.Checked = _folder.Page.Library.Settings.ApplyBannerForAllWindows;
			layoutControlGroupBanner.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			layoutControlGroupWidget.Enabled = MainController.Instance.Lists.Widgets.MainFolder.ExistsLocal();
			buttonEditWindowHeaderFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditWindowHeaderFont.Click += EditorHelper.FontEdit_Click;
			textEditName.EnableSelectAll();

			layoutControlItemSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSave.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSave.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSave.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void LoadData()
		{
			switch (_formParameters.Type)
			{
				case WindowPropertiesType.None:
					layoutControlGroupAppearance.Visibility = LayoutVisibility.Always;
					layoutControlGroupWidget.Visibility = LayoutVisibility.Always;
					layoutControlGroupBanner.Visibility = LayoutVisibility.Always;
					tabbedControlGroupProperties.ShowTabHeader = DefaultBoolean.True;
					layoutControlGroupAppearance.GroupBordersVisible = true;
					layoutControlGroupWidget.GroupBordersVisible = true;
					layoutControlGroupBanner.GroupBordersVisible = true;
					break;
				case WindowPropertiesType.Appearnce:
					layoutControlGroupAppearance.Visibility = LayoutVisibility.Always;
					layoutControlGroupWidget.Visibility = LayoutVisibility.Never;
					layoutControlGroupBanner.Visibility = LayoutVisibility.Never;
					tabbedControlGroupProperties.ShowTabHeader = DefaultBoolean.False;
					layoutControlGroupAppearance.GroupBordersVisible = false;
					break;
				case WindowPropertiesType.Widget:
					layoutControlGroupAppearance.Visibility = LayoutVisibility.Never;
					layoutControlGroupWidget.Visibility = LayoutVisibility.Always;
					layoutControlGroupBanner.Visibility = LayoutVisibility.Never;
					tabbedControlGroupProperties.ShowTabHeader = DefaultBoolean.False;
					layoutControlGroupWidget.GroupBordersVisible = false;
					OnSelectedPageChanging(tabbedControlGroupProperties, new LayoutTabPageChangedEventArgs(null, layoutControlGroupWidget));
					break;
				case WindowPropertiesType.Banner:
					layoutControlGroupAppearance.Visibility = LayoutVisibility.Never;
					layoutControlGroupWidget.Visibility = LayoutVisibility.Never;
					layoutControlGroupBanner.Visibility = LayoutVisibility.Always;
					tabbedControlGroupProperties.ShowTabHeader = DefaultBoolean.False;
					layoutControlGroupBanner.GroupBordersVisible = false;
					OnSelectedPageChanging(tabbedControlGroupProperties, new LayoutTabPageChangedEventArgs(null, layoutControlGroupBanner));
					break;
			}

			labelControlLocation.Text = String.Format("Location: {0}", "Window " + (_folder.RowOrder + 1).ToString("#,##0") + " - Column " + (_folder.ColumnOrder + 1).ToString("#,##0"));
			textEditName.EditValue = _folder.Name;
			colorEditWindowHeaderBackColor.Color = _folder.Settings.BackgroundHeaderColor;
			colorEditWindowHeaderForeColor.Color = _folder.Settings.ForeHeaderColor;
			colorEditWindowBackColor.Color = _folder.Settings.BackgroundWindowColor;
			colorEditWindowForeColor.Color = _folder.Settings.ForeWindowColor;
			colorEditWindowBorderColor.Color = _folder.Settings.BorderColor;
			checkEditUseForeHeaderColorForWidget.Checked = _folder.Settings.UseForeHeaderColorForWidget;
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
					checkEditWindowHeaderAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditWindowHeaderAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditWindowHeaderAlignmentRight.Checked = true;
					break;
			}
			layoutControlItemApllyForAllWindowsAppearance.Visibility = _formParameters.Type == WindowPropertiesType.None ? LayoutVisibility.Always : LayoutVisibility.Never;
			checkEditApllyForAllWindowsAppearance.Checked = _formParameters.Type == WindowPropertiesType.None && _folder.Page.Library.Settings.ApplyAppearanceForAllWindows;
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
			_folder.Settings.UseForeHeaderColorForWidget = checkEditUseForeHeaderColorForWidget.Checked;
			if (checkEditWindowHeaderAlignmentLeft.Checked)
				_folder.Settings.HeaderAlignment = HorizontalAlignment.Left;
			else if (checkEditWindowHeaderAlignmentCenter.Checked)
				_folder.Settings.HeaderAlignment = HorizontalAlignment.Center;
			else if (checkEditWindowHeaderAlignmentRight.Checked)
				_folder.Settings.HeaderAlignment = HorizontalAlignment.Right;
			if (_formParameters.Type == WindowPropertiesType.None)
				_folder.Page.Library.Settings.ApplyAppearanceForAllWindows = checkEditApllyForAllWindowsAppearance.Checked;

			_widgetControl?.SaveData();
			if (_bannerControl == null && _folder.Widget.Enabled)
				_folder.Banner.Enable = false;

			_folder.Page.Library.Settings.ApplyWidgetForAllWindows = checkEditApllyForAllWindowsWidget.Checked;
			_folder.Page.Library.Settings.ApplyWidgetColorForAllWindows = checkEditApllyForAllWindowsWidgetColor.Checked;

			_bannerControl?.SaveData();
			if (_widgetControl == null && _folder.Banner.Enable)
				_folder.Widget.WidgetType = _folder.Widget.DefaultWidgetType;
			_folder.Page.Library.Settings.ApplyBannerForAllWindows = checkEditApllyForAllWindowsBanner.Checked;

			_folder.Page.ApplyFolderSettings(_folder);
			_folder.BeforeSave();
		}

		private void FormWindowSettings_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveData();
		}

		private void OnSelectedPageChanging(object sender, LayoutTabPageChangedEventArgs pageArgs)
		{
			if (pageArgs.Page == layoutControlGroupWidget)
			{
				if (_widgetControl == null)
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
						checkEditUseForeHeaderColorForWidget.Checked = _widgetControl.checkEditUseTextColor.Checked;
					};
					_widgetControl.ControlClicked += OnFormClick;
					_widgetControl.UpdateColor(checkEditUseForeHeaderColorForWidget.Checked ? colorEditWindowHeaderForeColor.Color : (Color?)null);
					checkEditApllyForAllWindowsWidget.Checked = _folder.Page.Library.Settings.ApplyWidgetForAllWindows;
					checkEditApllyForAllWindowsWidgetColor.Checked = _folder.Page.Library.Settings.ApplyWidgetColorForAllWindows;
					Cursor = Cursors.Default;
				}
			}
			else if (pageArgs.Page == layoutControlGroupBanner && _bannerControl == null)
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
				checkEditApllyForAllWindowsBanner.Checked = _folder.Page.Library.Settings.ApplyBannerForAllWindows;
				Cursor = Cursors.Default;
			}
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXSave.Focus();
		}

		private void colorEditWindowHeaderForeColor_EditValueChanged(object sender, EventArgs e)
		{
			_widgetControl?.UpdateColor(checkEditUseForeHeaderColorForWidget.Checked ? colorEditWindowHeaderForeColor.Color : (Color?)null);
		}

		private void checkEditUseForeHeaderColorForWidget_CheckedChanged(object sender, EventArgs e)
		{
			_widgetControl?.UpdateColor(checkEditUseForeHeaderColorForWidget.Checked ? colorEditWindowHeaderForeColor.Color : (Color?)null);
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
			Width = 680;
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
