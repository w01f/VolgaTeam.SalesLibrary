using System;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class LibraryFilesModel
	{
		public DateTime? LibraryDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(libraryDate, out temp))
					return temp;
				return null;
			}
		}

		public DateTime? FileDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(fileDate, out temp))
					return temp;
				return null;
			}
		}
	}
}
