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
					case LinkType.PowerPoint:
						return Properties.Resources.FolderContentPptx;
					case LinkType.Excel:
						return Properties.Resources.FolderContentXlsx;
					case LinkType.Folder:
						return Properties.Resources.FolderContentFolder;
					case LinkType.Video:
						return Properties.Resources.FolderContentVideo;
					case LinkType.Pdf:
						return Properties.Resources.FolderContentPdf;
					case LinkType.Word:
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
