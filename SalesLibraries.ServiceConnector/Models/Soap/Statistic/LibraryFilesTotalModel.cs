using System;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public class LibraryFilesTotalModel
	{
		public string Name { get; set; }
		public int VideoCount { get; set; }
		public int FilesCount { get; set; }
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
