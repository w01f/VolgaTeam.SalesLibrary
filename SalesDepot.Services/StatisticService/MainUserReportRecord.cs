namespace SalesDepot.Services.StatisticService
{
	public partial class MainUserReportRecord
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}
	}
}
