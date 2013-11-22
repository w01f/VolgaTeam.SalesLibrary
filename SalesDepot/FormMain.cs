using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.Floater;
using SalesDepot.InteropClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using SalesDepot.TabPages;
using SalesDepot.ToolForms;

namespace SalesDepot
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		private bool _alowToSave;

		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		public TabHomeControl TabHome { get; set; }
		public TabSearchControl TabSearch { get; set; }
		public TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }
		public TabProgramSchedule TabProgramSchedule { get; set; }
		public TabProgramSearch TabProgramSearch { get; set; }
		public TabQBuilder TabQBuilder { get; set; }

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
				Image floaterLogo = null;
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings || ribbonControl.SelectedRibbonTabItem == ribbonTabItemQBuilder || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSearch)
					floaterLogo = labelItemPackageLogo.Image;
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSearch)
					floaterLogo = labelItemSearchLogo.Image;
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
					floaterLogo = labelItemCalendarLogo.Image;
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule || ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
					floaterLogo = labelItemProgramScheduleStationLogo.Image;
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
			TabHome = new TabHomeControl();
			TabHome.InitController();

			TabSearch = new TabSearchControl();
			TabSearch.InitController();

			TabOvernightsCalendar = new TabOvernightsCalendarControl();
			TabOvernightsCalendar.InitController();

			TabProgramSchedule = new TabProgramSchedule();
			TabProgramSchedule.InitController();

			TabProgramSearch = new TabProgramSearch();
			TabProgramSearch.InitController();

			TabQBuilder = new TabQBuilder();
			TabQBuilder.InitController();
		}

		private void LoadApplicationSettings()
		{
			if (File.Exists(SettingsManager.Instance.IconPath))
				Icon = new Icon(SettingsManager.Instance.IconPath);
			if (File.Exists(SettingsManager.Instance.CalendarLogoPath))
				labelItemCalendarLogo.Image = new Bitmap(SettingsManager.Instance.CalendarLogoPath);
			Text = string.Format("{0} - User: {1}", SettingsManager.Instance.SalesDepotName, Environment.UserName);
			ribbonTabItemSearch.Text = !string.IsNullOrEmpty(SettingsManager.Instance.SolutionTitle) ? SettingsManager.Instance.SolutionTitle : ribbonTabItemSearch.Text;
			buttonItemProgramScheduleOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;
			buttonItemProgramSearchOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;

			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			FloaterManager.Instance.ShowFloater(this, null);
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			TabHome.IsActive = false;
			TabOvernightsCalendar.IsActive = false;
			TabProgramSchedule.IsActive = false;
			TabProgramSearch.IsActive = false;
			TabQBuilder.IsActive = false;

			SettingsManager.Instance.HomeView = false;
			SettingsManager.Instance.SearchView = false;
			SettingsManager.Instance.CalendarView = false;
			SettingsManager.Instance.SaveSettings();

			if (_alowToSave)
			{
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				{
					if (!pnContainer.Controls.Contains(TabHome))
						pnContainer.Controls.Add(TabHome);
					TabHome.ShowTab();
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSearch)
				{
					if (!pnContainer.Controls.Contains(TabSearch))
						pnContainer.Controls.Add(TabSearch);
					TabSearch.ShowTab();
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				{
					if (!pnContainer.Controls.Contains(TabOvernightsCalendar))
						pnContainer.Controls.Add(TabOvernightsCalendar);
					TabOvernightsCalendar.ShowTab();
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule)
				{
					if (!pnContainer.Controls.Contains(TabProgramSchedule))
						pnContainer.Controls.Add(TabProgramSchedule);
					TabProgramSchedule.ShowTab();
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
				{
					if (!pnContainer.Controls.Contains(TabProgramSearch))
						pnContainer.Controls.Add(TabProgramSearch);
					TabProgramSearch.ShowTab();
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemQBuilder)
				{
					if (!pnContainer.Controls.Contains(TabQBuilder))
						pnContainer.Controls.Add(TabQBuilder);
					TabQBuilder.ShowTab();
				}
			}
		}

		#region Form Event Handlers
		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadApplicationSettings();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = true;
			ribbonControl.Visible = false;
			pnEmpty.BringToFront();
			var thread = new Thread(delegate()
										{
											LibraryManager.Instance.LoadLibraryPackages(new DirectoryInfo(SettingsManager.Instance.LibraryRootFolder));
											if (LibraryManager.Instance.LibraryPackageCollection.Count > 0)
											{
												Invoke((MethodInvoker)delegate
																		  {
																			  DecoratorManager.Instance.BuildPackageViewers();
																			  Application.DoEvents();
																			  DecoratorManager.Instance.BuildOvernightsCalendars();
																			  Application.DoEvents();
																			  DecoratorManager.Instance.BuildProgramManagers();
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
				thread = new Thread(delegate()
										{
											Invoke((MethodInvoker)delegate
																	  {
																		  TabHome.LoadTab();
																		  Application.DoEvents();
																		  _alowToSave = true;
																		  Application.DoEvents();
																	  });
										});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}

			if (SettingsManager.Instance.SearchView)
				ribbonTabItemSearch.Select();
			else if (SettingsManager.Instance.CalendarView)
				ribbonTabItemCalendar.Select();
			else
				ribbonControl_SelectedRibbonTabChanged(null, null);

			ribbonControl.Visible = true;
			pnContainer.BringToFront();
			AppManager.Instance.ActivateMainForm();
			if (LibraryManager.Instance.LibraryPackageCollection.Count == 0)
			{
				ribbonBarStations.Enabled = false;
				ribbonTabItemSettings.Enabled = false;
				AppManager.Instance.ShowWarning("Library is not available...\nCheck your network connections....");
			}
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			PowerPointHelper.Instance.Disconnect();
			WordHelper.Instance.Close();
		}
		#endregion
	}
}