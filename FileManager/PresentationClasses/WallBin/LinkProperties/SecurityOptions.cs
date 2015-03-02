using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.Services.IPadAdminService;
using Font = System.Drawing.Font;
using LibraryLink = SalesDepot.CoreObjects.BusinessClasses.LibraryLink;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class SecurityOptions : UserControl, ILinkProperties
	public partial class SecurityOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;
		private bool _dataLoading;
		private readonly List<GroupModel> _securityGroups = new List<GroupModel>();

		private readonly List<string> _assignedUsers = new List<string>();
		protected string AssignedUsers
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

		private readonly List<string> _deniedUsers = new List<string>();
		protected string DeniedUsers
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

		public event EventHandler OnForseClose;

		public SecurityOptions(LibraryLink data)
		{
			InitializeComponent();
			_data = data;

			LoadData();

			gridViewSecurityGroups.MasterRowEmpty += OnGroupChildListIsEmpty;
			gridViewSecurityGroups.MasterRowGetRelationCount += OnGetGroupRelationCount;
			gridViewSecurityGroups.MasterRowGetRelationName += OnGetGroupRelationName;
			gridViewSecurityGroups.MasterRowGetChildList += OnGetGroupChildList;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				rbSecurityAllowed.Font = new Font(rbSecurityAllowed.Font.FontFamily, rbSecurityAllowed.Font.Size - 2, rbSecurityAllowed.Font.Style);
				rbSecurityForbidden.Font = new Font(rbSecurityForbidden.Font.FontFamily, rbSecurityForbidden.Font.Size - 2, rbSecurityForbidden.Font.Style);
				rbSecurityDenied.Font = new Font(rbSecurityDenied.Font.FontFamily, rbSecurityDenied.Font.Size - 2, rbSecurityDenied.Font.Style);
				rbSecurityWhiteList.Font = new Font(rbSecurityWhiteList.Font.FontFamily, rbSecurityWhiteList.Font.Size - 2, rbSecurityWhiteList.Font.Style);
				rbSecurityBlackList.Font = new Font(rbSecurityBlackList.Font.FontFamily, rbSecurityBlackList.Font.Size - 2, rbSecurityBlackList.Font.Style);
				ckSecurityShareLink.Font = new Font(ckSecurityShareLink.Font.FontFamily, ckSecurityShareLink.Font.Size - 2, ckSecurityShareLink.Font.Style);
			}
		}

		private void LoadData()
		{
			_dataLoading = true;
			rbSecurityAllowed.Checked = !_data.ExtendedProperties.IsRestricted;
			rbSecurityDenied.Checked = _data.ExtendedProperties.IsRestricted &&
				String.IsNullOrEmpty(_data.ExtendedProperties.AssignedUsers) &&
				String.IsNullOrEmpty(_data.ExtendedProperties.DeniedUsers);
			rbSecurityWhiteList.Checked = _data.ExtendedProperties.IsRestricted &&
				!String.IsNullOrEmpty(_data.ExtendedProperties.AssignedUsers);
			rbSecurityBlackList.Checked = _data.ExtendedProperties.IsRestricted &&
				!String.IsNullOrEmpty(_data.ExtendedProperties.DeniedUsers);
			rbSecurityForbidden.Checked = _data.ExtendedProperties.IsForbidden;
			AssignedUsers = _data.ExtendedProperties.IsRestricted &&
				!String.IsNullOrEmpty(_data.ExtendedProperties.AssignedUsers) ? _data.ExtendedProperties.AssignedUsers : null;
			DeniedUsers = _data.ExtendedProperties.IsRestricted &&
				!String.IsNullOrEmpty(_data.ExtendedProperties.DeniedUsers) ? _data.ExtendedProperties.DeniedUsers : null;
			ckSecurityShareLink.Checked = !_data.ExtendedProperties.NoShare;

			_dataLoading = false;
		}

		public void SaveData()
		{
			_data.ExtendedProperties.IsRestricted = rbSecurityDenied.Checked ||
				rbSecurityWhiteList.Checked ||
				rbSecurityBlackList.Checked;
			_data.ExtendedProperties.IsForbidden = rbSecurityForbidden.Checked;
			_data.ExtendedProperties.NoShare = !ckSecurityShareLink.Checked;
			if (rbSecurityWhiteList.Checked && !String.IsNullOrEmpty(AssignedUsers))
				_data.ExtendedProperties.AssignedUsers = AssignedUsers;
			else
				_data.ExtendedProperties.AssignedUsers = null;
			if (rbSecurityBlackList.Checked && !String.IsNullOrEmpty(DeniedUsers))
				_data.ExtendedProperties.DeniedUsers = DeniedUsers;
			else
				_data.ExtendedProperties.DeniedUsers = null;
		}

		public void LoadSecurityGroups()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Loading Security Groups...";
				formProgress.TopMost = true;
				formProgress.Show();

				rbSecurityWhiteList.Enabled = false;
				rbSecurityBlackList.Enabled = false;
				pnSecurityUserListGrid.Visible = false;
				gridControlSecurityUserList.DataSource = null;
				_securityGroups.Clear();
				laSecurityUserListInfo.Visible = true;
				laSecurityUserListInfo.BringToFront();
				if (!SettingsManager.Instance.WebServiceConnected)
					laSecurityUserListInfo.Text = String.Format("Service coonection is not configured");
				laSecurityUserListInfo.Text = String.Format("Loading user list from {0}...", SettingsManager.Instance.WebServiceSite);
				var message = String.Empty;
				var isCompleted = false;
				Task.Run(() =>
				{
					_securityGroups.AddRange(((SalesDepot.CoreObjects.BusinessClasses.Library)_data.Parent.Parent.Parent).IPadManager.GetGroupsByLibrary(out message));
					isCompleted = true;
				});
				while (!isCompleted)
					Application.DoEvents();
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
					rbSecurityBlackList.Enabled = true;
				}

				formProgress.Close();
			}
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

		private void rbSecurityRestricted_CheckedChanged(object sender, EventArgs e)
		{
			pnSecurityUserListGrid.Enabled = rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked;
			if (_dataLoading) return;
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
