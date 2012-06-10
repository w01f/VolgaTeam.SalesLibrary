using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class PowerPointViewer : UserControl, IFileViewer
    {
        #region Properties
        public BusinessClasses.LibraryFile File { get; private set; }

        public string DisplayName
        {
            get
            {
                return this.File.DisplayName;
            }
        }

        public string CriteriaOverlap
        {
            get
            {
                return this.File.CriteriaOverlap;
            }
        }

        public Image Widget
        {
            get
            {
                return this.File.Widget;
            }
        }
        #endregion

        public PowerPointViewer(BusinessClasses.LibraryFile file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;

            laFileInfo.Text = string.Empty;
            laSlideSize.Text = string.Empty;
            pictureBoxPreview.Image = null;
            laSlideNumber.Text = string.Empty;

            if (this.File.PreviewContainer != null)
            {
                laFileInfo.Text = this.File.NameWithExtension + Environment.NewLine + "Added: " + this.File.AddDate.ToString("MM/dd/yy h:mm:ss tt") + Environment.NewLine + (this.File.ExpirationDateOptions.EnableExpirationDate && this.File.ExpirationDateOptions.ExpirationDate != DateTime.MinValue ? ("Expires: " + this.File.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt")) : "No Expiration Date");
                if (this.File.PresentationProperties != null)
                    laSlideSize.Text = string.Format("{0} {1} x {2}", new object[] { this.File.PresentationProperties.Orientation, this.File.PresentationProperties.Width.ToString("#.##"), this.File.PresentationProperties.Height.ToString("#.##") });
                comboBoxEditSlides.SelectedIndexChanged -= new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
                comboBoxEditSlides.Properties.Items.Clear();
                comboBoxEditSlides.Properties.Items.AddRange(this.File.PreviewContainer.Slides.Select(x => x.Index + 1).ToArray());
                if (this.File.PreviewContainer.Slides.Count > 0)
                    comboBoxEditSlides.SelectedIndex = 0;
                comboBoxEditSlides_SelectedIndexChanged(null, null);
                comboBoxEditSlides.SelectedIndexChanged += new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
            }
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
            pictureBoxPreview.Image = null;
            if (this.File.PreviewContainer != null)
                this.File.PreviewContainer.ReleasePreviewImages();
        }

        public void Open()
        {
            BusinessClasses.LinkManager.Instance.OpenCopyOfFile(new FileInfo(this.File.LocalPath));
        }

        public void Save()
        {
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the presentation as", new FileInfo(this.File.LocalPath));
        }

        public void Email()
        {
            InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.File.LocalPath));
            using (ToolForms.WallBin.FormEmailPresentation form = new ToolForms.WallBin.FormEmailPresentation())
            {
                form.SelectedFile = this.File;
                form.ActiveSlide = this.File.PreviewContainer.SelectedIndex + 1;
                form.ShowDialog();
            }
        }

        public void Print()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.File.LocalPath));
            InteropClasses.PowerPointHelper.Instance.PrintPresentation(this.File.PreviewContainer.SelectedIndex + 1);
        }
        #endregion

        #region PowerPoint Methods
        public void InsertSlide()
        {
            if (InteropClasses.PowerPointHelper.Instance.GetActiveSlideIndex() != -1)
            {
                if (this.File.PresentationProperties != null)
                {
                    AppManager.Instance.ActivateMainForm();
                    if ((InteropClasses.PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal && this.File.PresentationProperties.Orientation.Equals("Portrait")) ||
                        (InteropClasses.PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == Microsoft.Office.Core.MsoOrientation.msoOrientationVertical && this.File.PresentationProperties.Orientation.Equals("Landscape")))
                        if (AppManager.Instance.ShowWarningQuestion("This slide is not the same size as your presentation.\nDo you still want to add it?") != DialogResult.Yes)
                            return;
                }
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Inserting selected slide...";
                    form.TopMost = true;
                    Thread thread = new Thread(delegate()
                    {
                        AppManager.Instance.ActivatePowerPoint();
                        AppManager.Instance.ActivateMiniBar();
                        ToolClasses.ActivityRecorder.Instance.WriteActivity();
                        InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.File.LocalPath));
                        InteropClasses.PowerPointHelper.Instance.AppendSlide(this.File.PreviewContainer.SelectedIndex + 1);
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

        public void SaveAsPDF()
        {
            using (ToolForms.WallBin.FormSaveAsPDF form = new ToolForms.WallBin.FormSaveAsPDF())
            {
                DialogResult result = form.ShowDialog();
                bool wholeFile = form.WholeFile;

                if (result != System.Windows.Forms.DialogResult.Cancel)
                {
                    string destinationFileName = Path.Combine(Path.GetTempPath(), this.File.NameWithoutExtesion + ".pdf");

                    using (ToolForms.FormProgress progressForm = new ToolForms.FormProgress())
                    {
                        progressForm.laProgress.Text = "Saving as PDF...";
                        progressForm.TopMost = true;
                        Thread thread = new Thread(delegate()
                        {
                            ToolClasses.ActivityRecorder.Instance.WriteActivity();
                            InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(this.File.LocalPath));
                            InteropClasses.PowerPointHelper.Instance.ExportPresentationAsPDF(wholeFile ? -1 : (this.File.PreviewContainer.SelectedIndex + 1), destinationFileName);
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

        public void OpenInQuickView()
        {
            if (ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView)
                BusinessClasses.LinkManager.Instance.ViewPresentationOld(this.File);
            else
                BusinessClasses.LinkManager.Instance.ViewPresentation(this.File);
        }
        #endregion

        #region GUI Event Handlers
        private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.File != null)
            {
                if (this.File.PreviewContainer != null)
                {
                    this.File.PreviewContainer.SelectedIndex = comboBoxEditSlides.SelectedIndex;
                    pictureBoxPreview.Image = this.File.PreviewContainer.SelectedSlide;
                    laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (this.File.PreviewContainer.SelectedIndex + 1).ToString(), this.File.PreviewContainer.Slides.Count.ToString() });
                }
            }
        }

        private void comboBoxEditSlides_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.File != null)
            {
                if (this.File.PreviewContainer != null)
                {
                    if (e.Button.Index == 1)
                    {
                        this.File.PreviewContainer.SelectedIndex++;
                        if (this.File.PreviewContainer.SelectedIndex >= this.File.PreviewContainer.Slides.Count)
                            this.File.PreviewContainer.SelectedIndex = 0;
                        comboBoxEditSlides.SelectedIndex = this.File.PreviewContainer.SelectedIndex;
                    }
                    else if (e.Button.Index == 2)
                    {
                        this.File.PreviewContainer.SelectedIndex--;
                        if (this.File.PreviewContainer.SelectedIndex < 0)
                            this.File.PreviewContainer.SelectedIndex = this.File.PreviewContainer.Slides.Count - 1;
                        comboBoxEditSlides.SelectedIndex = this.File.PreviewContainer.SelectedIndex;
                    }
                }
            }
        }

        private void pnNavigationArea_Resize(object sender, EventArgs e)
        {
            comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
            laFileInfo.Width = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
        }

        private void pictureBoxPreview_DoubleClick(object sender, EventArgs e)
        {
            OpenInQuickView();
        }
        #endregion
    }
}
