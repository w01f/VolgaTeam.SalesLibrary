using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;

namespace FileManager.PresentationClasses.IPad
{
	[ToolboxItem(false)]
	public partial class IPadUsersControl : UserControl
	{
		private List<SalesDepot.CoreObjects.IPadAdminService.UserRecord> _users = new List<SalesDepot.CoreObjects.IPadAdminService.UserRecord>();
		private bool _hasConnection = false;

		public WallBin.Decorators.LibraryDecorator ParentDecorator { get; private set; }

		public IPadUsersControl(WallBin.Decorators.LibraryDecorator parent)
		{
			InitializeComponent();
			this.ParentDecorator = parent;
			this.Dock = DockStyle.Fill;
		}

		private void UpdateControlsState()
		{
			FormMain.Instance.buttonItemIPadUsersAdd.Enabled = _hasConnection;
			FormMain.Instance.buttonItemIPadUsersEdit.Enabled = gridViewUsers.FocusedRowHandle != GridControl.InvalidRowHandle;
			FormMain.Instance.buttonItemIPadUsersDelete.Enabled = gridViewUsers.FocusedRowHandle != GridControl.InvalidRowHandle;
		}

		public void UpdateUsers(bool showMessages)
		{
			gridControlUsers.DataSource = null;
			_users.Clear();

			string message = string.Empty;
			if (showMessages)
			{
				using (ToolForms.FormProgress form = new ToolForms.FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					this.Enabled = false;
					PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
					form.laProgress.Text = "Loading user list...";
					form.TopMost = true;
					Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
					{
						_users.AddRange(this.ParentDecorator.Library.IPadManager.GetUsers(out message));
					}));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						System.Windows.Forms.Application.DoEvents();
					}
					form.Close();
					this.Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
				_users.AddRange(this.ParentDecorator.Library.IPadManager.GetUsers(out message));

			_hasConnection = _users.Count > 0;
			gridControlUsers.DataSource = new BindingList<SalesDepot.CoreObjects.IPadAdminService.UserRecord>(_users.ToArray());
			UpdateControlsState();
		}

		public void AddUser()
		{
			string message = string.Empty;
			var libraries = new SalesDepot.CoreObjects.IPadAdminService.Library[] { };
			if (_users.FirstOrDefault() != null)
				libraries = _users.FirstOrDefault().libraries;
			using (ToolForms.IPad.FormEditUser formEdit = new ToolForms.IPad.FormEditUser(true, _users.Select(x => x.login).ToArray(), libraries))
			{
				if (formEdit.ShowDialog() == DialogResult.OK)
				{
					string login = formEdit.textEditLogin.EditValue != null ? formEdit.textEditLogin.EditValue.ToString() : string.Empty;
					string password = formEdit.buttonEditPassword.EditValue != null ? formEdit.buttonEditPassword.EditValue.ToString() : string.Empty;
					string firstName = formEdit.textEditFirstName.EditValue != null ? formEdit.textEditFirstName.EditValue.ToString() : string.Empty;
					string lastName = formEdit.textEditLastName.EditValue != null ? formEdit.textEditLastName.EditValue.ToString() : string.Empty;
					string email = formEdit.textEditEmail.EditValue != null ? formEdit.textEditEmail.EditValue.ToString() : string.Empty;
					List<SalesDepot.CoreObjects.IPadAdminService.LibraryPage> pages = new List<SalesDepot.CoreObjects.IPadAdminService.LibraryPage>(formEdit.AssignedPages);
					using (ToolForms.FormProgress form = new ToolForms.FormProgress())
					{
						FormMain.Instance.ribbonControl.Enabled = false;
						this.Enabled = false;
						PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
						form.laProgress.Text = "Adding user...";
						form.TopMost = true;
						Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
						{
							this.ParentDecorator.Library.IPadManager.SetUser(login, password, firstName, lastName, email, pages.ToArray(), out message);
						}));
						form.Show();
						thread.Start();
						while (thread.IsAlive)
						{
							Thread.Sleep(100);
							System.Windows.Forms.Application.DoEvents();
						}
						form.Close();
						this.Enabled = true;
						FormMain.Instance.ribbonControl.Enabled = true;
					}
					UpdateUsers(true);
				}
			}
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
		}

		public void EditUser()
		{
			string message = string.Empty;
			SalesDepot.CoreObjects.IPadAdminService.UserRecord userRecord = _users[gridViewUsers.GetDataSourceRowIndex(gridViewUsers.FocusedRowHandle)];
			using (ToolForms.IPad.FormEditUser formEdit = new ToolForms.IPad.FormEditUser(false, _users.Select(x => x.login).ToArray(), userRecord.libraries))
			{
				formEdit.textEditLogin.EditValue = userRecord.login;
				formEdit.textEditFirstName.EditValue = userRecord.firstName;
				formEdit.textEditLastName.EditValue = userRecord.lastName;
				formEdit.textEditEmail.EditValue = userRecord.email;
				if (formEdit.ShowDialog() == DialogResult.OK)
				{
					string login = formEdit.textEditLogin.EditValue != null ? formEdit.textEditLogin.EditValue.ToString() : string.Empty;
					string password = formEdit.buttonEditPassword.EditValue != null ? formEdit.buttonEditPassword.EditValue.ToString() : string.Empty;
					string firstName = formEdit.textEditFirstName.EditValue != null ? formEdit.textEditFirstName.EditValue.ToString() : string.Empty;
					string lastName = formEdit.textEditLastName.EditValue != null ? formEdit.textEditLastName.EditValue.ToString() : string.Empty;
					string email = formEdit.textEditEmail.EditValue != null ? formEdit.textEditEmail.EditValue.ToString() : string.Empty;
					List<SalesDepot.CoreObjects.IPadAdminService.LibraryPage> pages = new List<SalesDepot.CoreObjects.IPadAdminService.LibraryPage>(formEdit.AssignedPages);
					using (ToolForms.FormProgress form = new ToolForms.FormProgress())
					{
						FormMain.Instance.ribbonControl.Enabled = false;
						this.Enabled = false;
						WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
						form.laProgress.Text = "Updating user...";
						form.TopMost = true;
						Thread thread = new Thread(new System.Threading.ThreadStart(delegate()
						{
							this.ParentDecorator.Library.IPadManager.SetUser(login, password, firstName, lastName, email, pages.ToArray(), out message);
						}));
						form.Show();
						thread.Start();
						while (thread.IsAlive)
						{
							Thread.Sleep(100);
							System.Windows.Forms.Application.DoEvents();
						}
						form.Close();
						this.Enabled = true;
						FormMain.Instance.ribbonControl.Enabled = true;
					}
					UpdateUsers(true);
				}
			}
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
		}

		public void DeleteUser()
		{
			SalesDepot.CoreObjects.IPadAdminService.UserRecord userRecord = _users[gridViewUsers.GetDataSourceRowIndex(gridViewUsers.FocusedRowHandle)];
			if (AppManager.Instance.ShowWarningQuestion(string.Format("Are you sure want to delete user {0}?", userRecord.FullName)) == DialogResult.Yes)
			{
				string message = string.Empty;
				using (ToolForms.FormProgress form = new ToolForms.FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					this.Enabled = false;
					WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
					form.laProgress.Text = "Deleting user...";
					form.TopMost = true;
					Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
					{
						this.ParentDecorator.Library.IPadManager.DeleteUser(userRecord.login, out message);
					}));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					this.Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				UpdateUsers(true);
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
		}

		private void repositoryItemButtonEditUsersActions_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (gridViewUsers.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
					EditUser();
				else if (e.Button.Index == 1)
					DeleteUser();
			}
		}

		private void gridViewUsers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			UpdateControlsState();
		}
	}
}
