
using System;
using System.Text;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class FileActivityReportModel
	{
		public string FileName => Encoding.UTF8.GetString(Convert.FromBase64String(fileName));

		public bool IsUrl
		{
			get
			{
				Uri uriResult;
				return Uri.TryCreate(FileName, UriKind.Absolute, out uriResult)
					&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
			}
		}
	}
}
