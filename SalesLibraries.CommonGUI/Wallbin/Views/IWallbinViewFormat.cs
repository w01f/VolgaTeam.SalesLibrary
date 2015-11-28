using System;

namespace SalesLibraries.CommonGUI.Wallbin.Views
{
	public interface IWallbinViewFormat
	{
		int FontSize { get; }
		int RowSpace { get; }
		bool AllowEdit { get; }
		bool AllowMultiSelect { get; }
		bool ShowCategoryTags { get; }
		bool ShowSuperFilterTags { get; }
		bool ShowKeywordTags { get; }
		bool ShowSecurityTags { get; }

		event EventHandler<EventArgs> StateChanged;
		void Update();
	}
}
