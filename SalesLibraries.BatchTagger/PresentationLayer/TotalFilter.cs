﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	[ToolboxItem(false)]
	public partial class TotalFilter : UserControl
	{
		private bool _init;

		private bool _enableFilter;
		public bool EnableFilter
		{
			get { return _enableFilter; }
			set
			{
				_init = true;
				_enableFilter = value;
				checkEditEnableFilter.Checked = _enableFilter;
				_init = false;
			}
		}
		public event EventHandler<EventArgs> FilterChanged;

		public List<string> AllGroups { get; private set; }
		public List<string> SelectedGroups { get; private set; }

		public TotalFilter()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllGroups = new List<string>();
			SelectedGroups = new List<string>();
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

			labelControlGroupsTitle.Text = string.Format("Libraries: {0}", AllGroups.Count);

			_init = false;
		}

		private void checkEditFilterEnable_CheckedChanged(object sender, EventArgs e)
		{
			_enableFilter = checkEditEnableFilter.Checked;
			checkedListBoxControlGroups.Enabled = _enableFilter;
			buttonXGroupsAll.Enabled = _enableFilter;
			buttonXGroupsNone.Enabled = _enableFilter;
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