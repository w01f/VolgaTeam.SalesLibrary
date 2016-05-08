using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Common;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	//public partial class TagsOptions : UserControl, ILinkProperties
	[IntendForClass(typeof(BaseLibraryLink))]
	public partial class TagsOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly BaseLibraryLink _data;
		private bool _loading;
		private readonly List<SearchGroupContainer> _searchGroups = new List<SearchGroupContainer>();

		public LinkSettingsType SettingsType => LinkSettingsType.Tags;
		public int Order => 0;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public TagsOptions(BaseLibraryLink data)
		{
			InitializeComponent();
			_data = data;

			if ((CreateGraphics()).DpiX > 96)
			{
				buttonXAddKeyWord.Font = new Font(buttonXAddKeyWord.Font.FontFamily, buttonXAddKeyWord.Font.Size - 2, buttonXAddKeyWord.Font.Style);
			}

			repositoryItemButtonEditKeyword.Enter += EditorHelper.EditorEnter;
			repositoryItemButtonEditKeyword.MouseUp += EditorHelper.EditorMouseUp;
			repositoryItemButtonEditKeyword.MouseDown += EditorHelper.EditorMouseUp;
		}

		public void LoadData()
		{
			_loading = true;

			xtraScrollableControlSearchTagsCategories.Controls.Clear();
			splitContainerSearchTagsCategories.Panel2.Controls.Clear();
			_searchGroups.Clear();
			_searchGroups.AddRange(MainController.Instance.Lists.SearchTags.SearchGroups.Select(sg => new SearchGroupContainer(sg)));
			foreach (var searchGroup in _searchGroups)
			{

				searchGroup.ToggleButton.Dock = DockStyle.Top;
				searchGroup.ToggleButton.Click += CategoriesGroup_Click;
				searchGroup.ToggleButton.CheckedChanged += CategoriesGroup_CheckedChanged;
				xtraScrollableControlSearchTagsCategories.Controls.Add(searchGroup.ToggleButton);
				searchGroup.ToggleButton.BringToFront();

				searchGroup.ListBox.ItemChecking += ListBox_ItemChecking;
				searchGroup.ListBox.ItemCheck += (o, ea) => UpdateCategoriesHeader();
				splitContainerSearchTagsCategories.Panel2.Controls.Add(searchGroup.ListBox);
				searchGroup.ListBox.BringToFront();
			}

			_searchGroups.ForEach(g => g.ListBox.UnCheckAll());
			foreach (var searchGroup in _searchGroups)
			{
				var group = _data.Tags.Categories.FirstOrDefault(catGroup => String.Equals(catGroup.Name, searchGroup.DataSource.Name));
				if (group != null)
					foreach (var item in searchGroup.ListBox.Items.Cast<CheckedListBoxItem>().Where(item => group.Tags.Select(x => x.Name).Contains(item.Value.ToString())))
						item.CheckState = CheckState.Checked;
			}
			var firstGroup = _searchGroups.FirstOrDefault();
			if (firstGroup != null)
			{
				firstGroup.ToggleButton.Checked = false;
				CategoriesGroup_Click(firstGroup.ToggleButton, new EventArgs());
			}
			UpdateCategoriesHeader();


			UpdateKeywordsDataSource();

			_loading = false;
		}

		public void SaveData()
		{
			_data.Tags.Categories.Clear();
			foreach (var searchGroup in _searchGroups)
			{
				var group = new SearchGroup();
				group.Name = searchGroup.DataSource.Name;
				group.Description = searchGroup.DataSource.Description;
				foreach (CheckedListBoxItem item in searchGroup.ListBox.Items)
					if (item.CheckState == CheckState.Checked)
						group.Tags.Add(new SearchTag { Name = item.Value.ToString() });
				if (group.Tags.Any())
					_data.Tags.Categories.Add(group);
			}

			SaveKeywordsDataSource();
		}

		#region Categories Processing
		private void UpdateCategoriesHeader()
		{
			var totalTags = _searchGroups.Sum(g => g.ListBox.CheckedItemsCount);
			labelControlSearchTagsCategoriesHeader.Text = String.Format("{0}{1}",
				MainController.Instance.Lists.SearchTags.MaxTags > 0 ? String.Format("Only {0} Search Tags are ALLOWED{1}", MainController.Instance.Lists.SearchTags.MaxTags, Environment.NewLine) : String.Empty,
				totalTags > 0 ? String.Join(", ", _searchGroups.SelectMany(g => g.ListBox.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()))) : "No Tags Selected"
				);
		}

		private void ListBox_ItemChecking(object sender, ItemCheckingEventArgs e)
		{
			if (_loading || MainController.Instance.Lists.SearchTags.MaxTags <= 0 || e.NewValue != CheckState.Checked) return;
			var totalTags = _searchGroups.Sum(g => g.ListBox.CheckedItemsCount);
			if (totalTags < MainController.Instance.Lists.SearchTags.MaxTags) return;
			MainController.Instance.PopupMessages.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", MainController.Instance.Lists.SearchTags.MaxTags));
			e.Cancel = true;
		}

		private void buttonXWipeTags_Click(object sender, EventArgs e)
		{
			foreach (var searchGroup in _searchGroups)
				searchGroup.ListBox.UnCheckAll();
		}

		private void CategoriesGroup_Click(object sender, EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonX;
			if (button == null || button.Checked) return;
			_searchGroups.ForEach(g => g.ToggleButton.Checked = false);
			button.Checked = true;
		}

		private void CategoriesGroup_CheckedChanged(object sender, EventArgs e)
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
			gridControlSearchTagsKeywords.DataSource = _data.Tags.Keywords;
			gridViewSearchTagsKeywords.RefreshData();
		}

		private void SaveKeywordsDataSource()
		{
			gridViewSearchTagsKeywords.CloseEditor();
			_data.Tags.Keywords.RemoveAll(tag => String.IsNullOrEmpty(tag.Name));
		}

		private void repositoryItemButtonEditKeyword_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewSearchTagsKeywords.CloseEditor();
			if (gridViewSearchTagsKeywords.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			_data.Tags.Keywords.RemoveAt(gridViewSearchTagsKeywords.GetDataSourceRowIndex(gridViewSearchTagsKeywords.FocusedRowHandle));
			gridViewSearchTagsKeywords.RefreshData();
		}

		private void buttonXAddKeyWord_Click(object sender, EventArgs e)
		{
			SaveKeywordsDataSource();
			_data.Tags.Keywords.Add(new SearchTag());
			gridViewSearchTagsKeywords.RefreshData();
			if (gridViewSearchTagsKeywords.RowCount <= 0) return;
			gridViewSearchTagsKeywords.FocusedRowHandle = gridViewSearchTagsKeywords.RowCount - 1;
			gridViewSearchTagsKeywords.MakeRowVisible(gridViewSearchTagsKeywords.FocusedRowHandle, true);
		}
		#endregion
	}
}
