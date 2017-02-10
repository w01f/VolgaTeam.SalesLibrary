using System.Collections.Generic;
using System.Drawing;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IThumbnailSettingsHolder
	{
		string Name { get; }
		bool ShowSourceFilesList { get; }
		Color ThumbnailBackColor { get; }
		ThumbnailSettings Thumbnail { get; set; }
		IList<string> GetThumbnailSourceFiles();
	}
}
