using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using SalesDepot.Services.IPadAdminService;
using Font = System.Drawing.Font;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class SecurityEditor : UserControl, ITagsEditor
	{
		private bool _loading;
		private bool _securityGroupsLoaded;
		private readonly List<GroupModel> _securityGroups = new List<GroupModel>();
		private readonly List<string> _assignedUsers = new List<string>();
		private readonly List<string> _deniedUsers = new List<string>();

		private string AssignedUsers
		{
			get
			{
				_assignedUsers.Clear();
				if (rbSecurityWhiteList.Checked)
					_assignedUsers.AddRange(_securityGroups.Where(g => g.users != null).SelectMany(g => g.users).Where(u => u.selected).Select(u => u.login));
				return String.Join(",", _assignedUsers);
			}
			set
			{
				_assignedUsers.Clear();
				if (!String.IsNullOrEmpty(value))
					_assignedUsers.AddRange(value.Split(',').Select(item => item.Trim()));
				ApplyAssignedUsers();
			}
		}

		public string DeniedUsers
		{
			get
			{
				_deniedUsers.Clear();
				if (rbSecurityBlackList.Checked)
					_deniedUsers.AddRange(_securityGroups.Where(g => g.users != null).SelectMany(g => g.users).Where(u => u.selected).Select(u => u.login));
				return String.Join(",", _deniedUsers);
			}
			set
			{
				_deniedUsers.Clear();
				if (!String.IsNullOrEmpty(value))
					_deniedUsers.AddRange(value.Split(',').Select(item => item.Trim()));
				ApplyDeniedUsers();
			}
		}

		public SecurityEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			gridViewSecurityGroups.MasterRowEmpty += OnGroupChildListIsEmpty;
			gridViewSecurityGroups.MasterRowGetRelationCount += OnGetGroupRelationCount;
			gridViewSecurityGroups.MasterRowGetRelationName += OnGetGroupRelationName;
			gridViewSecurityGroups.MasterRowGetChildList += OnGetGroupChildList;

			if (!((CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
			rbSecurityAllowed.Font = new Font(rbSecurityAllowed.Font.FontFamily, rbSecurityAllowed.Font.Size - 2, rbSecurityAllowed.Font.Style);
			rbSecurityDenied.Font = new Font(rbSecurityDenied.Font.FontFamily, rbSecurityDenied.Font.Size - 2, rbSecurityDenied.Font.Style);
			rbSecurityWhiteList.Font = new Font(rbSecurityWhiteList.Font.FontFamily, rbSecurityWhiteList.Font.Size - 2, rbSecurityWhiteList.Font.Style);
			rbSecurityBlackList.Font = new Font(rbSecurityBlackList.Font.FontFamily, rbSecurityBlackList.Font.Size - 2, rbSecurityBlackList.Font.Style);
			rbSecurityForbidden.Font = new Font(rbSecurityForbidden.Font.FontFamily, rbSecurityForbidden.Font.Size - 2, rbSecurityForbidden.Font.Style);
			ckSecurityShareLink.Font = new Font(ckSecurityShareLink.Font.FontFamily, ckSecurityShareLink.Font.Size - 2, ckSecurityShareLink.Font.Style);
		}

		#region ITagsEditor Members
		private bool _needToApply;
		public bool NeedToApply
		{
			get { return _needToApply; }
			set
			{
				_needToApply = value;
				var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
				if (activePage != null) activePage.Parent.StateChanged = true;
			}
		}
		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			_loading = true;
			pnButtons.Enabled = false;
			pnData.Enabled = false;
			rbSecurityAllowed.Checked = true;
			ckSecurityShareLink.Checked = true;
			AssignedUsers = null;
			Enabled = false;

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = activePage.SelectedLinks.All(x => !x.IsRestricted && !x.NoShare && !x.IsForbidden);
			var sameData = defaultLink != null && activePage.SelectedLinks.All(x => x.IsRestricted == defaultLink.IsRestricted && x.IsForbidden == defaultLink.IsForbidden && x.AssignedUsers == defaultLink.AssignedUsers && x.DeniedUsers == defaultLink.DeniedUsers && x.NoShare == defaultLink.NoShare);

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				rbSecurityAllowed.Checked = !defaultLink.IsRestricted;
				rbSecurityDenied.Checked = defaultLink.IsRestricted && string.IsNullOrEmpty(defaultLink.AssignedUsers) && string.IsNullOrEmpty(defaultLink.DeniedUsers);
				rbSecurityWhiteList.Checked = defaultLink.IsRestricted && !string.IsNullOrEmpty(defaultLink.AssignedUsers);
				rbSecurityBlackList.Checked = defaultLink.IsRestricted && !string.IsNullOrEmpty(defaultLink.DeniedUsers);
				rbSecurityForbidden.Checked = defaultLink.IsForbidden;
				ckSecurityShareLink.Checked = defaultLink.NoShare;
				AssignedUsers = defaultLink.IsRestricted && !string.IsNullOrEmpty(defaultLink.AssignedUsers) ? defaultLink.AssignedUsers : null;
				DeniedUsers = defaultLink.IsRestricted && !string.IsNullOrEmpty(defaultLink.AssignedUsers) ? defaultLink.AssignedUsers : null;
			}

			if (!_securityGroupsLoaded)
				LoadSecurityGroups();
			else
			{
				ApplyAssignedUsers();
				ApplyDeniedUsers();
				_loading = false;
			}
		}

		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			var assignedUsers = AssignedUsers;
			var deniedUsers = DeniedUsers;
			foreach (var link in activePage.SelectedLinks)
			{
				link.IsForbidden = rbSecurityForbidden.Checked;
				link.IsRestricted = rbSecurityDenied.Checked || rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked;
				link.NoShare = !ckSecurityShareLink.Checked;
				if (rbSecurityWhiteList.Checked && !String.IsNullOrEmpty(assignedUsers))
					link.AssignedUsers = assignedUsers;
				else
					link.AssignedUsers = null;
				if (rbSecurityBlackList.Checked && !String.IsNullOrEmpty(deniedUsers))
					link.DeniedUsers = deniedUsers;
				else
					link.DeniedUsers = null;
			}
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}
		#endregion

		private void LoadSecurityGroups()
		{
			rbSecurityWhiteList.Enabled = false;
			pnSecurityUserListGrid.Visible = false;
			gridControlSecurityUserList.DataSource = null;
			_securityGroups.Clear();
			laSecurityUserListInfo.Visible = true;
			laSecurityUserListInfo.BringToFront();
			var library = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.Library : null;
			if (!SettingsManager.Instance.WebServiceConnected)
				laSecurityUserListInfo.Text = String.Format("Service coonection is not configured");
			circularSecurityUserListProgress.Visible = true;
			circularSecurityUserListProgress.BringToFront();
			laSecurityUserListInfo.Text = String.Format("Loading user list from {0}...", SettingsManager.Instance.WebServiceSite);
			Application.DoEvents();
			var message = String.Empty;
			var thread = new Thread(() =>
			{
				_securityGroups.AddRange(library.IPadManager.GetGroupsByLibrary(out message));
				Invoke((MethodInvoker)delegate
				{
					circularSecurityUserListProgress.Visible = false;
					if (!String.IsNullOrEmpty(message))
						laSecurityUserListInfo.Text = String.Format("Couldn't load user list from {0}", SettingsManager.Instance.WebServiceSite);
					else if (!_securityGroups.Any())
						laSecurityUserListInfo.Text = String.Format("There is no users on {0}", SettingsManager.Instance.WebServiceSite);
					else
					{
						laSecurityUserListInfo.Visible = false;
						pnSecurityUserListGrid.Visible = true;
						gridControlSecurityUserList.DataSource = _securityGroups.Where(g => g.users != null).ToList();
						ApplyAssignedUsers();
						ApplyDeniedUsers();
						rbSecurityWhiteList.Enabled = true;
					}
				});
				_securityGroupsLoaded = true;
				_loading = false;
			});
			thread.Start();
		}

		private void ApplyAssignedUsers()
		{
			if (rbSecurityWhiteList.Checked)
			{
				foreach (var groupModel in _securityGroups.Where(g => g.users != null))
				{
					foreach (var userModel in groupModel.users)
						userModel.selected = _assignedUsers.Contains(userModel.login);
					groupModel.selected = groupModel.users.Any(u => u.selected);
				}
				gridControlSecurityUserList.RefreshDataSource();
			}
		}

		private void ApplyDeniedUsers()
		{
			if (rbSecurityBlackList.Checked)
			{
				foreach (var groupModel in _securityGroups.Where(g => g.users != null))
				{
					foreach (var userModel in groupModel.users)
						userModel.selected = _deniedUsers.Contains(userModel.login);
					groupModel.selected = groupModel.users.Any(u => u.selected);
				}
				gridControlSecurityUserList.RefreshDataSource();
			}
		}

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure You want to DELETE ALL KEYWORD TAGS for the selected files?") != DialogResult.Yes) return;
			foreach (var link in activePage.SelectedLinks)
			{
				link.NoShare = false;
				link.IsForbidden = false;
				link.IsRestricted = false;
				link.AssignedUsers = null;
			}
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

			UpdateData();
		}

		private void rbSecurityRestricted_CheckedChanged(object sender, EventArgs e)
		{
			pnSecurityUserListGrid.Enabled = rbSecurityWhiteList.Checked;
			if (_loading) return;
			NeedToApply = true;
			if (!rbSecurityWhiteList.Checked)
				_assignedUsers.Clear();
			if (!rbSecurityBlackList.Checked)
				_assignedUsers.Clear();
			ApplyAssignedUsers();
			ApplyDeniedUsers();
		}

		private void buttonXSecurityUserListSelectAll_Click(object sender, EventArgs e)
		{
			foreach (var groupModel in _securityGroups.Where(g => g.users != null))
			{
				foreach (var userModel in groupModel.users)
					userModel.selected = true;
				groupModel.selected = groupModel.users.Any(u => u.selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void buttonXSecurityUserListClearAll_Click(object sender, EventArgs e)
		{
			foreach (var groupModel in _securityGroups.Where(g => g.users != null))
			{
				foreach (var userModel in groupModel.users)
					userModel.selected = false;
				groupModel.selected = groupModel.users.Any(u => u.selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void ValueCheckedChanged(object sender, EventArgs e)
		{
			if (!_loading)
				NeedToApply = true;
		}

		private void OnGroupChildListIsEmpty(object sender, MasterRowEmptyEventArgs e)
		{
			e.IsEmpty = !(e.RowHandle != GridControl.InvalidRowHandle && _securityGroups[e.RowHandle].users != null && _securityGroups[e.RowHandle].users.Any());
		}

		private void OnGetGroupRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
		{
			e.RelationCount = 1;
		}

		private void OnGetGroupRelationName(object sender, MasterRowGetRelationNameEventArgs e)
		{
			e.RelationName = "Users";
		}

		private void OnGetGroupChildList(object sender, MasterRowGetChildListEventArgs e)
		{
			if (e.RowHandle != GridControl.InvalidRowHandle && _securityGroups[e.RowHandle].users != null)
				e.ChildList = _securityGroups[e.RowHandle].users.ToArray();
		}

		private void RepositoryItemCheckEditCheckedChanged(object sender, EventArgs e)
		{
			if (!_loading)
				NeedToApply = true;
			var focussedView = gridControlSecurityUserList.FocusedView as GridView;
			if (focussedView == null) return;
			focussedView.CloseEditor();
			if (focussedView == gridViewSecurityGroups)
			{
				if (focussedView.FocusedRowHandle == GridControl.InvalidRowHandle) return;
				var groupModel = focussedView.GetFocusedRow() as GroupModel;
				if (groupModel == null) return;
				if (groupModel.users == null) return;
				foreach (var userModel in groupModel.users)
					userModel.selected = groupModel.selected;
				var usersView = focussedView.GetDetailView(focussedView.FocusedRowHandle, 0) as GridView;
				if (usersView != null)
					usersView.RefreshData();
			}
			else
			{
				var groupModel = focussedView.SourceRow as GroupModel;
				var userModel = focussedView.GetFocusedRow() as UserModel;
				if (groupModel == null || userModel == null || !userModel.selected) return;
				groupModel.selected = userModel.selected;
				gridControlSecurityUserList.MainView.RefreshData();
			}
		}
	}
}