namespace SalesDepot.Services.StatisticService
{
	public partial class MainUserReportModel
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string GroupName
		{
			get { return !string.IsNullOrEmpty(group) ? string.Format("{0} ({1} {2})", group, groupUserCount, (groupUserCount > 1 ? "users" : "user")) : null; }
		}

		public double LoginsPercent
		{
			get { return groupLogins > 0 ? (double)userLogins / groupLogins : 0; }
		}

		public double DocsPercent
		{
			get { return groupDocs > 0 ? (double)userDocs / groupDocs : 0; }
		}

		public double VideosPercent
		{
			get { return groupVideos > 0 ? (double)userVideos / groupVideos : 0; }
		}

		public double TotalPercent
		{
			get { return groupTotal > 0 ? (double)userTotal / groupTotal : 0; }
		}
	}
}
