using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin.Decorators
{
    public class LibraryDecorator
    {
        private int _selectedPageIndex = -1;

        public PackageDecorator Parent { get; private set; }
        public BusinessClasses.Library Library { get; set; }
        public PageDecorator CurrentPage { get; set; }
        public List<PageDecorator> Pages { get; set; }
        
        public Panel Container { get; private set; }
        public Panel EmptyPanel { get; private set; }

        public PresentationClasses.WallBin.MultitabLibraryControl TabControl { get; private set; }
        public PresentationClasses.WallBin.WallBinTreeListControl TreeListControl { get; private set; }
        public PresentationClasses.OvernightsCalendar.OvernightsCalendarControl OvernightsCalendar { get; private set; }
        public PresentationClasses.ProgramManager.ProgramScheduleControl ProgramSchedule { get; private set; }
        public PresentationClasses.ProgramManager.ProgramSearchControl ProgramSearch { get; private set; }
        public bool StateChanged { get; set; }

        public LibraryDecorator(PackageDecorator parent, BusinessClasses.Library library)
        {
            this.Parent = parent;
            this.Pages = new List<PageDecorator>();
            this.Library = library;
            this.TabControl = new PresentationClasses.WallBin.MultitabLibraryControl();
            this.TreeListControl = new WallBinTreeListControl();
            this.OvernightsCalendar = new OvernightsCalendar.OvernightsCalendarControl(this);
            this.ProgramSchedule = new ProgramManager.ProgramScheduleControl(this);
            this.ProgramSearch = new ProgramManager.ProgramSearchControl(this);
            this.Container = new Panel();
            this.Container.Dock = DockStyle.Fill;
            this.EmptyPanel = new Panel();
            this.EmptyPanel.Dock = DockStyle.Fill;
            this.Container.Controls.Add(this.EmptyPanel);
            BuildPages();
            BuildTreeList();
        }

        private void BuildPages()
        {
            this.Pages.Clear();
            foreach (BusinessClasses.LibraryPage page in this.Library.Pages)
            {
                this.Pages.Add(new PageDecorator(this, page));
                Application.DoEvents();
            }
        }

        private void BuildTreeList()
        {
            if (this.Library.UseDirectAccess)
            {
                this.TreeListControl.Init(this.Library);
            }
        }

        private void PageChanged(object sender)
        {
            DevComponents.DotNetBar.ComboBoxItem comboBox = (DevComponents.DotNetBar.ComboBoxItem)sender;
            _selectedPageIndex = comboBox.SelectedIndex;
            if (comboBox.SelectedItem != null)
            {
                ConfigurationClasses.SettingsManager.Instance.SelectedPage = comboBox.SelectedItem.ToString();
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
            ApplyPage();
        }

        private void ApplyPage()
        {
            if (_selectedPageIndex < this.Pages.Count)
            {
                this.CurrentPage = this.Pages[_selectedPageIndex];
                this.CurrentPage.Container.Parent = null;
                if (!this.Container.Controls.Contains(this.CurrentPage.Container))
                    this.Container.Controls.Add(this.CurrentPage.Container);
                this.CurrentPage.Container.BringToFront();
                this.CurrentPage.Apply();
            }
        }

        public void ApplyDecorator(bool firstRun = false)
        {
            ApplyWallBin();
            ApplyOvernightsCalebdar();
            ApplyProgramManager();
        }

        private void ApplyWallBin()
        {
            if (!this.Library.UseDirectAccess)
            {
                if (ConfigurationClasses.SettingsManager.Instance.MultitabView)
                    FillTabControlWithPages();
                else
                    FillDropdownWithPages();
            }
            else
            {
                FillTreeListControl();
            }
            if (!this.Parent.Container.Controls.Contains(this.Container))
                this.Parent.Container.Controls.Add(this.Container);
            this.Container.BringToFront();

        }

        private void FillTreeListControl()
        {
            FormMain.Instance.TabHome.PageChanged = null;
            if (!this.Container.Controls.Contains(this.TreeListControl))
                this.Container.Controls.Add(this.TreeListControl);
            this.TreeListControl.BringToFront();
        }

        private void FillTabControlWithPages()
        {
            FormMain.Instance.TabHome.PageChanged = null;
            FormMain.Instance.comboBoxItemPages.Items.Clear();
            FormMain.Instance.comboBoxItemPages.Enabled = false;
            foreach (PageDecorator page in this.Pages)
            {
                page.Container.Parent = null;
                page.TabPage.Controls.Add(page.Container);
            }
            if (!this.Container.Controls.Contains(this.TabControl))
                this.Container.Controls.Add(this.TabControl);
            this.TabControl.AddPages(this.Pages.ToArray());
            this.TabControl.BringToFront();
        }

        private void FillDropdownWithPages()
        {
            FormMain.Instance.TabHome.PageChanged = PageChanged;
            FormMain.Instance.comboBoxItemPages.Items.Clear();
            FormMain.Instance.comboBoxItemPages.Enabled = false;

            FormMain.Instance.comboBoxItemPages.Items.AddRange(this.Library.Pages.Select(x => x.Name).ToArray());

            if (FormMain.Instance.comboBoxItemPages.Items.Count > 1)
                FormMain.Instance.comboBoxItemPages.Enabled = true;
            if (FormMain.Instance.comboBoxItemPages.Items.Count > 0)
            {
                _selectedPageIndex = FormMain.Instance.comboBoxItemPages.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.SelectedPage);
                if (_selectedPageIndex >= 0)
                    FormMain.Instance.comboBoxItemPages.SelectedIndex = _selectedPageIndex;
                else
                    FormMain.Instance.comboBoxItemPages.SelectedIndex = 0;
            }
        }

        public void UpdateView()
        {
            foreach (PageDecorator page in this.Pages)
                page.FitPage();
        }

        #region Overnights Calendar Stuff
        public void BuildOvernightsCalendar()
        {
            this.Library.OvernightsCalendar.LoadYears();
            Application.DoEvents();
            if (this.Library.OvernightsCalendar.Enabled)
            {
                this.OvernightsCalendar.Build();
                Application.DoEvents();
            }
        }

        private void ApplyOvernightsCalebdar()
        {
            if (this.Library.OvernightsCalendar.Enabled)
            {
                FormMain.Instance.ribbonTabItemCalendar.Visible = true;
                UpdateCalendarFontButtonsStatus();
                if (!FormMain.Instance.TabOvernightsCalendar.Controls.Contains(this.OvernightsCalendar))
                    FormMain.Instance.TabOvernightsCalendar.Controls.Add(this.OvernightsCalendar);
                this.OvernightsCalendar.BringToFront();
            }
            else
            {
                FormMain.Instance.ribbonTabItemCalendar.Visible = false;
            }
            FormMain.Instance.ribbonControl.RecalcLayout();
        }

        public void UpdateCalendarFontButtonsStatus()
        {
            FormMain.Instance.buttonItemCalendarFontSizeLarger.Enabled = ConfigurationClasses.SettingsManager.Instance.CalendarFontSize < 14;
            FormMain.Instance.buttonItemCalendarFontSizeSmaler.Enabled = ConfigurationClasses.SettingsManager.Instance.CalendarFontSize > 10;
        }
        #endregion

        #region Program Manager Stuff
        public void BuildProgramManager()
        {
            this.Library.ProgramManager.LoadData();
            Application.DoEvents();
        }

        private void ApplyProgramManager()
        {
            if (this.Library.ProgramManager.Enabled)
            {
                FormMain.Instance.TabProgramSchedule.AllowToSave = false;
                FormMain.Instance.TabProgramSearch.AllowToSave = false;

                FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditProgramSearchStation.Properties.Items.Clear();
                FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.AddRange(this.Library.ProgramManager.GetStationList());
                FormMain.Instance.comboBoxEditProgramSearchStation.Properties.Items.AddRange(this.Library.ProgramManager.GetStationList());
                if (FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.Contains(ConfigurationClasses.SettingsManager.Instance.ProgramScheduleSelectedStation))
                {
                    FormMain.Instance.comboBoxEditProgramScheduleStation.SelectedIndex = FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.ProgramScheduleSelectedStation);
                    FormMain.Instance.comboBoxEditProgramSearchStation.SelectedIndex = FormMain.Instance.comboBoxEditProgramScheduleStation.SelectedIndex;
                }
                else if (FormMain.Instance.comboBoxEditProgramScheduleStation.Properties.Items.Count > 0)
                {
                    FormMain.Instance.comboBoxEditProgramScheduleStation.SelectedIndex = 0;
                    FormMain.Instance.comboBoxEditProgramSearchStation.SelectedIndex = 0;
                }

                DateTime nowDate = DateTime.Now;
                FormMain.Instance.dateEditProgramScheduleDay.DateTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day);

                FormMain.Instance.buttonItemProgramScheduleInfo.Checked = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleShowInfo;

                this.ProgramSchedule.gridViewPrograms.OptionsView.ShowPreview = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleShowInfo;

                switch (ConfigurationClasses.SettingsManager.Instance.ProgramScheduleBrowseType)
                {
                    case ConfigurationClasses.BrowseType.Day:
                        FormMain.Instance.TabProgramSchedule.buttonItemScheduleBrowseType_Click(FormMain.Instance.buttonItemProgramScheduleBrowseDay, null);
                        break;
                    case ConfigurationClasses.BrowseType.Week:
                        FormMain.Instance.TabProgramSchedule.buttonItemScheduleBrowseType_Click(FormMain.Instance.buttonItemProgramScheduleBrowseWeek, null);
                        break;
                    case ConfigurationClasses.BrowseType.Month:
                        FormMain.Instance.TabProgramSchedule.buttonItemScheduleBrowseType_Click(FormMain.Instance.buttonItemProgramScheduleBrowseMonth, null);
                        break;
                }

                this.ProgramSchedule.LoadStation();
                this.ProgramSchedule.LoadDay();

                this.ProgramSearch.ClearSearchParameters();
                this.ProgramSearch.LoadStation();
                this.ProgramSearch.LoadProgramsList();

                FormMain.Instance.TabProgramSchedule.AllowToSave = true;
                FormMain.Instance.TabProgramSearch.AllowToSave = true;

                if (!FormMain.Instance.TabProgramSchedule.Controls.Contains(this.ProgramSchedule))
                    FormMain.Instance.TabProgramSchedule.Controls.Add(this.ProgramSchedule);
                this.ProgramSchedule.BringToFront();

                if (!FormMain.Instance.TabProgramSearch.Controls.Contains(this.ProgramSearch))
                    FormMain.Instance.TabProgramSearch.Controls.Add(this.ProgramSearch);
                this.ProgramSearch.BringToFront();
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
