using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.ServiceConnector.AdminService;

namespace SalesLibraries.SiteManager.ToolForms
{
    public partial class FormEditPage : MetroForm
    {
        private readonly List<UserViewModel> _users = new List<UserViewModel>();
        private readonly List<GroupViewModel> _groups = new List<GroupViewModel>();

        public UserViewModel[] AssignedUsers
        {
            get { return _users.Where(x => x.selected).ToArray(); }
        }

        public GroupViewModel[] AssignedGroups
        {
            get { return _groups.Where(x => x.selected).ToArray(); }
        }

        public FormEditPage(UserViewModel[] users, GroupViewModel[] groups)
        {
            InitializeComponent();
            _users.AddRange(users);
            gridControlUsers.DataSource = _users;

            _groups.AddRange(groups);
            gridControlGroups.DataSource = _groups;

            Text = "Edit Page";
        }

        private void FormEditPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
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
