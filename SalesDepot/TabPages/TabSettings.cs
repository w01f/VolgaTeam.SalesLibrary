using System;
using System.ComponentModel;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;

namespace SalesDepot.TabPages
{
	[ToolboxItem(false)]
	public partial class TabSettings : UserControl, IController
	{
		public TabSettings()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
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
			FormMain.Instance.buttonItemSettingsHelp.Click += buttonItemHelp_Click;
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

		public void ShowTab()
		{
			OnResize(this, EventArgs.Empty);
			IsActive = true;
			BringToFront();
			AppManager.Instance.ActivityManager.AddUserActivity("Settings selected");
		}
		#endregion

		public void buttonItemHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("settings");
		}
	}
}