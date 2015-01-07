using System;

namespace SalesDepot.Services.StatisticService
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
