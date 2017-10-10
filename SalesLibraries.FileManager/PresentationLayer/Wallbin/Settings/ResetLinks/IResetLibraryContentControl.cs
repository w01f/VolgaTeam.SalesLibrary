using System;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	interface IResetLibraryContentControl
	{
		bool SelectionMade { get; }
		event EventHandler<EventArgs> SelectionChanged;
		void ResetContent(Library library);
	}
}
