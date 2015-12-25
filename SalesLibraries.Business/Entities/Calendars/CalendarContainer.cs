using System.Collections.Generic;
using System.IO;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.Business.Entities.Calendars
{
	public class CalendarContainer
	{
		public CalendarSettings Settings { get; private set; }

		public List<CalendarPart> Parts { get; private set; }

		protected CalendarContainer(CalendarSettings settings)
		{
			Settings = settings.Clone<CalendarSettings>(null);
			Parts = new List<CalendarPart>();
		}

		public void Load()
		{
			if (!Settings.Enabled || !Directory.Exists(Settings.Path)) return;
			Parts.Clear();
			foreach (var directory in Directory.GetDirectories(Settings.Path))
			{
				var calendarPart = new CalendarPart(this, directory);
				calendarPart.Load();
				if (calendarPart.Configured)
					Parts.Add(calendarPart);
			}
		}

		public void ApplyColorSettings(CalendarSettings settings)
		{
			Settings.CalendarBackColor = settings.CalendarBackColor;
			Settings.CalendarBorderColor = settings.CalendarBorderColor;
			Settings.CalendarHeaderBackColor = settings.CalendarHeaderBackColor;
			Settings.CalendarHeaderForeColor = settings.CalendarHeaderForeColor;
			Settings.DeadLinksForeColor = settings.DeadLinksForeColor;
			Settings.MonthBodyBackColor = settings.MonthBodyBackColor;
			Settings.MonthBodyForeColor = settings.MonthBodyForeColor;
			Settings.MonthHeaderBackColor = settings.MonthHeaderBackColor;
			Settings.MonthHeaderForeColor = settings.MonthHeaderForeColor;
			Settings.SweepBackColor = settings.SweepBackColor;
			Settings.SweepForeColor = settings.SweepForeColor;
		}


		public static CalendarContainer Create(CalendarSettings settings)
		{
			var caledar = new CalendarContainer(settings);
			caledar.Load();
			return caledar;
		}
	}
}
