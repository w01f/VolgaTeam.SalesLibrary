﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Security;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.AdminService;
using SalesLibraries.SiteManager.ToolClasses;

namespace SalesLibraries.SiteManager.ToolForms
{
	public partial class FormEditUser : MetroForm
	{
		private readonly bool _newUser;
		private readonly bool _complexPassword = true;
		private readonly List<string> _existedUsers = new List<string>();

		private readonly List<GroupModel> _groups = new List<GroupModel>();
		private readonly List<SoapLibrary> _libraries = new List<SoapLibrary>();
		private readonly List<SoapLibraryPage> _pages = new List<SoapLibraryPage>();

		public GroupModel[] AssignedGroups
		{
			get { return _groups.Where(x => x.selected).ToArray(); }
		}

		public SoapLibraryPage[] AssignedPages
		{
			get { return _pages.Where(x => x.selected).ToArray(); }
		}

		public FormEditUser(bool newUser, bool complexPassword, string[] existedUsers, GroupModel[] groups, SoapLibrary[] libraries)
		{
			InitializeComponent();

			_newUser = newUser;
			_complexPassword = complexPassword;
			_existedUsers.AddRange(existedUsers);

			_groups.AddRange(groups);
			gridControlGroups.DataSource = _groups;

			_libraries.Clear();
			_libraries.AddRange(libraries);
			_pages.Clear();
			_pages.AddRange(libraries.SelectMany(x => x.pages));

			gridViewLibraries.MasterRowEmpty += OnLibraryChildListIsEmpty;
			gridViewLibraries.MasterRowGetRelationCount += OnGetLibraryRelationCount;
			gridViewLibraries.MasterRowGetRelationName += OnGetLibrariesRelationName;
			gridViewLibraries.MasterRowGetChildList += OnGetLibraryChildList;
			gridControlLibraries.DataSource = _libraries;

			textEditLogin.Enter += FormMain.Instance.Editor_Enter;
			textEditLogin.MouseUp += FormMain.Instance.Editor_MouseUp;
			textEditLogin.MouseDown += FormMain.Instance.Editor_MouseDown;
			textEditFirstName.Enter += FormMain.Instance.Editor_Enter;
			textEditFirstName.MouseUp += FormMain.Instance.Editor_MouseUp;
			textEditFirstName.MouseDown += FormMain.Instance.Editor_MouseDown;
			textEditLastName.Enter += FormMain.Instance.Editor_Enter;
			textEditLastName.MouseUp += FormMain.Instance.Editor_MouseUp;
			textEditLastName.MouseDown += FormMain.Instance.Editor_MouseDown;
			textEditPhone.Enter += FormMain.Instance.Editor_Enter;
			textEditPhone.MouseUp += FormMain.Instance.Editor_MouseUp;
			textEditPhone.MouseDown += FormMain.Instance.Editor_MouseDown;
			textEditEmail.Enter += FormMain.Instance.Editor_Enter;
			textEditEmail.MouseUp += FormMain.Instance.Editor_MouseUp;
			textEditEmail.MouseDown += FormMain.Instance.Editor_MouseDown;
			textEditEmailConfirm.Enter += FormMain.Instance.Editor_Enter;
			textEditEmailConfirm.MouseUp += FormMain.Instance.Editor_MouseUp;
			textEditEmailConfirm.MouseDown += FormMain.Instance.Editor_MouseDown;
			buttonEditPassword.Enter += FormMain.Instance.Editor_Enter;
			buttonEditPassword.MouseUp += FormMain.Instance.Editor_MouseUp;
			buttonEditPassword.MouseDown += FormMain.Instance.Editor_MouseDown;

			if (_newUser)
			{
				Text = "Add User";
				checkEditPassword.Visible = false;
				laPassword.Visible = true;
				textEditLogin.Enabled = true;
				buttonEditPassword_ButtonClick(null, null);
			}
			else
			{
				Text = "Edit User";
				checkEditPassword.Visible = true;
				checkEditPassword.Checked = false;
				laPassword.Visible = false;
				textEditLogin.Enabled = false;
			}
		}

		private void FormEditUser_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				gridViewGroups.PostEditor();
				gridViewLibraries.PostEditor();
				gridViewPages.PostEditor();
				textEditLogin.Focus();
				textEditFirstName.Focus();
				textEditLastName.Focus();
				textEditPhone.Focus();
				textEditEmail.Focus();
				textEditEmailConfirm.Focus();
				buttonEditPassword.Focus();
				if (!ValidateChildren())
					e.Cancel = true;
				else if (_newUser && textEditLogin.EditValue != null && _existedUsers.Contains(textEditLogin.EditValue.ToString()))
				{
					if (AppManager.Instance.ShowWarningQuestion("User with given login already exist.\nDo you want to update his data?") == DialogResult.No)
						e.Cancel = true;
				}
			}
		}

		#region User
		private void checkEditPassword_CheckedChanged(object sender, EventArgs e)
		{
			buttonEditPassword.Enabled = checkEditPassword.Checked;
			if (!checkEditPassword.Checked)
				buttonEditPassword.EditValue = null;
		}

		private void textEdit_Validating(object sender, CancelEventArgs e)
		{
			BaseEdit edit = sender as BaseEdit;
			if (edit != null && edit.Enabled && string.IsNullOrEmpty(edit.Text))
			{
				edit.ErrorText = "Empty value is not allowed";
				e.Cancel = true;
			}
		}

		private void textEditEmail_Validating(object sender, CancelEventArgs e)
		{
			string email = textEditEmail.EditValue != null ? textEditEmail.EditValue.ToString() : string.Empty;
			string emailConfirm = textEditEmailConfirm.EditValue != null ? textEditEmailConfirm.EditValue.ToString() : string.Empty;
			if (!email.Equals(emailConfirm))
			{
				textEditEmail.ErrorText = textEditEmailConfirm.ErrorText = "Email and confirmation field have different values";
				e.Cancel = true;
			}
		}

		private void buttonEditPassword_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			buttonEditPassword.EditValue = _complexPassword ? Membership.GeneratePassword(10, 3) : new PasswordGenerator().Generate();
		}
		#endregion

		#region Libraraies
		private void buttonXLibrariesSelectAll_Click(object sender, EventArgs e)
		{
			foreach (var library in _libraries)
				library.selected = true;
			foreach (var page in _pages)
				page.selected = true;
			gridViewLibraries.RefreshData();
			if (gridViewLibraries.FocusedRowHandle != GridControl.InvalidRowHandle && gridViewLibraries.GetDetailView(gridViewLibraries.FocusedRowHandle, 0) != null)
				gridViewLibraries.GetDetailView(gridViewLibraries.FocusedRowHandle, 0).RefreshData();
		}

		private void buttonXLibrariesClearAll_Click(object sender, EventArgs e)
		{
			foreach (var library in _libraries)
				library.selected = false;
			foreach (var page in _pages)
				page.selected = false;
			gridViewLibraries.RefreshData();
			if (gridViewLibraries.FocusedRowHandle != GridControl.InvalidRowHandle && gridViewLibraries.GetDetailView(gridViewLibraries.FocusedRowHandle, 0) != null)
				gridViewLibraries.GetDetailView(gridViewLibraries.FocusedRowHandle, 0).RefreshData();
		}

		#region Grid Events
		private void OnLibraryChildListIsEmpty(object sender, MasterRowEmptyEventArgs e)
		{
			e.IsEmpty = !(e.RowHandle != GridControl.InvalidRowHandle && _libraries[e.RowHandle].pages.Length > 0);
		}

		private void OnGetLibraryRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
		{
			e.RelationCount = 1;
		}

		private void OnGetLibrariesRelationName(object sender, MasterRowGetRelationNameEventArgs e)
		{
			e.RelationName = "Pages";
		}

		private void OnGetLibraryChildList(object sender, MasterRowGetChildListEventArgs e)
		{
			if (e.RowHandle != GridControl.InvalidRowHandle)
				e.ChildList = _pages.Where(x => x.libraryId == _libraries[e.RowHandle].id).ToArray();
		}

		private void RepositoryItemCheckEditCheckedChanged(object sender, EventArgs e)
		{
			var focussedView = gridControlLibraries.FocusedView as GridView;
			if (focussedView != null)
			{
				focussedView.CloseEditor();
				if (focussedView == gridViewLibraries)
				{
					if (focussedView.FocusedRowHandle != GridControl.InvalidRowHandle)
					{
						var libraray = focussedView.GetFocusedRow() as SoapLibrary;
						if (libraray != null)
						{
							foreach (var page in _pages.Where(x => x.libraryId == libraray.id))
								page.selected = libraray.selected;
							var pagesView = focussedView.GetDetailView(focussedView.FocusedRowHandle, 0) as GridView;
							if (pagesView != null)
								pagesView.RefreshData();
						}
					}
				}
				else
				{
					var libraray = focussedView.SourceRow as SoapLibrary;
					var page = focussedView.GetFocusedRow() as SoapLibraryPage;
					if (libraray != null && page != null && page.selected)
					{
						libraray.selected = page.selected;
						gridControlLibraries.MainView.RefreshData();
					}
				}
			}
		}
		#endregion
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

	public class ValidatableTabControl : XtraTabControl
	{
		public ValidatableTabControl()
		{
			SetStyle(ControlStyles.ContainerControl, true);
		}
	}
}
