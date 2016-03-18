using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.RawData
{
	[ToolboxItem(false)]
	public partial class Filter : UserControl
	{
		private bool _init;

		public bool EnableFilter { get; private set; }
		public event EventHandler<EventArgs> FilterChanged;

		public bool ShowDetails
		{
			get { return checkEditShowDetails.Checked; }
		}
		public bool ShowActionGroup
		{
			get { return checkEditShowActionGroup.Checked; }
		}
		public bool ShowAction
		{
			get { return checkEditShowAction.Checked; }
		}
		public bool ShowFile
		{
			get { return checkEditShowFile.Checked; }
		}
		public event EventHandler<EventArgs> ColumnsChanged;

		public List<string> AllGroups { get; private set; }
		public List<string> AllUsers { get; private set; }
		public List<string> SelectedGroups { get; private set; }

		public Filter()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllGroups = new List<string>();
			AllUsers = new List<String>();
			SelectedGroups = new List<string>();
		}

		public void UpdateDataSource(string[] groups, string[] users)
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

			labelControlGroupsTitle.Text = String.Format("Groups: {0}", AllGroups.Count);

			AllUsers.Clear();
			AllUsers.AddRange(users);
			labelControlUsersTitle.Text = String.Format("Users: {0}", AllUsers.Count);

			_init = false;
		}

		private void checkEditFilterEnable_CheckedChanged(object sender, EventArgs e)
		{
			EnableFilter = checkEditEnableFilter.Checked;
			checkedListBoxControlGroups.Enabled = EnableFilter;
			buttonXGroupsAll.Enabled = EnableFilter;
			buttonXGroupsNone.Enabled = EnableFilter;
			if (FilterChanged != null)
				FilterChanged(this, new EventArgs());
		}

		private void checkedListBoxControlGroups_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (_init) return;
			SelectedGroups.Clear();
			foreach (CheckedListBoxItem item in checkedListBoxControlGroups.CheckedItems)
				SelectedGroups.Add(item.Value.ToString());
			if (FilterChanged != null)
				FilterChanged(this, new EventArgs());
		}

		private void buttonXGroupsAll_Click(object sender, EventArgs e)
		{
			_init = true;
			SelectedGroups.Clear();
			SelectedGroups.AddRange(AllGroups);
			checkedListBoxControlGroups.CheckAll();
			if (FilterChanged != null)
				FilterChanged(this, new EventArgs());
			_init = false;
		}

		private void buttonXGroupsNone_Click(object sender, EventArgs e)
		{
			_init = true;
			SelectedGroups.Clear();
			checkedListBoxControlGroups.UnCheckAll();
			if (FilterChanged != null)
				FilterChanged(this, new EventArgs());
			_init = false;
		}

		private void checkEditShowColumns_CheckedChanged(object sender, EventArgs e)
		{
			if (ColumnsChanged != null)
				ColumnsChanged(this, new EventArgs());
		}
	}
}