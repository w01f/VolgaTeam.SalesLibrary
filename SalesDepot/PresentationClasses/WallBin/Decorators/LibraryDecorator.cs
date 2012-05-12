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
            BuildPages();
        }

        private void BuildPages()
        {
            this.Pages.Clear();
            foreach (BusinessClasses.LibraryPage page in this.Library.Pages)
                this.Pages.Add(new PageDecorator(this, page));
        }

        public void BuildOvernightsCalendar()
        {
            this.Library.OvernightsCalendar.LoadYears();
            if (this.Library.OvernightsCalendar.Enabled)
                this.OvernightsCalendar.Build();
        }

        private void ApplyPage(object sender)
        {
            DevComponents.DotNetBar.ComboBoxItem comboBox = (DevComponents.DotNetBar.ComboBoxItem)sender;
            _selectedPageIndex = comboBox.SelectedIndex;
            if (comboBox.SelectedItem != null)
            {
                ConfigurationClasses.SettingsManager.Instance.SelectedPage = comboBox.SelectedItem.ToString();
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
            FormMain.Instance.TabHome.ClassicViewControl.pnEmpty.BringToFront();
            foreach (Control control in FormMain.Instance.TabHome.ClassicViewControl.pnSalesDepotContainer.Controls)
                control.Parent = null;
            if (_selectedPageIndex < this.Pages.Count)
            {
                this.CurrentPage = this.Pages[_selectedPageIndex];
                this.CurrentPage.Apply();
            }
            FormMain.Instance.TabHome.ClassicViewControl.pnEmpty.SendToBack();
        }

        public void ApplyDecorator(bool firstRun = false)
        {
            ApplyWallBin();
            ApplyOvernightsCalebdar();
        }

        private void ApplyWallBin()
        {
            FormMain.Instance.TabHome.ClassicViewControl.pnEmpty.BringToFront();
            foreach (Control control in FormMain.Instance.TabHome.ClassicViewControl.pnSalesDepotContainer.Controls)
                control.Parent = null;
            if (ConfigurationClasses.SettingsManager.Instance.MultitabView)
                FillTabControlWithPages();
            else
                FillDropdownWithPages();
            FormMain.Instance.TabHome.ClassicViewControl.pnEmpty.SendToBack();
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
                page.FitPage();
            }
            this.TabControl.AddPages(this.Pages.ToArray());
            FormMain.Instance.TabHome.ClassicViewControl.pnSalesDepotContainer.Controls.Add(this.TabControl);
        }

        private void FillDropdownWithPages()
        {
            FormMain.Instance.TabHome.PageChanged = ApplyPage;
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
    }
}
