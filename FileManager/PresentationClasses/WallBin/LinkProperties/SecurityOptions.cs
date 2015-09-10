using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using FileManager.BusinessClasses;
using Font = System.Drawing.Font;
using LibraryLink = SalesDepot.CoreObjects.BusinessClasses.LibraryLink;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class SecurityOptions : UserControl, ILinkProperties
	public partial class SecurityOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;
		private bool _dataLoading;
		private readonly List<LibraryGroup> _securityGroups = new List<LibraryGroup>();

		private readonly List<string> _assignedUsers = new List<string>();
		protected string AssignedUsers
		{
			get
			{
				_assignedUsers.Clear();
				if (rbSecurityWhiteList.Checked)
					_assignedUsers.AddRange(_securityGroups.Where(g => g.Users != null).SelectMany(g => g.Users).Where(u => u.Selected).Select(u => u.Login));
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
					_deniedUsers.AddRange(_securityGroups.Where(g => g.Users != null).SelectMany(g => g.Users).Where(u => u.Selected).Select(u => u.Login));
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

			_securityGroups.AddRange(ListManager.Instance.SecurityGroups.Groups);

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

			gridControlSecurityUserList.DataSource = _securityGroups;
			ApplyAssignedUsers();
			ApplyDeniedUsers();

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

		private void ApplyAssignedUsers()
		{
			if (rbSecurityWhiteList.Checked)
			{
				foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
				{
					foreach (var userModel in groupModel.Users)
						userModel.Selected = _assignedUsers.Contains(userModel.Login);
					groupModel.Selected = groupModel.Users.Any(u => u.Selected);
				}
				gridControlSecurityUserList.RefreshDataSource();
			}
		}

		private void ApplyDeniedUsers()
		{
			if (rbSecurityBlackList.Checked)
			{
				foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
				{
					foreach (var userModel in groupModel.Users)
						userModel.Selected = _deniedUsers.Contains(userModel.Login);
					groupModel.Selected = groupModel.Users.Any(u => u.Selected);
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
			foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
			{
				foreach (var userModel in groupModel.Users)
					userModel.Selected = true;
				groupModel.Selected = groupModel.Users.Any(u => u.Selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void buttonXSecurityUserListClearAll_Click(object sender, EventArgs e)
		{
			foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
			{
				foreach (var userModel in groupModel.Users)
					userModel.Selected = false;
				groupModel.Selected = groupModel.Users.Any(u => u.Selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void buttonXImport_Click(object sender, EventArgs e)
		{
			var library = (SalesDepot.CoreObjects.BusinessClasses.Library)_data.Parent.Parent.Parent;
			if (library == null) return;
			using (var dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.Filter = "Xml files|*.xml";
				dialog.Title = "Import Users from File";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				var users = ServiceConnector.Instance.LoadUserLoginsFromFile(dialog.FileName).ToList();
				foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
				{
					foreach (var userModel in groupModel.Users)
						userModel.Selected = false;
					foreach (var userModel in groupModel.Users.Where(u => users.Any(loadedUser => loadedUser == u.Login.ToLower())))
						userModel.Selected = true;
					groupModel.Selected = groupModel.Users.Any(u => u.Selected);
				}
				gridControlSecurityUserList.RefreshDataSource();
				AppManager.Instance.ShowInfo("Import Complete");
			}
		}

		private void OnGroupChildListIsEmpty(object sender, MasterRowEmptyEventArgs e)
		{
			e.IsEmpty = !(e.RowHandle != GridControl.InvalidRowHandle && _securityGroups[e.RowHandle].Users != null && _securityGroups[e.RowHandle].Users.Any());
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
			if (e.RowHandle != GridControl.InvalidRowHandle && _securityGroups[e.RowHandle].Users != null)
				e.ChildList = _securityGroups[e.RowHandle].Users.ToArray();
		}

		private void RepositoryItemCheckEditCheckedChanged(object sender, EventArgs e)
		{
			var focussedView = gridControlSecurityUserList.FocusedView as GridView;
			if (focussedView == null) return;
			focussedView.CloseEditor();
			if (focussedView == gridViewSecurityGroups)
			{
				if (focussedView.FocusedRowHandle == GridControl.InvalidRowHandle) return;
				var groupModel = focussedView.GetFocusedRow() as LibraryGroup;
				if (groupModel == null) return;
				if (groupModel.Users == null) return;
				foreach (var userModel in groupModel.Users)
					userModel.Selected = groupModel.Selected;
				var usersView = focussedView.GetDetailView(focussedView.FocusedRowHandle, 0) as GridView;
				if (usersView != null)
					usersView.RefreshData();
			}
			else
			{
				var groupModel = focussedView.SourceRow as LibraryGroup;
				var userModel = focussedView.GetFocusedRow() as LibraryUser;
				if (groupModel == null || userModel == null || !userModel.Selected) return;
				groupModel.Selected = userModel.Selected;
				gridControlSecurityUserList.MainView.RefreshData();
			}
		}
	}
}
