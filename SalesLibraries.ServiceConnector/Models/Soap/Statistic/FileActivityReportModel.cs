
using System;
using System.Text;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class FileActivityReportModel
	{
		public string FileName => !String.IsNullOrEmpty(fileName) ? Encoding.UTF8.GetString(Convert.FromBase64String(fileName)) : null;
		public string FileLink => !String.IsNullOrEmpty(fileLink) ? Encoding.UTF8.GetString(Convert.FromBase64String(fileLink)) : null;
		public string FileDetail => !String.IsNullOrEmpty(fileDetail) ? Encoding.UTF8.GetString(Convert.FromBase64String(fileDetail)) : null;

		public bool IsUrl => "qpage".Equals(fileType, StringComparison.OrdinalIgnoreCase);
	}
}
