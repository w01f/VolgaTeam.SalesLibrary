using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public sealed partial class CategoriesEditor : UserControl, IGroupSettingsEditor
	{
		private readonly List<SearchGroup> _groupTemplates = new List<SearchGroup>();

		private SelectionManager Selection => MainController.Instance.WallbinViews.Selection;

		public CategoriesEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			_groupTemplates.AddRange(MainController.Instance.Lists.SearchTags.SearchGroups);

			gridViewGroups.MasterRowEmpty += OnGroupChildListIsEmpty;
			gridViewGroups.MasterRowGetRelationCount += OnGetGroupRelationCount;
			gridViewGroups.MasterRowGetRelationName += OnGetGroupRelationName;
			gridViewGroups.MasterRowGetChildList += OnGetGroupChildList;
			gridControl.DataSource = _groupTemplates;
		}

		#region IGroupSettingsEditor Members
		public string Title => "Manage Search Tags";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			buttonXReset.Enabled = false;
			pnData.Enabled = false;
			Enabled = false;
			foreach (var group in _groupTemplates)
			{
				group.Selected = false;
				group.Tags.ForEach(tag => tag.Selected = false);
			}
			gridViewGroups.CollapseAllDetails();
			labelControlCategoryInfo.Text = String.Empty;

			var defaultLink = Selection.SelectedFiles.FirstOrDefault(link => link.Tags.HasCategories) ?? Selection.SelectedFiles.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = Selection.SelectedFiles.All(link => !link.Tags.HasCategories);
			var sameData = Selection.SelectedFiles.All(link => link.Tags.Categories.Compare(defaultLink.Tags.Categories));

			buttonXReset.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				foreach (var group in defaultLink.Tags.Categories)
				{
					var groupTemplate = _groupTemplates.FirstOrDefault(x => x.Name.Equals(group.Name));
					if (groupTemplate == null) continue;
					groupTemplate.Selected = true;
					foreach (var tagTemplate in groupTemplate.Tags.Where(tagTemplate => group.Tags.Any(tag => tag.Equals(tagTemplate))))
						tagTemplate.Selected = true;
				}
			}
			gridViewGroups.RefreshData();
			if (gridViewGroups.FocusedRowHandle != GridControl.InvalidRowHandle && gridViewGroups.GetDetailView(gridViewGroups.FocusedRowHandle, 0) != null)
				gridViewGroups.GetDetailView(gridViewGroups.FocusedRowHandle, 0).RefreshData();

			UpdateCategoryInfo(sameData || noData);
		}

		public void UpdateCategoryInfo(bool sameData)
		{
			var selectedTags = _groupTemplates.SelectMany(group => group.Tags).Where(tag => tag.Selected).ToList();
			var totalTags = selectedTags.Count;
			labelControlCategoryInfo.Text = String.Format("{0}{1}",
				MainController.Instance.Lists.SearchTags.MaxTags > 0 ? String.Format("Only {0} Search Tags are ALLOWED{1}", MainController.Instance.Lists.SearchTags.MaxTags, Environment.NewLine) : String.Empty,
				totalTags > 0 ? String.Join(", ", selectedTags.Select(t => t.Name)) : "No Tags Selected");
		}

		private void ApplyData()
		{
			gridViewGroups.CloseEditor();
			gridViewTags.CloseEditor();
			Selection.SelectedFiles.ApplyCategories(_groupTemplates
				.Where(x => x.Selected)
				.Select(g =>
				{
					var searchGroup = new SearchGroup
					{
						Name = g.Name,
						Description = g.Description,
					};
					searchGroup.Tags.AddRange(g.Tags.Where(t => t.Selected));
					return searchGroup;
				})
				.Where(g=>g.Tags.Any())
				.ToArray());
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL CATEGORY TAGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedFiles.ApplyCategories(new SearchGroup[] { });
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
			UpdateData();
		}
		#endregion

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			ResetData();
		}

		private void OnGroupChildListIsEmpty(object sender, MasterRowEmptyEventArgs e)
		{
			e.IsEmpty = !(e.RowHandle != GridControl.InvalidRowHandle && _groupTemplates[e.RowHandle].Tags.Count > 0);
		}

		private void OnGetGroupRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
		{
			e.RelationCount = 1;
		}

		private void OnGetGroupRelationName(object sender, MasterRowGetRelationNameEventArgs e)
		{
			e.RelationName = "Tags";
		}

		private void OnGetGroupChildList(object sender, MasterRowGetChildListEventArgs e)
		{
			if (e.RowHandle == GridControl.InvalidRowHandle) return;
			var group = gridViewGroups.GetRow(e.RowHandle) as SearchGroup;
			e.ChildList = group != null ? group.Tags.ToArray() : new SearchTag[] { };
		}

		private void RepositoryItemCheckEditCheckedChanged(object sender, EventArgs e)
		{
			var focussedView = gridControl.FocusedView as GridView;
			if (focussedView == null) return;
			focussedView.CloseEditor();
			var searchGroup = focussedView.SourceRow as SearchGroup;
			var searchTag = focussedView.GetFocusedRow() as SearchTag;
			if (searchGroup != null && searchTag != null && searchTag.Selected)
			{
				searchGroup.Selected = searchTag.Selected;
				gridControl.MainView.RefreshData();
			}
			UpdateCategoryInfo(true);
			ApplyData();
		}

		private void repositoryItemCheckEditLibrary_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			var newValue = e.NewValue is bool && (bool)e.NewValue;
			if (!newValue || MainController.Instance.Lists.SearchTags.MaxTags <= 0) return;
			var totalTags = _groupTemplates.SelectMany(group => @group.Tags).Count(tag => tag.Selected);
			if (totalTags < MainController.Instance.Lists.SearchTags.MaxTags) return;
			MainController.Instance.PopupMessages.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", MainController.Instance.Lists.SearchTags.MaxTags));
			e.Cancel = true;
		}
	}
}