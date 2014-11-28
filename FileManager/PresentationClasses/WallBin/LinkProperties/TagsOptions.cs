using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class TagsOptions : UserControl, ILinkProperties
	public partial class TagsOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;
		private bool _loading;

		protected List<StringDataSourceWrapper> Keywords { get; private set; }

		public event EventHandler OnForseClose;

		public TagsOptions(LibraryLink data)
		{
			InitializeComponent();
			_data = data;

			LoadData();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXAddKeyWord.Font = new Font(buttonXAddKeyWord.Font.FontFamily, buttonXAddKeyWord.Font.Size - 2, buttonXAddKeyWord.Font.Style);
			}

			repositoryItemButtonEditKeyword.Enter += FormMain.Instance.EditorEnter;
			repositoryItemButtonEditKeyword.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditKeyword.MouseDown += FormMain.Instance.EditorMouseUp;
		}

		private void LoadData()
		{
			_loading = true;
			
			Keywords = new List<StringDataSourceWrapper>();
			
			xtraScrollableControlSearchTagsCategories.Controls.Clear();
			splitContainerSearchTagsCategories.Panel2.Controls.Clear();
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
			{
				searchGroup.InitGroupControls();

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

			ListManager.Instance.SearchTags.SearchGroups.ForEach(g => g.ListBox.UnCheckAll());
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
			{
				var group = _data.SearchTags.SearchGroups.FirstOrDefault(x => x.Name.Equals(searchGroup.Name));
				if (group != null)
					foreach (var item in searchGroup.ListBox.Items.Cast<CheckedListBoxItem>().Where(item => group.Tags.Select(x => x.Name).Contains(item.Value.ToString())))
						item.CheckState = CheckState.Checked;
			}
			var firstGroup = ListManager.Instance.SearchTags.SearchGroups.FirstOrDefault();
			if (firstGroup != null)
			{
				firstGroup.ToggleButton.Checked = false;
				CategoriesGroup_Click(firstGroup.ToggleButton, new EventArgs());
			}
			UpdateCategoriesHeader();
			

			Keywords.AddRange(_data.CustomKeywords.Tags.Select(searchTag => new StringDataSourceWrapper(searchTag.Name)));
			UpdateKeywordsDataSource();

			_loading = false;
		}

		public void SaveData()
		{
			_data.SearchTags.SearchGroups.Clear();
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
			{
				var group = new SearchGroup();
				group.Name = searchGroup.Name;
				foreach (CheckedListBoxItem item in searchGroup.ListBox.Items)
					if (item.CheckState == CheckState.Checked)
						group.Tags.Add(new SearchTag(group.Name) { Name = item.Value.ToString() });
				_data.SearchTags.SearchGroups.Add(group);
			}

			_data.CustomKeywords.Tags.Clear();
			_data.CustomKeywords.Tags.AddRange(Keywords.Where(kw => !String.IsNullOrEmpty(kw.Value)).Select(kw => new SearchTag(_data.CustomKeywords.Name) { Name = kw.Value }));
		}

		#region Categories Processing
		private void UpdateCategoriesHeader()
		{
			var totalTags = ListManager.Instance.SearchTags.SearchGroups.Sum(g => g.ListBox.CheckedItemsCount);
			labelControlSearchTagsCategoriesHeader.Text = String.Format("{0}{1}",
				ListManager.Instance.SearchTags.MaxTags > 0 ? String.Format("Only {0} Search Tags are ALLOWED{1}", ListManager.Instance.SearchTags.MaxTags, Environment.NewLine) : String.Empty,
				totalTags > 0 ? String.Join(", ", ListManager.Instance.SearchTags.SearchGroups.SelectMany(g => g.ListBox.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()))) : "No Tags Selected"
				);
		}

		private void ListBox_ItemChecking(object sender, ItemCheckingEventArgs e)
		{
			if (_loading || ListManager.Instance.SearchTags.MaxTags <= 0 || e.NewValue != CheckState.Checked) return;
			var totalTags = ListManager.Instance.SearchTags.SearchGroups.Sum(g => g.ListBox.CheckedItemsCount);
			if (totalTags < ListManager.Instance.SearchTags.MaxTags) return;
			AppManager.Instance.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", ListManager.Instance.SearchTags.MaxTags));
			e.Cancel = true;
		}

		private void buttonXWipeTags_Click(object sender, EventArgs e)
		{
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
				searchGroup.ListBox.UnCheckAll();
		}

		private void CategoriesGroup_Click(object sender, EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonX;
			if (button == null || button.Checked) return;
			ListManager.Instance.SearchTags.SearchGroups.ForEach(g => g.ToggleButton.Checked = false);
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
			gridControlSearchTagsKeywords.DataSource = Keywords;
			gridViewSearchTagsKeywords.RefreshData();
		}

		private void SaveKeywordsDataSource()
		{
			gridViewSearchTagsKeywords.CloseEditor();
			Keywords.RemoveAll(x => string.IsNullOrEmpty(x.Value));
		}

		private void repositoryItemButtonEditKeyword_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewSearchTagsKeywords.CloseEditor();
			if (gridViewSearchTagsKeywords.FocusedRowHandle >= 0 && gridViewSearchTagsKeywords.FocusedRowHandle < gridViewSearchTagsKeywords.RowCount)
			{
				Keywords.RemoveAt(gridViewSearchTagsKeywords.GetDataSourceRowIndex(gridViewSearchTagsKeywords.FocusedRowHandle));
				gridViewSearchTagsKeywords.RefreshData();
			}
		}

		private void buttonXAddKeyWord_Click(object sender, EventArgs e)
		{
			SaveKeywordsDataSource();
			Keywords.Add(new StringDataSourceWrapper());
			gridViewSearchTagsKeywords.RefreshData();
			if (gridViewSearchTagsKeywords.RowCount > 0)
			{
				gridViewSearchTagsKeywords.FocusedRowHandle = gridViewSearchTagsKeywords.RowCount - 1;
				gridViewSearchTagsKeywords.MakeRowVisible(gridViewSearchTagsKeywords.FocusedRowHandle, true);
			}
		}
		#endregion
	}
}
