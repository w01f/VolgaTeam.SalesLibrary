using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace SalesLibraries.SiteManager.PresentationClasses.InactiveUsers
{
	[ToolboxItem(false)]
	public partial class InactiveUsersFilter : UserControl
	{
		private bool _init;

		public bool EnableFilter { get; private set; }
		public event EventHandler<EventArgs> FilterChanged;

		public List<string> AllGroups { get; }
		public List<string> SelectedGroups { get; }

		public InactiveUsersFilter()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllGroups = new List<string>();
			SelectedGroups = new List<string>();

			if (CreateGraphics().DpiX > 96)
			{
				checkedListBoxControlGroups.ItemHeight =
					RectangleHelper.ScaleVertical(checkedListBoxControlGroups.ItemHeight,
						checkedListBoxControlGroups.ScaleFactor.Height);
			}
		}

		public void UpdateDataSource(string[] groups)
		{
			_init = true;

			AllGroups.Clear();
			AllGroups.AddRange(groups);
			if (SelectedGroups.Count > 0)
				SelectedGroups.RemoveAll(x => !AllGroups.Contains(x));
			else
				SelectedGroups.AddRange(AllGroups);
			checkedListBoxControlGroups.Items.Clear();
			foreach (var group in groups)
				checkedListBoxControlGroups.Items.Add(group, SelectedGroups.Contains(group));

			_init = false;
		}

		public void UpdateUsersCount(int totalUsers, int selectedUsers)
		{
			labelControlGroupsTitle.Text = String.Format("Groups: {0}   Users:{1}   Selected:{2}", AllGroups.Count, totalUsers, selectedUsers);
		}

		private void checkEditFilterEnable_CheckedChanged(object sender, EventArgs e)
		{
			EnableFilter = checkEditEnableFilter.Checked;
			checkedListBoxControlGroups.Enabled = EnableFilter;
			buttonXGroupsAll.Enabled = EnableFilter;
			buttonXGroupsNone.Enabled = EnableFilter;
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