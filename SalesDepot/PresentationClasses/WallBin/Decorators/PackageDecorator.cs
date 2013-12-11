using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.Properties;

namespace SalesDepot.PresentationClasses.WallBin.Decorators
{
	public class PackageDecorator
	{
		private readonly List<LibraryDecorator> _decorators = new List<LibraryDecorator>();
		private int _selectedLibrary = -1;

		public PackageDecorator(LibraryPackage package)
		{
			Container = new Panel();
			Container.Dock = DockStyle.Fill;
			Package = package;
			LoadLogo();
			BuildDecorators();
		}

		public LibraryPackage Package { get; set; }
		public Panel Container { get; private set; }
		public Bitmap Logo { get; private set; }

		public string Name
		{
			get { return Package.Name; }
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

		private void LoadLogo()
		{
			string defaultLogoFile = Path.Combine(SettingsManager.Instance.LibraryLogoFolder, Package.Folder.Name, SettingsManager.NoLogoFileName);
			if (File.Exists(defaultLogoFile))
				Logo = new Bitmap(defaultLogoFile);
			else
			{
				defaultLogoFile = Path.Combine(SettingsManager.Instance.LibraryLogoFolder, SettingsManager.NoLogoFileName);
				if (File.Exists(defaultLogoFile))
					Logo = new Bitmap(defaultLogoFile);
				else
					Logo = Resources.PackageLogo;
			}
		}

		private void BuildDecorators()
		{
			foreach (var salesDepot in Package.LibraryCollection)
			{
				_decorators.Add(new LibraryDecorator(this, salesDepot));
				Application.DoEvents();
			}
		}

		public void BuildOvernightsCalendar()
		{
			foreach (var decorator in _decorators)
			{
				decorator.BuildOvernightsCalendar();
				Application.DoEvents();
			}
		}

		public void BuildProgramManager()
		{
			foreach (var decorator in _decorators)
			{
				decorator.BuildProgramManager();
				Application.DoEvents();
			}
		}

		private void ApplyDecorator()
		{
			if (SelectedLibrary != null)
				SelectedLibrary.ApplyDecorator();
		}

		private void FillLogo()
		{
			FormMain.Instance.labelItemPackageLogo.Image = Logo;
			FormMain.Instance.labelItemSearchLogo.Image = Logo;
			FormMain.Instance.ribbonBarStations.RecalcLayout();
			FormMain.Instance.ribbonPanelHome.PerformLayout();
		}

		private void FillStations()
		{
			FormMain.Instance.TabHome.StationChanged = StationChanged;
			FormMain.Instance.comboBoxItemStations.Items.Clear();
			FormMain.Instance.comboBoxItemStations.Items.AddRange(Package.LibraryCollection.Select(x => x.Name).ToArray());

			FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;

			if (FormMain.Instance.comboBoxItemStations.Items.Count > 0)
			{
				_selectedLibrary = FormMain.Instance.comboBoxItemStations.Items.IndexOf(SettingsManager.Instance.SelectedLibrary);
				if (_selectedLibrary >= 0)
					FormMain.Instance.comboBoxItemStations.SelectedIndex = _selectedLibrary;
				else
					FormMain.Instance.comboBoxItemStations.SelectedIndex = 0;
			}
			StationChanged(FormMain.Instance.comboBoxItemStations);
		}

		private void StationChanged(object sender)
		{
			var comboBox = (ComboBoxItem)sender;
			_selectedLibrary = comboBox.SelectedIndex;
			if (comboBox.SelectedItem != null)
			{
				SettingsManager.Instance.SelectedLibrary = comboBox.SelectedItem.ToString();
				SettingsManager.Instance.SaveSettings();
			}
			ApplyDecorator();
		}

		public void FormatWallBin()
		{
			if (SelectedLibrary != null)
				foreach (var page in SelectedLibrary.Pages)
					page.UpdatePage();
		}

		public void FormatCalendar()
		{
			if (SelectedLibrary != null)
			{
				SelectedLibrary.UpdateCalendarFontButtonsStatus();
				SelectedLibrary.OvernightsCalendar.RefreshFont();
			}
		}

		public void UpdateView()
		{
			if (SelectedLibrary != null)
				SelectedLibrary.UpdateView();
		}

		public void Apply()
		{
			FillLogo();
			FillStations();
			UpdateView();
		}

		public void ReleaseResources()
		{
			if (Logo != null)
				Logo.Dispose();
		}
	}
}