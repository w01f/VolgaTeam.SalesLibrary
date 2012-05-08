using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    public partial class FormPowerPointQuickView : Form
    {
        public BusinessClasses.LibraryFile SelectedFile { get; set; }

        public FormPowerPointQuickView()
        {
            InitializeComponent();
        }

        #region Form GUI Event Habdlers
        private void FormQuickView_Shown(object sender, EventArgs e)
        {
            if (this.SelectedFile != null)
            {
                this.Text = "QuickView - " + this.SelectedFile.PropertiesName;
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

                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Loading Presentation...";
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        if (this.SelectedFile.PreviewContainer != null)
                            this.SelectedFile.PreviewContainer.GetPreviewImages();
                    }));
                    thread.Start();

                    form.Show();

                    while (thread.ThreadState == System.Threading.ThreadState.Running)
                        Application.DoEvents();

                    form.Close();
                }

                comboBoxEditSlides.SelectedIndexChanged -= new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
                comboBoxEditSlides.Properties.Items.Clear();
                comboBoxEditSlides.Properties.Items.AddRange(this.SelectedFile.PreviewContainer.Slides.Select(x => x.Index + 1).ToArray());
                if (this.SelectedFile.PreviewContainer.Slides.Count > 0)
                    comboBoxEditSlides.SelectedIndex = 0;
                barLargeButtonItemAddAllSlides.Enabled = this.SelectedFile.PreviewContainer.Slides.Count > 1;
                comboBoxEditSlides_SelectedIndexChanged(null, null);
                comboBoxEditSlides.SelectedIndexChanged += new EventHandler(comboBoxEditSlides_SelectedIndexChanged);

                barLargeButtonItemPDF.Enabled = !InteropClasses.PowerPointHelper.Instance.Is2003;
                barLargeButtonItemEmail.Enabled = (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayQuickView;
            }
            ConfigurationClasses.RegistryHelper.SalesDepotHandle = this.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = false;
        }

        private void FormQuickView_Resize(object sender, EventArgs e)
        {
            comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
            laFileInfo.Width = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
        }
        #endregion

        #region Button Clicks
        private void barButtonItemOpenLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.SelectedFile != null)
                BusinessClasses.LinkManager.OpenCopyOfFile(new FileInfo(this.SelectedFile.FullPath));
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.SelectedFile != null)
            {
                ActivityRecorder.Instance.WriteActivity();
                BusinessClasses.LinkManager.SaveFile("Save copy of the presentation as", new FileInfo(this.SelectedFile.FullPath));
            }
        }

        private void barButtonItemSaveAsPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.SelectedFile != null)
            {
                using (ToolForms.FormSaveAsPDF form = new ToolForms.FormSaveAsPDF())
                {
                    DialogResult result = form.ShowDialog();
                    bool wholeFile = form.WholeFile;

                    if (result != System.Windows.Forms.DialogResult.Cancel)
                    {
                        string destinationFileName = Path.Combine(Path.GetTempPath(), this.SelectedFile.NameWithoutExtesion + ".pdf");

                        using (ToolForms.FormProgress progressForm = new ToolForms.FormProgress())
                        {
                            progressForm.laProgress.Text = "Saving as PDF...";
                            progressForm.TopMost = true;
                            Thread thread = new Thread(delegate()
                            {
                                ActivityRecorder.Instance.WriteActivity();
                                InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.SelectedFile.FullPath));
                                InteropClasses.PowerPointHelper.Instance.ExportPresentationAsPDF(wholeFile ? -1 : (this.SelectedFile.PreviewContainer.SelectedIndex + 1), destinationFileName);
                            });
                            thread.Start();
                            progressForm.Show();

                            while (thread.IsAlive)
                                Application.DoEvents();

                            progressForm.Close();

                            BusinessClasses.LinkManager.SaveFile("Save PDF as", new FileInfo(destinationFileName), false);
                        }
                    }
                }
            }
        }

        private void barButtonItemEmailLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.SelectedFile != null)
            {
                InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.SelectedFile.FullPath));
                using (ToolForms.FormEmailPresentation form = new ToolForms.FormEmailPresentation())
                {
                    form.SelectedFile = this.SelectedFile;
                    form.ActiveSlide = this.SelectedFile.PreviewContainer.SelectedIndex + 1;
                    form.ShowDialog();
                }
            }
        }

        private void barButtonItemPrintLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.SelectedFile != null)
            {
                ActivityRecorder.Instance.WriteActivity();
                InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.SelectedFile.FullPath));
                InteropClasses.PowerPointHelper.Instance.PrintPresentation(this.SelectedFile.PreviewContainer.SelectedIndex + 1);
            }
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
            if (this.SelectedFile != null)
            {
                if (this.SelectedFile.PreviewContainer != null)
                {
                    this.SelectedFile.PreviewContainer.SelectedIndex = comboBoxEditSlides.SelectedIndex;
                    pictureBoxPreview.Image = this.SelectedFile.PreviewContainer.SelectedSlide;
                    laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (this.SelectedFile.PreviewContainer.SelectedIndex + 1).ToString(), this.SelectedFile.PreviewContainer.Slides.Count.ToString() });
                }
            }
        }

        private void comboBoxEditSlides_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.SelectedFile != null)
            {
                if (this.SelectedFile.PreviewContainer != null)
                {
                    if (e.Button.Index == 1)
                    {
                        this.SelectedFile.PreviewContainer.SelectedIndex++;
                        if (this.SelectedFile.PreviewContainer.SelectedIndex >= this.SelectedFile.PreviewContainer.Slides.Count)
                            this.SelectedFile.PreviewContainer.SelectedIndex = 0;
                        comboBoxEditSlides.SelectedIndex = this.SelectedFile.PreviewContainer.SelectedIndex;
                    }
                    else if (e.Button.Index == 2)
                    {
                        this.SelectedFile.PreviewContainer.SelectedIndex--;
                        if (this.SelectedFile.PreviewContainer.SelectedIndex < 0)
                            this.SelectedFile.PreviewContainer.SelectedIndex = this.SelectedFile.PreviewContainer.Slides.Count - 1;
                        comboBoxEditSlides.SelectedIndex = this.SelectedFile.PreviewContainer.SelectedIndex;
                    }
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
                        Thread thread = new Thread(delegate()
                        {
                            ActivityRecorder.Instance.WriteActivity();
                            InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.SelectedFile.FullPath));
                            InteropClasses.PowerPointHelper.Instance.AppendSlide(allSlides ? -1 : (this.SelectedFile.PreviewContainer.SelectedIndex + 1), checkEditChangeSlideTemplate.Checked && comboBoxEditSlideTemplate.EditValue != null ? BusinessClasses.MasterWizardManager.Instance.MasterWizards[comboBoxEditSlideTemplate.EditValue.ToString()].TemplatePath : string.Empty);
                        });
                        thread.Start();
                        form.Show();
                        while (thread.IsAlive)
                            Application.DoEvents();
                        form.Close();
                    }
                    using (ToolForms.FormSlideOutput form = new ToolForms.FormSlideOutput())
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
                    using (ToolForms.FormSelectSlideWarning warningForm = new ToolForms.FormSelectSlideWarning())
                        warningForm.ShowDialog();
                }
            }
        }
        #endregion
    }
}
