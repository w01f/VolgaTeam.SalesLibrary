using System;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class VideoLinkInfo
	{
		public DateTime? DateModify
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(linkDate, out temp))
					return temp;
				return null;
			}
		}
	}
}
