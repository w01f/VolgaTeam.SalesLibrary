using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;

namespace SalesDepot.TabPages
{
	[ToolboxItem(false)]
	public partial class TabOvernightsCalendarControl : UserControl, IController
	{
		public TabOvernightsCalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.labelItemCalendarDisclaimerLogo.Click += buttonItemCalendarDisclaimer_Click;
			FormMain.Instance.buttonItemCalendarFontSizeLarger.Click += buttonItemCalendarFontLarger_Click;
			FormMain.Instance.buttonItemCalendarFontSizeSmaler.Click += buttonItemCalendarFontSmaller_Click;
			FormMain.Instance.buttonItemCalendarHelp.Click += buttonItemHelp_Click;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			AppManager.Instance.ActivityManager.AddUserActivity("Overnights Calendar selected");
			SettingsManager.Instance.CalendarView = true;
			SettingsManager.Instance.SaveSettings();
		}
		#endregion

		public void buttonItemCalendarDisclaimer_Click(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.DisclaimerPath))
				Process.Start(SettingsManager.Instance.DisclaimerPath);
		}

		public void buttonItemCalendarFontLarger_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.CalendarFontSize++;
			SettingsManager.Instance.SaveSettings();
			if (DecoratorManager.Instance.ActivePackageViewer != null)
				DecoratorManager.Instance.ActivePackageViewer.FormatCalendar();
		}

		public void buttonItemCalendarFontSmaller_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.CalendarFontSize--;
			SettingsManager.Instance.SaveSettings();
			if (DecoratorManager.Instance.ActivePackageViewer != null)
				DecoratorManager.Instance.ActivePackageViewer.FormatCalendar();
		}

		public void buttonItemHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("overnights");
		}
	}
}