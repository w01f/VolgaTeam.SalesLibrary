using System.Text;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class AccessReportModel
	{
		public string Groups { get; set; }
		public int AllActive { get; set; }
		public int AllInactive { get; set; }
		public int AllUsers { get; set; }

		public double ActivePercent
		{
			get { return userCount > 0 ? (double)activeCount / userCount : 0; }
		}

		public double InactivePercent
		{
			get { return userCount > 0 ? (double)inactiveCount / userCount : 0; }
		}

		public double AllActivePercent
		{
			get { return AllUsers > 0 ? (double)AllActive / AllUsers : 0; }
		}

		public double AllInactivePercent
		{
			get { return AllUsers > 0 ? (double)AllInactive / AllUsers : 0; }
		}

		public string Details
		{
			get
			{
				var result = new StringBuilder();
				if (!string.IsNullOrEmpty(activeNames))
					result.AppendLine("Active Users - " + activeNames);
				if (!string.IsNullOrEmpty(inactiveNames))
				{
					result.AppendLine();
					result.AppendLine("Inactive Users - " + inactiveNames);
				}
				return result.ToString();
			}
		}
	}
}
