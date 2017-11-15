using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	[ToolboxItem(false)]
	public partial class SettingsContainer : UserControl
	{
		private const int ColumnsCount = 3;
		private const int ControlHeight = 135;
		private const int ControlWidth = 335;
		private const int ControlMarginTop = 30;
		private readonly List<BaseSettingsControl> _settingsControls = new List<BaseSettingsControl>();

		public SettingsContainer()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_settingsControls.Add(powerPointSettingsControl);
			_settingsControls.Add(pdfSettingsControl);
			_settingsControls.Add(wordSettingsControl);
			_settingsControls.Add(excelSettingsControl);
			_settingsControls.Add(videoSettingsControl);
			_settingsControls.Add(folderSettingsControl);
			_settingsControls.Add(quickViewSettingsControl);
			_settingsControls.Add(emailSettingsControl);
			_settingsControls.Add(saveSettingsControl);
			_settingsControls.Add(powerPointStartupSettingsControl);

			//layoutControlItemSettingsPowerPoint.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsPowerPoint.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsPowerPoint.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsPowerPoint.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsWord.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsWord.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsWord.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsWord.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsPdf.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsPdf.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsPdf.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsPdf.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsExcel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsExcel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsExcel.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsExcel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsVideo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsVideo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsVideo.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsVideo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsFolder.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsFolder.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsFolder.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsFolder.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsQuickView.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsQuickView.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsQuickView.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsQuickView.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsEmail.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsEmail.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsEmail.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsEmail.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsSave.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsSave.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsSave.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsStartup.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSettingsStartup.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			//layoutControlItemSettingsStartup.MinSize = RectangleHelper.ScaleSize(layoutControlItemSettingsStartup.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void InitControl()
		{
			foreach (var settingsControl in _settingsControls)
				settingsControl.LoadData();
			Resize += OnResize;
		}

		private void OnResize(object sender, EventArgs e)
		{
			var columnWidth = (Width - 20) / 3;
			var currentColumn = 0;
			var currentRow = 0;
			foreach (var settingsControl in _settingsControls)
			{
				settingsControl.Left = (currentColumn * columnWidth) + ((columnWidth - ControlWidth) / 2);
				settingsControl.Top = (currentRow * (ControlHeight + ControlMarginTop)) + ControlMarginTop;
				settingsControl.Height = ControlHeight;
				if (currentColumn == ColumnsCount - 1)
				{
					currentColumn = 0;
					currentRow++;
				}
				else
					currentColumn++;
			}
		}
	}
}