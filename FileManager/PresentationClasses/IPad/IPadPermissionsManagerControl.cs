using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolForms;
using FileManager.ToolForms.IPad;
using SalesDepot.Services.IPadAdminService;

namespace FileManager.PresentationClasses.IPad
{
	[ToolboxItem(false)]
	public sealed partial class IPadPermissionsManagerControl : UserControl
	{
		private readonly List<GroupModel> _groups = new List<GroupModel>();
		private readonly List<Library> _libraries = new List<Library>();
		private readonly List<UserModel> _users = new List<UserModel>();
		private readonly List<string> _groupTemplates = new List<string>();
		private bool _complexPassword = true;
		private bool _groupsCollectionChanged;
		private bool _libraraiesCollectionChanged;
		private bool _userCollectionChanged;

		public bool HasConnection { get; set; }

		public IPadPermissionsManagerControl(LibraryDecorator parent)
		{
			InitializeComponent();
			ParentDecorator = parent;
			Dock = DockStyle.Fill;
		}

		public LibraryDecorator ParentDecorator { get; private set; }

		private void UpdateControlsState()
		{
			HasConnection = _users.Count > 0 || _groups.Count > 0 || _libraries.Count > 0;

			if (xtraTabControl.SelectedTabPage == xtraTabPageUsers)
			{
				FormMain.Instance.buttonItemIPadUsersAdd.Enabled = HasConnection;
				FormMain.Instance.buttonItemIPadUsersEdit.Enabled = gridControlUsers.FocusedView as GridView != null && (gridControlUsers.FocusedView as GridView).GetFocusedRow() != null;
				FormMain.Instance.buttonItemIPadUsersDelete.Enabled = gridControlUsers.FocusedView as GridView != null && (gridControlUsers.FocusedView as GridView).GetFocusedRow() != null;
			}
			else if (xtraTabControl.SelectedTabPage == xtraTabPageGroups)
			{
				FormMain.Instance.buttonItemIPadUsersAdd.Enabled = HasConnection;
				FormMain.Instance.buttonItemIPadUsersEdit.Enabled = gridControlGroups.FocusedView as GridView != null && (gridControlGroups.FocusedView as GridView).GetFocusedRow() != null;
				FormMain.Instance.buttonItemIPadUsersDelete.Enabled = gridControlGroups.FocusedView as GridView != null && (gridControlGroups.FocusedView as GridView).GetFocusedRow() != null;
			}
			else if (xtraTabControl.SelectedTabPage == xtraTabPageLibraries)
			{
				FormMain.Instance.buttonItemIPadUsersAdd.Enabled = false;
				FormMain.Instance.buttonItemIPadUsersEdit.Enabled = gridControlPages.FocusedView as GridView != null && ((gridControlPages.FocusedView as GridView).GetFocusedRow() as LibraryPage) != null;
				FormMain.Instance.buttonItemIPadUsersDelete.Enabled = false;
			}
			else
			{
				FormMain.Instance.buttonItemIPadUsersAdd.Enabled = false;
				FormMain.Instance.buttonItemIPadUsersEdit.Enabled = false;
				FormMain.Instance.buttonItemIPadUsersDelete.Enabled = false;
			}
		}

		public void RefreshData(bool showMessages)
		{
			string message = string.Empty;

			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading Users, Groups, Libraries\nfrom website...";
					form.TopMost = true;
					form.Show();
					Application.DoEvents();
					UpdateUsers(false, ref message);
					if (string.IsNullOrEmpty(message))
						UpdateGroups(false, ref message);
					if (string.IsNullOrEmpty(message))
						UpdateLibraries(false, ref message);
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				UpdateUsers(showMessages, ref message);
				UpdateGroups(showMessages, ref message);
				UpdateLibraries(showMessages, ref message);
			}
		}

		public void AddObject()
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageUsers)
				AddUser();
			else if (xtraTabControl.SelectedTabPage == xtraTabPageGroups)
				AddGroup();
		}

		public void EditObject()
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageUsers)
				EditUser();
			else if (xtraTabControl.SelectedTabPage == xtraTabPageGroups)
				EditGroup();
			else if (xtraTabControl.SelectedTabPage == xtraTabPageLibraries)
				EditPage();
		}

		public void DeleteObject()
		{
			if (xtraTabControl.SelectedTabPage == xtraTabPageUsers)
				DeleteUser();
			else if (xtraTabControl.SelectedTabPage == xtraTabPageGroups)
				DeleteGroup();
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			string message = string.Empty;
			if (e.Page == xtraTabPageUsers && _userCollectionChanged)
			{
				UpdateUsers(true, ref message);
			}
			else if (e.Page == xtraTabPageGroups && _groupsCollectionChanged)
			{
				UpdateGroups(true, ref message);
			}
			else if (e.Page == xtraTabPageLibraries && _libraraiesCollectionChanged)
			{
				UpdateLibraries(true, ref message);
			}
			UpdateControlsState();
		}

		private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			UpdateControlsState();
		}

		#region Users
		public void UpdateUsers(bool showMessages, ref string updateMessage)
		{
			gridControlUsers.DataSource = null;
			_users.Clear();

			string message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading users...";
					form.TopMost = true;
					var thread = new Thread(() =>
					{
						_complexPassword = ParentDecorator.Library.IPadManager.IsUserPasswordComplex(out message);
						_users.AddRange(ParentDecorator.Library.IPadManager.GetUsers(out message));
					});
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() =>
				{
					_complexPassword = ParentDecorator.Library.IPadManager.IsUserPasswordComplex(out message);
					_users.AddRange(ParentDecorator.Library.IPadManager.GetUsers(out message));
				});
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			gridControlUsers.DataSource = _users;
			UpdateControlsState();

			_userCollectionChanged = false;
		}

		public void AddUser()
		{
			string message = string.Empty;
			using (var formEdit = new FormEditUser(true, _complexPassword, _users.Select(x => x.login).ToArray(), _groups.Select(x => new GroupModel { id = x.id, name = x.name }).ToArray(), _libraries.Select(x => new Library { id = x.id, name = x.name, pages = x.pages.Select(y => new LibraryPage { id = y.id, name = y.name, libraryId = y.libraryId }).ToArray() }).ToArray()))
			{
				if (formEdit.ShowDialog() == DialogResult.OK)
				{
					string login = formEdit.textEditLogin.EditValue != null ? formEdit.textEditLogin.EditValue.ToString() : string.Empty;
					string password = formEdit.buttonEditPassword.EditValue != null ? formEdit.buttonEditPassword.EditValue.ToString() : string.Empty;
					string firstName = formEdit.textEditFirstName.EditValue != null ? formEdit.textEditFirstName.EditValue.ToString() : string.Empty;
					string lastName = formEdit.textEditLastName.EditValue != null ? formEdit.textEditLastName.EditValue.ToString() : string.Empty;
					string email = formEdit.textEditEmail.EditValue != null ? formEdit.textEditEmail.EditValue.ToString() : string.Empty;
					string phone = formEdit.textEditPhone.EditValue != null ? formEdit.textEditPhone.EditValue.ToString() : string.Empty;
					var role = 0;
					var groups = new List<GroupModel>(formEdit.AssignedGroups);
					var pages = new List<LibraryPage>(formEdit.AssignedPages);
					using (var form = new FormProgress())
					{
						FormMain.Instance.ribbonControl.Enabled = false;
						Enabled = false;
						form.laProgress.Text = "Adding user...";
						form.TopMost = true;
						var thread = new Thread(() => ParentDecorator.Library.IPadManager.SetUser(login, password, firstName, lastName, email, phone, role, groups.ToArray(), pages.ToArray(), out message));
						form.Show();
						thread.Start();
						while (thread.IsAlive)
						{
							Thread.Sleep(100);
							Application.DoEvents();
						}
						form.Close();
						Enabled = true;
						FormMain.Instance.ribbonControl.Enabled = true;
					}

					_userCollectionChanged = true;
					_groupsCollectionChanged = true;
					_libraraiesCollectionChanged = true;

					UpdateUsers(true, ref message);
				}
			}
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
		}

		public void EditUser()
		{
			string message = string.Empty;
			var userRecord = gridViewUsers.GetFocusedRow() as UserModel;
			if (userRecord != null)
			{
				using (var formEdit = new FormEditUser(false, _complexPassword, _users.Select(x => x.login).ToArray(),
									   _groups.Select(x => new GroupModel
									   {
										   id = x.id,
										   name = x.name,
										   selected = (userRecord.groups != null && userRecord.groups.Any(y => y.id == x.id))
									   }).ToArray(),
									   _libraries.Select(x => new Library
									   {
										   id = x.id,
										   name = x.name,
										   selected = (userRecord.libraries != null && userRecord.libraries.Any(y => y.id == x.id)),
										   pages = x.pages.Select(y => new LibraryPage
										   {
											   id = y.id,
											   name = y.name,
											   libraryId = y.libraryId,
											   selected = (userRecord.libraries != null && userRecord.libraries.Where(library => library.pages != null).SelectMany(library => library.pages).Select(userPage => userPage.id).Contains(y.id))
										   }).ToArray()
									   }).ToArray()))
				{
					formEdit.textEditLogin.EditValue = userRecord.login;
					formEdit.textEditFirstName.EditValue = userRecord.firstName;
					formEdit.textEditLastName.EditValue = userRecord.lastName;
					formEdit.textEditPhone.EditValue = userRecord.phone;
					formEdit.textEditEmail.EditValue = userRecord.email;
					formEdit.textEditEmailConfirm.EditValue = userRecord.email;
					if (formEdit.ShowDialog() == DialogResult.OK)
					{
						string login = formEdit.textEditLogin.EditValue != null ? formEdit.textEditLogin.EditValue.ToString() : string.Empty;
						string password = formEdit.buttonEditPassword.EditValue != null ? formEdit.buttonEditPassword.EditValue.ToString() : string.Empty;
						string firstName = formEdit.textEditFirstName.EditValue != null ? formEdit.textEditFirstName.EditValue.ToString() : string.Empty;
						string lastName = formEdit.textEditLastName.EditValue != null ? formEdit.textEditLastName.EditValue.ToString() : string.Empty;
						string email = formEdit.textEditEmail.EditValue != null ? formEdit.textEditEmail.EditValue.ToString() : string.Empty;
						string phone = formEdit.textEditPhone.EditValue != null ? formEdit.textEditPhone.EditValue.ToString() : string.Empty;
						var role = 0;
						var groups = new List<GroupModel>(formEdit.AssignedGroups);
						var pages = new List<LibraryPage>(formEdit.AssignedPages);
						using (var form = new FormProgress())
						{
							FormMain.Instance.ribbonControl.Enabled = false;
							Enabled = false;
							form.laProgress.Text = "Updating user...";
							form.TopMost = true;
							var thread = new Thread(() => ParentDecorator.Library.IPadManager.SetUser(login, password, firstName, lastName, email, phone, role, groups.ToArray(), pages.ToArray(), out message));
							form.Show();
							thread.Start();
							while (thread.IsAlive)
							{
								Thread.Sleep(100);
								Application.DoEvents();
							}
							form.Close();
							Enabled = true;
							FormMain.Instance.ribbonControl.Enabled = true;
						}

						_userCollectionChanged = true;
						_groupsCollectionChanged = true;
						_libraraiesCollectionChanged = true;

						UpdateUsers(true, ref message);
					}
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
		}

		public void DeleteUser()
		{
			var userRecord = gridViewUsers.GetFocusedRow() as UserModel;
			if (userRecord != null && AppManager.Instance.ShowWarningQuestion(string.Format("Are you sure want to delete user {0}?", userRecord.FullName)) == DialogResult.Yes)
			{
				string message = string.Empty;
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Deleting user...";
					form.TopMost = true;
					var thread = new Thread(() => ParentDecorator.Library.IPadManager.DeleteUser(userRecord.login, out message));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}

				_userCollectionChanged = true;
				_groupsCollectionChanged = true;
				_libraraiesCollectionChanged = true;

				UpdateUsers(true, ref message);
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
		}

		private void repositoryItemButtonEditUsersActions_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewUsers.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
					EditUser();
				else if (e.Button.Index == 1)
					DeleteUser();
			}
		}
		#endregion

		#region Groups
		public void UpdateGroups(bool showMessages, ref string updateMessage)
		{
			gridControlGroups.DataSource = null;
			_groups.Clear();
			_groupTemplates.Clear();
			string message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading groups...";
					form.TopMost = true;
					var thread = new Thread(() =>
					{
						_groups.AddRange(ParentDecorator.Library.IPadManager.GetGroups(out message));
						_groupTemplates.AddRange(ParentDecorator.Library.IPadManager.GetGroupTemplates(out message));
					});
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() =>
				{
					_groups.AddRange(ParentDecorator.Library.IPadManager.GetGroups(out message));
					_groupTemplates.AddRange(ParentDecorator.Library.IPadManager.GetGroupTemplates(out message));
				});
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			gridControlGroups.DataSource = _groups;
			UpdateControlsState();

			_groupsCollectionChanged = false;
		}

		private void AddGroup()
		{
			string message = string.Empty;
			using (var formEdit = new FormEditGroup(true, _groupTemplates.ToArray(), _groups.Select(x => x.name).ToArray(), _users.Select(x => new UserModel { id = x.id, login = x.login, firstName = x.firstName, lastName = x.lastName, email = x.email }).ToArray(), _libraries.Select(x => new Library { id = x.id, name = x.name, pages = x.pages.Select(y => new LibraryPage { id = y.id, name = y.name, libraryId = y.libraryId }).ToArray() }).ToArray()))
			{
				if (formEdit.ShowDialog() == DialogResult.OK)
				{
					string id = Guid.NewGuid().ToString();
					string name = formEdit.comboBoxEditName.EditValue != null ? formEdit.comboBoxEditName.EditValue.ToString() : string.Empty;
					var users = new List<UserModel>(formEdit.AssignedUsers);
					var pages = new List<LibraryPage>(formEdit.AssignedPages);
					using (var form = new FormProgress())
					{
						FormMain.Instance.ribbonControl.Enabled = false;
						Enabled = false;
						form.laProgress.Text = "Adding group...";
						form.TopMost = true;
						var thread = new Thread(() => ParentDecorator.Library.IPadManager.SetGroup(id, name, users.ToArray(), pages.ToArray(), out message));
						form.Show();
						thread.Start();
						while (thread.IsAlive)
						{
							Thread.Sleep(100);
							Application.DoEvents();
						}
						form.Close();
						Enabled = true;
						FormMain.Instance.ribbonControl.Enabled = true;
					}

					_userCollectionChanged = true;
					_groupsCollectionChanged = true;
					_libraraiesCollectionChanged = true;

					UpdateGroups(true, ref message);
				}
			}
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
		}

		private void EditGroup()
		{
			string message = string.Empty;
			var groupRecord = gridViewGroups.GetFocusedRow() as GroupModel;
			if (groupRecord != null)
			{
				using (var formEdit = new FormEditGroup(false,
														_groupTemplates.ToArray(),
														_groups.Where(x => !x.name.Equals(groupRecord.name)).Select(x => x.name).ToArray(),
														_users.Select(x => new UserModel
														{
															id = x.id,
															login = x.login,
															firstName = x.firstName,
															lastName = x.lastName,
															email = x.email,
															selected = (groupRecord.users != null && groupRecord.users.Any(y => y.id == x.id))
														}).ToArray(),
														_libraries.Select(x => new Library
														{
															id = x.id,
															name = x.name,
															selected = (groupRecord.libraries != null && groupRecord.libraries.Any(y => y.id == x.id)),
															pages = x.pages.Select(y => new LibraryPage
															{
																id = y.id,
																name = y.name,
																libraryId = y.libraryId,
																selected = (groupRecord.libraries != null && groupRecord.libraries.SelectMany(library => library.pages).Select(groupPage => groupPage.id).Contains(y.id))
															}).ToArray()
														}).ToArray()))
				{
					formEdit.comboBoxEditName.EditValue = groupRecord.name;
					if (formEdit.ShowDialog() == DialogResult.OK)
					{
						string id = groupRecord.id;
						string name = formEdit.comboBoxEditName.EditValue != null ? formEdit.comboBoxEditName.EditValue.ToString() : string.Empty;
						var users = new List<UserModel>(formEdit.AssignedUsers);
						var pages = new List<LibraryPage>(formEdit.AssignedPages);
						using (var form = new FormProgress())
						{
							FormMain.Instance.ribbonControl.Enabled = false;
							Enabled = false;
							form.laProgress.Text = "Updating group...";
							form.TopMost = true;
							var thread = new Thread(() => ParentDecorator.Library.IPadManager.SetGroup(id, name, users.ToArray(), pages.ToArray(), out message));
							form.Show();
							thread.Start();
							while (thread.IsAlive)
							{
								Thread.Sleep(100);
								Application.DoEvents();
							}
							form.Close();
							Enabled = true;
							FormMain.Instance.ribbonControl.Enabled = true;
						}

						_userCollectionChanged = true;
						_groupsCollectionChanged = true;
						_libraraiesCollectionChanged = true;

						UpdateGroups(true, ref message);
					}
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
		}

		private void DeleteGroup()
		{
			var groupRecord = gridViewGroups.GetFocusedRow() as GroupModel;
			if (groupRecord != null && AppManager.Instance.ShowWarningQuestion(string.Format("Are you sure want to delete group {0}?", groupRecord.name)) == DialogResult.Yes)
			{
				string message = string.Empty;
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Deleting group...";
					form.TopMost = true;
					var thread = new Thread(() => ParentDecorator.Library.IPadManager.DeleteGroup(groupRecord.id, out message));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}

				_userCollectionChanged = true;
				_groupsCollectionChanged = true;
				_libraraiesCollectionChanged = true;

				UpdateGroups(true, ref message);
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
		}

		private void repositoryItemButtonEditGroupActions_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewUsers.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
					EditGroup();
				else if (e.Button.Index == 1)
					DeleteGroup();
			}
		}
		#endregion

		#region Libraries
		public void UpdateLibraries(bool showMessages, ref string updateMessage)
		{
			gridControlPages.DataSource = null;
			_libraries.Clear();

			string message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading libraries...";
					form.TopMost = true;
					var thread = new Thread(() => _libraries.AddRange(ParentDecorator.Library.IPadManager.GetLibraries(out message)));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() => _libraries.AddRange(ParentDecorator.Library.IPadManager.GetLibraries(out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			gridControlPages.DataSource = _libraries.SelectMany(x => x.pages).ToList();
			gridViewPages.ExpandAllGroups();
			UpdateControlsState();

			_libraraiesCollectionChanged = false;
		}

		private void EditPage()
		{
			string message = string.Empty;
			var pageRecord = gridViewPages.GetFocusedRow() as LibraryPage;
			if (pageRecord != null)
			{
				using (var formEdit = new FormEditPage(_users.Select(x => new UserModel
				{
					id = x.id,
					login = x.login,
					firstName = x.firstName,
					lastName = x.lastName,
					email = x.email,
					selected = (pageRecord.users != null && pageRecord.users.Any(y => y.id == x.id))
				}).ToArray(),
													   _groups.Select(x => new GroupModel
													   {
														   id = x.id,
														   name = x.name,
														   selected = (pageRecord.groups != null && pageRecord.groups.Any(y => y.id == x.id))
													   }).ToArray()))
				{
					formEdit.laLibrary.Text = string.Format(formEdit.laLibrary.Text, pageRecord.libraryName);
					formEdit.laPage.Text = string.Format(formEdit.laPage.Text, pageRecord.name);
					formEdit.checkEditapplyForLibrary.Text = string.Format(formEdit.checkEditapplyForLibrary.Text, pageRecord.libraryName);
					if (formEdit.ShowDialog() == DialogResult.OK)
					{
						var users = new List<UserModel>(formEdit.AssignedUsers);
						var groups = new List<GroupModel>(formEdit.AssignedGroups);
						bool allLibrary = formEdit.checkEditapplyForLibrary.Checked;
						using (var form = new FormProgress())
						{
							FormMain.Instance.ribbonControl.Enabled = false;
							Enabled = false;
							form.laProgress.Text = allLibrary ? "Updating library..." : "Updating page...";
							form.TopMost = true;
							var thread = new Thread(() =>
							{
								if (allLibrary)
								{
									var libraray = _libraries.FirstOrDefault(x => x.id.Equals(pageRecord.libraryId));
									if (libraray != null)
										foreach (var page in libraray.pages)
											ParentDecorator.Library.IPadManager.SetPage(page.id, users.ToArray(), groups.ToArray(), out message);
								}
								else
									ParentDecorator.Library.IPadManager.SetPage(pageRecord.id, users.ToArray(), groups.ToArray(), out message);
							});
							form.Show();
							thread.Start();
							while (thread.IsAlive)
							{
								Thread.Sleep(100);
								Application.DoEvents();
							}
							form.Close();
							Enabled = true;
							FormMain.Instance.ribbonControl.Enabled = true;
						}

						_userCollectionChanged = true;
						_groupsCollectionChanged = true;
						_libraraiesCollectionChanged = true;

						UpdateLibraries(true, ref message);
					}
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
		}

		private void repositoryItemButtonEditPageActions_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			EditPage();
		}

		private void buttonXExpandLibraries_Click(object sender, EventArgs e)
		{
			gridViewPages.ExpandAllGroups();
		}

		private void buttonXCollapseLibraries_Click(object sender, EventArgs e)
		{
			gridViewPages.CollapseAllGroups();
		}
		#endregion
	}
}