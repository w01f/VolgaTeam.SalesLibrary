using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Models.Security;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(BaseLibraryLink))]
	//public partial class SecurityOptions : UserControl, ILinkSetSettingsEditControl
	public partial class SecurityOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private bool _dataLoading;

		private readonly List<BaseLibraryLink> _sourceLinks = new List<BaseLibraryLink>();

		private BaseLibraryLink DefaultLink => _sourceLinks.First();

		private readonly List<LibraryGroup> _securityGroups = new List<LibraryGroup>();
		private readonly List<string> _assignedUsers = new List<string>();
		private readonly List<string> _deniedUsers = new List<string>();

		protected string AssignedUsers
		{
			get
			{
				_assignedUsers.Clear();
				if (checkEditSecurityWhiteList.Checked)
					_assignedUsers.AddRange(_securityGroups
						.Where(g => g.Users != null)
						.SelectMany(g => g.Users)
						.Where(u => u.Selected)
						.Select(u => u.Login));
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

		protected string DeniedUsers
		{
			get
			{
				_deniedUsers.Clear();
				if (checkEditSecurityBlackList.Checked)
					_deniedUsers.AddRange(_securityGroups
						.Where(g => g.Users != null)
						.SelectMany(g => g.Users)
						.Where(u => u.Selected)
						.Select(u => u.Login));
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

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Security };
		public int Order => 0;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public SecurityOptions()
		{
			InitializeComponent();

			gridViewSecurityGroups.MasterRowEmpty += OnGroupChildListIsEmpty;
			gridViewSecurityGroups.MasterRowGetRelationCount += OnGetGroupRelationCount;
			gridViewSecurityGroups.MasterRowGetRelationName += OnGetGroupRelationName;
			gridViewSecurityGroups.MasterRowGetChildList += OnGetGroupChildList;
		}

		public SecurityOptions(FileTypes? defaultLinkType = null) : this() { }

		public SecurityOptions(ILinksGroup linksGroup, FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_sourceLinks.Clear();
			_sourceLinks.Add(sourceLink);

			LoadData();
		}

		public void LoadData(IEnumerable<BaseLibraryLink> sourceLinks)
		{
			_sourceLinks.Clear();
			_sourceLinks.AddRange(sourceLinks);

			LoadData();
		}

		private void LoadData()
		{
			_dataLoading = true;
			checkEditSecurityAllowed.Checked = !DefaultLink.Security.IsRestricted;
			checkEditSecurityDenied.Checked = DefaultLink.Security.IsRestricted &&
				String.IsNullOrEmpty(DefaultLink.Security.AssignedUsers) &&
				String.IsNullOrEmpty(DefaultLink.Security.DeniedUsers);
			checkEditSecurityWhiteList.Checked = DefaultLink.Security.IsRestricted &&
				!String.IsNullOrEmpty(DefaultLink.Security.AssignedUsers);
			checkEditSecurityBlackList.Checked = DefaultLink.Security.IsRestricted &&
				!String.IsNullOrEmpty(DefaultLink.Security.DeniedUsers);
			checkEditSecurityForbidden.Checked = DefaultLink.Security.IsForbidden;
			AssignedUsers = DefaultLink.Security.IsRestricted &&
				!String.IsNullOrEmpty(DefaultLink.Security.AssignedUsers) ? DefaultLink.Security.AssignedUsers : null;
			DeniedUsers = DefaultLink.Security.IsRestricted &&
				!String.IsNullOrEmpty(DefaultLink.Security.DeniedUsers) ? DefaultLink.Security.DeniedUsers : null;
			checkEditSecurityShareLink.Checked = !DefaultLink.Security.NoShare;

			LoadSecurityGroups(DefaultLink.ParentLibrary.ExtId);

			layoutControlGroupSecurityUserList.Enabled = layoutControlGroupSecurityUserList.Enabled && (checkEditSecurityWhiteList.Checked || checkEditSecurityBlackList.Checked);

			_dataLoading = false;
		}

		public void SaveData()
		{
			if (!_sourceLinks.Any()) return;
			var assignedUsers = AssignedUsers;
			var deniedUsers = DeniedUsers;
			foreach (var link in _sourceLinks)
			{
				link.Security.IsRestricted = checkEditSecurityDenied.Checked ||
													checkEditSecurityWhiteList.Checked ||
													checkEditSecurityBlackList.Checked;
				link.Security.IsForbidden = checkEditSecurityForbidden.Checked;
				link.Security.NoShare = checkEditSecurityDenied.Checked;
				if (checkEditSecurityWhiteList.Checked && !String.IsNullOrEmpty(assignedUsers))
					link.Security.AssignedUsers = assignedUsers;
				else
					link.Security.AssignedUsers = null;
				if (checkEditSecurityBlackList.Checked && !String.IsNullOrEmpty(deniedUsers))
					link.Security.DeniedUsers = deniedUsers;
				else
					link.Security.DeniedUsers = null;
			}
		}

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
			if (checkEditSecurityWhiteList.Checked)
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
			if (checkEditSecurityBlackList.Checked)
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
			layoutControlGroupSecurityUserList.Enabled = checkEditSecurityWhiteList.Checked || checkEditSecurityBlackList.Checked;
			if (_dataLoading) return;
			if (!checkEditSecurityWhiteList.Checked)
				_assignedUsers.Clear();
			if (!checkEditSecurityBlackList.Checked)
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
