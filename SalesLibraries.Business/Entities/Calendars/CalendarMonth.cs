using System;
using System.Collections.Generic;

namespace SalesLibraries.Business.Entities.Calendars
{
	public class CalendarMonth
	{
		public CalendarMonth(CalendarYear parent)
		{
			Parent = parent;
			Days = new List<CalendarDay>();
		}

		public CalendarYear Parent { get; private set; }
		public DateTime MonthFirstDay { get; set; }
		public List<CalendarDay> Days { get; private set; }

		public string Name => MonthFirstDay.ToString("MMMM").ToUpper();

		public void LoadDays()
		{
			Days.Clear();

			DateTime startDay = MonthFirstDay;
			while (startDay.DayOfWeek != DayOfWeek.Monday)
				startDay = startDay.AddDays(-1);

			DateTime lastDay = MonthFirstDay.AddMonths(1).AddDays(-1);
			while (lastDay.DayOfWeek != DayOfWeek.Sunday)
				lastDay = lastDay.AddDays(-1);

			while (startDay <= lastDay)
			{
				var day = new CalendarDay(this);
				day.Date = startDay;
				day.IsSweepDay = Parent.SweepDays.Contains(day.Date);
				Days.Add(day);

				startDay = startDay.AddDays(1);
			}
		}
	}
}
