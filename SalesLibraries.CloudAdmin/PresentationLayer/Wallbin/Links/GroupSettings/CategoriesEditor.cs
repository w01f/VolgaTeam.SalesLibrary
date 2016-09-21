using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public sealed partial class CategoriesEditor : UserControl, IGroupSettingsEditor
	{
		private readonly List<SearchGroup> _groupTemplates = new List<SearchGroup>();
		private TreeListNode _rootNode;
		private bool _handleCheckEvens = false;

		private SelectionManager Selection => MainController.Instance.WallbinViews.Selection;

		public CategoriesEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			_groupTemplates.AddRange(MainController.Instance.Lists.SearchTags.SearchGroups);

			LoadTreeView();
		}

		#region IGroupSettingsEditor Members
		public string Title => MainController.Instance.Lists.SearchTags.MaxTags > 0 ?
					String.Format("Only {0} Search Tags are ALLOWED", MainController.Instance.Lists.SearchTags.MaxTags) :
					"Manage Search Tags";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			_handleCheckEvens = false;

			treeListCategories.SuspendLayout();

			_rootNode.Nodes.ToList().ForEach(groupNode =>
			{
				groupNode.Checked = false;
				groupNode.Nodes.ToList().ForEach(tagNode => tagNode.Checked = false);
			});

			Enabled = Selection.SelectedObjects.Any();

			var commonCategories = Selection.SelectedObjects.GetCommonCategories();
			foreach (var link in Selection.SelectedObjects)
			{
				foreach (var group in link.Tags.Categories)
				{
					var groupNode = _rootNode.Nodes.FirstOrDefault(node => ((SearchGroup)node.Tag).Equals(group));
					if (groupNode == null) continue;
					var isCommonGroup = commonCategories.Any(commonGroup => commonGroup.Equals(group));
					groupNode.CheckState = isCommonGroup ? CheckState.Checked : CheckState.Indeterminate;

					foreach (var tag in group.Tags)
					{
						var tagNode = groupNode.Nodes.FirstOrDefault(node => ((SearchTag)node.Tag).Equals(tag));
						if (tagNode == null) continue;
						var isCommonTag = commonCategories
							.Where(commonCategory => commonCategory.Equals(group))
							.SelectMany(commonCategory => commonCategory.Tags)
							.Any(commonTag => commonTag.Equals(tag));
						tagNode.CheckState = isCommonTag ? CheckState.Checked : CheckState.Indeterminate;
					}
				}
			}

			_handleCheckEvens = true;

			treeListCategories.ResumeLayout();
		}

		private void ApplyData()
		{
			var sharedGroups = _rootNode.Nodes
				.Where(groupNode => groupNode.CheckState == CheckState.Checked)
				.Select(groupNode =>
				{
					var sourceGroup = (SearchGroup)groupNode.Tag;
					var newGroup = new SearchGroup
					{
						Name = sourceGroup.Name,
						Description = sourceGroup.Description
					};
					newGroup.Tags.AddRange(groupNode.Nodes
						.Where(tagNode => tagNode.CheckState == CheckState.Checked)
						.Select(tagNode =>
						{
							var sourceTag = (SearchTag)tagNode.Tag;
							var newTag = new SearchTag { Name = sourceTag.Name };
							return newTag;
						}));
					return newGroup;
				})
				.Where(g => g.Tags.Any())
				.ToArray();

			var partialGroups = _rootNode.Nodes
				.Where(groupNode => groupNode.CheckState != CheckState.Unchecked)
				.Select(groupNode =>
				{
					var sourceGroup = (SearchGroup)groupNode.Tag;
					var newGroup = new SearchGroup
					{
						Name = sourceGroup.Name,
						Description = sourceGroup.Description
					};
					newGroup.Tags.AddRange(groupNode.Nodes
						.Where(tagNode => tagNode.CheckState == CheckState.Indeterminate)
						.Select(tagNode =>
						{
							var sourceTag = (SearchTag)tagNode.Tag;
							var newTag = new SearchTag { Name = sourceTag.Name };
							return newTag;
						}));
					return newGroup;
				})
				.Where(g => g.Tags.Any())
				.ToArray();

			Selection.SelectedObjects.ApplyCategories(sharedGroups, partialGroups);
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL CATEGORY TAGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedObjects.ApplyCategories(new SearchGroup[] { });
			EditorChanged?.Invoke(this, new EventArgs());
			UpdateData();
		}
		#endregion

		private void LoadTreeView()
		{
			treeListCategories.Nodes.Clear();

			_rootNode = treeListCategories.AppendNode(new object[] { "Search Engine Tags" }, null);
			_rootNode.StateImageIndex = 0;

			foreach (var searchGroup in _groupTemplates)
			{
				var groupNode = treeListCategories.AppendNode(new object[] { searchGroup.Name }, _rootNode);
				groupNode.Tag = searchGroup;
				groupNode.StateImageIndex = 0;

				foreach (var searchTag in searchGroup.Tags)
				{
					var tagNode = treeListCategories.AppendNode(new object[] { searchTag.Name }, groupNode);
					tagNode.Tag = searchTag;
				}
			}
			_rootNode.Expanded = true;
		}

		private void CollapseNode(TreeListNode node)
		{
			if (node.Tag is SearchTag) return;
			foreach (TreeListNode childNode in node.Nodes)
				CollapseNode(childNode);
			if (node.Tag is SearchGroup)
			{
				node.Expanded = false;
				node.StateImageIndex = 0;
			}
		}

		private void ExpandNode(TreeListNode node)
		{
			node.ExpandAll();
			node.StateImageIndex = 1;
		}

		private void OnResetClick(object sender, EventArgs e)
		{
			ResetData();
		}

		private void OnExpandClick(object sender, EventArgs e)
		{
			ExpandNode(_rootNode);
		}

		private void OnCollapseClick(object sender, EventArgs e)
		{
			CollapseNode(_rootNode);
		}

		private void OnCategoriesBeforeCheckNode(object sender, CheckNodeEventArgs e)
		{
			if (!_handleCheckEvens) return;
			if (e.State != CheckState.Checked) return;

			var curentCheckedTagsCount = _rootNode.Nodes.SelectMany(node => node.Nodes).Count(node => node.CheckState != CheckState.Unchecked);
			var newCheckedTagsCount = curentCheckedTagsCount;
			if (e.Node.Tag is SearchGroup)
				newCheckedTagsCount += e.Node.Nodes.Count;
			else if (e.Node.Tag is SearchTag)
				newCheckedTagsCount++;
			if (newCheckedTagsCount <= MainController.Instance.Lists.SearchTags.MaxTags) return;

			MainController.Instance.PopupMessages.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", MainController.Instance.Lists.SearchTags.MaxTags));

			_handleCheckEvens = false;
			e.State = e.PrevState;
			e.CanCheck = false;
			_handleCheckEvens = true;
		}

		private void OnCategoriesAfterCheckNode(object sender, NodeEventArgs e)
		{
			if (!_handleCheckEvens) return;

			_handleCheckEvens = false;

			if (e.Node.Tag is SearchGroup && e.Node.CheckState != CheckState.Indeterminate)
			{
				foreach (TreeListNode childNode in e.Node.Nodes)
					childNode.CheckState = e.Node.CheckState;
			}
			else if (e.Node.Tag is SearchTag)
			{
				var parentNode = e.Node.ParentNode;
				if (e.Node.CheckState == CheckState.Checked)
					parentNode.CheckState = CheckState.Checked;
				else
				{
					if (parentNode.Nodes.Any(n => n.CheckState == CheckState.Checked))
						parentNode.CheckState = CheckState.Checked;
					else if (parentNode.Nodes.Any(n => n.CheckState == CheckState.Indeterminate))
						parentNode.CheckState = CheckState.Indeterminate;
					else
						parentNode.CheckState = CheckState.Unchecked;
				}
			}

			_handleCheckEvens = true;

			ApplyData();
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
			e.CanCollapse = e.Node.Tag is SearchGroup;
		}
	}
}