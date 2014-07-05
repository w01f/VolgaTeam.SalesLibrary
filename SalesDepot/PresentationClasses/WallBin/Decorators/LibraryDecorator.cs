using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.PresentationClasses.OvernightsCalendar;
using SalesDepot.PresentationClasses.ProgramManager;

namespace SalesDepot.PresentationClasses.WallBin.Decorators
{
	public class LibraryDecorator
	{
		private int _selectedPageIndex = -1;

		public LibraryDecorator(PackageDecorator parent, Library library)
		{
			Parent = parent;
			Pages = new List<PageDecorator>();
			Library = library;
			TabControl = new MultitabLibraryControl();
			TreeListControl = new WallBinTreeListControl();
			OvernightsCalendar = new OvernightsCalendarControl(this);
			ProgramSchedule = new ProgramScheduleControl(this);
			ProgramSearch = new ProgramSearchControl(this);
			Container = new Panel();
			Container.Dock = DockStyle.Fill;
			EmptyPanel = new Panel();
			EmptyPanel.Dock = DockStyle.Fill;
			Container.Controls.Add(EmptyPanel);
			BuildPages();
			BuildTreeList();
		}

		public PackageDecorator Parent { get; private set; }
		public Library Library { get; set; }
		public PageDecorator CurrentPage { get; set; }
		public List<PageDecorator> Pages { get; set; }

		public Panel Container { get; private set; }
		public Panel EmptyPanel { get; private set; }

		public MultitabLibraryControl TabControl { get; private set; }
		public WallBinTreeListControl TreeListControl { get; private set; }
		public OvernightsCalendarControl OvernightsCalendar { get; private set; }
		public ProgramScheduleControl ProgramSchedule { get; private set; }
		public ProgramSearchControl ProgramSearch { get; private set; }
		public bool StateChanged { get; set; }

		private void BuildPages()
		{
			Pages.Clear();
			foreach (var page in Library.Pages)
			{
				Pages.Add(new PageDecorator(this, page));
				Application.DoEvents();
			}
		}

		private void BuildTreeList()
		{
			if (Library.UseDirectAccess)
			{
				TreeListControl.Init(Library);
			}
		}

		private void PageChanged(object sender)
		{
			var comboBox = (ComboBoxItem)sender;
			_selectedPageIndex = comboBox.SelectedIndex;
			if (comboBox.SelectedItem != null)
			{
				SettingsManager.Instance.SelectedPage = comboBox.SelectedItem.ToString();
				SettingsManager.Instance.SaveSettings();
			}
			ApplyPage();
		}

		private void ApplyPage()
		{
			if (_selectedPageIndex >= Pages.Count) return;
			CurrentPage = Pages[_selectedPageIndex];
			CurrentPage.Container.Parent = null;
			if (!Container.Controls.Contains(CurrentPage.Container))
				Container.Controls.Add(CurrentPage.Container);
			CurrentPage.Container.BringToFront();
			CurrentPage.Apply();
		}

		public void ApplyDecorator(bool firstRun = false)
		{
			ApplyWallBin();
			ApplyOvernightsCalebdar();
			ApplyProgramManager();
		}

		private void ApplyWallBin()
		{
			if (!Library.UseDirectAccess)
			{
				if (SettingsManager.Instance.MultitabView)
					FillTabControlWithPages();
				else
					FillDropdownWithPages();
			}
			else
			{
				FillTreeListControl();
			}
			if (!Parent.Container.Controls.Contains(Container))
				Parent.Container.Controls.Add(Container);
			Container.BringToFront();
		}

		private void FillTreeListControl()
		{
			FormMain.Instance.TabHome.PageChanged = null;
			if (!Container.Controls.Contains(TreeListControl))
				Container.Controls.Add(TreeListControl);
			TreeListControl.BringToFront();
		}

		private void FillTabControlWithPages()
		{
			FormMain.Instance.TabHome.PageChanged = null;
			FormMain.Instance.comboBoxItemPages.Items.Clear();
			FormMain.Instance.comboBoxItemPages.Enabled = false;
			foreach (var page in Pages)
			{
				page.Container.Parent = null;
				page.TabPage.Controls.Add(page.Container);
			}
			if (!Container.Controls.Contains(TabControl))
				Container.Controls.Add(TabControl);
			TabControl.AddPages(Pages.ToArray());
			TabControl.BringToFront();
		}

		private void FillDropdownWithPages()
		{
			FormMain.Instance.TabHome.PageChanged = PageChanged;
			FormMain.Instance.comboBoxItemPages.Items.Clear();
			FormMain.Instance.comboBoxItemPages.Enabled = false;

			FormMain.Instance.comboBoxItemPages.Items.AddRange(Library.Pages.Select(x => x.Name).ToArray());

			if (FormMain.Instance.comboBoxItemPages.Items.Count > 1)
				FormMain.Instance.comboBoxItemPages.Enabled = true;
			if (FormMain.Instance.comboBoxItemPages.Items.Count <= 0) return;
			_selectedPageIndex = FormMain.Instance.comboBoxItemPages.Items.IndexOf(SettingsManager.Instance.SelectedPage);
			if (_selectedPageIndex >= 0)
				FormMain.Instance.comboBoxItemPages.SelectedIndex = _selectedPageIndex;
			else
				FormMain.Instance.comboBoxItemPages.SelectedIndex = 0;
		}

		public void UpdateView()
		{
			foreach (var page in Pages.Where(p=>p.ReadyToShow))
				page.UpdatePage();
		}

		#region Overnights Calendar Stuff
		public void BuildOvernightsCalendar()
		{
			Library.OvernightsCalendar.LoadParts();
			Application.DoEvents();
			if (!Library.OvernightsCalendar.Enabled) return;
			OvernightsCalendar.Build();
			Application.DoEvents();
		}

		private void ApplyOvernightsCalebdar()
		{
			if (Library.OvernightsCalendar.Enabled)
			{
				FormMain.Instance.ribbonTabItemCalendar.Visible = true;
				UpdateCalendarFontButtonsStatus();
				if (!FormMain.Instance.TabOvernightsCalendar.Controls.Contains(OvernightsCalendar))
					FormMain.Instance.TabOvernightsCalendar.Controls.Add(OvernightsCalendar);
				FormMain.Instance.ribbonBarCalendarParts.Items.Clear();
				FormMain.Instance.ribbonBarCalendarParts.Items.AddRange(OvernightsCalendar.PartToggles.ToArray());
				OvernightsCalendar.BringToFront();
			}
			else
			{
				FormMain.Instance.ribbonTabItemCalendar.Visible = false;
			}
			FormMain.Instance.ribbonControl.RecalcLayout();
		}

		public void UpdateCalendarFontButtonsStatus()
		{
			FormMain.Instance.buttonItemCalendarFontSizeLarger.Enabled = SettingsManager.Instance.CalendarFontSize < 14;
			FormMain.Instance.buttonItemCalendarFontSizeSmaler.Enabled = SettingsManager.Instance.CalendarFontSize > 10;
		}
		#endregion

		#region Program Manager Stuff
		public void BuildProgramManager()
		{
			Library.ProgramManager.LoadData();
			Application.DoEvents();
		}

		private void ApplyProgramManager()
		{
			if (Library.ProgramManager.Enabled)
			{
				FormMain.Instance.TabProgramSchedule.AllowToSave = false;
				FormMain.Instance.TabProgramSearch.AllowToSave = false;

				FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditProgramSearchStation.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.AddRange(Library.ProgramManager.GetStationList());
				FormMain.Instance.comboBoxEditProgramSearchStation.Properties.Items.AddRange(Library.ProgramManager.GetStationList());
				if (FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.Contains(SettingsManager.Instance.ProgramScheduleSelectedStation))
				{
					FormMain.Instance.comboBoxEditProgramScheduleStation.SelectedIndex = FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.IndexOf(SettingsManager.Instance.ProgramScheduleSelectedStation);
					FormMain.Instance.comboBoxEditProgramSearchStation.SelectedIndex = FormMain.Instance.comboBoxEditProgramScheduleStation.SelectedIndex;
				}
				else if (FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.Count > 0)
				{
					FormMain.Instance.comboBoxEditProgramScheduleStation.SelectedIndex = 0;
					FormMain.Instance.comboBoxEditProgramSearchStation.SelectedIndex = 0;
				}

				DateTime nowDate = DateTime.Now;
				FormMain.Instance.dateEditProgramScheduleDay.DateTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day);

				FormMain.Instance.buttonItemProgramScheduleInfo.Checked = SettingsManager.Instance.ProgramScheduleShowInfo;

				ProgramSchedule.gridViewPrograms.OptionsView.ShowPreview = SettingsManager.Instance.ProgramScheduleShowInfo;

				switch (SettingsManager.Instance.ProgramScheduleBrowseType)
				{
					case BrowseType.Day:
						FormMain.Instance.TabProgramSchedule.buttonItemScheduleBrowseType_Click(FormMain.Instance.buttonItemProgramScheduleBrowseDay, null);
						break;
					case BrowseType.Week:
						FormMain.Instance.TabProgramSchedule.buttonItemScheduleBrowseType_Click(FormMain.Instance.buttonItemProgramScheduleBrowseWeek, null);
						break;
					case BrowseType.Month:
						FormMain.Instance.TabProgramSchedule.buttonItemScheduleBrowseType_Click(FormMain.Instance.buttonItemProgramScheduleBrowseMonth, null);
						break;
				}

				ProgramSchedule.LoadStation();
				ProgramSchedule.LoadDay();

				ProgramSearch.ClearSearchParameters();
				ProgramSearch.LoadStation();
				ProgramSearch.LoadProgramsList();

				FormMain.Instance.TabProgramSchedule.AllowToSave = true;
				FormMain.Instance.TabProgramSearch.AllowToSave = true;

				if (!FormMain.Instance.TabProgramSchedule.Controls.Contains(ProgramSchedule))
					FormMain.Instance.TabProgramSchedule.Controls.Add(ProgramSchedule);
				ProgramSchedule.BringToFront();

				if (!FormMain.Instance.TabProgramSearch.Controls.Contains(ProgramSearch))
					FormMain.Instance.TabProgramSearch.Controls.Add(ProgramSearch);
				ProgramSearch.BringToFront();
			}
			else
			{
				FormMain.Instance.ribbonTabItemProgramSchedule.Visible = false;
				FormMain.Instance.ribbonTabItemProgramSearch.Visible = false;
			}
			FormMain.Instance.ribbonControl.RecalcLayout();
		}
		#endregion
	}
}