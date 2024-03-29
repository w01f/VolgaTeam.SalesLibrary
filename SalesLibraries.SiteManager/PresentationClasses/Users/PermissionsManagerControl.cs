﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.AdminService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.SiteManager.ToolForms;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace SalesLibraries.SiteManager.PresentationClasses.Users
{
    [ToolboxItem(false)]
    public sealed partial class PermissionsManagerControl : UserControl
    {
        private readonly List<string> _groupTemplates = new List<string>();
        private readonly List<GroupViewModel> _groups = new List<GroupViewModel>();
        private readonly List<LibraryViewModel> _libraries = new List<LibraryViewModel>();
        private readonly List<UserViewModel> _users = new List<UserViewModel>();
        private bool _complexPassword = true;
        private bool _groupsCollectionChanged;
        private bool _libraraiesCollectionChanged;
        private bool _userCollectionChanged;

        public PermissionsManagerControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            xtraTabPageUsers.PageEnabled = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.Enabled;
            xtraTabPageUsers.PageVisible = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.Visible;

            xtraTabPageGroups.PageEnabled = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab2.Enabled;
            xtraTabPageGroups.PageVisible = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab2.Visible;

            xtraTabPageLibraries.PageEnabled = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab3.Enabled;
            xtraTabPageLibraries.PageVisible = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab3.Visible;

            if (CreateGraphics().DpiX > 96)
            {
                gridColumnUsersCreateDate.Width =
                    RectangleHelper.ScaleHorizontal(gridColumnUsersCreateDate.Width, gridControlUsers.ScaleFactor.Width);

                checkedListBoxControlUserFilterGroups.ItemHeight =
                    RectangleHelper.ScaleVertical(checkedListBoxControlUserFilterGroups.ItemHeight,
                        checkedListBoxControlUserFilterGroups.ScaleFactor.Height);
            }
        }

        public bool HasConnection { get; set; }

        private void UpdateControlsState()
        {
            HasConnection = _users.Count > 0 || _groups.Count > 0 || _libraries.Count > 0;

            if (xtraTabControl.SelectedTabPage == xtraTabPageUsers)
            {
                FormMain.Instance.buttonItemUsersAdd.Enabled = HasConnection;
                FormMain.Instance.buttonItemUsersEdit.Enabled = gridControlUsers.FocusedView as GridView != null && (gridControlUsers.FocusedView as GridView).GetFocusedRow() != null;
                FormMain.Instance.buttonItemUsersDelete.Enabled = gridControlUsers.FocusedView as GridView != null && (gridControlUsers.FocusedView as GridView).GetFocusedRow() != null;
            }
            else if (xtraTabControl.SelectedTabPage == xtraTabPageGroups)
            {
                FormMain.Instance.buttonItemUsersAdd.Enabled = HasConnection;
                FormMain.Instance.buttonItemUsersEdit.Enabled = gridControlGroups.FocusedView as GridView != null && (gridControlGroups.FocusedView as GridView).GetFocusedRow() != null;
                FormMain.Instance.buttonItemUsersDelete.Enabled = gridControlGroups.FocusedView as GridView != null && (gridControlGroups.FocusedView as GridView).GetFocusedRow() != null;
            }
            else if (xtraTabControl.SelectedTabPage == xtraTabPageLibraries)
            {
                FormMain.Instance.buttonItemUsersAdd.Enabled = false;
                FormMain.Instance.buttonItemUsersEdit.Enabled = gridControlPages.FocusedView as GridView != null && ((gridControlPages.FocusedView as GridView).GetFocusedRow() as SoapLibraryPage) != null;
                FormMain.Instance.buttonItemUsersDelete.Enabled = false;
            }
            else
            {
                FormMain.Instance.buttonItemUsersAdd.Enabled = false;
                FormMain.Instance.buttonItemUsersEdit.Enabled = false;
                FormMain.Instance.buttonItemUsersDelete.Enabled = false;
            }
        }

        public void RefreshData(bool showMessages)
        {
            var message = string.Empty;

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
                    AppManager.Instance.PopupMessages.ShowWarning(message);
            }
            else
            {
                UpdateUsers(showMessages, ref message);
                UpdateGroups(showMessages, ref message);
                UpdateLibraries(showMessages, ref message);
            }
        }

        public void ImportUsers()
        {
            var message = string.Empty;
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Import Users";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dialog.Filter = "Excel files|*.xls;*.xlsx";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (var form = new FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        Enabled = false;
                        form.laProgress.Text = "Import users...";
                        form.TopMost = true;
                        var thread = new Thread(() =>
                                                    {
                                                        var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
                                                                            new UsersEmailSettings();

                                                        var sendServerMessage = !(emailSettings.SendLocalEmail &&
                                                                                  LocalUsersEmailManager.Instance.IsAvailable());
                                                        var users = ImportManager.ImportUsers(dialog.FileName, _users.ToArray(), _groups.ToArray(), _complexPassword, out message).ToList();
                                                        if (string.IsNullOrEmpty(message))
                                                            WebSiteManager.Instance.SelectedSite.SetUsers(users.ToArray(), sendServerMessage, out message);
                                                        if (!sendServerMessage)
                                                        {
                                                            foreach (var user in users)
                                                            {
                                                                LocalUsersEmailManager.Instance.SendUserChangeNotificationEmail(
                                                                    String.Format("{0} {1}", user.FirstName, user.LastName),
                                                                    user.Login,
                                                                    user.Email,
                                                                    user.Password,
                                                                    true
                                                                );
                                                            }
                                                        }
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

                    if (string.IsNullOrEmpty(message))
                        UpdateUsers(true, ref message);
                }
            }
            if (!string.IsNullOrEmpty(message))
                AppManager.Instance.PopupMessages.ShowWarning(message);
        }

        public void Export()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                dialog.FileName = string.Format("Users({0:MMddyy-hmmtt}).xlsx", DateTime.Now);
                dialog.Filter = "Excel files|*.xlsx";
                dialog.Title = "Export Users";
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var options = new XlsxExportOptions();
                options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
                options.TextExportMode = TextExportMode.Text;
                options.ExportHyperlinks = true;
                options.ShowGridLines = true;
                options.ExportMode = XlsxExportMode.SingleFile;

                using (var form = new FormProgress())
                {
                    FormMain.Instance.ribbonControl.Enabled = false;
                    Enabled = false;
                    form.laProgress.Text = "Exporting data...";
                    form.TopMost = true;
                    form.Show();
                    Application.DoEvents();
                    var thread = new Thread(() =>
                    {
                        Invoke(new Action(() =>
                        {
                            using (var printingSystem = new PrintingSystem())
                            {
                                gridViewUsers.CheckLoaded();
                                var printLink = new PrintableComponentLink()
                                {
                                    Landscape = true,
                                    PaperKind = PaperKind.A4,
                                    Component = gridControlUsers
                                };
                                printLink.CreateReportHeaderArea += OnCreateUsersReportHeaderArea;
                                printLink.CreateDocument(printingSystem);
                                Application.DoEvents();
                                printingSystem.ExportToXlsx(dialog.FileName, options);
                                Application.DoEvents();
                            }
                        }));
                    });
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

                if (File.Exists(dialog.FileName))
                    Process.Start(dialog.FileName);
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
            var message = string.Empty;
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

            var message = string.Empty;
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
                                                    _complexPassword = WebSiteManager.Instance.SelectedSite.IsUserPasswordComplex(out message);

                                                    var loadedUsers = WebSiteManager.Instance.SelectedSite.GetUsers(out message).ToList();
                                                    var filteredUsers = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.AllowedGroups.Any() ?
                                                        loadedUsers.Where(loadedUser =>
                                                            UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.AllowedGroups.Any(allowedGroup => loadedUser.assignedGroups != null && loadedUser.assignedGroups.Any(userGroup => String.Equals(allowedGroup, userGroup, StringComparison.OrdinalIgnoreCase)))).ToList() :
                                                        loadedUsers;
                                                    _users.AddRange(filteredUsers);
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
                    AppManager.Instance.PopupMessages.ShowWarning(message);
            }
            else
            {
                var thread = new Thread(() =>
                {
                    _complexPassword = WebSiteManager.Instance.SelectedSite.IsUserPasswordComplex(out message);

                    var loadedUsers = WebSiteManager.Instance.SelectedSite.GetUsers(out message).ToList();
                    var filteredUsers = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.AllowedGroups.Any() ?
                        loadedUsers.Where(loadedUser =>
                            UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.AllowedGroups.Any(allowedGroup => loadedUser.assignedGroups != null && loadedUser.assignedGroups.Any(userGroup => String.Equals(allowedGroup, userGroup, StringComparison.OrdinalIgnoreCase)))).ToList() :
                        loadedUsers;
                    _users.AddRange(filteredUsers);
                });
                thread.Start();
                while (thread.IsAlive)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            updateMessage = message;
            UpdateFilter(_users.SelectMany(x => x.assignedGroups ?? new string[] { }).Where(group => !UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.AllowedGroups.Any() || UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab1.AllowedGroups.Any(allowedGroup => String.Equals(allowedGroup, group, StringComparison.OrdinalIgnoreCase))).OrderBy(g => g).Distinct().ToArray());
            FilterUsers();
            _userCollectionChanged = false;
        }

        public void AddUser()
        {
            var message = string.Empty;
            using (var formEdit = new FormEditUser(true, _complexPassword, _users.Select(x => x.login).ToArray(), _groups.Select(x => new GroupViewModel { id = x.id, name = x.name }).ToArray(), _libraries.Select(x => new LibraryViewModel { id = x.id, name = x.name, pages = x.pages.Select(y => new LibraryPageViewModel { id = y.id, name = y.name, libraryId = y.libraryId }).ToArray() }).ToArray()))
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    var login = formEdit.textEditLogin.EditValue?.ToString() ?? string.Empty;
                    var password = formEdit.buttonEditPassword.EditValue?.ToString() ?? string.Empty;
                    var firstName = formEdit.textEditFirstName.EditValue?.ToString() ?? string.Empty;
                    var lastName = formEdit.textEditLastName.EditValue?.ToString() ?? string.Empty;
                    var email = formEdit.textEditEmail.EditValue?.ToString() ?? string.Empty;
                    var phone = formEdit.textEditPhone.EditValue?.ToString() ?? string.Empty;
                    var role = 0;
                    var groups = new List<GroupViewModel>(formEdit.AssignedGroups);
                    var pages = new List<LibraryPageViewModel>(formEdit.AssignedPages);
                    using (var form = new FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        Enabled = false;
                        form.laProgress.Text = "Adding user...";
                        form.TopMost = true;
                        var thread = new Thread(() =>
                        {
                            var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
                                                new UsersEmailSettings();
                            var sendServerMessage = !(emailSettings.SendLocalEmail &&
                                                    LocalUsersEmailManager.Instance.IsAvailable());
                            WebSiteManager.Instance.SelectedSite.SetUser(login, password, firstName, lastName, email, phone, role,
                                    groups.ToArray(), pages.ToArray(), sendServerMessage, out message);
                            if (!sendServerMessage)
                            {
                                LocalUsersEmailManager.Instance.SendUserChangeNotificationEmail(
                                    String.Format("{0} {1}", firstName, lastName),
                                    login,
                                    email,
                                    password,
                                    true
                                    );
                            }
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

                    UpdateUsers(true, ref message);
                }
            }
            if (!string.IsNullOrEmpty(message))
                AppManager.Instance.PopupMessages.ShowWarning(message);
        }

        public void EditUser()
        {
            var message = string.Empty;
            var userViewModel = gridViewUsers.GetFocusedRow() as UserViewModel;
            if (userViewModel == null) return;

            UserEditModel userRecord = null;
            using (var form = new FormProgress())
            {
                FormMain.Instance.ribbonControl.Enabled = false;
                Enabled = false;
                form.laProgress.Text = "Loading user info...";
                form.TopMost = true;
                var thread = new Thread(() =>
                {
                    _complexPassword = WebSiteManager.Instance.SelectedSite.IsUserPasswordComplex(out message);
                    userRecord = WebSiteManager.Instance.SelectedSite.GetUser(userViewModel.id, out message);
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
                AppManager.Instance.PopupMessages.ShowWarning(message);

            if (userRecord == null) return;
            using (var formEdit = new FormEditUser(false, _complexPassword, _users.Select(x => x.login).ToArray(),
                                                   _groups.Select(x => new GroupViewModel
                                                   {
                                                       id = x.id,
                                                       name = x.name,
                                                       selected = (userRecord.groups != null && userRecord.groups.Any(y => y.id == x.id))
                                                   }).ToArray(),
                                                   _libraries.Select(x => new LibraryViewModel
                                                   {
                                                       id = x.id,
                                                       name = x.name,
                                                       selected = (userRecord.libraries != null && userRecord.libraries.Any(y => y.id == x.id)),
                                                       pages = x.pages.Select(y => new LibraryPageViewModel
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
                    var login = formEdit.textEditLogin.EditValue != null ? formEdit.textEditLogin.EditValue.ToString() : string.Empty;
                    var password = formEdit.buttonEditPassword.EditValue != null ? formEdit.buttonEditPassword.EditValue.ToString() : string.Empty;
                    var firstName = formEdit.textEditFirstName.EditValue != null ? formEdit.textEditFirstName.EditValue.ToString() : string.Empty;
                    var lastName = formEdit.textEditLastName.EditValue != null ? formEdit.textEditLastName.EditValue.ToString() : string.Empty;
                    var email = formEdit.textEditEmail.EditValue != null ? formEdit.textEditEmail.EditValue.ToString() : string.Empty;
                    var phone = formEdit.textEditPhone.EditValue != null ? formEdit.textEditPhone.EditValue.ToString() : string.Empty;
                    var role = 0;
                    var groups = new List<GroupViewModel>(formEdit.AssignedGroups);
                    var pages = new List<LibraryPageViewModel>(formEdit.AssignedPages);
                    using (var form = new FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        Enabled = false;
                        form.laProgress.Text = "Updating user...";
                        form.TopMost = true;
                        var thread = new Thread(() =>
                        {
                            var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
                                                new UsersEmailSettings();
                            var sendServerMessage = !(emailSettings.SendLocalEmail &&
                                                      LocalUsersEmailManager.Instance.IsAvailable());
                            WebSiteManager.Instance.SelectedSite.SetUser(login, password, firstName, lastName, email, phone, role,
                                    groups.ToArray(), pages.ToArray(), sendServerMessage, out message);
                            if (!String.IsNullOrWhiteSpace(password) && !sendServerMessage)
                            {
                                LocalUsersEmailManager.Instance.SendUserChangeNotificationEmail(
                                    String.Format("{0} {1}", firstName, lastName),
                                    login,
                                    email,
                                    password,
                                    false
                                );
                            }
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

                    UpdateUsers(true, ref message);
                }
            }
            if (!string.IsNullOrEmpty(message))
                AppManager.Instance.PopupMessages.ShowWarning(message);
        }

        public void DeleteUser()
        {
            if (!(gridViewUsers.GetFocusedRow() is UserViewModel userViewModel) ||
                AppManager.Instance.PopupMessages.ShowWarningQuestion(string.Format("Are you sure want to delete user {0}?", userViewModel.FullName)) != DialogResult.Yes)
                return;
            var message = string.Empty;
            using (var form = new FormProgress())
            {
                FormMain.Instance.ribbonControl.Enabled = false;
                Enabled = false;
                form.laProgress.Text = "Deleting user...";
                form.TopMost = true;
                var thread = new Thread(() =>
                {
                    var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
                                        new UsersEmailSettings();
                    var sendServerMessage = !(emailSettings.SendLocalEmail && LocalUsersEmailManager.Instance.IsAvailable());
                    WebSiteManager.Instance.SelectedSite.DeleteUser(userViewModel.login, out message);
                    if (!sendServerMessage)
                    {
                        LocalUsersEmailManager.Instance.SendUserDeleteNotificationEmail(
                            String.Format("{0} {1}", userViewModel.firstName, userViewModel.lastName),
                            userViewModel.login);
                    }
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

            UpdateUsers(true, ref message);
            if (!string.IsNullOrEmpty(message))
                AppManager.Instance.PopupMessages.ShowWarning(message);
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

        private void OnGridUsersRowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column != gridColumnUsersCreateDate) return;
            var userModel = gridViewUsers.GetRow(e.RowHandle) as UserViewModel;
            if (userModel == null) return;
            e.Appearance.ForeColor = userModel.IsModified ? Color.Red : Color.Black;
        }

        private void OnCreateUsersReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            var reportHeader = string.Format("Users");
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None);
        }

        #region Users Filter
        private bool _userFilterInit;
        private List<string> _userFilterAllGroups = new List<string>();
        private List<string> _userFilterSelectedGroups = new List<string>();

        private void FilterUsers()
        {
            var filteredRecords = new List<UserViewModel>();
            filteredRecords.AddRange(checkEditEnableUserFilter.Checked ? _users.Where(x => x.assignedGroups != null && x.assignedGroups.Any(y => _userFilterSelectedGroups.Contains(y))) : _users);
            gridControlUsers.DataSource = filteredRecords;
            UpdateControlsState();
        }

        private void UpdateFilter(string[] groups)
        {
            _userFilterInit = true;
            _userFilterAllGroups.Clear();
            _userFilterAllGroups.AddRange(groups);
            if (_userFilterSelectedGroups.Count > 0)
                _userFilterSelectedGroups.RemoveAll(x => !_userFilterAllGroups.Contains(x));
            else
                _userFilterSelectedGroups.AddRange(_userFilterAllGroups);
            checkedListBoxControlUserFilterGroups.Items.Clear();
            foreach (var group in groups)
                checkedListBoxControlUserFilterGroups.Items.Add(group, _userFilterSelectedGroups.Contains(group));
            labelControlUserFilterGroupsTitle.Text = string.Format("Groups: {0}", _userFilterAllGroups.Count);
            _userFilterInit = false;
        }

        private void checkEditEnableUserFilter_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBoxControlUserFilterGroups.Enabled = checkEditEnableUserFilter.Checked;
            buttonXUserFilterGroupsAll.Enabled = checkEditEnableUserFilter.Checked;
            buttonXUserFilterGroupsNone.Enabled = checkEditEnableUserFilter.Checked;
            FilterUsers();
        }

        private void checkedListBoxControlUserFilterGroups_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_userFilterInit) return;
            _userFilterSelectedGroups.Clear();
            foreach (CheckedListBoxItem item in checkedListBoxControlUserFilterGroups.CheckedItems)
                _userFilterSelectedGroups.Add(item.Value.ToString());
            FilterUsers();
        }

        private void buttonXUserFilterGroupsAll_Click(object sender, EventArgs e)
        {
            _userFilterInit = true;
            _userFilterSelectedGroups.Clear();
            _userFilterSelectedGroups.AddRange(_userFilterAllGroups);
            checkedListBoxControlUserFilterGroups.CheckAll();
            FilterUsers();
            _userFilterInit = false;
        }

        private void buttonXUserFilterGroupsNone_Click(object sender, EventArgs e)
        {
            _userFilterInit = true;
            _userFilterSelectedGroups.Clear();
            checkedListBoxControlUserFilterGroups.UnCheckAll();
            FilterUsers();
            _userFilterInit = false;
        }
        #endregion
        #endregion

        #region Groups
        public void UpdateGroups(bool showMessages, ref string updateMessage)
        {
            gridControlGroups.DataSource = null;
            _groups.Clear();
            _groupTemplates.Clear();
            var message = string.Empty;
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
                        var loadedGroups = WebSiteManager.Instance.SelectedSite.GetGroups(out message).ToList();
                        var filteredGroups = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab2.AllowedGroups.Any() ?
                                                loadedGroups.Where(loadedGroup =>
                                                        UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab2.AllowedGroups.Any(allowedGroup =>
                                                            String.Equals(allowedGroup, loadedGroup.name, StringComparison.OrdinalIgnoreCase))).ToList() :
                                                loadedGroups;
                        _groups.AddRange(filteredGroups);
                        _groupTemplates.AddRange(WebSiteManager.Instance.SelectedSite.GetGroupTemplates(out message));
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
                    AppManager.Instance.PopupMessages.ShowWarning(message);
            }
            else
            {
                var thread = new Thread(() =>
                {
                    var loadedGroups = WebSiteManager.Instance.SelectedSite.GetGroups(out message).ToList();
                    var filteredGroups = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab2.AllowedGroups.Any() ?
                        loadedGroups.Where(loadedGroup =>
                            UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab2.AllowedGroups.Any(allowedGroup =>
                                String.Equals(allowedGroup, loadedGroup.name, StringComparison.OrdinalIgnoreCase))).ToList() :
                        loadedGroups;
                    _groups.AddRange(filteredGroups);
                    _groupTemplates.AddRange(WebSiteManager.Instance.SelectedSite.GetGroupTemplates(out message));
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
            var message = string.Empty;
            using (var formEdit = new FormEditGroup(true, _groupTemplates.ToArray(), _groups.Select(x => x.name).ToArray(), _users.Select(x => new UserViewModel { id = x.id, login = x.login, firstName = x.firstName, lastName = x.lastName, email = x.email }).ToArray(), _libraries.Select(x => new LibraryViewModel() { id = x.id, name = x.name, pages = x.pages.Select(y => new LibraryPageViewModel { id = y.id, name = y.name, libraryId = y.libraryId }).ToArray() }).ToArray()))
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    var id = Guid.NewGuid().ToString();
                    var name = formEdit.comboBoxEditName.EditValue != null ? formEdit.comboBoxEditName.EditValue.ToString() : string.Empty;
                    var users = new List<UserViewModel>(formEdit.AssignedUsers);
                    var pages = new List<LibraryPageViewModel>(formEdit.AssignedPages);
                    using (var form = new FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        Enabled = false;
                        form.laProgress.Text = "Adding group...";
                        form.TopMost = true;
                        var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.SetGroup(id, name, users.ToArray(), pages.ToArray(), out message));
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
                AppManager.Instance.PopupMessages.ShowWarning(message);
        }

        private void EditGroup()
        {
            var message = string.Empty;
            var groupViewModel = gridViewGroups.GetFocusedRow() as GroupViewModel;
            if (groupViewModel == null) return;

            GroupEditModel groupRecord = null;
            using (var form = new FormProgress())
            {
                FormMain.Instance.ribbonControl.Enabled = false;
                Enabled = false;
                form.laProgress.Text = "Loading group info...";
                form.TopMost = true;
                var thread = new Thread(() =>
                {
                    _complexPassword = WebSiteManager.Instance.SelectedSite.IsUserPasswordComplex(out message);
                    groupRecord = WebSiteManager.Instance.SelectedSite.GetGroup(groupViewModel.id, out message);
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
                AppManager.Instance.PopupMessages.ShowWarning(message);

            if (groupRecord == null) return;
            {
                using (var formEdit = new FormEditGroup(false,
                    _groupTemplates.ToArray(),
                    _groups.Where(x => !x.name.Equals(groupRecord.name)).Select(x => x.name).ToArray(),
                    _users.Select(x => new UserViewModel
                    {
                        id = x.id,
                        login = x.login,
                        firstName = x.firstName,
                        lastName = x.lastName,
                        email = x.email,
                        selected = (groupRecord.users != null && groupRecord.users.Any(y => y.id == x.id))
                    }).ToArray(),
                    _libraries.Select(x => new LibraryViewModel
                    {
                        id = x.id,
                        name = x.name,
                        selected = (groupRecord.libraries != null && groupRecord.libraries.Any(y => y.id == x.id)),
                        pages = x.pages.Select(y => new LibraryPageViewModel
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
                        var id = groupRecord.id;
                        var name = formEdit.comboBoxEditName.EditValue != null ? formEdit.comboBoxEditName.EditValue.ToString() : string.Empty;
                        var users = new List<UserViewModel>(formEdit.AssignedUsers);
                        var pages = new List<LibraryPageViewModel>(formEdit.AssignedPages);
                        using (var form = new FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            Enabled = false;
                            form.laProgress.Text = "Updating group...";
                            form.TopMost = true;
                            var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.SetGroup(id, name, users.ToArray(), pages.ToArray(), out message));
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
                    AppManager.Instance.PopupMessages.ShowWarning(message);
            }
        }

        private void DeleteGroup()
        {
            var groupRecord = gridViewGroups.GetFocusedRow() as GroupViewModel;
            if (groupRecord != null && AppManager.Instance.PopupMessages.ShowWarningQuestion(string.Format("Are you sure want to delete group {0}?", groupRecord.name)) == DialogResult.Yes)
            {
                var message = string.Empty;
                using (var form = new FormProgress())
                {
                    FormMain.Instance.ribbonControl.Enabled = false;
                    Enabled = false;
                    form.laProgress.Text = "Deleting group...";
                    form.TopMost = true;
                    var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.DeleteGroup(groupRecord.id, out message));
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
                    AppManager.Instance.PopupMessages.ShowWarning(message);
            }
        }

        private void repositoryItemButtonEditGroupActions_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewGroups.FocusedRowHandle != GridControl.InvalidRowHandle)
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

            var message = string.Empty;
            if (showMessages)
            {
                using (var form = new FormProgress())
                {
                    FormMain.Instance.ribbonControl.Enabled = false;
                    Enabled = false;
                    form.laProgress.Text = "Loading libraries...";
                    form.TopMost = true;
                    var thread = new Thread(() =>
                    {
                        var loadedLibraries = WebSiteManager.Instance.SelectedSite.GetLibraries(out message).ToList();
                        var filteredLibraries = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab3.AllowedLibraries.Any() ?
                            loadedLibraries.Where(loadedLibrary =>
                                UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab3.AllowedLibraries.Any(allowedLibrary =>
                                    String.Equals(allowedLibrary, loadedLibrary.name, StringComparison.OrdinalIgnoreCase))).ToList() :
                            loadedLibraries;
                        _libraries.AddRange(filteredLibraries);
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
                    AppManager.Instance.PopupMessages.ShowWarning(message);
            }
            else
            {
                var thread = new Thread(() =>
                {
                    var loadedLibraries = WebSiteManager.Instance.SelectedSite.GetLibraries(out message).ToList();
                    var filteredLibraries = UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab3.AllowedLibraries.Any() ?
                        loadedLibraries.Where(loadedLibrary =>
                            UsersEditPermissionsManager.Instance.CurrentUserPermissions.Tab3.AllowedLibraries.Any(allowedLibrary =>
                                String.Equals(allowedLibrary, loadedLibrary.name, StringComparison.OrdinalIgnoreCase))).ToList() :
                        loadedLibraries;
                    _libraries.AddRange(filteredLibraries);
                });
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
            var message = string.Empty;
            var pageViewModel = gridViewPages.GetFocusedRow() as LibraryPageViewModel;
            if (pageViewModel == null) return;

            SoapLibraryPage pageRecord = null;
            using (var form = new FormProgress())
            {
                FormMain.Instance.ribbonControl.Enabled = false;
                Enabled = false;
                form.laProgress.Text = "Loading library info...";
                form.TopMost = true;
                var thread = new Thread(() =>
                {
                    _complexPassword = WebSiteManager.Instance.SelectedSite.IsUserPasswordComplex(out message);
                    pageRecord = WebSiteManager.Instance.SelectedSite.GetLibraryPage(pageViewModel.id, out message);
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
                AppManager.Instance.PopupMessages.ShowWarning(message);

            if (pageRecord == null) return;
            using (var formEdit = new FormEditPage(_users.Select(x => new UserViewModel
            {
                id = x.id,
                login = x.login,
                firstName = x.firstName,
                lastName = x.lastName,
                email = x.email,
                selected = (pageRecord.users != null && pageRecord.users.Any(y => y.id == x.id))
            }).ToArray(),
                _groups.Select(x => new GroupViewModel
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
                    var users = new List<UserViewModel>(formEdit.AssignedUsers);
                    var groups = new List<GroupViewModel>(formEdit.AssignedGroups);
                    var allLibrary = formEdit.checkEditapplyForLibrary.Checked;
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
                                        WebSiteManager.Instance.SelectedSite.SetPage(page.id, users.ToArray(), groups.ToArray(), out message);
                            }
                            else
                                WebSiteManager.Instance.SelectedSite.SetPage(pageRecord.id, users.ToArray(), groups.ToArray(), out message);
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
                AppManager.Instance.PopupMessages.ShowWarning(message);
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