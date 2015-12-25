namespace SalesLibraries.CommonGUI.Calendars
{
	public class CalendarYearSelectedEventArgs
	{
		public int Year { get; private set; }

		public CalendarYearSelectedEventArgs(int year)
		{
			Year = year;
		}
	}
}
