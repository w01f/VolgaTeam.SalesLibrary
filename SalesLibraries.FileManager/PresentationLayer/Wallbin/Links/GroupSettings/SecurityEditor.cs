using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Models.Security;
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

		private SelectionManager Selection => MainController.Instance.WallbinViews.Selection;

		private string AssignedUsers
		{
			get
			{
				_assignedUsers.Clear();
				if (checkEditSecurityWhiteList.Checked)
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
				if (checkEditSecurityBlackList.Checked)
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

			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSecurityUserListSelectAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemSecurityUserListSelectAll.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSecurityUserListSelectAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSecurityUserListSelectAll.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSecurityUserListClearAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemSecurityUserListClearAll.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSecurityUserListClearAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSecurityUserListClearAll.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemImport.MinSize = RectangleHelper.ScaleSize(layoutControlItemImport.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemImport.MaxSize = RectangleHelper.ScaleSize(layoutControlItemImport.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		#region IGroupSettingsEditor Members
		public string Title => "Manage Security";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			_loading = true;
			layoutControlItemReset.Enabled = false;
			layoutControlGroupData.Enabled = false;
			checkEditSecurityAllowed.Checked = true;
			checkEditSecurityShareLink.Checked = true;
			AssignedUsers = null;
			DeniedUsers = null;
			Enabled = false;

			var defaultLink = Selection.SelectedLinks.FirstOrDefault(link => link.Security.HasSecuritySettings) ?? Selection.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = Selection.SelectedLinks.All(link => !link.Security.HasSecuritySettings);
			var sameData = Selection.SelectedLinks
				.All(link =>
					link.Security.IsRestricted == defaultLink.Security.IsRestricted &&
					link.Security.IsForbidden == defaultLink.Security.IsForbidden &&
					link.Security.AssignedUsers == defaultLink.Security.AssignedUsers &&
					link.Security.DeniedUsers == defaultLink.Security.DeniedUsers &&
					link.Security.NoShare == defaultLink.Security.NoShare);

			layoutControlItemReset.Enabled = !noData;
			layoutControlGroupData.Enabled = sameData || noData;

			if (sameData)
			{
				checkEditSecurityAllowed.Checked = !defaultLink.Security.IsRestricted;
				checkEditSecurityDenied.Checked = defaultLink.Security.IsRestricted &&
					String.IsNullOrEmpty(defaultLink.Security.AssignedUsers) &&
					String.IsNullOrEmpty(defaultLink.Security.DeniedUsers);
				checkEditSecurityWhiteList.Checked = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.AssignedUsers);
				checkEditSecurityBlackList.Checked = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.DeniedUsers);
				checkEditSecurityForbidden.Checked = defaultLink.Security.IsForbidden;
				checkEditSecurityShareLink.Checked = defaultLink.Security.NoShare;
				AssignedUsers = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.AssignedUsers) ? defaultLink.Security.AssignedUsers : null;
				DeniedUsers = defaultLink.Security.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.Security.DeniedUsers) ? defaultLink.Security.DeniedUsers : null;
			}

			LoadSecurityGroups(defaultLink.ParentLibrary.ExtId);

			layoutControlGroupSecurityUserList.Enabled = layoutControlGroupSecurityUserList.Enabled && (checkEditSecurityWhiteList.Checked || checkEditSecurityBlackList.Checked);
			_loading = false;
		}

		private void ApplyData()
		{
			var assignedUsers = AssignedUsers;
			var deniedUsers = DeniedUsers;
			Selection.SelectedLinks.ApplySecurity(new SecuritySettings
			{
				IsForbidden = checkEditSecurityForbidden.Checked,
				IsRestricted = checkEditSecurityDenied.Checked || checkEditSecurityWhiteList.Checked || checkEditSecurityBlackList.Checked,
				NoShare = checkEditSecurityDenied.Checked,
				AssignedUsers = checkEditSecurityWhiteList.Checked && !String.IsNullOrEmpty(assignedUsers) ? assignedUsers : null,
				DeniedUsers = checkEditSecurityBlackList.Checked && !String.IsNullOrEmpty(deniedUsers) ? deniedUsers : null
			});
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL SECURITY SETTINGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedLinks.ApplySecurity(new SecuritySettings());
			EditorChanged?.Invoke(this, new EventArgs());

			UpdateData();
		}
		#endregion

		private void LoadSecurityGroups(Guid libraryId)
		{
			layoutControlGroupSecurityUserList.Enabled = false;
			layoutControlItemSecurityWhiteList.Enabled = false;
			layoutControlItemSecurityBlackList.Enabled = false;
			gridControlSecurityUserList.DataSource = null;
			_securityGroups.Clear();

			if (!MainController.Instance.Lists.Security.IsLoaded)
			{
				MainController.Instance.ProcessManager.Run(
					"Loading Site Security Data...",
					(cancelationToken, formProgess) =>
					{
						MainController.Instance.Lists.Security.Load();
					});
			}

			_securityGroups.AddRange(MainController.Instance.Lists.Security.GetGroupsByLibrary(libraryId));
			if (_securityGroups.Any())
			{
				gridControlSecurityUserList.DataSource = _securityGroups.ToList();
				ApplyAssignedUsers();
				ApplyDeniedUsers();
				layoutControlGroupSecurityUserList.Enabled = true;
				layoutControlItemSecurityWhiteList.Enabled = true;
				layoutControlItemSecurityBlackList.Enabled = true;
			}
		}

		private void ApplyAssignedUsers()
		{
			if (!checkEditSecurityWhiteList.Checked) return;
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
			if (!checkEditSecurityBlackList.Checked) return;
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
			layoutControlGroupSecurityUserList.Enabled = checkEditSecurityWhiteList.Checked || checkEditSecurityBlackList.Checked; ;
			if (_loading) return;
			var radioButton = sender as RadioButton;
			if (radioButton == null || !radioButton.Checked) return;
			if (!checkEditSecurityWhiteList.Checked)
				_assignedUsers.Clear();
			if (!checkEditSecurityBlackList.Checked)
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