using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class CategoriesEditor : UserControl, ITagsEditor
	{
		private readonly List<SearchGroup> _groupTemplates = new List<SearchGroup>();
		private readonly List<SearchTag> _tagTemplates = new List<SearchTag>();
		private bool _needToApply;
		public bool NeedToApply
		{
			get { return _needToApply; }
			set
			{
				_needToApply = value;
				var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
				if (activePage != null) activePage.Parent.StateChanged = true;
			}
		}
		public CategoriesEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_groupTemplates.AddRange(ListManager.Instance.SearchTags.SearchGroups);
			_tagTemplates.AddRange(ListManager.Instance.SearchTags.SearchGroups.SelectMany(x => x.Tags));

			gridViewGroups.MasterRowEmpty += OnGroupChildListIsEmpty;
			gridViewGroups.MasterRowGetRelationCount += OnGetGroupRelationCount;
			gridViewGroups.MasterRowGetRelationName += OnGetGroupRelationName;
			gridViewGroups.MasterRowGetChildList += OnGetGroupChildList;
			gridControl.DataSource = _groupTemplates;
		}

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			buttonXReset.Enabled = false;
			pnData.Enabled = false;
			Enabled = false;
			foreach (var group in _groupTemplates)
				group.Selected = false;
			foreach (var tag in _tagTemplates)
				tag.Selected = false;
			gridViewGroups.CollapseAllDetails();

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault(link => link.HasCategories) ?? activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = activePage.SelectedLinks.All(link => !link.HasCategories);
			var sameData = defaultLink != null && activePage.SelectedLinks.All(x => x.SearchTags.Compare(defaultLink.SearchTags));

			buttonXReset.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				foreach (var group in defaultLink.SearchTags.SearchGroups)
				{
					var groupTemplate = _groupTemplates.FirstOrDefault(x => x.Name.Equals(group.Name));
					if (groupTemplate != null)
						groupTemplate.Selected = true;
					foreach (var tagTemplate in group.Tags.Select(tag => _tagTemplates.FirstOrDefault(x => x.Name.Equals(tag.Name) && x.Parent.Equals(group.Name))).Where(tagTemplate => tagTemplate != null))
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
			var totalTags = _tagTemplates.Count(t => t.Selected);
			labelControlCategoryInfo.Text = String.Format("{0}{1}",
				ListManager.Instance.SearchTags.MaxTags > 0 ? String.Format("Only {0} Search Tags are ALLOWED{1}", ListManager.Instance.SearchTags.MaxTags, Environment.NewLine) : String.Empty,
				totalTags > 0 ? String.Join(", ", _tagTemplates.Where(t => t.Selected).Select(t => t.Name)) : "No Tags Selected"
				);
		}

		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			gridViewGroups.CloseEditor();
			gridViewTags.CloseEditor();

			foreach (var link in activePage.SelectedLinks)
			{
				link.SearchTags.SearchGroups.Clear();
				foreach (var groupTemplate in _groupTemplates.Where(x => x.Selected))
				{
					var group = new SearchGroup();
					group.Name = groupTemplate.Name;
					group.Tags.AddRange(groupTemplate.Tags.Where(x => x.Selected).Select(x => new SearchTag(group.Name) { Name = x.Name }));
					link.SearchTags.SearchGroups.Add(group);
				}
			}
			MainController.Instance.WallbinController.UpdateTagCountInfo();
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		public void ResetData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure You want to DELETE ALL CATEGORY TAGS for the selected files?") != DialogResult.Yes) return;
			foreach (var link in activePage.SelectedLinks)
				link.SearchTags.SearchGroups.Clear();
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
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
			if (e.RowHandle != GridControl.InvalidRowHandle)
				e.ChildList = _tagTemplates.Where(x => x.Parent == _groupTemplates[e.RowHandle].Name).ToArray();
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
			NeedToApply = true;
		}

		private void repositoryItemCheckEditLibrary_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			var newValue = e.NewValue is bool && (bool)e.NewValue;
			if (newValue && ListManager.Instance.SearchTags.MaxTags > 0)
			{
				var totalTags = _tagTemplates.Count(t => t.Selected);
				if (totalTags >= ListManager.Instance.SearchTags.MaxTags)
				{
					AppManager.Instance.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", ListManager.Instance.SearchTags.MaxTags));
					e.Cancel = true;
				}
			}
		}
	}
}