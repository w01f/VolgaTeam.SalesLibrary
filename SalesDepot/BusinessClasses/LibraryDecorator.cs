using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    public class LibraryDecorator
    {
        private int _selectedPageIndex = -1;

        public PackageDecorator Parent { get; private set; }
        public Library Library { get; set; }
        public PageDecorator CurrentPage { get; set; }
        public List<PageDecorator> Pages { get; set; }
        public CustomControls.MultitabLibraryControl TabControl { get; private set; }
        public bool StateChanged { get; set; }

        public LibraryDecorator(PackageDecorator parent, Library library)
        {
            this.Parent = parent;
            this.Pages = new List<PageDecorator>();
            this.Library = library;
            this.TabControl = new CustomControls.MultitabLibraryControl();
            BuildPages();
        }

        private void BuildPages()
        {
            this.Pages.Clear();
            foreach (LibraryPage page in this.Library.Pages)
                this.Pages.Add(new PageDecorator(this, page));
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
            FormMain.Instance.ClassicViewControl.pnEmpty.BringToFront();
            foreach (Control control in FormMain.Instance.ClassicViewControl.pnSalesDepotContainer.Controls)
                control.Parent = null;
            if (_selectedPageIndex < this.Pages.Count)
            {
                this.CurrentPage = this.Pages[_selectedPageIndex];
                this.CurrentPage.Apply();
            }
            FormMain.Instance.ClassicViewControl.pnEmpty.SendToBack();
        }

        public void Apply(bool firstRun = false)
        {
            FormMain.Instance.ClassicViewControl.pnEmpty.BringToFront();
            foreach (Control control in FormMain.Instance.ClassicViewControl.pnSalesDepotContainer.Controls)
                control.Parent = null;
            if (ConfigurationClasses.SettingsManager.Instance.MultitabView)
                FillTabControlWithPages();
            else
                FillDropdownWithPages();
            FormMain.Instance.ClassicViewControl.pnEmpty.SendToBack();
        }

        private void FillTabControlWithPages()
        {
            FormMain.Instance.PageChanged = null;
            FormMain.Instance.comboBoxItemPages.Items.Clear();
            FormMain.Instance.comboBoxItemPages.Enabled = false;
            foreach (PageDecorator page in this.Pages)
            {
                page.Container.Parent = null;
                page.TabPage.Controls.Add(page.Container);
                page.FitPage();
            }
            this.TabControl.AddPages(this.Pages.ToArray());
            FormMain.Instance.ClassicViewControl.pnSalesDepotContainer.Controls.Add(this.TabControl);
        }

        private void FillDropdownWithPages()
        {
            FormMain.Instance.PageChanged = ApplyPage;
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
