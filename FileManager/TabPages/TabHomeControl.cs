using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabHomeControl : UserControl
    {
        private PresentationClasses.WallBin.WallBinTreeListControl _treeList = null;
        private string _currentPage = string.Empty;
        private bool _firstRun = true;

        private ToolForms.Settings.FormPaths _formPath = null;
        private ToolForms.Settings.FormExtraRoots _formExtraRoots = null;
        private ToolForms.Settings.FormBranding _formBranding = null;
        private ToolForms.Settings.FormSync _formSync = null;
        private ToolForms.Settings.FormPages _formPages = null;
        private ToolForms.Settings.FormColumns _formColumns = null;
        private ToolForms.Settings.FormAutoWidgets _formAutoWidgets = null;
        private ToolForms.Settings.FormDeadLinks _formDeadLinks = null;
        private ToolForms.Settings.FormEmailList _formEmailList = null;
        private ToolForms.Settings.FormAutoSync _formAutoSync = null;

        public TabHomeControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        #region GUI Event Handlers
        private void TabHomeControl_Load(object sender, EventArgs e)
        {
            _formPath = new ToolForms.Settings.FormPaths();
            _formExtraRoots = new ToolForms.Settings.FormExtraRoots();
            _formBranding = new ToolForms.Settings.FormBranding();
            _formSync = new ToolForms.Settings.FormSync();
            _formPages = new ToolForms.Settings.FormPages();
            _formColumns = new ToolForms.Settings.FormColumns();
            _formAutoWidgets = new ToolForms.Settings.FormAutoWidgets();
            _formDeadLinks = new ToolForms.Settings.FormDeadLinks();
            _formEmailList = new ToolForms.Settings.FormEmailList();
            _formAutoSync = new ToolForms.Settings.FormAutoSync();
        }

        public void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveLibraryWarning())
            {
                SaveDockPanelState();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        public void comboBoxEditLibraries_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (SaveLibraryWarning())
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        public void comboBoxEditLibraries_EditValueChanged(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemHomeFileTreeView.CheckedChanged -= new EventHandler(buttonItemHomeFileTreeView_CheckedChanged);
            FormMain.Instance.buttonItemHomeFileTreeView.Checked = false;
            if (FormMain.Instance.comboBoxEditLibraries.EditValue != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Where(x => x.Library.Name.Equals(FormMain.Instance.comboBoxEditLibraries.EditValue.ToString())).FirstOrDefault();
            else
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator = null;
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                pnEmpty.Visible = true;
                pnEmpty.BringToFront();
                Cursor cursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;

                BusinessClasses.LibraryManager.Instance.SelectedLibrary = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library;

                BuildTreeView();

                if (!BusinessClasses.LibraryManager.Instance.SelectedLibrary.UseDirectAccess)
                {
                    dockPanelTreeView_Container.Controls.Add(_treeList);
                    ApplyDecorator();
                    FillPages();

                    FormMain.Instance.ribbonBarHomeAddLink.Enabled = true;
                    FormMain.Instance.ribbonBarHomeDelete.Enabled = true;
                    FormMain.Instance.ribbonBarHomeFileTreeView.Enabled = true;
                    FormMain.Instance.ribbonBarHomeFontSize.Enabled = true;
                    FormMain.Instance.ribbonBarHomeLibraries.Enabled = true;
                    FormMain.Instance.ribbonBarHomeNudge.Enabled = true;
                    FormMain.Instance.ribbonBarHomeOpen.Enabled = true;
                    FormMain.Instance.ribbonBarHomeProperties.Enabled = true;
                    FormMain.Instance.ribbonBarHomeSave.Enabled = true;
                    FormMain.Instance.ribbonBarSettingsAutoWidgets.Enabled = true;
                    FormMain.Instance.ribbonBarSettingsBranding.Enabled = true;
                    FormMain.Instance.ribbonBarSettingsColumns.Enabled = true;
                    FormMain.Instance.ribbonBarSettingsDeadLinks.Enabled = true;
                    FormMain.Instance.ribbonBarSettingsEmailList.Enabled = true;
                    FormMain.Instance.ribbonBarSettingsMultitab.Enabled = true;
                    FormMain.Instance.ribbonBarSettingsPages.Enabled = true;
                }
                else
                {
                    if (!pnMain.Controls.Contains(_treeList))
                        pnMain.Controls.Add(_treeList);
                    _treeList.BringToFront();

                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ApplyOvernightsCalebdar();

                    btSetupWallBin.Visible = false;
                    FormMain.Instance.ribbonBarHomeAddLink.Enabled = false;
                    FormMain.Instance.ribbonBarHomeDelete.Enabled = false;
                    FormMain.Instance.ribbonBarHomeFileTreeView.Enabled = false;
                    FormMain.Instance.ribbonBarHomeFontSize.Enabled = false;
                    FormMain.Instance.ribbonBarHomeLibraries.Enabled = false;
                    FormMain.Instance.ribbonBarHomeNudge.Enabled = false;
                    FormMain.Instance.ribbonBarHomeOpen.Enabled = false;
                    FormMain.Instance.ribbonBarHomeProperties.Enabled = false;
                    FormMain.Instance.ribbonBarHomeSave.Enabled = false;
                    FormMain.Instance.ribbonBarSettingsAutoWidgets.Enabled = false;
                    FormMain.Instance.ribbonBarSettingsBranding.Enabled = false;
                    FormMain.Instance.ribbonBarSettingsColumns.Enabled = false;
                    FormMain.Instance.ribbonBarSettingsDeadLinks.Enabled = false;
                    FormMain.Instance.ribbonBarSettingsEmailList.Enabled = false;
                    FormMain.Instance.ribbonBarSettingsMultitab.Enabled = false;
                    FormMain.Instance.ribbonBarSettingsPages.Enabled = false;
                }
                this.Cursor = cursor;

                pnEmpty.Visible = false;
            }
            FormMain.Instance.buttonItemHomeFileTreeView.Checked = ConfigurationClasses.SettingsManager.Instance.TreeViewVisible & !BusinessClasses.LibraryManager.Instance.SelectedLibrary.UseDirectAccess;
            ShowTreeView(ConfigurationClasses.SettingsManager.Instance.TreeViewVisible & !BusinessClasses.LibraryManager.Instance.SelectedLibrary.UseDirectAccess);
            FormMain.Instance.buttonItemHomeFileTreeView.CheckedChanged += new EventHandler(buttonItemHomeFileTreeView_CheckedChanged);
        }

        public void comboBoxEditPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                _currentPage = FormMain.Instance.comboBoxEditPages.EditValue.ToString();
                ConfigurationClasses.SettingsManager.Instance.SelectedPage = _currentPage;
                ConfigurationClasses.SettingsManager.Instance.Save();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.SelectPage(FormMain.Instance.comboBoxEditPages.SelectedIndex);
            }
        }

        private void dockPanelTreeView_DockChanged(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.TreeViewDocked = dockPanelTreeView.Dock == DevExpress.XtraBars.Docking.DockingStyle.Left ? true : false;
            ConfigurationClasses.SettingsManager.Instance.Save();
        }

        private void dockManager_Sizing(object sender, DevExpress.XtraBars.Docking.SizingEventArgs e)
        {
            if (e.Panel.Name.Equals("dockPanelTreeView") && (e.NewSize.Width < 300 || e.NewSize.Height < 450))
                e.Cancel = true;
        }

        private void dockPanelTreeView_ClosedPanel(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            FormMain.Instance.buttonItemHomeFileTreeView.Checked = false;
        }

        private void dockPanelTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dockPanelTreeView.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
        }
        #endregion

        #region Buttons Click Handlers
        public void buttonItemHomeFileTreeView_CheckedChanged(object sender, EventArgs e)
        {
            ShowTreeView(FormMain.Instance.buttonItemHomeFileTreeView.Checked);
        }

        public void btPathSettings_Click(object sender, EventArgs e)
        {
            if (SaveLibraryWarning())
            {
                if (_formPath.ShowDialog() == DialogResult.OK)
                {
                    BusinessClasses.LibraryManager.Instance.LoadLibraries(new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.BackupPath));
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        formProgress.TopMost = true;

                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                formProgress.laProgress.Text = BusinessClasses.LibraryManager.Instance.OldStyleProceed ? "Converting Your Sales Library to the Latest Version…" : "Loading Libraries...";
                                formProgress.Refresh();
                                Application.DoEvents();
                                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildDecorators();

                                formProgress.laProgress.Text = "Loading Overnights Calendar...";
                                formProgress.Refresh();
                                Application.DoEvents();
                                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildOvernightsCalendars();
                            });
                        }));

                        formProgress.Show();

                        thread.Start();

                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();

                        formProgress.Close();
                        FormMain.Instance.ribbonControl.Enabled = true;
                    }
                    FillLibraries();
                    BusinessClasses.LibraryManager.Instance.OldStyleProceed = false;
                }
            }
        }

        public void btExtraRoot_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (SaveLibraryWarning())
                {
                    _formExtraRoots.Library = new BusinessClasses.Library(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Name, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Folder, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.UseDirectAccess);
                    if (_formExtraRoots.ShowDialog() == DialogResult.OK)
                    {
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            formProgress.laProgress.Text = "Apply Settings...";
                            formProgress.TopMost = true;

                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    int libraryIndex = BusinessClasses.LibraryManager.Instance.LibraryCollection.IndexOf(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formExtraRoots.Library);

                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator = new PresentationClasses.WallBin.Decorators.LibraryDecorator(_formExtraRoots.Library);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Add(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                });
                            }));

                            formProgress.Show();

                            thread.Start();

                            while (thread.IsAlive)
                                System.Windows.Forms.Application.DoEvents();

                            formProgress.Close();
                            FormMain.Instance.ribbonControl.Enabled = true;
                        }

                        _firstRun = false;
                        comboBoxEditLibraries_EditValueChanged(null, null);
                    }
                }
            }
        }


        public void buttonItemSettingsBranding_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                _formBranding.ShowDialog();
            }
        }

        public void buttonItemSettingsSync_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                _formSync.ShowDialog();
            }
        }

        public void buttonItemSettingsPages_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (SaveLibraryWarning())
                {
                    _formPages.Library = new BusinessClasses.Library(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Name, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Folder, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.UseDirectAccess);
                    if (_formPages.ShowDialog() == DialogResult.OK)
                    {
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            formProgress.laProgress.Text = "Apply Settings...";
                            formProgress.TopMost = true;

                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    int libraryIndex = BusinessClasses.LibraryManager.Instance.LibraryCollection.IndexOf(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formPages.Library);

                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator = new PresentationClasses.WallBin.Decorators.LibraryDecorator(_formPages.Library);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Add(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                });
                            }));

                            formProgress.Show();

                            thread.Start();

                            while (thread.IsAlive)
                                System.Windows.Forms.Application.DoEvents();

                            formProgress.Close();
                            FormMain.Instance.ribbonControl.Enabled = true;
                        }

                        _firstRun = false;
                        comboBoxEditLibraries_EditValueChanged(null, null);
                    }
                }
            }
        }

        public void buttonItemSettingsColumns_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (SaveLibraryWarning())
                {
                    _formColumns.Library = new BusinessClasses.Library(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Name, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Folder, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.UseDirectAccess);
                    if (_formColumns.ShowDialog() == DialogResult.OK)
                    {
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            formProgress.laProgress.Text = "Apply Settings...";
                            formProgress.TopMost = true;

                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    int libraryIndex = BusinessClasses.LibraryManager.Instance.LibraryCollection.IndexOf(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formColumns.Library);

                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator = new PresentationClasses.WallBin.Decorators.LibraryDecorator(_formColumns.Library);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Add(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                });
                            }));

                            formProgress.Show();

                            thread.Start();

                            while (thread.IsAlive)
                                System.Windows.Forms.Application.DoEvents();

                            formProgress.Close();
                            FormMain.Instance.ribbonControl.Enabled = true;
                        }
                        _firstRun = false;
                        comboBoxEditLibraries_EditValueChanged(null, null);
                    }
                }
            }
        }

        public void buttonItemSettingsAutoWidgets_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (SaveLibraryWarning())
                {
                    _formAutoWidgets.Library = new BusinessClasses.Library(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Name, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Folder, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.UseDirectAccess);
                    if (_formAutoWidgets.ShowDialog() == DialogResult.OK)
                    {
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            formProgress.laProgress.Text = "Apply Settings...";
                            formProgress.TopMost = true;

                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    int libraryIndex = BusinessClasses.LibraryManager.Instance.LibraryCollection.IndexOf(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formAutoWidgets.Library);

                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator = new PresentationClasses.WallBin.Decorators.LibraryDecorator(_formAutoWidgets.Library);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Add(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                });
                            }));

                            formProgress.Show();

                            thread.Start();

                            while (thread.IsAlive)
                                System.Windows.Forms.Application.DoEvents();

                            formProgress.Close();
                            FormMain.Instance.ribbonControl.Enabled = true;
                        }
                        _firstRun = false;
                        comboBoxEditLibraries_EditValueChanged(null, null);
                    }
                }
            }
        }

        public void buttonItemSettingsDeadLinks_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (SaveLibraryWarning())
                {
                    _formDeadLinks.Library = new BusinessClasses.Library(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Name, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Folder, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.UseDirectAccess);
                    if (_formDeadLinks.ShowDialog() == DialogResult.OK)
                    {
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            formProgress.laProgress.Text = "Apply Settings...";
                            formProgress.TopMost = true;

                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    int libraryIndex = BusinessClasses.LibraryManager.Instance.LibraryCollection.IndexOf(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                                    BusinessClasses.LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formDeadLinks.Library);

                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Remove(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator = new PresentationClasses.WallBin.Decorators.LibraryDecorator(_formDeadLinks.Library);
                                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.Decorators.Add(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator);
                                });
                            }));

                            formProgress.Show();

                            thread.Start();

                            while (thread.IsAlive)
                                System.Windows.Forms.Application.DoEvents();

                            formProgress.Close();
                            FormMain.Instance.ribbonControl.Enabled = true;
                        }
                        _firstRun = false;
                        comboBoxEditLibraries_EditValueChanged(null, null);
                    }
                }
            }
        }

        public void buttonItemSettingsEmailList_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                _formEmailList.ShowDialog();
            }
        }

        public void buttonItemSettingsAutoSync_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                _formAutoSync.ShowDialog();
            }
        }

        public void buttonItemSettingsMultitab_CheckedChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                ConfigurationClasses.SettingsManager.Instance.MultitabView = FormMain.Instance.buttonItemSettingsMultitab.Checked;
                ConfigurationClasses.SettingsManager.Instance.Save();
                _firstRun = false;
                comboBoxEditLibraries_EditValueChanged(null, null);
            }
        }

        public void btAddUrl_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage != null)
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.AddUrl();
        }

        public void btAddNeworkShare_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage != null)
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.AddNetworkFolder();
        }

        public void btLineBreak_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage != null)
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.AddLineBreak();
        }

        public void btDownLink_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage != null)
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.DownLink();
        }

        public void btUpLink_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage != null)
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.UpLink();
        }

        public void btOpenLink_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage != null)
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.OpenLink();
        }

        public void btDeleteLink_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage != null)
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.DeleteLink();
        }

        public void btFontUp_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize += 2;
            ConfigurationClasses.SettingsManager.Instance.Save();
            UpdateFontButtonStatus();
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.FitObjectsToPage();
            pnMain.Focus();
        }

        public void btFontDown_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize -= 2;
            ConfigurationClasses.SettingsManager.Instance.Save();
            UpdateFontButtonStatus();
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.FitObjectsToPage();
            pnMain.Focus();
        }

        public void buttonItemHomeProperties_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ActivePage.ActiveBox.ShowLinkProperties(Point.Empty);
        }

        public void btSave_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
            AppManager.Instance.ShowInfo("All Links are saved!");
        }

        public void btSync_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    FormMain.Instance.ribbonControl.Enabled = false;
                    pnEmpty.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                    form.laProgress.Text = "Updating your Sales Library on the network…" + Environment.NewLine + "Chill out and relax for a few minutes…";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        BusinessClasses.LibraryManager.Instance.SynchronizeLibraries();
                    }));
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.ShowProgressDuringSync)
                        form.Show();
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.MinimizeOnSync)
                        FormMain.Instance.WindowState = FormWindowState.Minimized;
                    thread.Start();
                    while (thread.IsAlive)
                        System.Windows.Forms.Application.DoEvents();
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.ShowProgressDuringSync)
                        form.Close();
                    FormMain.Instance.ribbonControl.Enabled = true;
                    pnMain.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.CloseAfterSync)
                        Application.Exit();
                }
            }
        }

        public void btExit_Click(object sender, EventArgs e)
        {
            FormMain.Instance.Close();
        }

        #endregion

        #region Other Methods
        public void InitPage(ToolForms.FormProgress form)
        {
            form.Show();
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
            {
                if (!string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.BackupPath))
                {
                    BusinessClasses.LibraryManager.Instance.LoadLibraries(new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.BackupPath));
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        form.laProgress.Text = BusinessClasses.LibraryManager.Instance.OldStyleProceed ? "Upgrading your Sales Library to Version 6..." : "Loading Libraries...";
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildDecorators();
                        form.Refresh();
                        Application.DoEvents();
                    });

                    foreach (BusinessClasses.Library library in BusinessClasses.LibraryManager.Instance.LibraryCollection)
                        library.ProceedPresentationProperties();

                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        form.laProgress.Text = "Loading Overnights Calendar...";
                        form.Refresh();
                        Application.DoEvents();
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildOvernightsCalendars();

                        FormMain.Instance.buttonItemSettingsMultitab.CheckedChanged -= new EventHandler(buttonItemSettingsMultitab_CheckedChanged);
                        FormMain.Instance.buttonItemSettingsMultitab.Checked = ConfigurationClasses.SettingsManager.Instance.MultitabView;
                        FormMain.Instance.buttonItemSettingsMultitab.CheckedChanged += new EventHandler(buttonItemSettingsMultitab_CheckedChanged);
                        UpdateFontButtonStatus();
                    });
                }
            }));

            thread.Start();

            while (thread.IsAlive)
                System.Windows.Forms.Application.DoEvents();

            form.Close();

            FillLibraries();
            BusinessClasses.LibraryManager.Instance.OldStyleProceed = false;
        }

        private void FillLibraries()
        {
            FormMain.Instance.comboBoxEditLibraries.EditValueChanged -= new EventHandler(comboBoxEditLibraries_EditValueChanged);
            FormMain.Instance.comboBoxEditLibraries.EditValueChanging -= new DevExpress.XtraEditors.Controls.ChangingEventHandler(comboBoxEditLibraries_EditValueChanging);
            FormMain.Instance.comboBoxEditLibraries.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditLibraries.Properties.Items.AddRange(BusinessClasses.LibraryManager.Instance.LibraryCollection.Select(x => x.Name).ToArray());
            if (FormMain.Instance.comboBoxEditLibraries.Properties.Items.Count >= 0)
            {
                if (BusinessClasses.LibraryManager.Instance.SelectedLibrary != null)
                    FormMain.Instance.comboBoxEditLibraries.SelectedIndex = BusinessClasses.LibraryManager.Instance.LibraryCollection.IndexOf(BusinessClasses.LibraryManager.Instance.SelectedLibrary);
                else
                    FormMain.Instance.comboBoxEditLibraries.SelectedIndex = 0;
            }
            _firstRun = true;
            comboBoxEditLibraries_EditValueChanged(null, null);
            FormMain.Instance.comboBoxEditLibraries.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(comboBoxEditLibraries_EditValueChanging);
            FormMain.Instance.comboBoxEditLibraries.EditValueChanged += new EventHandler(comboBoxEditLibraries_EditValueChanged);
        }

        private void FillPages()
        {
            FormMain.Instance.comboBoxEditPages.SelectedIndexChanged -= new EventHandler(comboBoxEditPages_SelectedIndexChanged);
            FormMain.Instance.comboBoxEditPages.Properties.Items.Clear();
            if (!ConfigurationClasses.SettingsManager.Instance.MultitabView)
            {
                FormMain.Instance.comboBoxEditPages.Enabled = true;
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                {
                    FormMain.Instance.comboBoxEditPages.Properties.Items.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Pages.Select(y => y.Name).ToArray());
                    if (FormMain.Instance.comboBoxEditPages.Properties.Items.Count > 1)
                    {
                        int selectedIndex = FormMain.Instance.comboBoxEditPages.Properties.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.SelectedPage);
                        if (selectedIndex != -1)
                            FormMain.Instance.comboBoxEditPages.SelectedIndex = selectedIndex;
                        else
                            FormMain.Instance.comboBoxEditPages.SelectedIndex = 0;
                    }
                    else if (FormMain.Instance.comboBoxEditPages.Properties.Items.Count == 1)
                    {
                        FormMain.Instance.comboBoxEditPages.SelectedIndex = 0;
                    }
                }
                comboBoxEditPages_SelectedIndexChanged(null, null);
            }
            else
                FormMain.Instance.comboBoxEditPages.Enabled = false;
            FormMain.Instance.comboBoxEditPages.SelectedIndexChanged += new EventHandler(comboBoxEditPages_SelectedIndexChanged);
        }

        private void BuildTreeView()
        {
            dockPanelTreeView_Container.Controls.Clear();
            if (_treeList != null)
                _treeList.Dispose();
            _treeList = new PresentationClasses.WallBin.WallBinTreeListControl();
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                List<BusinessClasses.FolderLink> rootFolders = new List<BusinessClasses.FolderLink>();
                rootFolders.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.ExtraFolders);
                rootFolders.Sort((x, y) => (x as BusinessClasses.RootFolder).Order.CompareTo((y as BusinessClasses.RootFolder).Order));
                rootFolders.Insert(0, PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.RootFolder);
                _treeList.Init(rootFolders.ToArray());
            }
        }

        private void ApplyDecorator()
        {
            foreach (Control control in FormMain.Instance.TabHome.pnMain.Controls)
                control.Parent = null;
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.IsConfigured)
            {
                btSetupWallBin.Visible = false;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ApplyWallBin(_firstRun);
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.ApplyOvernightsCalebdar();
            }

            else
            {
                btSetupWallBin.Visible = true;
                btSetupWallBin.BringToFront();
            }
        }

        private bool SaveLibraryWarning()
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged)
                {
                    if (AppManager.Instance.ShowQuestion("Before you EXIT, do you want to save the changes you made?") == DialogResult.Yes)
                    {
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                    }
                    else
                    {
                        if (MessageBox.Show("You are about to lose your changes.\nThe changes will be LOST FOREVER & EVER & EVER!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            return false;
                    }
                }
            }
            return true;
        }

        private void SaveDockPanelState()
        {
            ConfigurationClasses.SettingsManager.Instance.TreeViewVisible = dockPanelTreeView.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible;
            ConfigurationClasses.SettingsManager.Instance.Save();
        }

        private void ShowTreeView(bool show)
        {
            dockPanelTreeView.DockChanged -= new EventHandler(dockPanelTreeView_DockChanged);
            if (show)
                dockPanelTreeView.Show();
            else
                dockPanelTreeView.Hide();
            dockPanelTreeView.Dock = ConfigurationClasses.SettingsManager.Instance.TreeViewDocked ? DevExpress.XtraBars.Docking.DockingStyle.Left : DevExpress.XtraBars.Docking.DockingStyle.Float;
            dockPanelTreeView.FloatLocation = new Point(200, 200);
            if (show)
                dockPanelTreeView.DockChanged += new EventHandler(dockPanelTreeView_DockChanged);
            ConfigurationClasses.SettingsManager.Instance.TreeViewVisible = show;
            ConfigurationClasses.SettingsManager.Instance.Save();
        }

        private void UpdateFontButtonStatus()
        {
            FormMain.Instance.buttonItemHomeFontUp.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize < 20;
            FormMain.Instance.buttonItemHomeFontDown.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize > 8;
        }
        #endregion
    }
}
