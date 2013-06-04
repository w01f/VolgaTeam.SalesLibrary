using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
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

		private FormMain()
		{
			InitializeComponent();
		}

		private void InitControllers()
		{
			TabHome = new TabHomeControl();
			TabHome.InitController();

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

			buttonItemHomeClassicView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.ClassicTitle) ? SettingsManager.Instance.ClassicTitle : Instance.buttonItemHomeClassicView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeClassicView, new SuperTooltipInfo(SettingsManager.Instance.ClassicTitle, "", SettingsManager.Instance.ClassicDescription, null, null, eTooltipColor.Gray));

			buttonItemHomeListView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.ListTitle) ? SettingsManager.Instance.ListTitle : Instance.buttonItemHomeListView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeListView, new SuperTooltipInfo(SettingsManager.Instance.ListTitle, "", SettingsManager.Instance.ListDescription, null, null, eTooltipColor.Gray));

			buttonItemHomeAccordionView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.AccordionTitle) ? SettingsManager.Instance.AccordionTitle : Instance.buttonItemHomeAccordionView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeAccordionView, new SuperTooltipInfo(SettingsManager.Instance.AccordionTitle, "", SettingsManager.Instance.AccordionDescription, null, null, eTooltipColor.Gray));

			buttonItemHomeSolutionView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.SolutionTitle) ? SettingsManager.Instance.SolutionTitle : Instance.buttonItemHomeSolutionView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeSolutionView, new SuperTooltipInfo(SettingsManager.Instance.SolutionTitle, "", SettingsManager.Instance.SolutionDescription, null, null, eTooltipColor.Gray));

			ribbonBarHomeView.RecalcLayout();

			buttonItemProgramScheduleOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;
			buttonItemProgramSearchOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;

			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			Instance.Opacity = 0;
			RegistryHelper.MaximizeSalesDepot = false;

			Image floaterLogo = null;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings || ribbonControl.SelectedRibbonTabItem == ribbonTabItemQBuilder)
				floaterLogo = labelItemPackageLogo.Image;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				floaterLogo = labelItemCalendarLogo.Image;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule || ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
				floaterLogo = labelItemProgramScheduleStationLogo.Image;

			using (var form = new FormFloater(Left + Width - 50, Top + 50, _floaterPositionX, _floaterPositionY, floaterLogo, ribbonBarStations.Text))
			{
				if (form.ShowDialog() != DialogResult.No)
				{
					_floaterPositionY = form.Top;
					_floaterPositionX = form.Left;
					Instance.Opacity = 1;
					RegistryHelper.SalesDepotHandle = Handle;
					RegistryHelper.MaximizeSalesDepot = true;
					AppManager.Instance.ActivateMainForm();
				}
				else
					Close();
			}
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

			if (_alowToSave)
			{
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				{
					if (!pnContainer.Controls.Contains(TabHome))
						pnContainer.Controls.Add(TabHome);
					TabHome.ShowTab();
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
			using (var form = new FormProgress())
			{
				form.laProgress.Text = SettingsManager.Instance.UseRemoteConnection ? "Loading Remote Sales Libraries..." : "Loading Sales Libraries...";
				form.TopMost = true;
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
				form.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();

				if (LibraryManager.Instance.LibraryPackageCollection.Count > 0)
				{
					thread = new Thread(delegate()
											{
												Invoke((MethodInvoker)delegate
																		  {
																			  TabHome.LoadPage();
																			  Application.DoEvents();
																			  _alowToSave = true;
																			  if (SettingsManager.Instance.CalendarView)
																				  ribbonTabItemCalendar.Select();
																			  else
																				  ribbonControl_SelectedRibbonTabChanged(null, null);
																			  Application.DoEvents();
																		  });
											});
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
				}

				ribbonControl.Visible = true;
				pnContainer.BringToFront();
			}
			AppManager.Instance.ActivateMainForm();
			if (LibraryManager.Instance.LibraryPackageCollection.Count == 0)
			{
				ribbonBarStations.Enabled = false;
				ribbonBarHomeView.Enabled = false;
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