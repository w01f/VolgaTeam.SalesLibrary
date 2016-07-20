using System;
using System.Linq;
using System.Text;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class AccessReportModel
	{
		public string GroupHeader { get; set; }
		public double? AllActive { get; set; }
		public double? AllInactive { get; set; }
		public int? AllUsers { get; set; }

		public double ActivePercent => userCount > 0 ? (double)activeCount / userCount : 0;

		public double InactivePercent => userCount > 0 ? (double)inactiveCount / userCount : 0;

		public double? AllActivePercent => AllUsers > 0 ? AllActive / AllUsers : null;

		public double? AllInactivePercent => AllUsers > 0 ? AllInactive / AllUsers : null;

		public string AllGroups
		{
			get
			{
				if (!String.IsNullOrEmpty(GroupHeader))
					return String.Format("{0}{2}{2}{1}",
						AllUsers,
						GroupHeader,
						Environment.NewLine);
				return null;
			}
		}

		public string ActiveNames
		{
			get
			{
				if (!String.IsNullOrEmpty(activeNames))
					return String.Format("{0}{2}{2}{1}",
						activeCount,
						String.Join(Environment.NewLine, activeNames
							.Split(',')
							.Select(item => item.Trim())
							.OrderBy(item=>item)),
						Environment.NewLine);
				return null;
			}
		}

		public string InactiveNames
		{
			get
			{
				if (!String.IsNullOrEmpty(inactiveNames))
					return String.Format("{0}{2}{2}{1}",
						inactiveCount,
						String.Join(Environment.NewLine, inactiveNames
							.Split(',')
							.Select(item => item.Trim())
							.OrderBy(item => item)),
						Environment.NewLine);
				return null;
			}
		}
	}
}
