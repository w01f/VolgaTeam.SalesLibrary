using System;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	public class FormatState : IWallbinViewFormat
	{
		#region IWallbinViewFormat
		public int FontSize
		{
			get { return MainController.Instance.Settings.FontSize; }
			set
			{
				MainController.Instance.Settings.FontSize = value;
				MainController.Instance.Settings.Save();
				Update();
			}
		}

		public int RowSpace
		{
			get { return 2; }
		}

		private bool _allowEdit;
		public bool AllowEdit
		{
			get { return _allowEdit; }
			set
			{
				_allowEdit = value;
			}
		}

		private bool _allowMultiSelect;
		public bool AllowMultiSelect
		{
			get { return _allowMultiSelect; }
			set
			{
				_allowMultiSelect = value;
			}
		}

		private bool _showCategoryTags;
		public bool ShowCategoryTags
		{
			get { return _showCategoryTags; }
			set
			{
				_showCategoryTags = value;
			}
		}

		private bool _showSuperFilterTags;
		public bool ShowSuperFilterTags
		{
			get { return _showSuperFilterTags; }
			set
			{
				_showSuperFilterTags = value;
			}
		}

		private bool _showKeywordTags;
		public bool ShowKeywordTags
		{
			get { return _showKeywordTags; }
			set
			{
				_showKeywordTags = value;
			}
		}

		private bool _showSecurityTags;
		public bool ShowSecurityTags
		{
			get { return _showSecurityTags; }
			set
			{
				_showSecurityTags = value;
			}
		}

		public event EventHandler<EventArgs> StateChanged;
		#endregion

		private bool _showFiles;
		public bool ShowFiles
		{
			get { return _showFiles; }
			set
			{
				_showFiles = value;
			}
		}

		private bool _showTagsEditor;
		public bool ShowTagsEditor
		{
			get { return _showTagsEditor; }
			set
			{
				_showTagsEditor = value;
			}
		}

		private bool _showTagsCleaner;
		public bool ShowTagsCleaner
		{
			get { return _showTagsCleaner; }
			set
			{
				_showTagsCleaner = value;
			}
		}

		public FormatState()
		{
			_allowEdit = true;
			_allowMultiSelect = false;
			_showFiles = true;
			_showTagsEditor = false;
			_showCategoryTags = false;
			_showKeywordTags = false;
			_showSuperFilterTags = false;
			_showTagsCleaner = false;
			_showSecurityTags = false;
		}

		public void Update()
		{
			if (StateChanged != null)
				StateChanged(this, EventArgs.Empty);
		}

		public void SwitchAccordingPage(TabPageEnum pageType)
		{
			switch (pageType)
			{
				case TabPageEnum.Tags:
					_allowEdit = false;
					_allowMultiSelect = true;
					_showFiles = false;
					_showTagsEditor = true;
					_showCategoryTags = true;
					_showKeywordTags = false;
					_showSuperFilterTags = false;
					_showTagsCleaner = false;
					_showSecurityTags = false;
					break;
				case TabPageEnum.Security:
					_allowEdit = false;
					_allowMultiSelect = true;
					_showFiles = false;
					_showTagsEditor = true;
					_showCategoryTags = false;
					_showKeywordTags = false;
					_showSuperFilterTags = false;
					_showTagsCleaner = false;
					_showSecurityTags = true;
					break;
				default:
					_allowEdit = true;
					_allowMultiSelect = false;
					_showFiles = true;
					_showTagsEditor = false;
					_showCategoryTags = false;
					_showKeywordTags = false;
					_showSuperFilterTags = false;
					_showTagsCleaner = false;
					_showSecurityTags = false;
					break;
			}
			Update();
		}

		public void ShowCategories()
		{
			_showCategoryTags = true;
			_showKeywordTags = false;
			_showSuperFilterTags = false;
			_showTagsCleaner = false;
			Update();
		}

		public void ShowKeywords()
		{
			_showCategoryTags = false;
			_showKeywordTags = true;
			_showSuperFilterTags = false;
			_showTagsCleaner = false;
			Update();
		}

		public void ShowSuperFilters()
		{
			_showCategoryTags = false;
			_showKeywordTags = false;
			_showSuperFilterTags = true;
			_showTagsCleaner = false;
			Update();
		}

		public void ShowCleaner()
		{
			_showCategoryTags = false;
			_showKeywordTags = false;
			_showSuperFilterTags = false;
			_showTagsCleaner = true;
			Update();
		}
	}
}
