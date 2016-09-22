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

		public string Extension => fileType?.Replace(".", "").Replace("other", "").ToLower();

		public string ExtensionGroup
		{
			get
			{
				switch (fileType?.Replace(".", "").ToLower())
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
