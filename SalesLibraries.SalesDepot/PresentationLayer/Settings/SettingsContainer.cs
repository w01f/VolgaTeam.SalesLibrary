using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	[ToolboxItem(false)]
	public partial class SettingsContainer : UserControl
	{
		public SettingsContainer()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void InitControl()
		{
			powerPointSettingsControl.LoadData();
			pdfSettingsControl.LoadData();
			wordSettingsControl.LoadData();
			excelSettingsControl.LoadData();
			videoSettingsControl.LoadData();
			folderSettingsControl.LoadData();
			quickViewSettingsControl.LoadData();
			Resize += OnResize;
			panel1.Resize += OnColumnResize;
			panel2.Resize += OnColumnResize;
			panel3.Resize += OnColumnResize;
		}

		private void OnResize(object sender, EventArgs e)
		{
			var columnWidth = Width / 3;
			panel1.Width = panel2.Width = columnWidth;
		}

		private void OnColumnResize(object sender, EventArgs e)
		{
			var control = sender as Panel;
			if (control == null) return;
			foreach (Control child in control.Controls)
				child.Left = (control.Width - child.Width) / 2;
		}
	}
}