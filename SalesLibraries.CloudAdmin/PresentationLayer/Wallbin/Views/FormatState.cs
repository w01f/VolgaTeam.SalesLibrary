using System;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
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

		public int RowSpace => 2;

		private bool _allowEdit;
		public bool AllowEdit
		{
			get { return _allowEdit; }
			set
			{
				_allowEdit = value;
			}
		}

		private bool _showSelectedFolder;
		public bool ShowSelectedFolder
		{
			get { return _showSelectedFolder; }
			set
			{
				_showSelectedFolder = value;
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

		private bool _showLinkBundles;
		public bool ShowLinkBundles
		{
			get { return _showLinkBundles; }
			set
			{
				_showLinkBundles = value;
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

		public FormatState()
		{
			_allowEdit = true;
			_showFiles = true;
			_showLinkBundles = false;
			_showTagsEditor = false;
			_showCategoryTags = false;
			_showKeywordTags = false;
			_showSuperFilterTags = false;
			_showSecurityTags = false;

		}

		public void Update()
		{
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SwitchAccordingPage(TabPageEnum pageType)
		{
			switch (pageType)
			{
				case TabPageEnum.Tags:
					_allowEdit = false;
					_showSelectedFolder = false;
					_showFiles = false;
					_showLinkBundles = false;
					_showTagsEditor = true;
					_showCategoryTags = true;
					_showKeywordTags = false;
					_showSuperFilterTags = false;
					_showSecurityTags = false;
					break;
				case TabPageEnum.Security:
					_allowEdit = false;
					_showSelectedFolder = false;
					_showFiles = false;
					_showLinkBundles = false;
					_showTagsEditor = true;
					_showCategoryTags = false;
					_showKeywordTags = false;
					_showSuperFilterTags = false;
					_showSecurityTags = true;
					break;
				case TabPageEnum.Bundles:
					_allowEdit = true;
					_showSelectedFolder = false;
					_showFiles = false;
					_showLinkBundles = true;
					_showTagsEditor = false;
					_showCategoryTags = false;
					_showKeywordTags = false;
					_showSuperFilterTags = false;
					_showSecurityTags = false;
					break;
				default:
					_allowEdit = true;
					_showSelectedFolder = true;
					_showFiles = true;
					_showLinkBundles = false;
					_showTagsEditor = false;
					_showCategoryTags = false;
					_showKeywordTags = false;
					_showSuperFilterTags = false;
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
			Update();
		}

		public void ShowKeywords()
		{
			_showCategoryTags = false;
			_showKeywordTags = true;
			_showSuperFilterTags = false;
			Update();
		}

		public void ShowSuperFilters()
		{
			_showCategoryTags = false;
			_showKeywordTags = false;
			_showSuperFilterTags = true;
			Update();
		}
	}
}
