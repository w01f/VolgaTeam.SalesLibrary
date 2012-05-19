using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public PresentationClasses.OvernightsCalendar.OvernightsCalendarControl OvernightsCalendar { get; private set; }
        public bool StateChanged { get; set; }

        public LibraryDecorator(PackageDecorator parent, BusinessClasses.Library library)
        {
            this.Parent = parent;
            this.Pages = new List<PageDecorator>();
            this.Library = library;
            this.TabControl = new PresentationClasses.WallBin.MultitabLibraryControl();
            this.OvernightsCalendar = new OvernightsCalendar.OvernightsCalendarControl(this);
            this.Container = new Panel();
            this.Container.Dock = DockStyle.Fill;
            this.EmptyPanel = new Panel();
            this.EmptyPanel.Dock = DockStyle.Fill;
            this.Container.Controls.Add(this.EmptyPanel);
            BuildPages();
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
        }

        private void ApplyWallBin()
        {
            this.Container.Controls.Clear();
            this.Container.Controls.Add(this.EmptyPanel);
            if (ConfigurationClasses.SettingsManager.Instance.MultitabView)
                FillTabControlWithPages();
            else
                FillDropdownWithPages();
            if (!this.Parent.Container.Controls.Contains(this.Container))
                this.Parent.Container.Controls.Add(this.Container);
            this.Container.BringToFront();
            UpdateView();
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
            this.TabControl.AddPages(this.Pages.ToArray());
            if (!this.Container.Controls.Contains(this.TabControl))
                this.Container.Controls.Add(this.TabControl);
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
    }
}
