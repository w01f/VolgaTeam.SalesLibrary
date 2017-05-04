using System;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public class LibraryFilesTotalModel
	{
		public string Name { get; set; }
		public int VideoTotalCount { get; set; }
		public int VideoTaggedCount { get; set; }
		public int FilesTotalCount { get; set; }
		public int FilesTaggedCount { get; set; }
		public DateTime? LibraryDate { get; set; }
		public int? DaysFormLastUpdate
		{
			get
			{
				if (LibraryDate.HasValue)
					return (DateTime.Today - LibraryDate.Value).Days;
				return null;
			}
		}
	}
}
