using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(BaseLibraryLink))]
	//public partial class TagsOptions : UserControl, ILinkSettingsEditControl
	public partial class TagsOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private bool _loading;
		private readonly List<BaseLibraryLink> _links = new List<BaseLibraryLink>();

		private readonly List<SearchGroupContainer> _searchGroups = new List<SearchGroupContainer>();
		private readonly List<KeywordModel> _keywords = new List<KeywordModel>();

		public LinkSettingsType SettingsType => LinkSettingsType.Tags;
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

		public TagsOptions(IEnumerable<BaseLibraryLink> links) : this()
		{
			_links.AddRange(links.OfType<LibraryObjectLink>());
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
			xtraScrollableControlSearchTagsCategories.Controls.Clear();
			splitContainerSearchTagsCategories.Panel2.Controls.Clear();

			_searchGroups.Clear();
			_searchGroups.AddRange(MainController.Instance.Lists.SearchTags.SearchGroups.Select(sg => new SearchGroupContainer(sg)));
			foreach (var searchGroup in _searchGroups)
			{

				searchGroup.ToggleButton.Dock = DockStyle.Top;
				searchGroup.ToggleButton.Click += OnCategoriesGroupClick;
				searchGroup.ToggleButton.CheckedChanged += OnCategoriesGroupCheckedChanged;
				xtraScrollableControlSearchTagsCategories.Controls.Add(searchGroup.ToggleButton);
				searchGroup.ToggleButton.BringToFront();

				searchGroup.ListBox.ItemChecking += OnCategoriesListBoxItemChecking;
				searchGroup.ListBox.ItemCheck += (o, ea) => UpdateCategoriesHeader();
				splitContainerSearchTagsCategories.Panel2.Controls.Add(searchGroup.ListBox);
				searchGroup.ListBox.BringToFront();
			}

			_searchGroups.ForEach(g => g.ListBox.UnCheckAll());
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

			var firstGroup = _searchGroups.FirstOrDefault();
			if (firstGroup != null)
			{
				firstGroup.ToggleButton.Checked = false;
				OnCategoriesGroupClick(firstGroup.ToggleButton, new EventArgs());
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

		private void OnCategoriesGroupClick(object sender, EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonX;
			if (button == null || button.Checked) return;
			_searchGroups.ForEach(g => g.ToggleButton.Checked = false);
			button.Checked = true;
		}

		private void OnCategoriesGroupCheckedChanged(object sender, EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonX;
			if (button == null || !button.Checked) return;
			var assignedControl = button.Tag as Control;
			if (assignedControl == null) return;
			assignedControl.Dock = DockStyle.Fill;
			assignedControl.BringToFront();
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
