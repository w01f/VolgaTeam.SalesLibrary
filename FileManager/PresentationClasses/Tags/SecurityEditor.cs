using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using FileManager.BusinessClasses;
using FileManager.Controllers;
using SalesDepot.Services.IPadAdminService;
using Font = System.Drawing.Font;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class SecurityEditor : UserControl, ITagsEditor
	{
		private bool _loading;
		private readonly List<LibraryGroup> _securityGroups = new List<LibraryGroup>();
		private readonly List<string> _assignedUsers = new List<string>();
		private readonly List<string> _deniedUsers = new List<string>();

		private string AssignedUsers
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

		private string DeniedUsers
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

			_securityGroups.AddRange(ListManager.Instance.SecurityGroups.Groups);
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
			DeniedUsers = null;
			Enabled = false;

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault(link => link.ExtendedProperties.IsRestricted ||
					link.ExtendedProperties.NoShare ||
					link.ExtendedProperties.IsForbidden) ?? 
				activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = activePage.SelectedLinks.All(link => !(link.ExtendedProperties.IsRestricted ||
				link.ExtendedProperties.NoShare || 
				link.ExtendedProperties.IsForbidden));
			var sameData = defaultLink != null && activePage.SelectedLinks
				.All(x => x.ExtendedProperties.IsRestricted == defaultLink.ExtendedProperties.IsRestricted && 
					x.ExtendedProperties.IsForbidden == defaultLink.ExtendedProperties.IsForbidden && 
					x.ExtendedProperties.AssignedUsers == defaultLink.ExtendedProperties.AssignedUsers && 
					x.ExtendedProperties.DeniedUsers == defaultLink.ExtendedProperties.DeniedUsers && 
					x.ExtendedProperties.NoShare == defaultLink.ExtendedProperties.NoShare);

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				rbSecurityAllowed.Checked = !defaultLink.ExtendedProperties.IsRestricted;
				rbSecurityDenied.Checked = defaultLink.ExtendedProperties.IsRestricted &&
					String.IsNullOrEmpty(defaultLink.ExtendedProperties.AssignedUsers) &&
					String.IsNullOrEmpty(defaultLink.ExtendedProperties.DeniedUsers);
				rbSecurityWhiteList.Checked = defaultLink.ExtendedProperties.IsRestricted && 
					!String.IsNullOrEmpty(defaultLink.ExtendedProperties.AssignedUsers);
				rbSecurityBlackList.Checked = defaultLink.ExtendedProperties.IsRestricted && 
					!String.IsNullOrEmpty(defaultLink.ExtendedProperties.DeniedUsers);
				rbSecurityForbidden.Checked = defaultLink.ExtendedProperties.IsForbidden;
				ckSecurityShareLink.Checked = defaultLink.ExtendedProperties.NoShare;
				AssignedUsers = defaultLink.ExtendedProperties.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.ExtendedProperties.AssignedUsers) ? defaultLink.ExtendedProperties.AssignedUsers : null;
				DeniedUsers = defaultLink.ExtendedProperties.IsRestricted &&
					!String.IsNullOrEmpty(defaultLink.ExtendedProperties.DeniedUsers) ? defaultLink.ExtendedProperties.DeniedUsers : null;
			}

			gridControlSecurityUserList.DataSource = _securityGroups;
			ApplyAssignedUsers();
			ApplyDeniedUsers();
		}

		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			var assignedUsers = AssignedUsers;
			var deniedUsers = DeniedUsers;
			foreach (var link in activePage.SelectedLinks)
			{
				link.ExtendedProperties.IsForbidden = rbSecurityForbidden.Checked;
				link.ExtendedProperties.IsRestricted = rbSecurityDenied.Checked || rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked;
				link.ExtendedProperties.NoShare = !ckSecurityShareLink.Checked;
				if (rbSecurityWhiteList.Checked && !String.IsNullOrEmpty(assignedUsers))
					link.ExtendedProperties.AssignedUsers = assignedUsers;
				else
					link.ExtendedProperties.AssignedUsers = null;
				if (rbSecurityBlackList.Checked && !String.IsNullOrEmpty(deniedUsers))
					link.ExtendedProperties.DeniedUsers = deniedUsers;
				else
					link.ExtendedProperties.DeniedUsers = null;
			}
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		public void ResetData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure You want to DELETE ALL SECURITY SETTINGS for the selected files?") != DialogResult.Yes) return;
			foreach (var link in activePage.SelectedLinks)
			{
				link.ExtendedProperties.NoShare = false;
				link.ExtendedProperties.IsForbidden = false;
				link.ExtendedProperties.IsRestricted = false;
				link.ExtendedProperties.AssignedUsers = null;
				link.ExtendedProperties.DeniedUsers = null;
			}
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

			UpdateData();
		}
		#endregion

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

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			ResetData();
		}

		private void rbSecurityRestricted_CheckedChanged(object sender, EventArgs e)
		{
			pnSecurityUserListGrid.Enabled = rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked; ;
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
			var library = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.Library : null;
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

		private void ValueCheckedChanged(object sender, EventArgs e)
		{
			if (!_loading)
				NeedToApply = true;
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
			if (!_loading)
				NeedToApply = true;
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