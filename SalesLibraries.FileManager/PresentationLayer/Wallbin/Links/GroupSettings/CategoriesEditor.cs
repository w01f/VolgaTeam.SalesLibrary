using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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

			var defaultLink = Selection.SelectedLinks.FirstOrDefault(link => link.Tags.HasCategories) ?? Selection.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			var noData = defaultLink != null && Selection.SelectedLinks.All(link => !link.Tags.HasCategories);
			var sameData = defaultLink != null && Selection.SelectedLinks.All(link => link.Tags.Categories.Compare(defaultLink.Tags.Categories));
			buttonXReset.Enabled = !noData;
			buttonXExpand.Enabled = sameData || noData;
			buttonXCollapse.Enabled = sameData || noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				foreach (var group in defaultLink.Tags.Categories)
				{
					var groupNode = _rootNode.Nodes.FirstOrDefault(node => ((SearchGroup) node.Tag).Name == group.Name);
					if (groupNode == null) continue;
					groupNode.Checked = true;

					foreach (var tag in group.Tags)
					{
						var tagNode = groupNode.Nodes.FirstOrDefault(node => ((SearchTag)node.Tag).Name == tag.Name);
						if (tagNode == null) continue;
						tagNode.Checked = true;
					}
				}
			}

			_handleCheckEvens = true;

			treeListCategories.ResumeLayout();
		}

		private void ApplyData()
		{
			Selection.SelectedLinks.ApplyCategories(_rootNode.Nodes
				.Where(groupNode => groupNode.Checked)
				.Select(groupNode =>
				{
					var sourceGroup = (SearchGroup)groupNode.Tag;
					var newGroup = new SearchGroup
					{
						Name = sourceGroup.Name,
						Description = sourceGroup.Description
					};
					newGroup.Tags.AddRange(groupNode.Nodes
						.Where(tagNode => tagNode.Checked)
						.Select(tagNode =>
						{
							var sourceTag = (SearchTag)tagNode.Tag;
							var newTag = new SearchTag { Name = sourceTag.Name };
							return newTag;
						}));
					return newGroup;
				})
				.Where(g => g.Tags.Any())
				.ToArray());
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL CATEGORY TAGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedLinks.ApplyCategories(new SearchGroup[] { });
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

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			ResetData();
		}

		private void OnCategoriesBeforeCheckNode(object sender, CheckNodeEventArgs e)
		{
			if (!_handleCheckEvens) return;
			if (e.State != CheckState.Checked) return;

			var curentCheckedTagsCount = _rootNode.Nodes.SelectMany(node => node.Nodes).Count(node => node.Checked);
			var newCheckedTagsCount = curentCheckedTagsCount;
			if (e.Node.Tag is SearchGroup)
				newCheckedTagsCount += e.Node.Nodes.Count;
			else if (e.Node.Tag is SearchTag)
				newCheckedTagsCount++;
			if (newCheckedTagsCount <= MainController.Instance.Lists.SearchTags.MaxTags) return;

			MainController.Instance.PopupMessages.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", MainController.Instance.Lists.SearchTags.MaxTags));
			e.State = CheckState.Unchecked;
			e.CanCheck = false;
		}

		private void OnCategoriesAfterCheckNode(object sender, NodeEventArgs e)
		{
			if (!_handleCheckEvens) return;

			_handleCheckEvens = false;

			if (e.Node.Tag is SearchGroup)
			{
				foreach (TreeListNode childNode in e.Node.Nodes)
					childNode.Checked = e.Node.Checked;
			}
			else if (e.Node.Tag is SearchTag)
			{
				var parentNode = e.Node.ParentNode;
				if (e.Node.Checked)
					parentNode.Checked = true;
				else
				{
					var checkedChildNodesCount = parentNode.Nodes.Count(n => n.Checked);
					parentNode.Checked = checkedChildNodesCount > 0;
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

		private void OnExpandClick(object sender, EventArgs e)
		{
			ExpandNode(_rootNode);
		}

		private void OnCollapseClick(object sender, EventArgs e)
		{
			CollapseNode(_rootNode);
		}
	}
}