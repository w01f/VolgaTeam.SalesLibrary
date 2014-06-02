using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using OvernightsCalendarViewer.BusinessClasses;
using OvernightsCalendarViewer.ConfigurationClasses;
using OvernightsCalendarViewer.Properties;

namespace OvernightsCalendarViewer.PresentationClasses.Decorators
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
					Logo = Resources.CalendarLogo;
			}
		}

		private void BuildDecorators()
		{
			foreach (var salesDepot in Package.LibraryCollection)
			{
				var decorator = new LibraryDecorator(this, salesDepot);
				decorator.BuildOvernightsCalendar();
				_decorators.Add(decorator);
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
			FormMain.Instance.ribbonBarStations.RecalcLayout();
		}

		private void FillStations()
		{
			FormMain.Instance.TabOvernightsCalendar.StationChanged = StationChanged;
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

		public void FormatCalendar()
		{
			if (SelectedLibrary != null)
			{
				SelectedLibrary.UpdateCalendarFontButtonsStatus();
				SelectedLibrary.OvernightsCalendar.RefreshFont();
			}
		}

		public void Apply()
		{
			FillLogo();
			FillStations();
		}

		public void ReleaseResources()
		{
			if (Logo != null)
				Logo.Dispose();
		}
	}
}