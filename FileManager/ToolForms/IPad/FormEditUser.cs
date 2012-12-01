using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SalesDepot.CoreObjects.IPadAdminService;

namespace FileManager.ToolForms.IPad
{
	public partial class FormEditUser : Form
	{
		private bool _newUser = false;
		private List<string> _existedUsers = new List<string>();

		private List<Library> _libraries = new List<Library>();
		private List<LibraryPage> _pages = new List<LibraryPage>();

		public LibraryPage[] AssignedPages
		{
			get { return _pages.Where(x => x.selected).ToArray(); }
		}

		public FormEditUser(bool newUser, string[] existedUsers, Library[] libraries)
		{
			InitializeComponent();

			_newUser = newUser;
			_existedUsers.AddRange(existedUsers);

			_libraries.Clear();
			_libraries.AddRange(libraries);
			_pages.Clear();
			_pages.AddRange(libraries.SelectMany(x => x.pages));

			gridViewLibraries.MasterRowEmpty += OnLibraryChildListIsEmpty;
			gridViewLibraries.MasterRowGetRelationCount += OnGetLibraryRelationCount;
			gridViewLibraries.MasterRowGetRelationName += OnGetLibrariesRelationName;
			gridViewLibraries.MasterRowGetChildList += OnGetLibraryChildList;
			gridControlLibraries.DataSource = _libraries;

			textEditLogin.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			textEditLogin.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			textEditLogin.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			textEditFirstName.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			textEditFirstName.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			textEditFirstName.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			textEditLastName.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			textEditLastName.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			textEditLastName.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			textEditEmail.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			textEditEmail.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			textEditEmail.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
			buttonEditPassword.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
			buttonEditPassword.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			buttonEditPassword.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);

			if (_newUser)
			{
				this.Text = "Add User";
				checkEditPassword.Visible = false;
				laPassword.Visible = true;
				textEditLogin.Enabled = true;
				buttonEditPassword_ButtonClick(null, null);
			}
			else
			{
				this.Text = "Edit User";
				checkEditPassword.Visible = true;
				checkEditPassword.Checked = false;
				laPassword.Visible = false;
				textEditLogin.Enabled = false;
			}
		}

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

		private void buttonEditPassword_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			buttonEditPassword.EditValue = new ToolClasses.PasswordGenerator().Generate();
		}

		private void FormEditUser_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult == DialogResult.OK)
			{
				textEditLogin.Focus();
				textEditFirstName.Focus();
				textEditLastName.Focus();
				textEditEmail.Focus();
				buttonEditPassword.Focus();
				if (!this.ValidateChildren())
					e.Cancel = true;
				else if (_newUser && textEditLogin.EditValue != null && _existedUsers.Contains(textEditLogin.EditValue.ToString()))
				{
					if (AppManager.Instance.ShowWarningQuestion("User with given login already exist.\nDo you want to update his data?") == System.Windows.Forms.DialogResult.No)
						e.Cancel = true;
				}
			}
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
						var libraray = focussedView.GetFocusedRow() as Library;
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
					var libraray = focussedView.SourceRow as Library;
					var page = focussedView.GetFocusedRow() as LibraryPage;
					if (libraray != null && page != null && page.selected)
					{
						libraray.selected = page.selected;
						gridControlLibraries.MainView.RefreshData();
					}
				}
			}
		}
		#endregion

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
	}

	class ValidatableTabControl : DevExpress.XtraTab.XtraTabControl
	{
		public ValidatableTabControl()
		{
			this.SetStyle(ControlStyles.ContainerControl, true);
		}
	}
}
