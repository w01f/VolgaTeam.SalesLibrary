using System;
using System.Text;

namespace SalesLibraries.ServiceConnector.StatisticService
{
	public partial class FileActivityReportModel
	{
		public string FileName => !String.IsNullOrEmpty(fileName) ? Encoding.UTF8.GetString(Convert.FromBase64String(fileName)) : null;
		public string FileLink => !String.IsNullOrEmpty(fileLink) ? Encoding.UTF8.GetString(Convert.FromBase64String(fileLink)) : null;
		public string FileDetail => !String.IsNullOrEmpty(fileDetail) ? Encoding.UTF8.GetString(Convert.FromBase64String(fileDetail)) : null;

		public bool IsUrl => "qpage".Equals(fileType, StringComparison.OrdinalIgnoreCase) ||
			"secure_link".Equals(fileType, StringComparison.OrdinalIgnoreCase);

		public string Extension => fileExtension?.Replace(".", "").Replace("other", "").ToLower();

		public string ExtensionGroup
		{
			get
			{
				switch (fileExtension?.Replace(".", "").ToLower())
				{
					case "ppt":
					case "pptx":
					case "pptm":
						return "PowerPoint";
					case "doc":
					case "docx":
					case "pdf":
					case "xls":
					case "xlsx":
					case "txt":
					case "key":
						return "Document";
					case "mp4":
					case "mov":
					case "wmv":
					case "avi":
					case "m4v":
					case "mpeg":
					case "mpg":
						return "Video";
					case "png":
					case "jpeg":
					case "jpg":
					case "bmp":
					case "gif":
						return "Graphics";
					case "other":
						return "Other";
					default:
						return String.IsNullOrEmpty(Extension) ? "URL" : "Other";
				}
			}
		}
	}
}
