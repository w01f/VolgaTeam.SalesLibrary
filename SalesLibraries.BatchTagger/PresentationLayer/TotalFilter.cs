using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Common.Helpers;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	[ToolboxItem(false)]
	public partial class TotalFilter : UserControl
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
		public event EventHandler<EventArgs> FilterChanged;

		public List<string> AllGroups { get; }
		public List<string> SelectedGroups { get; }

		public TotalFilter()
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
			checkedListBoxControlGroups.ItemHeight = (Int32)(checkedListBoxControlGroups.ItemHeight * scaleleFactor.Height);
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

		private void checkEditFilterEnable_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupControls.Enabled = checkEditEnableFilter.Checked;
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
	}
}