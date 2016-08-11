using System;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class FileActivityReportModel
	{
		public bool IsUrl
		{
			get
			{
				Uri uriResult;
				return Uri.TryCreate(fileName, UriKind.Absolute, out uriResult)
					&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
			}
		}
	}
}
