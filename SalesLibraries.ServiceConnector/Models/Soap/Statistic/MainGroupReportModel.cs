namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class MainGroupReportModel
	{
		public int AllLogins { get; set; }
		public int AllDocs { get; set; }
		public int AllVideos { get; set; }
		public int AllTotals { get; set; }

		public double LoginsPercent => AllLogins > 0 ? (double)logins / AllLogins : 0;

		public double DocsPercent => AllDocs > 0 ? (double)docs / AllDocs : 0;

		public double VideosPercent => AllVideos > 0 ? (double)videos / AllVideos : 0;

		public double TotalPercent => AllTotals > 0 ? (double)totals / AllTotals : 0;
	}
}
