using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SalesLibraries.Business.Entities.Calendars
{
	public class CalendarYear
	{
		private const string SweepPeriodsFileName = @"SweepPeriods.xml";

		private readonly FileSystemWatcher _libraryStorageWatcher = new FileSystemWatcher();

		public CalendarYear(CalendarPart parent)
		{
			Parent = parent;
			Months = new List<CalendarMonth>();
			SweepDays = new List<DateTime>();
		}

		public CalendarPart Parent { get; private set; }
		public string RootFolderPath { get; set; }
		public int Year { get; set; }
		public List<CalendarMonth> Months { get; private set; }
		public List<DateTime> SweepDays { get; private set; }

		public void LoadMonths()
		{
			Months.Clear();
			for (int i = 1; i <= 12; i++)
			{
				var month = new CalendarMonth(this);
				month.MonthFirstDay = new DateTime(Year, i, 1);
				month.LoadDays();
				Months.Add(month);
			}

			_libraryStorageWatcher.Path = RootFolderPath;
			_libraryStorageWatcher.Created += (sender, e) =>
			{
				if (!Parent.Files.Select(x => x.FullName).Contains(e.FullPath))
					Parent.Files.Add(new FileInfo(e.FullPath));
			};
			_libraryStorageWatcher.EnableRaisingEvents = true;
		}

		public void LoadSweepPeriods()
		{
			SweepDays.Clear();
			if (!Directory.Exists(RootFolderPath)) return;
			var sweepPeriodsConnfigFile = Path.Combine(RootFolderPath, SweepPeriodsFileName);
			if (!File.Exists(sweepPeriodsConnfigFile)) return;
			try
			{
				var document = new XmlDocument();
				document.Load(sweepPeriodsConnfigFile);
				var node = document.SelectSingleNode(@"/SweepPeriods");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					if (!childNode.Name.Equals("SweepPeriod")) continue;
					var dateBegin = DateTime.MinValue;
					var dateEnd = DateTime.MinValue;
					foreach (XmlAttribute attribute in childNode.Attributes)
					{
						switch (attribute.Name)
						{
							case "DateBegin":
								DateTime.TryParse(attribute.Value, out dateBegin);
								break;
							case "DateEnd":
								DateTime.TryParse(attribute.Value, out dateEnd);
								break;
						}
					}
					if (dateBegin.Equals(DateTime.MinValue) || dateEnd.Equals(DateTime.MinValue) || dateBegin >= dateEnd) continue;
					while (dateBegin <= dateEnd)
					{
						SweepDays.Add(dateBegin);
						dateBegin = dateBegin.AddDays(1);
					}
				}
			}
			catch { }
		}
	}
}
