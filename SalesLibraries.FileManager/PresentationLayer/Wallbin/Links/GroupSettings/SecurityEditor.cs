using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.FileManager.Business.Models;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public partial class SecurityEditor : UserControl, IGroupSettingsEditor
	{
		private bool _loading;
		private readonly List<LibraryGroup> _securityGroups = new List<LibraryGroup>();
		private readonly List<string> _assignedUsers = new List<string>();
		private readonly List<string> _deniedUsers = new List<string>();

		private SelectionManager Selection
		{
			get { return MainController.Instance.WallbinViews.Selection; }
		}

		private string AssignedUsers
		{
			get
			{
				_assignedUsers.Clear();
				if (rbSecurityWhiteList.Checked)
					_assignedUsers.AddRange(_securityGroups.SelectMany(g => g.Users).Where(u => u.Selected).Select(u => u.Login));
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

		private string DeniedUsers
		{
			get
			{
				_deniedUsers.Clear();
				if (rbSecurityBlackList.Checked)
					_deniedUsers.AddRange(_securityGroups.SelectMany(g => g.Users).Where(u => u.Selected).Select(u => u.Login));
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

		#region IGroupSettingsEditor Members
		public string Title
		{
			get { return "Manage Security"; }
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
			DeniedUsers = null;
			Enabled = false;

			var defaultLink = Selection.SelectedFiles.FirstOrDefault(link => link.Security.HasSecuritySettings) ?? Selection.SelectedFiles.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = Selection.SelectedFiles.All(link => !link.Security.HasSecuritySettings);
			var sameData = Selection.SelectedFiles
				.All(link =>
					link.Security.IsRestricted == defaultLink.Security.IsRestricted &&
					link.Security.IsForbidden == defaultLink.Security.IsForbidden &&
					link.Security.AssignedUsers == defaultLink.Security.AssignedUsers &&
					link.Security.DeniedUsers == defaultLink.Security.DeniedUsers &&
					link.Security.NoShare == defaultLink.Security.NoShare);

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				rbSecurityAllowed.Checked = !defaultLink.Security.IsRestricted;
				rbSecurityDenied.Checked = defaultLink.Security.IsRestricted &&
					String.IsNullOrEmpty(defaultLink.Security.AssignedUsers) &&
					String.IsNullOrEmpty(defaultLink.Security.DeniedUsers);
				rbSecurityWhiteList.Checked = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.AssignedUsers);
				rbSecurityBlackList.Checked = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.DeniedUsers);
				rbSecurityForbidden.Checked = defaultLink.Security.IsForbidden;
				ckSecurityShareLink.Checked = defaultLink.Security.NoShare;
				AssignedUsers = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.AssignedUsers) ? defaultLink.Security.AssignedUsers : null;
				DeniedUsers = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.DeniedUsers) ? defaultLink.Security.DeniedUsers : null;
			}

			LoadSecurityGroups(defaultLink.ParentLibrary.ExtId);

			pnSecurityUserList.Enabled = pnSecurityUserList.Enabled && (rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked);
			_loading = false;
		}

		private void ApplyData()
		{
			var assignedUsers = AssignedUsers;
			var deniedUsers = DeniedUsers;
			Selection.SelectedFiles.ApplySecurity(new SecuritySettings
			{
				IsForbidden = rbSecurityForbidden.Checked,
				IsRestricted = rbSecurityDenied.Checked || rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked,
				NoShare = rbSecurityDenied.Checked,
				AssignedUsers = rbSecurityWhiteList.Checked && !String.IsNullOrEmpty(assignedUsers) ? assignedUsers : null,
				DeniedUsers = rbSecurityBlackList.Checked && !String.IsNullOrEmpty(deniedUsers) ? deniedUsers : null
			});
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL SECURITY SETTINGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedFiles.ApplySecurity(new SecuritySettings());
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

			UpdateData();
		}
		#endregion

		private void LoadSecurityGroups(Guid libraryId)
		{
			pnSecurityUserList.Enabled = false;
			rbSecurityWhiteList.Enabled = false;
			rbSecurityBlackList.Enabled = false;
			gridControlSecurityUserList.DataSource = null;
			_securityGroups.Clear();
			_securityGroups.AddRange(MainController.Instance.Lists.Security.GetGroupsByLibrary(libraryId));
			if (_securityGroups.Any())
			{
				gridControlSecurityUserList.DataSource = _securityGroups.ToList();
				ApplyAssignedUsers();
				ApplyDeniedUsers();
				pnSecurityUserList.Enabled = true;
				rbSecurityWhiteList.Enabled = true;
				rbSecurityBlackList.Enabled = true;
			}
		}

		private void ApplyAssignedUsers()
		{
			if (!rbSecurityWhiteList.Checked) return;
			foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
			{
				foreach (var userModel in groupModel.Users)
					userModel.Selected = _assignedUsers.Contains(userModel.Login);
				groupModel.Selected = groupModel.Users.Any(u => u.Selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void ApplyDeniedUsers()
		{
			if (!rbSecurityBlackList.Checked) return;
			foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
			{
				foreach (var userModel in groupModel.Users)
					userModel.Selected = _deniedUsers.Contains(userModel.Login);
				groupModel.Selected = groupModel.Users.Any(u => u.Selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			ResetData();
		}

		private void OnSecurityOptionCheckedChanged(object sender, EventArgs e)
		{
			pnSecurityUserList.Enabled = rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked; ;
			if (_loading) return;
			var radioButton = sender as RadioButton;
			if (radioButton == null || !radioButton.Checked) return;
			if (!rbSecurityWhiteList.Checked)
				_assignedUsers.Clear();
			if (!rbSecurityBlackList.Checked)
				_assignedUsers.Clear();
			ApplyAssignedUsers();
			ApplyDeniedUsers();
			ApplyData();
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
			using (var dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.Filter = "Xml files|*.xml";
				dialog.Title = "Import Users from File";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				var users = MainController.Instance.Lists.Security.LoadUserLoginsFromFile(dialog.FileName).ToList();
				foreach (var groupModel in _securityGroups.Where(g => g.Users != null))
				{
					foreach (var userModel in groupModel.Users)
						userModel.Selected = false;
					foreach (var userModel in groupModel.Users.Where(u => users.Any(loadedUser => loadedUser == u.Login.ToLower())))
						userModel.Selected = true;
					groupModel.Selected = groupModel.Users.Any(u => u.Selected);
				}
				gridControlSecurityUserList.RefreshDataSource();
				MainController.Instance.PopupMessages.ShowInfo("Import Complete");
			}
			ApplyData();
		}

		private void ValueCheckedChanged(object sender, EventArgs e)
		{
			if (!_loading)
				ApplyData();
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
			if (!_loading)
				ApplyData();
		}
	}
}