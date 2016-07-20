using System.Drawing;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.SalesDepot.Business.LinkViewers
{
	class FilePreviewShortcut
	{
		public LibraryFileLink SourceFileLink { get; private set; }

		public Image Widget
		{
			get
			{
				switch (SourceFileLink.Type)
				{
					case FileTypes.PowerPoint:
						return Properties.Resources.FolderContentPptx;
					case FileTypes.Excel:
						return Properties.Resources.FolderContentXlsx;
					case FileTypes.Folder:
						return Properties.Resources.FolderContentFolder;
					case FileTypes.Video:
						return Properties.Resources.FolderContentVideo;
					case FileTypes.Pdf:
						return Properties.Resources.FolderContentPdf;
					case FileTypes.Word:
						return Properties.Resources.FolderContentDocx;
				}
				return null;
			}
		}

		public string DisplayName => SourceFileLink.Name;

		public FilePreviewShortcut(LibraryFileLink sourceFileLink)
		{
			SourceFileLink = sourceFileLink;
		}
	}
}
