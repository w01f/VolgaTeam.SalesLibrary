using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ProgramManager.CoreObjects;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.InteropClasses;
using SalesDepot.ToolForms;
using Day = ProgramManager.CoreObjects.Day;

namespace SalesDepot.BusinessClasses
{
	public class ProgramScheduleManager
	{
		private const string StationsRoot = "Stations";
		private const string OutputRoot = "Output Templates";

		private readonly List<Station> _stations = new List<Station>();

		public ProgramScheduleManager(Library parent)
		{
			Parent = parent;
		}

		public Library Parent { get; set; }
		public Station SelectedStation { get; private set; }
		public Day SelectedDay { get; private set; }

		private string ProgramManagerRoot
		{
			get { return Path.Combine(Parent.Folder.FullName, Constants.ProgramManagerRootFolderName); }
		}

		public bool HasStations
		{
			get { return Directory.Exists(StationsFolderPath) && Directory.GetDirectories(StationsFolderPath).Any(); }
		}

		public bool Enabled
		{
			get { return _stations.Count > 0; }
		}

		public string StationsFolderPath
		{
			get { return Path.Combine(ProgramManagerRoot, StationsRoot); }
		}

		public string ReporActivityListTemplatePath
		{
			get { return Path.Combine(ProgramManagerRoot, OutputRoot, "ActivityList.xls"); }
		}

		public event EventHandler<EventArgs> StationChanged;

		public string GetReportWeekScheduleTemplatePath(bool landscape)
		{
			return Path.Combine(ProgramManagerRoot, OutputRoot, string.Format("WeekSchedule{0}.xls", landscape ? "Landscape" : "Portrait"));
		}

		public void LoadData()
		{
			foreach (var station in _stations)
				station.ReleaseResources();
			_stations.Clear();
			if (!HasStations) return;
			var rootFolder = new DirectoryInfo(StationsFolderPath);
			foreach (var stationFolder in rootFolder.GetDirectories())
			{
				var station = new Station(stationFolder);
				_stations.Add(station);
			}
		}

		public string[] GetStationList()
		{
			return _stations.Select(x => x.Name).ToArray();
		}

		public string[] GetProgramList()
		{
			if (SelectedStation != null)
				return SelectedStation.ProgramNames.ToArray();
			return new string[] { };
		}

		public void LoadStation(object sender, string selectedStationName)
		{
			SelectedStation = _stations.FirstOrDefault(x => x.Name.Equals(selectedStationName));
			if (SelectedStation != null)
			{
				if (StationChanged != null)
					StationChanged(sender, new EventArgs());
			}
		}

		public void LoadDay(DateTime day)
		{
			if (SelectedStation != null)
				SelectedDay = SelectedStation.GetDay(day);
		}

		public void SaveDay()
		{
			if (SelectedDay != null)
				SelectedDay.Save();
		}

		public void ReportWeekSchedule(string stationName, Week[] weeks, bool convertToPDF, bool landscape)
		{
			using (var form = new FormProgress())
			{
				var thread = new Thread(delegate()
				{
					FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						form.laProgress.Text = "Generating Program Schedule...";
						form.TopMost = true;
						form.Show();
						Application.DoEvents();
					});

					var station = _stations.FirstOrDefault(x => x.Name.Equals(stationName));
					if (station != null)
					{
						if (!Directory.Exists(SettingsManager.Instance.OutputCache))
							Directory.CreateDirectory(SettingsManager.Instance.OutputCache);

						string destinationPath = Path.Combine(SettingsManager.Instance.OutputCache, string.Format("{0}.{1}", new[] { DateTime.Now.ToString("MMddyy-hhmmtt"), convertToPDF ? "pdf" : "xls" }));

						var daysByWeeks = new List<Day[]>();
						foreach (Week week in weeks)
							daysByWeeks.Add(station.GetDays(week.DateStart, week.DateEnd));

						ExcelHelper.Instance.ReportWeekSchedule(GetReportWeekScheduleTemplatePath(landscape), daysByWeeks.ToArray(), destinationPath, convertToPDF, landscape);

						if (File.Exists(destinationPath))
							Process.Start(destinationPath);
					}

					FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						form.Hide();
						Application.DoEvents();
					});
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();

				form.Close();
			}
		}

		public void ReportActivityList(ProgramActivity[] activities, bool convertToPDF)
		{
			using (var form = new FormProgress())
			{
				var thread = new Thread(delegate()
				{
					FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						form.laProgress.Text = "Generating Program List...";
						form.TopMost = true;
						form.Show();
						Application.DoEvents();
					});

					if (!Directory.Exists(SettingsManager.Instance.OutputCache))
						Directory.CreateDirectory(SettingsManager.Instance.OutputCache);

					string destinationPath = Path.Combine(SettingsManager.Instance.OutputCache, string.Format("{0}.{1}", new[] { DateTime.Now.ToString("MMddyy-hhmmtt"), convertToPDF ? "pdf" : "xls" }));

					ExcelHelper.Instance.ReportActivityList(ReporActivityListTemplatePath, activities, destinationPath, convertToPDF);

					if (File.Exists(destinationPath))
						Process.Start(destinationPath);

					FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						form.Hide();
						Application.DoEvents();
					});
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();

				form.Close();
			}
		}
	}
}