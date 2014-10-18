using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;
using OutlookSalesDepotAddIn.Forms;
using SalesDepot.CommonGUI.Forms;

namespace OutlookSalesDepotAddIn.Controls.TabPages
{
	[ToolboxItem(false)]
	public partial class TabOvernightsCalendarControl : UserControl
	{
		public TabOvernightsCalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			NeedToUpdate = true;
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.labelItemCalendarDisclaimerLogo.Click += buttonItemCalendarDisclaimer_Click;
		}

		public void ShowTab()
		{
			if (NeedToUpdate)
			{
				using (var form = new FormProgress())
				{
					form.TopMost = true;
					form.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
					form.Show();
					var thread = new Thread(delegate()
					{
						LibraryManager.Instance.LoadLibraryPackages(new DirectoryInfo(SettingsManager.Instance.LibraryRootFolder));
						if (LibraryManager.Instance.LibraryPackageCollection.Count > 0)
						{
							Invoke((MethodInvoker)delegate
							{
								DecoratorManager.Instance.BuildOvernightsCalendars();
								DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ApplyOvernightsCalendar();
								Application.DoEvents();
							});
						}
					});
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
				NeedToUpdate = false;
			}
			IsActive = true;
			BringToFront();
		}
		#endregion

		public void buttonItemCalendarDisclaimer_Click(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.DisclaimerPath))
				Process.Start(SettingsManager.Instance.DisclaimerPath);
		}
	}
}