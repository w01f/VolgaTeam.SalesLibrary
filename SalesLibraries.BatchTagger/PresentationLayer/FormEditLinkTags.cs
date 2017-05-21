using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.BatchTagger.Properties;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	public partial class FormEditLinkTags : MetroForm
	{
		private const string HeaderTitleTemplate = "<size=+4>{0}</size>";

		private readonly BaseLibraryLink _sourceLink;
		private bool _allowHandleEvents;

		public FormEditLinkTags()
		{
			InitializeComponent();

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
			_sourceLink = sourceLink;
			labelControlTitle.Text = String.Format(HeaderTitleTemplate, _sourceLink);
		}

		public void LoadData()
		{
			_allowHandleEvents = false;

			LoadTreeView();

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
			_sourceLink.MarkAsModified();
		}

		private void UpdateTitle()
		{
			labelControlTitle.Appearance.Image = xtraTabControl.SelectedTabPage == xtraTabPageCategories
				? Resources.LinkSettingsTagsLogoCategories
				: Resources.LinkSettingsTagsLogoKeywords;
		}

		private void UpdateControlPanels()
		{
			panelTopControlsCategories.Visible = xtraTabControl.SelectedTabPage == xtraTabPageCategories;
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
			_searchGroups.ForEach(searchGroup => searchGroup.ListBox.UnCheckAll());
			foreach (var searchGroup in _searchGroups)
			{
				var linkCategortGroup = _sourceLink.Tags.Categories.FirstOrDefault(sg => sg.Equals(searchGroup.DataSource));
				if (linkCategortGroup != null)
					foreach (var item in searchGroup.ListBox.Items
						.Where(item => linkCategortGroup.Tags
							.Any(t => t.Equals((SearchTag)item.Value))))
					{
						item.CheckState = CheckState.Checked;
						searchGroup.ListBox.BringToFront();
					}
			}

			UpdateSuperGroupNodes();
			UpdateCategoryInfo();
		}

		private void SaveCategoriesDataSource()
		{
			var selectedCategories = _searchGroups
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
			_sourceLink.Tags.Categories.Clear();
			_sourceLink.Tags.Categories.AddRange(selectedCategories.Select(searchGroup => searchGroup.Clone()));
		}

		private void LoadTreeView()
		{
			splitContainerCategories.Panel2.Controls.Clear();
			treeListCategories.Nodes.Clear();
			_searchGroups.Clear();

			_rootNode = treeListCategories.AppendNode(new object[] { "Search Engine Tags" }, null);
			_rootNode.StateImageIndex = 0;

			foreach (var searchSuperGroup in AppManager.Instance.SearchTagList.SearchSuperGroups)
			{
				var superGroupNode = treeListCategories.AppendNode(new object[] { searchSuperGroup.Name }, _rootNode);
				superGroupNode.Tag = searchSuperGroup;
				superGroupNode.StateImageIndex = 0;

				foreach (var searchGroup in searchSuperGroup.Groups)
				{
					var groupNode = treeListCategories.AppendNode(new object[] { searchGroup.Name }, superGroupNode);
					var searchGroupContainer = new SearchGroupContainer(searchGroup);
					searchGroupContainer.ListBox.ItemChecking += OnCategoriesListBoxItemChecking;
					searchGroupContainer.ListBox.ItemCheck += (o, ea) =>
					{
						if (!_allowHandleEvents) return;
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
				foreach (TreeListNode childNode in node.Nodes)
				{
					treeListCategories.InvalidateNode(childNode);
					treeListCategories.RefreshNode(childNode);
				}
				treeListCategories.InvalidateNode(node);
				treeListCategories.RefreshNode(node);
			}
		}

		private void OnCategoriesListBoxItemChecking(object sender, ItemCheckingEventArgs e)
		{
			if (!_allowHandleEvents || AppManager.Instance.SearchTagList.MaxTags <= 0 || e.NewValue != CheckState.Checked) return;
			var totalTags = _searchGroups.SelectMany(g => g.ListBox.Items)
				.Count(item => item.CheckState != CheckState.Unchecked);
			if (totalTags < AppManager.Instance.SearchTagList.MaxTags) return;
			AppManager.Instance.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED",
				AppManager.Instance.SearchTagList.MaxTags));
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
				var groupContainers = e.Node.Nodes
					.Select(n => n.Tag)
					.OfType<SearchGroupContainer>()
					.ToList();
				if (groupContainers.Any(g => g.ListBox.Items.Any(item => item.CheckState != CheckState.Unchecked)))
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
			else if (e.Node.Tag is SearchGroupContainer)
			{
				var groupContainer = (SearchGroupContainer)e.Node.Tag;
				if (groupContainer.ListBox.Items.Any(item => item.CheckState != CheckState.Unchecked))
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
			if (AppManager.Instance.ShowWarningQuestion("Do you want to wipe all category tags?") ==
				DialogResult.Yes)
			{
				foreach (var searchGroup in _searchGroups)
					searchGroup.ListBox.UnCheckAll();
			}
		}

		private void buttonXCategoriesExpandAll_Click(object sender, EventArgs e)
		{
			treeListCategories.ExpandAll();
		}

		private void buttonXCategoriesCollapseAll_Click(object sender, EventArgs e)
		{
			treeListCategories.CollapseAll();
		}
		#endregion

		#region Keywords

		private readonly List<KeywordModel> _keywords = new List<KeywordModel>();

		private void UpdateKeywordsDataSource()
		{
			_keywords.Clear();
			_keywords.AddRange(_sourceLink.Tags.Keywords.Select(k => new KeywordModel { Name = k.Name }));
			gridControlKeywords.DataSource = _keywords;
			gridViewKeywords.RefreshData();
			UpdateKeywordsInfo();
		}

		private void SaveKeywordsDataSource()
		{
			gridViewKeywords.CloseEditor();
			_keywords.RemoveAll(tag => String.IsNullOrEmpty(tag.Name));
			_sourceLink.Tags.Keywords.Clear();
			_sourceLink.Tags.Keywords.AddRange(_keywords.Select(tag => new SearchTag { Name = tag.Name }));
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
					if (AppManager.Instance.ShowWarningQuestion("Do you want to delete selected keyword tag?") ==
						DialogResult.Yes)
						_keywords.Remove(keyword);
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

		private void OnWipeKeywordsClick(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			if (AppManager.Instance.ShowWarningQuestion("Do you want to wipe all keyword tags?") ==
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