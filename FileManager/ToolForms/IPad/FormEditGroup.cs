using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SalesDepot.CoreObjects.IPadAdminService;

namespace FileManager.ToolForms.IPad
{
	public partial class FormEditGroup : Form
	{
		private bool _newGroup = false;
		private List<string> _existedGroups = new List<string>();

		private List<UserRecord> _users = new List<UserRecord>();
		private List<Library> _libraries = new List<Library>();
		private List<LibraryPage> _pages = new List<LibraryPage>();

		public UserRecord[] AssignedUsers
		{
			get { return _users.Where(x => x.selected).ToArray(); }
		}

		public LibraryPage[] AssignedPages
		{
			get { return _pages.Where(x => x.selected).ToArray(); }
		}

		public FormEditGroup(bool newGroup, string[] groupTemplates, string[] existedGroups, UserRecord[] users, Library[] libraries)
		{
			InitializeComponent();

			_newGroup = newGroup;
			_existedGroups.AddRange(existedGroups);

			_users.AddRange(users);
			gridControlUsers.DataSource = _users;
			comboBoxEditName.Properties.Items.AddRange(groupTemplates);

			_libraries.Clear();
			_libraries.AddRange(libraries);
			_pages.Clear();
			_pages.AddRange(libraries.SelectMany(x => x.pages));

			gridViewLibraries.MasterRowEmpty += OnLibraryChildListIsEmpty;
			gridViewLibraries.MasterRowGetRelationCount += OnGetLibraryRelationCount;
			gridViewLibraries.MasterRowGetRelationName += OnGetLibrariesRelationName;
			gridViewLibraries.MasterRowGetChildList += OnGetLibraryChildList;
			gridControlLibraries.DataSource = _libraries;

			comboBoxEditName.Enter += FormMain.Instance.EditorEnter;
			comboBoxEditName.MouseUp += FormMain.Instance.EditorMouseUp;
			comboBoxEditName.MouseDown += FormMain.Instance.EditorMouseDown;

			if (_newGroup)
				this.Text = "Add Group";
			else
				this.Text = "Edit Group";
		}

		private void FormEditGroup_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult == DialogResult.OK)
			{
				gridViewUsers.PostEditor();
				gridViewLibraries.PostEditor();
				gridViewPages.PostEditor();
				comboBoxEditName.Focus();
				if (!this.ValidateChildren())
					e.Cancel = true;
			}
		}

		#region Group
		private void comboBoxEditName_Validating(object sender, CancelEventArgs e)
		{
			string groupName = comboBoxEditName.EditValue != null ? comboBoxEditName.EditValue.ToString() : string.Empty;
			if (string.IsNullOrEmpty(groupName) || _existedGroups.Any(x => x.ToLower().Equals(groupName.ToLower())))
			{
				comboBoxEditName.ErrorText = "Group with this name already exists";
				e.Cancel = true;
			}
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
		#endregion

		#region Users
		private void buttonXUsersSelectAll_Click(object sender, EventArgs e)
		{
			foreach (var user in _users)
				user.selected = true;
			gridViewUsers.RefreshData();
		}

		private void buttonXUsersClearAll_Click(object sender, EventArgs e)
		{
			foreach (var user in _users)
				user.selected = false;
			gridViewUsers.RefreshData();
		}
		#endregion

	}
}
