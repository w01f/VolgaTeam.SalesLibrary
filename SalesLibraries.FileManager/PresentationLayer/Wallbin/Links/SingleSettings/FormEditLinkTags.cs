using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkTags : MetroForm, ILinkSetSettingsEditForm
	{
		private const string HeaderTitleTemplate = "<size=+4>{0}</size>";

		private bool _allowHandleEvents;
		private readonly List<BaseLibraryLink> _links = new List<BaseLibraryLink>();

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Tags,
		};

		public FormEditLinkTags()
		{
			InitializeComponent();

			hyperLinkEditRequestNewCategories.Visible =
				!String.IsNullOrEmpty(MainController.Instance.Settings.CategoryRequestRecipients) &&
				!String.IsNullOrEmpty(MainController.Instance.Settings.CategoryRequestSubject) &&
				!String.IsNullOrEmpty(MainController.Instance.Settings.CategoryRequestBody);

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				buttonXAddKeyWord.Font = new Font(buttonXAddKeyWord.Font.FontFamily, buttonXAddKeyWord.Font.Size - 2, buttonXAddKeyWord.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			}
		}

		public FormEditLinkTags(BaseLibraryLink sourceLink) : this()
		{
			_links.Add(sourceLink);
		}

		public FormEditLinkTags(ILinksGroup linkGroup, FileTypes? defaultLinkType = null) : this()
		{
			_links.AddRange(linkGroup.AllLinks.OfType<LibraryObjectLink>().Where(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value));
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			Width = 960;
			Height = 590;
			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Link-Tags", false, false);
			StartPosition = FormStartPosition.CenterParent;
			LoadData();
		}

		private void LoadData()
		{
			_allowHandleEvents = false;

			UpdateCategoriesDataSource();
			UpdateKeywordsDataSource();

			UpdateTitle();
			UpdateControlPanels();

			_allowHandleEvents = true;
		}

		private void SaveData()
		{
			SaveCategoriesDataSource();
			SaveKeywordsDataSource();
		}

		private void UpdateTitle()
		{
			labelControlTitle.Appearance.Image = xtraTabControl.SelectedTabPage == xtraTabPageCategories
				? Resources.LinkSettingsTagsLogoCategories
				: Resources.LinkSettingsTagsLogoKeywords;
			labelControlTitle.Text = String.Format(HeaderTitleTemplate,
				_links.Count > 1 ?
					String.Format("{0} links", _links.Count) :
					_links.FirstOrDefault()?.ToString());
		}

		private void UpdateControlPanels()
		{
			panelTopControlsCategories.Visible = xtraTabControl.SelectedTabPage == xtraTabPageCategories;
			panelBottomControlsCategories.Visible = xtraTabControl.SelectedTabPage == xtraTabPageCategories;

			panelTopControlsKeywords.Visible = xtraTabControl.SelectedTabPage == xtraTabPageKeywords;
		}

		private void FormEditLinkSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXOK.Focus();
		}

		private void OnSelectedTabPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			UpdateTitle();
			UpdateControlPanels();
		}

		#region Categories

		private TreeListNode _rootNode;
		private readonly List<SearchGroupContainer> _searchGroups = new List<SearchGroupContainer>();

		private void UpdateCategoriesDataSource()
		{
			splitContainerCategories.Panel2.Controls.Clear();

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

			UpdateCategoryInfo();
		}

		private void SaveCategoriesDataSource()
		{
			var commonCategories = _searchGroups
				.Select(sg =>
				{
					var searchGroup = new SearchGroup
					{
						Name = sg.DataSource.Name,
						SuperGroup = sg.DataSource.SuperGroup
					};
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
					var searchGroup = new SearchGroup
					{
						Name = sg.DataSource.Name,
						SuperGroup = sg.DataSource.SuperGroup
					};
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
					searchGroupContainer.ListBox.ItemCheck += (o, ea) =>
					{
						UpdateSuperGroupNodes();
						UpdateCategoryInfo();
					};
					splitContainerCategories.Panel2.Controls.Add(searchGroupContainer.ListBox);
					_searchGroups.Add(searchGroupContainer);
					groupNode.Tag = searchGroupContainer;
					groupNode.StateImageIndex = 0;
				}
			}
			_rootNode.Expanded = true;
		}

		private void UpdateCategoryInfo()
		{
			labelControlCategoriesCount.Text = String.Format("Tags: <b><u>{0}</u></b>",
				_searchGroups
						.SelectMany(g => g.ListBox.Items)
						.Where(item => item.CheckState != CheckState.Unchecked)
						.Select(item => item.Value)
						.OfType<SearchTag>()
						.Count());

			labelControlCategoriesInfo.Text = String.Format("<color=gray><i>{0}</i></color>",
				String.Join("\n",
					_searchGroups
						.SelectMany(g => g.ListBox.Items)
						.Where(item => item.CheckState != CheckState.Unchecked)
						.Select(item => item.Value)
						.OfType<SearchTag>()
						.Select(t => String.Format("<image=LinkSettingsTagsIconCategories.png>  {0}", t.Name))
						));
		}

		private void UpdateSuperGroupNodes()
		{
			foreach (TreeListNode node in _rootNode.Nodes)
			{
				treeListCategories.InvalidateNode(node);
				treeListCategories.RefreshNode(node);
			}
		}

		private void OnCategoriesListBoxItemChecking(object sender, ItemCheckingEventArgs e)
		{
			if (!_allowHandleEvents || MainController.Instance.Lists.SearchTags.MaxTags <= 0 || e.NewValue != CheckState.Checked) return;
			var totalTags = _searchGroups.SelectMany(g => g.ListBox.Items)
				.Count(item => item.CheckState != CheckState.Unchecked);
			if (totalTags < MainController.Instance.Lists.SearchTags.MaxTags) return;
			MainController.Instance.PopupMessages.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED",
				MainController.Instance.Lists.SearchTags.MaxTags));
			e.Cancel = true;
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

		private void OnCategoriesNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (e.Node.Tag is SearchSuperGroup)
			{
				var groupContainer = e.Node.Nodes
					.Select(n => n.Tag)
					.OfType<SearchGroupContainer>()
					.ToList();
				if (groupContainer.Any(g => g.ListBox.Items.Any(item => item.CheckState != CheckState.Unchecked)))
				{
					e.Appearance.ForeColor = Color.Green;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Bold);
				}
				else
				{
					e.Appearance.ForeColor = Color.Black;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Regular);
				}
			}
		}

		private void OnWipeCategoriesClick(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Do you want to wipe all category tags?") ==
				DialogResult.Yes)
			{
				foreach (var searchGroup in _searchGroups)
					searchGroup.ListBox.UnCheckAll();
			}
		}

		private void OnRequestNewCategoriesOpenLink(object sender, OpenLinkEventArgs e)
		{
			try
			{
				Process.Start(String.Format("mailto:{0}?subject={1}&body={2}",
					MainController.Instance.Settings.CategoryRequestRecipients,
					MainController.Instance.Settings.CategoryRequestSubject,
					MainController.Instance.Settings.CategoryRequestBody));
			}
			catch { }
			e.Handled = true;
		}
		#endregion

		#region Keywords

		private readonly List<KeywordModel> _keywords = new List<KeywordModel>();

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
			gridControlKeywords.DataSource = _keywords;
			gridViewKeywords.RefreshData();

			UpdateKeywordsInfo();
		}

		private void SaveKeywordsDataSource()
		{
			gridViewKeywords.CloseEditor();
			_keywords.RemoveAll(tag => String.IsNullOrEmpty(tag.Name));
			_links.ApplyKeywords(_keywords.ToArray());
		}

		private void UpdateKeywordsInfo()
		{
			labelControlKeywordsCount.Text = String.Format("Keyword Tags: <b><u>{0}</u></b>",
				_keywords.Count);
		}

		private void OnKeywordsEditorButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewKeywords.CloseEditor();
			if (gridViewKeywords.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var keyword = (KeywordModel)gridViewKeywords.GetFocusedRow();
			switch (e.Button.Tag as String)
			{
				case "Delete":
					if (MainController.Instance.PopupMessages.ShowWarningQuestion("Do you want to delete selected keyword tag?") ==
						DialogResult.Yes)
						_keywords.Remove(keyword);
					break;
				case "MakeShared":
					keyword.IsShared = true;
					break;
			}
			gridViewKeywords.RefreshData();
			UpdateKeywordsInfo();
		}

		private void OnAddKeyWordClick(object sender, EventArgs e)
		{
			gridViewKeywords.CloseEditor();
			_keywords.Add(new KeywordModel { IsShared = true });
			gridViewKeywords.RefreshData();
			if (gridViewKeywords.RowCount <= 0) return;
			gridViewKeywords.FocusedRowHandle = gridViewKeywords.RowCount - 1;
			gridViewKeywords.MakeRowVisible(gridViewKeywords.FocusedRowHandle, true);
			UpdateKeywordsInfo();
		}

		private void OnKeywordsGridCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			var keyword = (KeywordModel)gridViewKeywords.GetRow(e.RowHandle);
			e.RepositoryItem = keyword.IsShared ?
				repositoryItemButtonEditKeywordShared :
				repositoryItemButtonEditKeywordPartial;
		}

		private void OnKeywordsGridRowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var keyword = (KeywordModel)gridViewKeywords.GetRow(e.RowHandle);
			e.Appearance.ForeColor = keyword.IsShared ?
				Color.Black :
				Color.Gray;
		}

		private void OnWipeKeywordsClick(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Do you want to wipe all keyword tags?") ==
				DialogResult.Yes)
			{
				_keywords.Clear();
				gridViewKeywords.RefreshData();
				UpdateKeywordsInfo();
			}
		}
		#endregion
	}
}