using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using OutlookSalesDepotAddIn.BusinessClasses;
using OutlookSalesDepotAddIn.Controls.TabPages;

namespace OutlookSalesDepotAddIn.Forms
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		public TabHomeControl TabHome { get; set; }

		private FormMain()
		{
			InitializeComponent();
			Text = SettingsManager.AppName;
			pnContainer.Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			TabHome = new TabHomeControl();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (LibraryManager.Instance.LibraryPackageCollection.Count != 0) return;
			TabHome.InitController();
			ribbonControl.Enabled = false;
			pnEmpty.BringToFront();
			using (var form = new FormProgress())
			{
				form.TopMost = true;
				form.laProgress.Text = "Loading...";
				form.Show();
				var thread = new Thread(delegate()
				{
					LibraryManager.Instance.LoadLibraryPackages(new DirectoryInfo(SettingsManager.Instance.LibraryRootFolder));
					if (LibraryManager.Instance.LibraryPackageCollection.Count > 0)
					{
						Invoke((MethodInvoker)delegate
						{
							DecoratorManager.Instance.BuildPackageViewers();
							Application.DoEvents();
						});
					}
				});
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();

				if (LibraryManager.Instance.LibraryPackageCollection.Count > 0)
				{
					thread = new Thread(() => Invoke((MethodInvoker)delegate
					{
						TabHome.LoadTab();
						Application.DoEvents();
					}));
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
				}
				form.Close();
			}

			if (!pnContainer.Controls.Contains(TabHome))
				pnContainer.Controls.Add(TabHome);
			TabHome.ShowTab();

			ribbonControl.Enabled = true;
			pnContainer.BringToFront();
			if (LibraryManager.Instance.LibraryPackageCollection.Count != 0) return;
			ribbonBarHomeStations.Enabled = false;
			Utils.ShowWarning("Library is not available...\nCheck your network connections....");
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			WordHelper.Instance.Close();
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}