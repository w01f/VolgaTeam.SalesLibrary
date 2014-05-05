using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.QuizPassData
{
	[ToolboxItem(false)]
	public partial class Filter : UserControl
	{
		private const string AllTopLevelGroups = "All Courses";
		private bool _init;

		public bool EnableFilter { get; private set; }
		public event EventHandler<EventArgs> FilterChanged;

		public List<string> AllGroups { get; private set; }
		public List<string> SelectedGroups { get; private set; }
		public string TopLevelQuizGroup { get; private set; }

		public Filter()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AllGroups = new List<string>();
			SelectedGroups = new List<string>();
		}

		public void UpdateDataSource(IEnumerable<string> groups, IEnumerable<string> topLevelQuizGroups)
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

			TopLevelQuizGroup = String.Empty;
			comboBoxTopLevel.Properties.Items.Clear();
			comboBoxTopLevel.Properties.Items.Add(AllTopLevelGroups);
			comboBoxTopLevel.Properties.Items.AddRange(topLevelQuizGroups.Where(qg => !String.IsNullOrEmpty(qg)).ToArray());
			comboBoxTopLevel.SelectedIndex = 0;

			_init = false;
		}

		private void checkEditFilterEnable_CheckedChanged(object sender, EventArgs e)
		{
			EnableFilter = checkEditEnableFilter.Checked;
			checkedListBoxControlGroups.Enabled = EnableFilter;
			buttonXGroupsAll.Enabled = EnableFilter;
			buttonXGroupsNone.Enabled = EnableFilter;
			comboBoxTopLevel.Enabled = EnableFilter;
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

		private void comboBoxTopLevel_EditValueChanged(object sender, EventArgs e)
		{
			if (_init) return;
			TopLevelQuizGroup = comboBoxTopLevel.SelectedIndex != 0 ? comboBoxTopLevel.EditValue as String : String.Empty;
			if (FilterChanged != null)
				FilterChanged(this, new EventArgs());
		}
	}
}