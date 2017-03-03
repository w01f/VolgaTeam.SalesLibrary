using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(BaseLibraryLink))]
	//public partial class TagsOptions : UserControl, ILinkSetSettingsEditControl
	public partial class TagsOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private bool _loading;
		private readonly List<BaseLibraryLink> _links = new List<BaseLibraryLink>();

		private readonly List<SearchGroupContainer> _searchGroups = new List<SearchGroupContainer>();
		private readonly List<KeywordModel> _keywords = new List<KeywordModel>();
		private TreeListNode _rootNode;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Tags };
		public int Order => 0;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo
		{
			Title = String.Format("{0}Max Tags allowed: <b><u>{1}</u></b>",
						_links.Count == 1 ? String.Empty : String.Format("Links: <b><u>{0}</u></b>    ", _links.Count),
						MainController.Instance.Lists.SearchTags.MaxTags)
		};

		public event EventHandler<EventArgs> ForceCloseRequested;

		private TagsOptions()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				buttonXAddKeyWord.Font = new Font(buttonXAddKeyWord.Font.FontFamily, buttonXAddKeyWord.Font.Size - 2, buttonXAddKeyWord.Font.Style);
			}
			repositoryItemButtonEditKeywordShared.Enter += EditorHelper.EditorEnter;
			repositoryItemButtonEditKeywordShared.MouseUp += EditorHelper.EditorMouseUp;
			repositoryItemButtonEditKeywordShared.MouseDown += EditorHelper.EditorMouseUp;
		}

		public TagsOptions(BaseLibraryLink link) : this()
		{
			_links.Add(link);
		}

		public TagsOptions(ILinksGroup linkGroup, FileTypes? defaultLinkType = null) : this()
		{
			_links.AddRange(linkGroup.AllLinks.OfType<LibraryObjectLink>().Where(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value));
		}

		public void LoadData()
		{
			_loading = true;

			UpdateCategoriesDataSource();
			UpdateKeywordsDataSource();

			_loading = false;
		}

		public void SaveData()
		{
			SaveCategoriesDataSource();
			SaveKeywordsDataSource();
		}

		#region Categories Processing
		private void UpdateCategoriesDataSource()
		{
			splitContainerSearchTagsCategories.Panel2.Controls.Clear();

			LoadTreeView();

			var commonCategories = _links.GetCommonCategories();
			foreach (var link in _links)
			{
				foreach (var searchGroup in _searchGroups)
				{
					var linkGroup = link.Tags.Categories.FirstOrDefault(sg => sg.Equals(searchGroup.DataSource));
					if (linkGroup != null)
						foreach (var item in searchGroup.ListBox.Items
							.Where(item => linkGroup.Tags
								.Any(t => t.Equals((SearchTag)item.Value))))
							item.CheckState = CheckState.Indeterminate;

					var commonGroup = commonCategories.FirstOrDefault(sg => sg.Equals(searchGroup.DataSource));
					if (commonGroup != null)
						foreach (var item in searchGroup.ListBox.Items
							.Where(item => commonGroup.Tags
								.Any(t => t.Equals((SearchTag)item.Value))))
							item.CheckState = CheckState.Checked;
				}
			}

			var firstSuperGroupNode = _rootNode.Nodes.FirstOrDefault();
			if (firstSuperGroupNode != null)
			{
				firstSuperGroupNode.Expanded = false;
				var firstGroupNode = firstSuperGroupNode.Nodes.FirstOrDefault();
				if (firstGroupNode != null)
				{
					treeListCategories.FocusedNode = firstGroupNode;
					firstGroupNode.Selected = true;
				}
			}

			UpdateCategoriesHeader();
		}

		private void SaveCategoriesDataSource()
		{
			var commonCategories = _searchGroups
				.Select(sg =>
					{
						var searchGroup = new SearchGroup { Name = sg.DataSource.Name };
						searchGroup.Tags.AddRange(sg.ListBox.Items
							.Where(item => item.CheckState == CheckState.Checked)
							.Select(item =>
							{
								var sourceTag = (SearchTag)item.Value;
								var searchTag = new SearchTag { Name = sourceTag.Name };
								return searchTag;
							}));
						return searchGroup;
					})
				.Where(searchGroup => searchGroup.Tags.Any())
				.ToArray();
			var partialCategories = _searchGroups
				.Select(sg =>
				{
					var searchGroup = new SearchGroup { Name = sg.DataSource.Name };
					searchGroup.Tags.AddRange(sg.ListBox.Items
						.Where(item => item.CheckState == CheckState.Indeterminate)
						.Select(item =>
						{
							var sourceTag = (SearchTag)item.Value;
							var searchTag = new SearchTag { Name = sourceTag.Name };
							return searchTag;
						}));
					return searchGroup;
				})
				.Where(searchGroup => searchGroup.Tags.Any())
				.ToArray();
			_links.ApplyCategories(commonCategories, partialCategories);
		}

		private void UpdateCategoriesHeader()
		{
			labelControlSearchTagsCategoriesHeader.Text = String.Format("<color=gray><i>Tags added: {0}</i></color>",
				String.Join(", ",
					_searchGroups
						.SelectMany(g => g.ListBox.Items)
						.Where(item => item.CheckState == CheckState.Checked)
						.Select(item => item.Value)
						.OfType<SearchTag>()
						.Select(t => t.Name)
						));
		}

		private void LoadTreeView()
		{
			treeListCategories.Nodes.Clear();
			_searchGroups.Clear();

			_rootNode = treeListCategories.AppendNode(new object[] { "Search Engine Tags" }, null);
			_rootNode.StateImageIndex = 0;

			foreach (var searchSuperGroup in MainController.Instance.Lists.SearchTags.SearchSuperGroups)
			{
				var superGroupNode = treeListCategories.AppendNode(new object[] { searchSuperGroup.Name }, _rootNode);
				superGroupNode.Tag = searchSuperGroup;
				superGroupNode.StateImageIndex = 0;

				foreach (var searchGroup in searchSuperGroup.Groups)
				{
					var groupNode = treeListCategories.AppendNode(new object[] { searchGroup.Name }, superGroupNode);
					var searchGroupContainer = new SearchGroupContainer(searchGroup);
					searchGroupContainer.ListBox.UnCheckAll();
					searchGroupContainer.ListBox.ItemChecking += OnCategoriesListBoxItemChecking;
					searchGroupContainer.ListBox.ItemCheck += (o, ea) => UpdateCategoriesHeader();
					splitContainerSearchTagsCategories.Panel2.Controls.Add(searchGroupContainer.ListBox);
					_searchGroups.Add(searchGroupContainer);
					groupNode.Tag = searchGroupContainer;
					groupNode.StateImageIndex = 0;
				}
			}
			_rootNode.Expanded = true;
		}

		private void OnCategoriesListBoxItemChecking(object sender, ItemCheckingEventArgs e)
		{
			if (_loading || MainController.Instance.Lists.SearchTags.MaxTags <= 0 || e.NewValue != CheckState.Checked) return;
			var totalTags = _searchGroups.SelectMany(g => g.ListBox.Items)
				.Count(item => item.CheckState != CheckState.Unchecked);
			if (totalTags < MainController.Instance.Lists.SearchTags.MaxTags) return;
			MainController.Instance.PopupMessages.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED",
				MainController.Instance.Lists.SearchTags.MaxTags));
			e.Cancel = true;
		}

		private void OnWipeTagsClick(object sender, EventArgs e)
		{
			foreach (var searchGroup in _searchGroups)
				searchGroup.ListBox.UnCheckAll();
		}

		private void OnCategoriesAfterCollapse(object sender, NodeEventArgs e)
		{
			e.Node.StateImageIndex = 0;
		}

		private void OnCategoriesAfterExpand(object sender, NodeEventArgs e)
		{
			e.Node.StateImageIndex = 1;
		}

		private void OnCategoriesBeforeCollapse(object sender, BeforeCollapseEventArgs e)
		{
			e.CanCollapse = e.Node.Tag is SearchSuperGroup;
		}

		private void OnCategoriesBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
		{
			if (e.Node == _rootNode)
				e.CanFocus = false;
			else if (e.Node?.Tag is SearchSuperGroup)
				e.CanFocus = false;
		}

		private void OnCategoriesFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			var searchGroup = e.Node?.Tag as SearchGroupContainer;
			searchGroup?.ListBox.BringToFront();
		}
		#endregion

		#region Keyword processing
		private void UpdateKeywordsDataSource()
		{
			var commonKeywords = _links.GetCommonKeywords().ToList();
			_keywords.AddRange(commonKeywords.Select(k => new KeywordModel { Name = k.Name, IsShared = true }));
			foreach (var link in _links)
			{
				_keywords.AddRange(link.Tags.Keywords
					.Where(k => !commonKeywords.Any(commonKeyword => commonKeyword.Equals(k)))
						.Select(k => new KeywordModel { Name = k.Name, IsShared = false }));
			}
			gridControlSearchTagsKeywords.DataSource = _keywords;
			gridViewSearchTagsKeywords.RefreshData();
		}

		private void SaveKeywordsDataSource()
		{
			gridViewSearchTagsKeywords.CloseEditor();
			_keywords.RemoveAll(tag => String.IsNullOrEmpty(tag.Name));
			_links.ApplyKeywords(_keywords.ToArray());
		}

		private void OnKeywordsEditorButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewSearchTagsKeywords.CloseEditor();
			if (gridViewSearchTagsKeywords.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var keyword = (KeywordModel)gridViewSearchTagsKeywords.GetFocusedRow();
			switch (e.Button.Tag as String)
			{
				case "Delete":
					_keywords.Remove(keyword);
					break;
				case "MakeShared":
					keyword.IsShared = true;
					break;
			}
			gridViewSearchTagsKeywords.RefreshData();
		}

		private void OnAddKeyWordClick(object sender, EventArgs e)
		{
			gridViewSearchTagsKeywords.CloseEditor();
			_keywords.Add(new KeywordModel { IsShared = true });
			gridViewSearchTagsKeywords.RefreshData();
			if (gridViewSearchTagsKeywords.RowCount <= 0) return;
			gridViewSearchTagsKeywords.FocusedRowHandle = gridViewSearchTagsKeywords.RowCount - 1;
			gridViewSearchTagsKeywords.MakeRowVisible(gridViewSearchTagsKeywords.FocusedRowHandle, true);
		}

		private void OnKeywordsGridCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			var keyword = (KeywordModel)gridViewSearchTagsKeywords.GetRow(e.RowHandle);
			e.RepositoryItem = keyword.IsShared ?
				repositoryItemButtonEditKeywordShared :
				repositoryItemButtonEditKeywordPartial;
		}

		private void OnKeywordsGridRowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var keyword = (KeywordModel)gridViewSearchTagsKeywords.GetRow(e.RowHandle);
			e.Appearance.ForeColor = keyword.IsShared ?
				Color.Black :
				Color.Gray;
		}

		#endregion
	}
}
