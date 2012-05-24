using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
    public partial class FormPowerPointQuickViewOld : Form
    {
        private double _scaleK;
        private FileInfo _viewedFile;
        private FileInfo _originalFile;

        public BusinessClasses.LibraryFile SelectedFile { get; set; }

        public FormPowerPointQuickViewOld()
        {
            InitializeComponent();
        }

        #region Form GUI Event Habdlers
        private void FormQuickView_Load(object sender, EventArgs e)
        {
            if (this.SelectedFile != null)
            {
                _originalFile = new FileInfo(this.SelectedFile.LocalPath);
                _viewedFile = _originalFile.CopyTo(Path.Combine(AppManager.Instance.TempFolder.FullName, DateTime.Now.ToString("yyyyMMdd-hmmsstt.PPT")), true);


                this.Text = "QuickView - " + this.SelectedFile.NameWithExtension;
                laFileInfo.Text = "Added: " + this.SelectedFile.AddDate.ToString("MM/dd/yy h:mm:ss tt") + Environment.NewLine + (this.SelectedFile.ExpirationDateOptions.EnableExpirationDate && this.SelectedFile.ExpirationDateOptions.ExpirationDate != DateTime.MinValue ? ("Expires: " + this.SelectedFile.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt")) : "No Expiration Date");
                if (this.SelectedFile.PresentationProperties != null)
                    laSlideSize.Text = string.Format("{0} {1} x {2}", new object[] { this.SelectedFile.PresentationProperties.Orientation, this.SelectedFile.PresentationProperties.Width.ToString("#.##"), this.SelectedFile.PresentationProperties.Height.ToString("#.##") });
                if (this.SelectedFile.PresentationProperties.Width == 10 && this.SelectedFile.PresentationProperties.Height == 7.5 && this.SelectedFile.PresentationProperties.Orientation.Equals("Landscape") && BusinessClasses.MasterWizardManager.Instance.MasterWizards.Count > 1)
                {
                    pnSlideTemplate.Visible = true;
                    comboBoxEditSlideTemplate.Properties.Items.AddRange(BusinessClasses.MasterWizardManager.Instance.MasterWizards.Keys);
                    comboBoxEditSlideTemplate.SelectedIndex = 0;
                }
                else
                    pnSlideTemplate.Visible = false;

                FormMain.Instance.TopMost = true;
                InteropClasses.PowerPointHelper.Instance.PowerPointVisible = true;

                if (IsLargeFont())
                    _scaleK = 1.67;
                else
                    _scaleK = 1.35;

                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Loading the presentation...";
                    form.TopMost = true;

                    IntPtr containerHandle = pnPreview.Handle;
                    int containerHeight = pnPreview.Height;
                    int containerWidth = pnPreview.Width;

                    Thread thread = new Thread(delegate()
                    {
                        InteropClasses.PowerPointHelper.Instance.ViewSlideShow(_viewedFile);
                        InteropClasses.PowerPointHelper.Instance.ResizeSlideShow(containerHandle, (int)(containerHeight / _scaleK), (int)(containerWidth / _scaleK));
                    });
                    thread.Start();
                    form.Show();
                    while (thread.IsAlive)
                        Application.DoEvents();
                    form.Close();
                }

                if (InteropClasses.PowerPointHelper.Instance.SlideSourcePresentation != null)
                {
                    comboBoxEditSlides.SelectedIndexChanged -= new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
                    comboBoxEditSlides.Properties.Items.Clear();
                    for (int i = 1; i <= InteropClasses.PowerPointHelper.Instance.SlideSourcePresentation.Slides.Count; i++)
                        comboBoxEditSlides.Properties.Items.Add(i.ToString());
                    if (comboBoxEditSlides.Properties.Items.Count > 0)
                        comboBoxEditSlides.SelectedIndex = 0;
                    barLargeButtonItemAddAllSlides.Enabled = comboBoxEditSlides.Properties.Items.Count > 1;
                    comboBoxEditSlides_SelectedIndexChanged(null, null);
                    comboBoxEditSlides.SelectedIndexChanged += new EventHandler(comboBoxEditSlides_SelectedIndexChanged);

                    bool differentOrientations = false;
                    try
                    {
                        differentOrientations = InteropClasses.PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation !=
                            InteropClasses.PowerPointHelper.Instance.SlideSourcePresentation.PageSetup.SlideOrientation;
                    }
                    catch
                    {
                        differentOrientations = InteropClasses.PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation !=
                            InteropClasses.PowerPointHelper.Instance.SlideSourcePresentation.PageSetup.SlideOrientation;
                    }

                    barLargeButtonItemPDF.Enabled = !InteropClasses.PowerPointHelper.Instance.Is2003;
                    barLargeButtonItemEmail.Enabled = (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView;
                }

                this.Activate();
                FormMain.Instance.TopMost = false;
                AppManager.Instance.ActivateMiniBar();
            }
            ConfigurationClasses.RegistryHelper.RemoteLibraryHandle = this.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeRemoteLibrary = false;
        }

        private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Closing the presentation...";
                form.TopMost = true;
                Thread thread = new Thread(delegate()
                {
                    InteropClasses.PowerPointHelper.Instance.ExitSlideShow();
                });
                thread.Start();
                form.Show();
                while (thread.IsAlive)
                    Application.DoEvents();
                form.Close();
            }

            try
            {
                _viewedFile.Delete();
            }
            catch
            {
            }
        }

        private void FormQuickView_Resize(object sender, EventArgs e)
        {
            comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
            laFileInfo.Width = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;

            FormMain.Instance.TopMost = true;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Resizing the presentation...";
                form.TopMost = true;
                IntPtr containerHandle = pnPreview.Handle;
                int containerHeight = pnPreview.Height;
                int containerWidth = pnPreview.Width;
                Thread thread = new Thread(delegate()
                {
                    InteropClasses.PowerPointHelper.Instance.ResizeSlideShow(containerHandle, (int)(containerHeight / _scaleK), (int)(containerWidth / _scaleK));
                });
                thread.Start();
                form.Show();
                while (thread.IsAlive)
                    Application.DoEvents();
                form.Close();
            }
            FormMain.Instance.TopMost = false;
            AppManager.Instance.ActivateMiniBar();
        }
        #endregion

        #region Button Clicks
        private void barButtonItemOpenLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BusinessClasses.LinkManager.Instance.OpenCopyOfFile(_originalFile);
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the presentation as", _originalFile);
        }

        private void barButtonItemSaveAsPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (ToolForms.WallBin.FormSaveAsPDF form = new ToolForms.WallBin.FormSaveAsPDF())
            {
                DialogResult result = form.ShowDialog();
                bool wholeFile = form.WholeFile;

                if (result != System.Windows.Forms.DialogResult.Cancel)
                {
                    string destinationFileName = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(_originalFile.FullName) + ".pdf");
                    int selectedIndex = comboBoxEditSlides.SelectedIndex + 1;
                    using (ToolForms.FormProgress progressForm = new ToolForms.FormProgress())
                    {
                        progressForm.laProgress.Text = "Saving as PDF...";
                        progressForm.TopMost = true;
                        Thread thread = new Thread(delegate()
                        {
                            ToolClasses.ActivityRecorder.Instance.WriteActivity();
                            InteropClasses.PowerPointHelper.Instance.ExportPresentationAsPDF(wholeFile ? -1 : selectedIndex, destinationFileName);
                        });
                        thread.Start();
                        progressForm.Show();

                        while (thread.IsAlive)
                            Application.DoEvents();

                        progressForm.Close();

                        BusinessClasses.LinkManager.Instance.SaveFile("Save PDF as", new FileInfo(destinationFileName), false);
                    }
                }
            }
        }

        private void barButtonItemEmailLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.SelectedFile != null)
            {
                using (ToolForms.WallBin.FormEmailPresentation form = new ToolForms.WallBin.FormEmailPresentation())
                {
                    form.SelectedFile = this.SelectedFile;
                    form.ActiveSlide = comboBoxEditSlides.SelectedIndex + 1;
                    form.ShowDialog();
                }
            }
        }

        private void barButtonItemPrintLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            InteropClasses.PowerPointHelper.Instance.PrintPresentation(comboBoxEditSlides.SelectedIndex + 1);
        }

        private void barLargeButtonItemAddAllSlides_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertSlide(true);
        }

        private void barLargeButtonItemAddSlide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertSlide();
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

        #region Other Event Handlers
        private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
        {
            InteropClasses.PowerPointHelper.Instance.SlideShowWindow.View.GotoSlide(comboBoxEditSlides.SelectedIndex + 1, Microsoft.Office.Core.MsoTriState.msoFalse);
            laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (comboBoxEditSlides.SelectedIndex + 1).ToString(), comboBoxEditSlides.Properties.Items.Count.ToString() });
        }

        private void comboBoxEditSlides_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (comboBoxEditSlides.Properties.Items.Count > 0)
            {
                if (e.Button.Index == 1)
                {
                    if (comboBoxEditSlides.SelectedIndex < comboBoxEditSlides.Properties.Items.Count - 1)
                        comboBoxEditSlides.SelectedIndex++;
                    else
                        comboBoxEditSlides.SelectedIndex = 0;
                }
                else if (e.Button.Index == 2)
                {
                    if (comboBoxEditSlides.SelectedIndex > 0)
                        comboBoxEditSlides.SelectedIndex--;
                    else
                        comboBoxEditSlides.SelectedIndex = comboBoxEditSlides.Properties.Items.Count - 1;
                }
            }
        }

        private void checkEditSlideTemplate_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditSlideTemplate.Enabled = checkEditChangeSlideTemplate.Checked;
        }
        #endregion

        #region Common Methods
        public void InsertSlide(bool allSlides = false)
        {
            if (this.SelectedFile != null)
            {
                if (InteropClasses.PowerPointHelper.Instance.GetActiveSlideIndex() != -1)
                {
                    AppManager.Instance.ActivatePowerPoint();
                    AppManager.Instance.ActivateMainForm();
                    if (this.SelectedFile.PresentationProperties != null)
                    {
                        if ((InteropClasses.PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal && this.SelectedFile.PresentationProperties.Orientation.Equals("Portrait")) ||
                            (InteropClasses.PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == Microsoft.Office.Core.MsoOrientation.msoOrientationVertical && this.SelectedFile.PresentationProperties.Orientation.Equals("Landscape")))
                            if (AppManager.Instance.ShowWarningQuestion("This slide is not the same size as your presentation.\nDo you still want to add it?") != DialogResult.Yes)
                                return;
                    }
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = "Inserting slides...";
                        form.TopMost = true;
                        int selectedIndex = comboBoxEditSlides.SelectedIndex + 1;
                        Thread thread = new Thread(delegate()
                        {
                            ToolClasses.ActivityRecorder.Instance.WriteActivity();
                            InteropClasses.PowerPointHelper.Instance.AppendSlide(allSlides ? -1 : selectedIndex, checkEditChangeSlideTemplate.Checked && comboBoxEditSlideTemplate.EditValue != null ? BusinessClasses.MasterWizardManager.Instance.MasterWizards[comboBoxEditSlideTemplate.EditValue.ToString()].TemplatePath : string.Empty);
                        });
                        thread.Start();
                        form.Show();
                        while (thread.IsAlive)
                            Application.DoEvents();
                        form.Close();
                    }
                    using (ToolForms.WallBin.FormSlideOutput form = new ToolForms.WallBin.FormSlideOutput())
                    {
                        DialogResult result = form.ShowDialog();
                        switch (result)
                        {
                            case System.Windows.Forms.DialogResult.Cancel:
                                AppManager.Instance.ActivateMainForm();
                                this.Activate();
                                this.Focus();
                                break;
                            case System.Windows.Forms.DialogResult.Abort:
                                Application.Exit();
                                break;
                        }
                    }
                }
                else
                {
                    using (ToolForms.WallBin.FormSelectSlideWarning warningForm = new ToolForms.WallBin.FormSelectSlideWarning())
                        warningForm.ShowDialog();
                }
            }
        }

        private bool IsLargeFont()
        {
            System.Drawing.Graphics g = base.CreateGraphics();
            if (g.DpiX > 96)
                return true;
            else
                return false;
        }
        #endregion
    }
}
