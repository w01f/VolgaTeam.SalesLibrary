using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Common.Helpers;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	[ToolboxItem(false)]
	public partial class LibraryFilter : UserControl
	{
		private bool _init;

		public bool EnableFilter
		{
			get => checkEditEnableFilter.Checked;
			set
			{
				_init = true;
				checkEditEnableFilter.Checked = value;
				_init = false;
			}
		}

		public bool ShowAllLinks => checkEditAllFiles.Checked;
		public bool ShowUntaggedLinks => checkEditUntaggedLinks.Checked;
		public bool ShowNokeywordLinks => checkEditNoKeywordLinks.Checked;

		public event EventHandler<EventArgs> FilterChanged;
		public event EventHandler<EventArgs> LinkTagFilterChanged;

		public List<string> AllGroups { get; }
		public List<string> SelectedGroups { get; }

		public LibraryFilter()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllGroups = new List<string>();
			SelectedGroups = new List<string>();

			var scaleleFactor = Utils.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemGroupsAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemGroupsAll.MaxSize, scaleleFactor);
			layoutControlItemGroupsAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemGroupsAll.MinSize, scaleleFactor);
			layoutControlItemGroupsNone.MaxSize = RectangleHelper.ScaleSize(layoutControlItemGroupsNone.MaxSize, scaleleFactor);
			layoutControlItemGroupsNone.MinSize = RectangleHelper.ScaleSize(layoutControlItemGroupsNone.MinSize, scaleleFactor);
			checkedListBoxControlGroups.ItemHeight = (Int32) (checkedListBoxControlGroups.ItemHeight * scaleleFactor.Height);
		}

		public void UpdateDataSource(string[] groups, bool reset = true)
		{
			_init = true;

			AllGroups.Clear();
			AllGroups.AddRange(groups);
			if (SelectedGroups.Count > 0 || !reset)
				SelectedGroups.RemoveAll(x => !AllGroups.Contains(x));
			else
				SelectedGroups.AddRange(AllGroups);
			checkedListBoxControlGroups.Items.Clear();
			foreach (var group in groups)
				checkedListBoxControlGroups.Items.Add(group, SelectedGroups.Contains(group));

			simpleLabelItemLibraries.Text = string.Format("Libraries: {0}", AllGroups.Count);

			_init = false;
		}

		public void ResetFilter()
		{
			_init = true;
			checkedListBoxControlGroups.CheckAll();
			checkEditAllFiles.Checked = true;
			_init = false;

			EnableFilter = true;

			FilterChanged?.Invoke(this, new EventArgs());
		}

		public void UpdateLiksInfo(int allLinksCount, int untaggedLinksCount, int noKeywordsLinksCount)
		{
			checkEditAllFiles.Text = String.Format("All Links ({0})", allLinksCount);
			checkEditUntaggedLinks.Text = String.Format("Un-Tagged Links <color=red>({0})</color>", untaggedLinksCount);
			checkEditNoKeywordLinks.Text = String.Format("No Keyword Links <color=blue>({0})</color>", noKeywordsLinksCount);
		}

		private void checkEditFilterEnable_CheckedChanged(object sender, EventArgs e)
		{
			var enableFilter = checkEditEnableFilter.Checked;
			layoutControlGroupControls.Enabled = enableFilter;
			layoutControlGroupToggles.Visibility = enableFilter ? LayoutVisibility.Always : LayoutVisibility.Never;
			if (_init) return;
			FilterChanged?.Invoke(this, new EventArgs());
		}

		private void checkedListBoxControlGroups_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (_init) return;
			SelectedGroups.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlGroups.CheckedItems)
				SelectedGroups.Add(item.Value.ToString());
			FilterChanged?.Invoke(this, new EventArgs());
		}

		private void buttonXGroupsAll_Click(object sender, EventArgs e)
		{
			_init = true;
			SelectedGroups.Clear();
			SelectedGroups.AddRange(AllGroups);
			checkedListBoxControlGroups.CheckAll();
			FilterChanged?.Invoke(this, new EventArgs());
			_init = false;
		}

		private void buttonXGroupsNone_Click(object sender, EventArgs e)
		{
			_init = true;
			SelectedGroups.Clear();
			checkedListBoxControlGroups.UnCheckAll();
			FilterChanged?.Invoke(this, new EventArgs());
			_init = false;
		}

		private void checkEditLinkTagFilter_CheckedChanged(object sender, EventArgs e)
		{
			if (_init) return;
			LinkTagFilterChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}