using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    public class PackageDecorator
    {
        public LibraryPackage Package { get; set; }
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

        public PackageDecorator(LibraryPackage package)
        {
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
            foreach (Library salesDepot in this.Package.SalesDepotCollection)
                _decorators.Add(new LibraryDecorator(this,salesDepot));
        }

        private void ApplyDecorator()
        {
            if (_selectedLibrary >= 0 && _selectedLibrary < _decorators.Count)
                _decorators[_selectedLibrary].Apply();
        }

        private void FillLogo()
        {
            FormMain.Instance.labelItemPackageLogo.Image = this.Logo;
            FormMain.Instance.ribbonBarStations.RecalcLayout();
            FormMain.Instance.ribbonPanelHome.PerformLayout();
        }

        private void FillStations()
        {
            FormMain.Instance.StationChanged = StationChanged;
            FormMain.Instance.comboBoxItemStations.Items.Clear();
            FormMain.Instance.comboBoxItemStations.Items.AddRange(this.Package.SalesDepotCollection.Select(x => x.Name).ToArray());

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

        public void Format()
        {
            if (_selectedLibrary >= 0 && _selectedLibrary < _decorators.Count)
                foreach (PageDecorator page in _decorators[_selectedLibrary].Pages)
                    page.FitObjectsToPage();
        }

        public void UpdateView()
        {
            if (_selectedLibrary >= 0 && _selectedLibrary < _decorators.Count)
                foreach (PageDecorator page in _decorators[_selectedLibrary].Pages)
                    page.FitPage();
        }

        public void Apply()
        {
            FormMain.Instance.ClassicViewControl.pnEmpty.BringToFront();
            FormMain.Instance.ClassicViewControl.pnSalesDepotContainer.Controls.Clear();
            FillLogo();
            FillStations();
            FormMain.Instance.ClassicViewControl.pnEmpty.SendToBack();
        }

        public void ReleaseResources()
        {
            if (this.Logo != null)
                this.Logo.Dispose();
        }
    }
}
