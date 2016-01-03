using System;

namespace SalesLibraries.ServiceConnector.LinkConfigProfileService
{
	public partial class LibraryLinkReference
	{
		public bool Selected { get; set; }

		public string LinkFileName
		{
			get
			{
				if (!String.IsNullOrEmpty(fileName))
					return fileName;
				return filePath;
			}
		}
	}
}
