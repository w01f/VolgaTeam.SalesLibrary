﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Business.Models.Security;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(BaseLibraryLink))]
	//public partial class SecurityOptions : UserControl, ILinkSettingsEditControl
	public partial class SecurityOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly BaseLibraryLink _data;
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

		public LinkSettingsType SettingsType => LinkSettingsType.Security;
		public int Order => 0;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public SecurityOptions(BaseLibraryLink data)
		{
			InitializeComponent();
			_data = data;

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

		public void LoadData()
		{
			_dataLoading = true;
			rbSecurityAllowed.Checked = !_data.Security.IsRestricted;
			rbSecurityDenied.Checked = _data.Security.IsRestricted &&
				String.IsNullOrEmpty(_data.Security.AssignedUsers) &&
				String.IsNullOrEmpty(_data.Security.DeniedUsers);
			rbSecurityWhiteList.Checked = _data.Security.IsRestricted &&
				!String.IsNullOrEmpty(_data.Security.AssignedUsers);
			rbSecurityBlackList.Checked = _data.Security.IsRestricted &&
				!String.IsNullOrEmpty(_data.Security.DeniedUsers);
			rbSecurityForbidden.Checked = _data.Security.IsForbidden;
			AssignedUsers = _data.Security.IsRestricted &&
				!String.IsNullOrEmpty(_data.Security.AssignedUsers) ? _data.Security.AssignedUsers : null;
			DeniedUsers = _data.Security.IsRestricted &&
				!String.IsNullOrEmpty(_data.Security.DeniedUsers) ? _data.Security.DeniedUsers : null;
			ckSecurityShareLink.Checked = !_data.Security.NoShare;

			LoadSecurityGroups(_data.ParentLibrary.ExtId);

			pnSecurityUserList.Enabled = pnSecurityUserList.Enabled && (rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked);

			_dataLoading = false;
		}

		public void SaveData()
		{
			_data.Security.IsRestricted = rbSecurityDenied.Checked ||
				rbSecurityWhiteList.Checked ||
				rbSecurityBlackList.Checked;
			_data.Security.IsForbidden = rbSecurityForbidden.Checked;
			_data.Security.NoShare = rbSecurityDenied.Checked;
			if (rbSecurityWhiteList.Checked && !String.IsNullOrEmpty(AssignedUsers))
				_data.Security.AssignedUsers = AssignedUsers;
			else
				_data.Security.AssignedUsers = null;
			if (rbSecurityBlackList.Checked && !String.IsNullOrEmpty(DeniedUsers))
				_data.Security.DeniedUsers = DeniedUsers;
			else
				_data.Security.DeniedUsers = null;
		}

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
			pnSecurityUserList.Enabled = rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked;
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