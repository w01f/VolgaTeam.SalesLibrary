using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SalesLibraries.Business.Entities.Calendars
{
	public class CalendarPart
	{
		private const string CalendarPartConfigFileName = @"config.xml";

		public CalendarContainer Parent { get; private set; }
		public bool Configured { get; private set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public string PartFolderPath { get; private set; }
		public List<CalendarYear> Years { get; private set; }
		public List<FileInfo> Files { get; private set; }

		public CalendarPart(CalendarContainer parent, string partFolderPath)
		{
			Parent = parent;
			PartFolderPath = partFolderPath;
			Years = new List<CalendarYear>();
			Files = new List<FileInfo>();
		}

		public void Load()
		{
			Configured = false;
			Years.Clear();
			Files.Clear();

			if (!Directory.Exists(PartFolderPath)) return;

			var configFile = Path.Combine(PartFolderPath, CalendarPartConfigFileName);
			if (!File.Exists(configFile)) return;

			try
			{
				var document = new XmlDocument();
				document.Load(configFile);
				var node = document.SelectSingleNode(@"/Config/Name");
				if (node != null)
				{
					Name = node.InnerText;
					Configured = true;
				}
				node = document.SelectSingleNode(@"/Config/Enabled");
				if (node != null)
				{
					bool temp;
					if (Boolean.TryParse(node.InnerText, out temp))
						Enabled = temp;
				}
			}
			catch { }

			if (!Configured || !Enabled) return;

			foreach (var yearFolderPath in Directory.GetDirectories(PartFolderPath))
			{
				int temp;
				if (!int.TryParse(Path.GetFileName(yearFolderPath), out temp)) continue;
				var year = new CalendarYear(this);
				year.RootFolderPath = yearFolderPath;
				year.Year = temp;
				Files.AddRange(Directory.GetFiles(year.RootFolderPath).Select(filePath => new FileInfo(filePath)));
				Years.Add(year);
			}
			foreach (var year in Years)
			{
				year.LoadSweepPeriods();
				year.LoadMonths();
			}
		}
	}
}
