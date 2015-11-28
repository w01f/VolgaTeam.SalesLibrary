using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProgramManager.CoreObjects;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.Business.ProgramSchedule
{
	public class ProgramScheduleContext
	{
		private const string StationsRoot = "Stations";
		private const string OutputRoot = "Output Templates";

		private readonly string _rootFolderPath;

		public bool Initialized { get; private set; }
		public List<Station> Stations { get; private set; }
		public Station ActiveStation { get; private set; }
		public event EventHandler<EventArgs> StationChanged;

		private string StationsFolderPath
		{
			get { return Path.Combine(_rootFolderPath, Constants.ProgramManagerRootFolderName, StationsRoot); }
		}

		private string ReporActivityListTemplatePath
		{
			get { return Path.Combine(_rootFolderPath, Constants.ProgramManagerRootFolderName, OutputRoot, "ActivityList.xls"); }
		}

		private string OutputFolder
		{
			get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Program Schedules"); }
		}

		public bool HasData
		{
			get { return Directory.Exists(StationsFolderPath) && Directory.GetDirectories(StationsFolderPath).Any(); }
		}

		public ProgramScheduleContext(string rootFolderPath)
		{
			_rootFolderPath = rootFolderPath;
			Stations = new List<Station>();
		}

		public void LoadData()
		{
			if (Initialized) return;

			foreach (var station in Stations)
				station.ReleaseResources();
			Stations.Clear();
			if (!HasData) return;
			var rootFolder = new DirectoryInfo(StationsFolderPath);
			foreach (var stationFolder in rootFolder.GetDirectories())
			{
				var station = new Station(stationFolder);
				Stations.Add(station);
			}

			ActiveStation = Stations.FirstOrDefault(station => station.Name == MainController.Instance.Settings.ProgramScheduleSettings.SelectedStation) ?? Stations.FirstOrDefault();

			Initialized = true;
		}

		public void SetActiveStation(Station station)
		{
			if (ActiveStation == station) return;
			ActiveStation = station;
			if (ActiveStation == null) return;
			MainController.Instance.Settings.ProgramScheduleSettings.SelectedStation = station.Name;
			MainController.Instance.Settings.SaveSettings();
			if (StationChanged != null)
				StationChanged(this, EventArgs.Empty);
		}

		private string GetReportWeekScheduleTemplatePath(bool landscape)
		{
			return Path.Combine(_rootFolderPath, Constants.ProgramManagerRootFolderName, OutputRoot, String.Format("WeekSchedule{0}.xls", landscape ? "Landscape" : "Portrait"));
		}

		public void GenerateWeekScheduleReport(Station station, Week[] weeks, bool convertToPDF, bool landscape)
		{
			if (!Directory.Exists(OutputFolder))
				Directory.CreateDirectory(OutputFolder);
			var destinationPath = Path.Combine(OutputFolder, String.Format("{0}.{1}", DateTime.Now.ToString("MMddyy-hhmmtt"), convertToPDF ? "pdf" : "xls"));
			var daysByWeeks = weeks.Select(week => station.GetDays(week.DateStart, week.DateEnd)).ToList();
			ProgramScheduleReportHelper.GenerateWeekSchedule(
				GetReportWeekScheduleTemplatePath(landscape),
				daysByWeeks.ToArray(),
				destinationPath,
				convertToPDF,
				landscape,
				MainController.Instance.Settings.ProgramScheduleSettings
				);
			if (File.Exists(destinationPath))
				Utils.OpenFile(destinationPath);
		}

		public void GenerateActivityListReport(ProgramActivity[] activities, bool convertToPDF)
		{
			if (!Directory.Exists(OutputFolder))
				Directory.CreateDirectory(OutputFolder);
			var destinationPath = Path.Combine(OutputFolder, String.Format("{0}.{1}", DateTime.Now.ToString("MMddyy-hhmmtt"), convertToPDF ? "pdf" : "xls"));
			ProgramScheduleReportHelper.GenerateActivityList(
				ReporActivityListTemplatePath,
				activities,
				destinationPath,
				convertToPDF);
			if (File.Exists(destinationPath))
				Utils.OpenFile(destinationPath);
		}
	}
}
