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

		public DateTime? LinkAddDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(linkAddDate, out temp))
					return temp;
				return null;
			}
		}

		public DateTime? LinkModifyDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(linkModifyDate, out temp))
					return temp;
				return null;
			}
		}

		public bool HasCategories => !String.IsNullOrEmpty(categories);

		public bool HasKeywords => !String.IsNullOrEmpty(keywords);

		public string Details => String.Format("{0}{2}{2}{1}",
			HasCategories ? String.Format("Tags: {0}", categories) : String.Empty,
			HasKeywords ? String.Format("Keywords: {0}", String.Join(", ", keywords.Split(' '))) : String.Empty,
			HasCategories && HasKeywords ? Environment.NewLine : String.Empty
			);

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
