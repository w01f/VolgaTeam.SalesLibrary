using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesLibraries.Business.Entities.Calendars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Calendars;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Calendars
{
	sealed partial class CalendarContainerControl : UserControl, ICalendarContainerControl
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
						String.Equals(cp.PartData.Name, MainController.Instance.Settings.SelectedCalendar, StringComparison.InvariantCultureIgnoreCase)) ??
					CalendarParts.FirstOrDefault(cp => cp.PartData.Enabled);
			}
		}

		public int CurrentFontSize => MainController.Instance.Settings.CalendarFontSize;

		public Control ContainerControl => pnContainer;

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
			var pathChanged = _currentSettings == null ||
				!String.Equals(calendarSettings.Path, _currentSettings.Path, StringComparison.InvariantCultureIgnoreCase);
			_currentSettings = calendarSettings.Clone<CalendarSettings>(null);
			if (pathChanged)
			{
				pnEmpty.BringToFront();
				LoadControls();
				pnContainer.BringToFront();
			}
			else
			{
				foreach (var calendarPartControl in CalendarParts)
				{
					calendarPartControl.PartData.Parent.ApplyColorSettings(_currentSettings);
					break;
				}
				RefreshView();
			}
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
		}

		public void RunProcessInBackground(string title, Action<CancellationToken, FormProgressCommon> process)
		{
			MainController.Instance.ProcessManager.Run(title, process);
		}

		public void UpFont()
		{
			MainController.Instance.Settings.CalendarFontSize++;
			MainController.Instance.Settings.Save();
			UpdateFont();
		}

		public void DownFont()
		{
			MainController.Instance.Settings.CalendarFontSize--;
			MainController.Instance.Settings.Save();
			UpdateFont();
		}

		private void LoadControls()
		{
			MainController.Instance.ProcessManager.Run("Loading Overnights Data...", (cancellationToken, formProgress) =>
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
			selectedCalendarPart.Show(MainController.Instance.Settings.SelectedCalendarYear);
		}

		private void RefreshView()
		{
			foreach (var calendarPartControl in CalendarParts)
				calendarPartControl.RefreshView();
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
			MainController.Instance.Settings.SelectedCalendar = calendarPartControl.PartData.Name;
			MainController.Instance.Settings.Save();
		}

		private void OnCalendarYearSelected(object sender, CalendarYearSelectedEventArgs e)
		{
			MainController.Instance.Settings.SelectedCalendarYear = e.Year;
			MainController.Instance.Settings.Save();
		}
	}
}
