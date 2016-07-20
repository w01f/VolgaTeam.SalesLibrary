namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class MainUserReportModel
	{
		public string FullName => (firstName + " " + lastName).Trim();

		public string GroupName => !string.IsNullOrEmpty(@group) ? string.Format("{0} ({1} {2})", @group, groupUserCount, (groupUserCount > 1 ? "users" : "user")) : null;

		public double LoginsPercent => groupLogins > 0 ? (double)userLogins / groupLogins : 0;

		public double DocsPercent => groupDocs > 0 ? (double)userDocs / groupDocs : 0;

		public double VideosPercent => groupVideos > 0 ? (double)userVideos / groupVideos : 0;

		public double TotalPercent => groupTotal > 0 ? (double)userTotal / groupTotal : 0;
	}
}
