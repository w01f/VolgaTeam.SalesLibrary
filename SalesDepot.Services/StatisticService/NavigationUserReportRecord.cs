namespace SalesDepot.Services.StatisticService
{
	public partial class NavigationUserReportRecord
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}
	}
}
