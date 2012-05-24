using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.WallBin.Decorators
{
    public class LibraryDecorator
    {
        public BusinessClasses.Library Library { get; set; }
        public PageDecorator ActivePage { get; set; }
        public List<PageDecorator> Pages { get; set; }
        public PresentationClasses.WallBin.MultitabLibraryControl TabControl { get; private set; }
        public PresentationClasses.OvernightsCalendar.OvernightsCalendarControl OvernightsCalendar { get; private set; }

        public bool AllowToSave { get; set; }
        public bool StateChanged { get; set; }

        public LibraryDecorator(BusinessClasses.Library library)
        {
            this.Pages = new List<PageDecorator>();
            this.TabControl = new PresentationClasses.WallBin.MultitabLibraryControl();
            this.OvernightsCalendar = new OvernightsCalendar.OvernightsCalendarControl(this);
            this.Library = library;
            BuildPages();
        }

        private void BuildPages()
        {
            this.Pages.Clear();
            foreach (BusinessClasses.LibraryPage page in this.Library.Pages)
            {
                PageDecorator pageDecorator = new PageDecorator(page);
                pageDecorator.Parent = this;
                this.Pages.Add(pageDecorator);
            }
        }

        public void BuildOvernightsCalendar(bool forceBuild = false)
        {
            this.Library.OvernightsCalendar.LoadYears();
            if (this.Library.OvernightsCalendar.Enabled)
                this.OvernightsCalendar.Build(forceBuild);
        }

        private void DeleteDeadLinks()
        {
            using (ToolForms.WallBin.FormDeleteIncorrectLinks form = new ToolForms.WallBin.FormDeleteIncorrectLinks())
            {
                form.Text = string.Format(form.Text, "Dead");
                form.ExpiredLinks = false;
                form.IncorrectLinks.Clear();
                form.IncorrectLinks.AddRange(this.Library.DeadLinks.ToArray());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.Library.DeleteDeadLinks(form.LinksForDelete.ToArray());
                    this.Library.Save();
                }
            }
        }

        private void DeleteExpiredLinks()
        {
            using (ToolForms.WallBin.FormDeleteIncorrectLinks form = new ToolForms.WallBin.FormDeleteIncorrectLinks())
            {
                form.Text = string.Format(form.Text, "Expired");
                form.ExpiredLinks = true;
                form.IncorrectLinks.Clear();
                form.IncorrectLinks.AddRange(this.Library.ExpiredLinks.ToArray());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.Library.DeleteExpiredLinks(form.LinksForDelete.ToArray());
                    this.Library.Save();
                }
            }
        }

        public void FitObjectsToPage()
        {
            if (ConfigurationClasses.SettingsManager.Instance.MultitabView)
                foreach (PageDecorator page in this.Pages)
                    page.FitObjectsToPage();
            else
                this.ActivePage.FitObjectsToPage();
        }

        public void ApplyDecorator(bool firstRun = false)
        {
            ApplyWallBin(firstRun);
            ApplyOvernightsCalebdar();
        }

        private void ApplyWallBin(bool firstRun)
        {
            FormMain.Instance.TabHome.pnEmpty.Visible = true;
            FormMain.Instance.TabHome.pnEmpty.BringToFront();
            DialogResult result = DialogResult.Cancel;
            if (this.Library.DeadLinks.Count > 0 && this.Library.EnableInactiveLinks && this.Library.InactiveLinksMessageAtStartup && !BusinessClasses.LibraryManager.Instance.OldStyleProceed && firstRun)
                using (ToolForms.WallBin.FormIncorrectLinksNotification form = new ToolForms.WallBin.FormIncorrectLinksNotification())
                {
                    form.pbLogo.Image = Properties.Resources.DeadLinks;
                    form.Text = string.Format(form.Text, "INACTIVE");
                    form.laTitle.Text = string.Format(form.laTitle.Text, "DEAD");
                    result = form.ShowDialog();
                    if (result == DialogResult.OK)
                        DeleteDeadLinks();
                }
            if (this.Library.ExpiredLinks.Count > 0 && !BusinessClasses.LibraryManager.Instance.OldStyleProceed && firstRun)
                using (ToolForms.WallBin.FormIncorrectLinksNotification form = new ToolForms.WallBin.FormIncorrectLinksNotification())
                {
                    form.pbLogo.Image = Properties.Resources.ExpiredLinks;
                    form.Text = string.Format(form.Text, "EXPIRED");
                    form.laTitle.Text = string.Format(form.laTitle.Text, "EXPIRED");
                    result = form.ShowDialog();
                    if (result == DialogResult.OK)
                        DeleteExpiredLinks();
                }

            if (result == DialogResult.OK)
                BuildPages();

            if (ConfigurationClasses.SettingsManager.Instance.MultitabView)
            {
                foreach (Control control in FormMain.Instance.TabHome.pnMain.Controls)
                    control.Parent = null;
                foreach (PageDecorator page in this.Pages)
                {
                    page.Container.Parent = null;
                    page.TabPage.Controls.Add(page.Container);
                    page.FitPage();
                }
                this.TabControl.AddPages(this.Pages.ToArray());
                FormMain.Instance.TabHome.pnMain.Controls.Add(this.TabControl);
            }
            FormMain.Instance.TabHome.pnEmpty.SendToBack();
        }

        private void ApplyOvernightsCalebdar()
        {
            LoadOvernightsCalebdarSettings();
            if (!FormMain.Instance.TabOvernightsCalendar.Controls.Contains(this.OvernightsCalendar))
                FormMain.Instance.TabOvernightsCalendar.Controls.Add(this.OvernightsCalendar);
            this.OvernightsCalendar.BringToFront();
        }

        private void LoadOvernightsCalebdarSettings()
        {
            this.AllowToSave = false;
            FormMain.Instance.buttonItemCalendarSyncStatusDisabled.Checked = !this.Library.OvernightsCalendar.Enabled;
            FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked = this.Library.OvernightsCalendar.Enabled;
            FormMain.Instance.buttonEditCalendarLocation.EditValue = this.Library.OvernightsCalendar.RootFolder.FullName;
            FormMain.Instance.ribbonBarCalendarLocation.Enabled = this.Library.OvernightsCalendar.Enabled;
            FormMain.Instance.ribbonBarCalendarSettings.Enabled = this.Library.OvernightsCalendar.Enabled;
            FormMain.Instance.ribbonBarCalendarFont.Enabled = this.Library.OvernightsCalendar.Enabled;
            FormMain.Instance.ribbonBarCalendarEmailGrabber.Enabled = this.Library.OvernightsCalendar.Enabled;
            FormMain.Instance.TabOvernightsCalendar.Enabled = this.Library.OvernightsCalendar.Enabled;
            this.AllowToSave = true;

            UpdateCalendarFontButtonsStatus();
        }

        public void UpdateCalendarFontButtonsStatus()
        {
            FormMain.Instance.buttonItemCalendarFontUp.Enabled = ConfigurationClasses.SettingsManager.Instance.CalendarFontSize < 14;
            FormMain.Instance.buttonItemCalendarFontDown.Enabled = ConfigurationClasses.SettingsManager.Instance.CalendarFontSize > 10;
        }

        public void Save()
        {
            foreach (PageDecorator page in this.Pages)
                page.Save();
            this.Library.Save();
            this.StateChanged = false;
        }

        public void SelectPage(int pageIndex)
        {
            FormMain.Instance.TabHome.pnEmpty.Visible = true;
            FormMain.Instance.TabHome.pnEmpty.BringToFront();
            foreach (Control control in FormMain.Instance.TabHome.pnMain.Controls)
                control.Parent = null;
            if (pageIndex < this.Pages.Count)
            {
                this.ActivePage = this.Pages[pageIndex];
                this.ActivePage.Apply();
            }
            FormMain.Instance.TabHome.pnEmpty.SendToBack();
        }
    }
}
