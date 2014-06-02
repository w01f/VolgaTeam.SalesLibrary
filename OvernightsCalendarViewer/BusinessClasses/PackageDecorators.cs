using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    class PackageDecorator
    {
        public LibraryPackage Package { get; set; }
        private Bitmap _logo;
        private int _selectedPackage = -1;
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
            string defaultLogoFile = Path.Combine(Application.StartupPath, "no_logo.png");
            if (File.Exists(defaultLogoFile))
                _logo = new Bitmap(defaultLogoFile);
            else
                _logo = Properties.Resources.PackageLogo;
            DirectoryInfo logoFolder = new DirectoryInfo(Path.Combine(this.Package.Folder.FullName, AppManager.LibraryLogoFolder));
            if (logoFolder.Exists)
            {
                foreach (var file in logoFolder.GetFiles(AppManager.LibraryLogoFile))
                {
                    _logo = new Bitmap(file.FullName);
                    break;
                }
            }
        }

        private void BuildDecorators()
        {
            foreach (Library salesDepot in this.Package.SalesDepotCollection)
            {
                _decorators.Add(new LibraryDecorator(salesDepot));
            }
        }

        private void ApplyDecorator()
        {
            if (_selectedPackage >= 0 && _selectedPackage < _decorators.Count)
                _decorators[_selectedPackage].Apply();
        }

        private void FillLogo()
        {
            FormMain.Instance.labelItemPackageLogo.Image = _logo;
            FormMain.Instance.ribbonBarStations.RecalcLayout();
            FormMain.Instance.ribbonPanelHome.PerformLayout();
        }

        private void FillStations()
        {
            FormMain.Instance.StationChanged = StationChanged;
            FormMain.Instance.comboBoxItemStations.Items.Clear();
            foreach (Library salesDepot in this.Package.SalesDepotCollection)
                FormMain.Instance.comboBoxItemStations.Items.Add(salesDepot.Name);

            if (FormMain.Instance.comboBoxItemStations.Items.Count > 1)
                FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.buttonItemHomeClassicView.Checked & true;
            else
                FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.buttonItemHomeClassicView.Checked & false;

            if (FormMain.Instance.comboBoxItemStations.Items.Count > 0)
                FormMain.Instance.comboBoxItemStations.SelectedIndex = 0;
        }

        private void StationChanged(object sender)
        {
            DevComponents.DotNetBar.ComboBoxItem comboBox = (DevComponents.DotNetBar.ComboBoxItem)sender;
            _selectedPackage = comboBox.SelectedIndex;
            ApplyDecorator();
        }

        public void Format()
        {
            if (_selectedPackage >= 0 && _selectedPackage < _decorators.Count)
                _decorators[_selectedPackage].CurrentPage.FitObjectsToPage();
        }

        public void Resize()
        {
            if (_selectedPackage >= 0 && _selectedPackage < _decorators.Count)
                _decorators[_selectedPackage].CurrentPage.FitObjectsToPage();
        }

        public void Apply()
        {
            FormMain.Instance.classicViewControl.pnEmpty.BringToFront();
            FormMain.Instance.classicViewControl.pnSalesDepotContainer.Controls.Clear();
            FillStations();
            FillLogo();
            FormMain.Instance.classicViewControl.pnEmpty.SendToBack();
        }

        public void ReleaseResources()
        {
            if (_logo != null)
                _logo.Dispose();
        }
    }
}
