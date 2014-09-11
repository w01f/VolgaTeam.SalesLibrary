using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.ToolClasses;
using SalesDepot.Floater;
using SalesDepot.InteropClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using SalesDepot.Properties;
using SalesDepot.TabPages;
using SalesDepot.ToolClasses;
using SalesDepot.ToolForms;
using PowerPointHelper = SalesDepot.InteropClasses.PowerPointHelper;
using WinAPIHelper = SalesDepot.CoreObjects.InteropClasses.WinAPIHelper;

namespace SalesDepot
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;
		private readonly TabPageManager _tabPageManager = new TabPageManager(SettingsManager.Instance.TabPageConfigPath);

		private bool _alowToSave;

		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		public FormMain()
		{
			InitializeComponent();
			FormStateHelper.Init(this, Path.GetDirectoryName(typeof(FormMain).Assembly.Location), true);
			if ((CreateGraphics()).DpiX > 96)
			{
				ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
			}
		}

		public TabHomeControl TabHome { get; set; }
		public TabSearchControl TabSearch { get; set; }
		public TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }
		public TabProgramSchedule TabProgramSchedule { get; set; }
		public TabProgramSearch TabProgramSearch { get; set; }
		public TabQBuilder TabQBuilder { get; set; }
		public TabGallery1 TabGallery1 { get; set; }
		public TabGallery2 TabGallery2 { get; set; }
		public TabFavorites TabFavorites { get; set; }
		public TabSettings TabSettings { get; set; }

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
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
					floaterLogo = labelItemSettingsLogo.Image;
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				{
					if (File.Exists(SettingsManager.Instance.CalendarLogoPath))
						floaterLogo = new Bitmap(SettingsManager.Instance.CalendarLogoPath);
					else
						floaterLogo = Resources.CalendarLogo;
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule || ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
					floaterLogo = labelItemProgramScheduleStationLogo.Image;
				return floaterLogo;
			}
		}

		public string FloaterText
		{
			get { return ribbonBarStations.Text; }
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (Environment.OSVersion.Version.Major < 6) return;
			int attrValue = 1;
			int res = WinAPIHelper.DwmSetWindowAttribute(Handle, WinAPIHelper.DWMWA_TRANSITIONS_FORCEDISABLED, ref attrValue, sizeof(int));
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

			TabGallery1 = new TabGallery1();
			TabGallery1.InitController();

			TabGallery2 = new TabGallery2();
			TabGallery2.InitController();

			TabFavorites = new TabFavorites();
			TabFavorites.InitController();

			TabSettings = new TabSettings();
			TabSettings.InitController();
		}

		private void LoadApplicationSettings()
		{
			if (File.Exists(SettingsManager.Instance.IconPath))
				Icon = new Icon(SettingsManager.Instance.IconPath);
			Text = string.Format("{0} - User: {1}", SettingsManager.Instance.SalesDepotName, Environment.UserName);

			ConfigureTabPages();
			ribbonControl.SelectedRibbonTabItem = ribbonTabItemHome;

			buttonItemProgramScheduleOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;
			buttonItemProgramSearchOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void ConfigureTabPages()
		{
			ribbonControl.Items.Clear();
			var tabPages = new List<BaseItem>();
			foreach (TabPageConfig tabPageConfig in _tabPageManager.TabPageSettings)
			{
				switch (tabPageConfig.Id)
				{
					case "Home":
						ribbonTabItemHome.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemHome);
						break;
					case "Search":
						ribbonTabItemSearch.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemSearch);
						break;
					case "QuickSites":
						ribbonTabItemQBuilder.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemQBuilder);
						break;
					case "Calendar":
						ribbonTabItemCalendar.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemCalendar);
						break;
					case "ProgramSchedule":
						ribbonTabItemProgramSchedule.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemProgramSchedule);
						break;
					case "ProgramSearch":
						ribbonTabItemProgramSearch.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemProgramSearch);
						break;
					case "Gallery1":
						ribbonTabItemGallery1.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemGallery1);
						break;
					case "Gallery2":
						ribbonTabItemGallery2.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemGallery2);
						break;
					case "Favorites":
						ribbonTabItemFavorites.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemFavorites);
						break;
					case "Settings":
						ribbonTabItemSettings.Text = tabPageConfig.Name;
						tabPages.Add(ribbonTabItemSettings);
						break;
				}
			}
			ribbonControl.Items.AddRange(tabPages.ToArray());
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
			TabGallery1.IsActive = false;
			TabGallery2.IsActive = false;
			TabFavorites.IsActive = false;
			TabSettings.IsActive = false;

			SettingsManager.Instance.HomeView = false;
			SettingsManager.Instance.SearchView = false;
			SettingsManager.Instance.CalendarView = false;
			SettingsManager.Instance.SaveSettings();

			if (!_alowToSave) return;

			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
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
				{
					pnContainer.Controls.Add(TabProgramSchedule);
				}
				TabProgramSchedule.ShowTab();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
			{
				if (!pnContainer.Controls.Contains(TabProgramSearch))
				{
					pnContainer.Controls.Add(TabProgramSearch);
				}
				TabProgramSearch.ShowTab();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemQBuilder)
			{
				if (!pnContainer.Controls.Contains(TabQBuilder))
					pnContainer.Controls.Add(TabQBuilder);
				TabQBuilder.ShowTab();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGallery1)
			{
				if (!pnContainer.Controls.Contains(TabGallery1))
				{
					TabGallery1.InitControl();
					pnContainer.Controls.Add(TabGallery1);
				}
				TabGallery1.ShowTab();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGallery2)
			{
				if (!pnContainer.Controls.Contains(TabGallery2))
				{
					TabGallery2.InitControl();
					pnContainer.Controls.Add(TabGallery2);
				}
				TabGallery2.ShowTab();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemFavorites)
			{
				if (!pnContainer.Controls.Contains(TabFavorites))
				{
					TabFavorites.Init();
					pnContainer.Controls.Add(TabFavorites);
				}
				TabFavorites.ShowTab();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
			{
				if (!pnContainer.Controls.Contains(TabSettings))
					pnContainer.Controls.Add(TabSettings);
				TabSettings.ShowTab();
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
			AppManager.Instance.ActivityManager.StartQueue();
			ribbonControl.Visible = false;
			pnEmpty.BringToFront();
			
			using (var form = new FormProgress())
			{
				form.TopMost = true;
				form.laProgress.Text = "Loading...";
				if (SettingsManager.Instance.ShowStartProgress)
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
							DecoratorManager.Instance.BuildOvernightsCalendars();
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
						_alowToSave = true;
						Application.DoEvents();
					}));
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
				}
				if (SettingsManager.Instance.ShowStartProgress)
					form.Close();
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
			AppManager.Instance.ActivityManager.CloseQueue();
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