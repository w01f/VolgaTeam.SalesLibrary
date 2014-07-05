namespace SalesDepot.Services.StatisticService
{
	public partial class MainGroupReportModel
	{
		public int AllLogins { get; set; }
		public int AllDocs { get; set; }
		public int AllVideos { get; set; }
		public int AllTotals { get; set; }

		public double LoginsPercent
		{
			get { return AllLogins > 0 ? (double)logins / AllLogins : 0; }
		}

		public double DocsPercent
		{
			get { return AllDocs > 0 ? (double)docs / AllDocs : 0; }
		}

		public double VideosPercent
		{
			get { return AllVideos > 0 ? (double)videos / AllVideos : 0; }
		}

		public double TotalPercent
		{
			get { return AllTotals > 0 ? (double)totals / AllTotals : 0; }
		}
	}
}
