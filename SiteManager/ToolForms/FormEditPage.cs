using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.Services.IPadAdminService;

namespace SalesDepot.SiteManager.ToolForms
{
	public partial class FormEditPage : Form
	{
		private List<UserModel> _users = new List<UserModel>();
		private List<GroupModel> _groups = new List<GroupModel>();
		
		public UserModel[] AssignedUsers
		{
			get { return _users.Where(x => x.selected).ToArray(); }
		}

		public GroupModel[] AssignedGroups
		{
			get { return _groups.Where(x => x.selected).ToArray(); }
		}

		public FormEditPage(UserModel[] users, GroupModel[] groups)
		{
			InitializeComponent();
			_users.AddRange(users);
			gridControlUsers.DataSource = _users;

			_groups.AddRange(groups);
			gridControlGroups.DataSource = _groups;

			this.Text = "Edit Page";
		}

		private void FormEditPage_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult == DialogResult.OK)
			{
				gridViewGroups.PostEditor();
				gridViewUsers.PostEditor();
			}
		}

		#region Users
		private void buttonXUsersSelectAll_Click(object sender, EventArgs e)
		{
			foreach (var user in _users)
				user.selected = true;
			gridViewUsers.RefreshData();
		}

		private void buttonXUsersClearAll_Click(object sender, EventArgs e)
		{
			foreach (var user in _users)
				user.selected = false;
			gridViewUsers.RefreshData();
		}
		#endregion

		#region Groups
		private void buttonXGroupsSelectAll_Click(object sender, EventArgs e)
		{
			foreach (var group in _groups)
				group.selected = true;
			gridViewGroups.RefreshData();
		}

		private void buttonXGroupsClearAll_Click(object sender, EventArgs e)
		{
			foreach (var group in _groups)
				group.selected = false;
			gridViewGroups.RefreshData();
		}
		#endregion

	}
}
