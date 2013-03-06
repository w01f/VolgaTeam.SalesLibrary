namespace SalesDepot.Services.StatisticService
{
	public partial class NavigationGroupReportRecord
	{
		public int AllLibraries { get; set; }
		public int AllPages { get; set; }
		public int AllTotals { get; set; }

		public double LibrariesPercent
		{
			get { return AllLibraries > 0 ? (double)libs / AllLibraries : 0; }
		}

		public double PagesPercent
		{
			get { return AllPages > 0 ? (double)pages / AllPages : 0; }
		}

		public double TotalPercent
		{
			get { return AllTotals > 0 ? (double)totals / AllTotals : 0; }
		}
	}
}
