using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin.Decorators
{
    public class PackageDecorator
    {
        public BusinessClasses.LibraryPackage Package { get; set; }
        public Panel Container { get; private set; }
        public Bitmap Logo { get; private set; }
        private int _selectedLibrary = -1;
        private List<LibraryDecorator> _decorators = new List<LibraryDecorator>();

        public string Name
        {
            get
            {
                return this.Package.Name;
            }
        }

        public LibraryDecorator SelectedLibrary
        {
            get
            {
                if (_selectedLibrary >= 0 && _selectedLibrary < _decorators.Count)
                    return _decorators[_selectedLibrary];
                else
                    return null;
            }
        }

        public PackageDecorator(BusinessClasses.LibraryPackage package)
        {
            this.Container = new Panel();
            this.Container.Dock = DockStyle.Fill;
            this.Package = package;
            LoadLogo();
            BuildDecorators();
        }

        private void LoadLogo()
        {
            string defaultLogoFile = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LibraryLogoFolder, this.Package.Folder.Name, ConfigurationClasses.SettingsManager.NoLogoFileName);
            if (File.Exists(defaultLogoFile))
                this.Logo = new Bitmap(defaultLogoFile);
            else
            {
                defaultLogoFile = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LibraryLogoFolder, ConfigurationClasses.SettingsManager.NoLogoFileName);
                if (File.Exists(defaultLogoFile))
                    this.Logo = new Bitmap(defaultLogoFile);
                else
                    this.Logo = Properties.Resources.PackageLogo;
            }
        }

        private void BuildDecorators()
        {
            foreach (BusinessClasses.Library salesDepot in this.Package.LibraryCollection)
            {
                _decorators.Add(new LibraryDecorator(this, salesDepot));
                Application.DoEvents();
            }
        }

        public void BuildOvernightsCalendar()
        {
            foreach (LibraryDecorator decorator in _decorators)
            {
                decorator.BuildOvernightsCalendar();
                Application.DoEvents();
            }
        }

        public void BuildProgramManager()
        {
            foreach (LibraryDecorator decorator in _decorators)
            {
                decorator.BuildProgramManager();
                Application.DoEvents();
            }
        }

        private void ApplyDecorator()
        {
            if (this.SelectedLibrary != null)
                this.SelectedLibrary.ApplyDecorator();
        }

        private void FillLogo()
        {
            FormMain.Instance.labelItemPackageLogo.Image = this.Logo;
            FormMain.Instance.ribbonBarStations.RecalcLayout();
            FormMain.Instance.ribbonPanelHome.PerformLayout();
        }

        private void FillStations()
        {
            FormMain.Instance.TabHome.StationChanged = StationChanged;
            FormMain.Instance.comboBoxItemStations.Items.Clear();
            FormMain.Instance.comboBoxItemStations.Items.AddRange(this.Package.LibraryCollection.Select(x => x.Name).ToArray());

            if (FormMain.Instance.comboBoxItemStations.Items.Count > 1)
                FormMain.Instance.comboBoxItemStations.Visible = (ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView) & true;
            else
                FormMain.Instance.comboBoxItemStations.Visible = (ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView) & false;

            if (FormMain.Instance.comboBoxItemStations.Items.Count > 0)
            {
                _selectedLibrary = FormMain.Instance.comboBoxItemStations.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.SelectedLibrary);
                if (_selectedLibrary >= 0)
                    FormMain.Instance.comboBoxItemStations.SelectedIndex = _selectedLibrary;
                else
                    FormMain.Instance.comboBoxItemStations.SelectedIndex = 0;
            }
            StationChanged(FormMain.Instance.comboBoxItemStations);
        }

        private void StationChanged(object sender)
        {
            DevComponents.DotNetBar.ComboBoxItem comboBox = (DevComponents.DotNetBar.ComboBoxItem)sender;
            _selectedLibrary = comboBox.SelectedIndex;
            if (comboBox.SelectedItem != null)
            {
                ConfigurationClasses.SettingsManager.Instance.SelectedLibrary = comboBox.SelectedItem.ToString();
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
            ApplyDecorator();
        }

        public void FormatWallBin()
        {
            if (this.SelectedLibrary != null)
                foreach (PageDecorator page in this.SelectedLibrary.Pages)
                    page.FitObjectsToPage();
        }

        public void FormatCalendar()
        {
            if (this.SelectedLibrary != null)
            {
                this.SelectedLibrary.UpdateCalendarFontButtonsStatus();
                this.SelectedLibrary.OvernightsCalendar.RefreshFont();
            }
        }

        public void UpdateView()
        {
            if (this.SelectedLibrary != null)
                this.SelectedLibrary.UpdateView();
        }

        public void Apply()
        {
            FillLogo();
            FillStations();
            UpdateView();
        }

        public void ReleaseResources()
        {
            if (this.Logo != null)
                this.Logo.Dispose();
        }
    }
}
