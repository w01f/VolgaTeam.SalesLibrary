namespace SalesDepot.Services.StatisticService
{
	public partial class NavigationUserReportRecord
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string GroupName
		{
			get { return !string.IsNullOrEmpty(group) ? string.Format("{0} ({1} {2})", group, groupUserCount, (groupUserCount > 1 ? "users" : "user")) : null; }
		}

		public double LibrariesPercent
		{
			get { return groupLibraries > 0 ? (double)userLibraries / groupLibraries : 0; }
		}

		public double PagesPercent
		{
			get { return groupPages > 0 ? (double)userPages / groupPages : 0; }
		}

		public double TotalPercent
		{
			get { return groupTotal > 0 ? (double)userTotal / groupTotal : 0; }
		}
	}
}
