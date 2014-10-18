using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using OvernightsCalendarViewer.BusinessClasses;
using OvernightsCalendarViewer.ConfigurationClasses;
using OvernightsCalendarViewer.InteropClasses;
using OvernightsCalendarViewer.PresentationClasses.Decorators;
using OvernightsCalendarViewer.TabPages;
using OvernightsCalendarViewer.ToolClasses;
using SalesDepot.CommonGUI.Floater;
using SalesDepot.CommonGUI.Forms;

namespace OvernightsCalendarViewer
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		private bool _alowToSave;

		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		public TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new FormMain();
					_instance.InitControllers();
				}
				return _instance;
			}
		}

		public Image FloaterLogo
		{
			get
			{
				var floaterLogo = labelItemPackageLogo.Image;
				return floaterLogo;
			}
		}

		public string FloaterText
		{
			get { return ribbonBarStations.Text; }
		}

		private FormMain()
		{
			InitializeComponent();
			FormStateHelper.Init(this, Path.GetDirectoryName(typeof(FormMain).Assembly.Location), true);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (Environment.OSVersion.Version.Major < 6) return;
			int attrValue = 1;
			var res = WinAPIHelper.DwmSetWindowAttribute(Handle, WinAPIHelper.DWMWA_TRANSITIONS_FORCEDISABLED, ref attrValue, sizeof(int));
			if (res < 0)
				throw new Exception("Can't disable aero animation");
		}

		private void InitControllers()
		{
			TabOvernightsCalendar = new TabOvernightsCalendarControl();
			TabOvernightsCalendar.InitController();
		}

		private void LoadApplicationSettings()
		{
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			FloaterManager.Instance.ShowFloater(this, String.Format("{0} - User: {1}", "Overnights", Environment.UserName), FloaterLogo, null);
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			TabOvernightsCalendar.IsActive = false;
			if (!_alowToSave) return;
			if (ribbonControl.SelectedRibbonTabItem != ribbonTabItemCalendar) return;
			if (!pnContainer.Controls.Contains(TabOvernightsCalendar))
				pnContainer.Controls.Add(TabOvernightsCalendar);
			TabOvernightsCalendar.ShowTab();
		}

		#region Form Event Handlers
		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadApplicationSettings();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			ribbonControl.Visible = false;
			pnEmpty.BringToFront();
			using (var form = new FormProgress())
			{
				form.laProgress.Text = string.Format("Loading Overnights data...");
				form.TopMost = true;
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
						TabOvernightsCalendar.LoadTab();
						Application.DoEvents();
						_alowToSave = true;
						Application.DoEvents();
					}));
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
				}
				form.Close();
			}
			ribbonTabItemCalendar.Select();
			pnContainer.BringToFront();
			ribbonControl.Visible = true;
			AppManager.Instance.ActivateMainForm();
			if (LibraryManager.Instance.LibraryPackageCollection.Count == 0)
			{
				ribbonBarStations.Enabled = false;
				AppManager.Instance.ShowWarning("Overnights data is not available");
			}
		}
		#endregion
	}
}