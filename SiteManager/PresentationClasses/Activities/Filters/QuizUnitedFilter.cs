﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.Filters
{
	[ToolboxItem(false)]
	public partial class QuizUnitedFilter : UserControl
	{
		private bool _init;

		public bool EnableFilter { get; private set; }
		public event EventHandler<EventArgs> FilterChanged;

		public event EventHandler<EventArgs> CollapsedAll;
		public event EventHandler<EventArgs> ExpandedAll;

		public List<string> AllGroups { get; private set; }
		public List<string> SelectedGroups { get; private set; }

		public QuizUnitedFilter()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllGroups = new List<string>();
			SelectedGroups = new List<string>();
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

			labelControlGroupsTitle.Text = string.Format("Groups: {0}", AllGroups.Count);

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

		private void checkedListBoxControlGroups_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
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

		private void buttonXCollapseAll_Click(object sender, EventArgs e)
		{
			if (CollapsedAll != null)
				CollapsedAll(this, new EventArgs());
		}

		private void buttonXExpandAll_Click(object sender, EventArgs e)
		{
			if (ExpandedAll != null)
				ExpandedAll(this, new EventArgs());
		}
	}
}