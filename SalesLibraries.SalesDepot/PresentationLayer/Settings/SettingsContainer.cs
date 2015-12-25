using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

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

		private void xtraScrollableControl_MouseMove(object sender, MouseEventArgs e)
		{
			xtraScrollableControl.Focus();
		}
	}
}