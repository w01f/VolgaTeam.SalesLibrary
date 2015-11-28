using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesLibraries.Business.Entities.Calendars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.CommonGUI.Calendars;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Calendars
{
	partial class CalendarContainerControl : UserControl, ICalendarContainerControl
	{
		private CalendarSettings _currentSettings;
		public List<CalendarPartControl> CalendarParts { get; private set; }
		public List<ButtonItem> CalendarToggles { get; private set; }

		private CalendarPartControl SelectedCalendarPart
		{
			get
			{
				return CalendarParts
					.FirstOrDefault(cp => cp.PartData.Enabled &&
						String.Equals(cp.PartData.Name, MainController.Instance.Settings.CalendarViewSettings.SelectedCalendar, StringComparison.InvariantCultureIgnoreCase)) ??
					CalendarParts.FirstOrDefault(cp => cp.PartData.Enabled);
			}
		}

		public int CurrentFontSize
		{
			get { return MainController.Instance.Settings.CalendarViewSettings.FontSize; }
		}

		public Control ContainerControl
		{
			get { return pnContainer; }
		}

		public CalendarContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			pnContainer.Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			pnEmpty.BringToFront();

			CalendarParts = new List<CalendarPartControl>();
			CalendarToggles = new List<ButtonItem>();
		}

		public void LoadData(CalendarSettings calendarSettings)
		{
			_currentSettings = calendarSettings;
			pnEmpty.BringToFront();
			LoadControls();
			pnContainer.BringToFront();
		}

		public IEnumerable<ButtonItem> GetPartToggleButtons()
		{
			if (CalendarToggles.Any() || !CalendarParts.Any())
				return CalendarToggles;
			var selectedCalendarPart = SelectedCalendarPart;
			foreach (var partControl in CalendarParts)
			{
				var button = new ButtonItem();
				button.Text = partControl.PartData.Name;
				button.Tag = partControl;
				button.Enabled = partControl.PartData.Enabled;
				button.Checked = partControl == selectedCalendarPart;
				button.Click += OnCalendarPartClicked;
				button.CheckedChanged += OnCalendarPartSelected;
				CalendarToggles.Add(button);
			}
			return CalendarToggles;
		}

		public void InvokeInContainer(Delegate method)
		{
			MainController.Instance.MainForm.Invoke(method);
			MainController.Instance.ActivateApplication();
		}

		public void RunProcessInBackground(string title, Action<CancellationToken> process)
		{
			MainController.Instance.ProcessManager.Run(title, process);
		}

		public void UpFont()
		{
			MainController.Instance.Settings.CalendarViewSettings.FontSize++;
			MainController.Instance.Settings.SaveSettings();
			UpdateFont();
		}

		public void DownFont()
		{
			MainController.Instance.Settings.CalendarViewSettings.FontSize--;
			MainController.Instance.Settings.SaveSettings();
			UpdateFont();
		}

		private void LoadControls()
		{
			MainController.Instance.ProcessManager.Run("Loading Overnights Data...", cancellationToken =>
			{
				MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					ContainerControl.Controls.Clear();
					CalendarToggles.Clear();
					CalendarParts.ForEach(cp => cp.DisposePart());
					CalendarParts.Clear();
				}));
				if (!_currentSettings.Enabled) return;
				var dataContainer = CalendarContainer.Create(_currentSettings);
				if (!dataContainer.Parts.Any()) return;
				MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					foreach (var calendarPart in dataContainer.Parts)
					{
						var calendarPartControl = new CalendarPartControl(calendarPart, this);
						calendarPartControl.YearSelected += OnCalendarYearSelected;
						CalendarParts.Add(calendarPartControl);
					}
				}));
			});
			var selectedCalendarPart = SelectedCalendarPart;
			if (selectedCalendarPart == null) return;
			selectedCalendarPart.Show(MainController.Instance.Settings.CalendarViewSettings.SelectedYear);
		}

		private void UpdateFont()
		{
			foreach (var calendarPartControl in CalendarParts)
				calendarPartControl.RefreshFont();
		}

		private void OnCalendarPartClicked(object sender, EventArgs e)
		{
			var buttonItem = (ButtonItem)sender;
			if (buttonItem.Checked) return;
			CalendarToggles.ForEach(ct => ct.Checked = false);
			buttonItem.Checked = true;
		}

		private void OnCalendarPartSelected(object sender, EventArgs e)
		{
			var buttonItem = (ButtonItem)sender;
			if (!buttonItem.Checked) return;
			var calendarPartControl = (CalendarPartControl)buttonItem.Tag;
			calendarPartControl.Show();
			MainController.Instance.Settings.CalendarViewSettings.SelectedCalendar = calendarPartControl.PartData.Name;
			MainController.Instance.Settings.SaveSettings();
		}

		private void OnCalendarYearSelected(object sender, CalendarYearSelectedEventArgs e)
		{
			MainController.Instance.Settings.CalendarViewSettings.SelectedYear = e.Year;
			MainController.Instance.Settings.SaveSettings();
		}
	}
}
