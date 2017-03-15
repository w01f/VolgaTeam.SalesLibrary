using System;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	class GroupSettingsManager
	{
		private readonly CategoriesEditor _categoriesEditor;
		private readonly KeywordsEditor _keywordsEditor;
		private readonly SuperFiltersEditor _superFiltersEditor;
		private readonly SecurityEditor _securityEditor;

		public IGroupSettingsEditor ActiveEditor { get; private set; }

		public event EventHandler<EventArgs> ChangesMade;

		public GroupSettingsManager()
		{
			MainController.Instance.WallbinViews.Selection.SelectionChanged += OnLinkSelectionChanged;

			_categoriesEditor = new CategoriesEditor();
			_categoriesEditor.EditorChanged += OnEditorChanged;
			_keywordsEditor = new KeywordsEditor();
			_keywordsEditor.EditorChanged += OnEditorChanged;
			_superFiltersEditor = new SuperFiltersEditor();
			_superFiltersEditor.EditorChanged += OnEditorChanged;
			_securityEditor = new SecurityEditor();
			_securityEditor.EditorChanged += OnEditorChanged;
		}

		private void OnEditorChanged(object sender, EventArgs e)
		{
			ChangesMade?.Invoke(sender, e);
		}

		private void OnLinkSelectionChanged(object sender, SelectionEventArgs e)
		{
			switch (e.SelectionType)
			{
				case SelectionEventType.SelectionReset:
					ActiveEditor?.UpdateData();
					break;
				case SelectionEventType.LinkSelected:
					ActiveEditor?.UpdateData();
					break;
			}
		}

		public void SwitchEditor()
		{
			ActiveEditor = null;
			if (MainController.Instance.WallbinViews.FormatState.ShowCategoryTags)
				ActiveEditor = _categoriesEditor;
			else if (MainController.Instance.WallbinViews.FormatState.ShowKeywordTags)
				ActiveEditor = _keywordsEditor;
			else if (MainController.Instance.WallbinViews.FormatState.ShowSuperFilterTags)
				ActiveEditor = _superFiltersEditor;
			else if (MainController.Instance.WallbinViews.FormatState.ShowSecurityTags)
				ActiveEditor = _securityEditor;
			ActiveEditor?.UpdateData();
		}
	}
}
