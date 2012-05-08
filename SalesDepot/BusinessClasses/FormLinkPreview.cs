﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    public partial class FormLinkPreview : Form
    {
        private BusinessClasses.IFileViewer _selectedFileViewer = null;
        public BusinessClasses.LibraryFile SelectedFile { get; set; }

        public FormLinkPreview()
        {
            InitializeComponent();
        }

        #region Form GUI Event Habdlers
        private void FormQuickView_Shown(object sender, EventArgs e)
        {
            if (this.SelectedFile != null)
            {
                this.Text = "Preview - " + this.SelectedFile.PropertiesName;

                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Loading preview...";
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        _selectedFileViewer = null;
                        if (this.SelectedFile.Type != FileTypes.MediaPlayerVideo)
                            barManager.Items.Remove(barLargeButtonItemInsert);
                        switch (this.SelectedFile.Type)
                        {
                            case BusinessClasses.FileTypes.Excel:
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { _selectedFileViewer = new CustomControls.Viewers.ExcelViewer(this.SelectedFile); }
                                    catch { _selectedFileViewer = new CustomControls.Viewers.DefaultViewer(this.SelectedFile); }
                                });
                                break;
                            case BusinessClasses.FileTypes.Word:
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { _selectedFileViewer = new CustomControls.Viewers.WordViewer(this.SelectedFile); }
                                    catch { _selectedFileViewer = new CustomControls.Viewers.DefaultViewer(this.SelectedFile); }
                                });
                                break;
                            case BusinessClasses.FileTypes.PDF:
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { _selectedFileViewer = new CustomControls.Viewers.PDFViewer(this.SelectedFile); }
                                    catch { _selectedFileViewer = new CustomControls.Viewers.DefaultViewer(this.SelectedFile); }
                                });
                                break;
                            case BusinessClasses.FileTypes.MediaPlayerVideo:
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    barManager.Items.Add(barLargeButtonItemInsert);
                                    try
                                    { _selectedFileViewer = new CustomControls.Viewers.VideoViewer(this.SelectedFile); }
                                    catch { _selectedFileViewer = new CustomControls.Viewers.DefaultViewer(this.SelectedFile); }
                                });
                                break;
                            default:
                                this.Invoke((MethodInvoker)delegate() { _selectedFileViewer = new CustomControls.Viewers.DefaultViewer(this.SelectedFile); });
                                break;
                        }
                        this.Invoke((MethodInvoker)delegate()
                        {
                            if (_selectedFileViewer != null)
                            {
                                (_selectedFileViewer as Control).Visible = true;
                                pnPreview.Controls.Add(_selectedFileViewer as Control);
                            }

                        });
                    }));
                    thread.Start();

                    form.Show();

                    while (thread.IsAlive)
                        Application.DoEvents();

                    form.Close();
                }
                barLargeButtonItemEmail.Visibility = (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView && (this.SelectedFile.Type == FileTypes.Word || this.SelectedFile.Type == FileTypes.Excel || this.SelectedFile.Type == FileTypes.PDF) ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                barLargeButtonItemPrint.Visibility = this.SelectedFile.Type == FileTypes.Word || this.SelectedFile.Type == FileTypes.Excel || this.SelectedFile.Type == FileTypes.PDF ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            }
            ConfigurationClasses.RegistryHelper.SalesDepotHandle = this.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = false;
        }
        #endregion

        #region Button Clicks
        private void barButtonItemOpenLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Open();
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Save();
        }

        private void barLargeButtonItemInsert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CustomControls.Viewers.VideoViewer viewer = _selectedFileViewer as CustomControls.Viewers.VideoViewer;
            if (viewer != null)
                viewer.InsertIntoPresentation();
        }

        private void barButtonItemEmailLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Email();
        }

        private void barButtonItemPrintLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Print();
        }

        private void barLargeButtonItemHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("qv");
        }

        private void barLargeButtonItemExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
