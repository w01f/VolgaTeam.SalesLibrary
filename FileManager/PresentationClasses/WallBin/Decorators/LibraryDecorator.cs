using System.Collections.Generic;
using System.Windows.Forms;
using FileManager.PresentationClasses.IPad;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.Decorators
{
	public class LibraryDecorator
	{
		public Library Library { get; set; }
		public PageDecorator ActivePage { get; set; }
		public List<PageDecorator> Pages { get; set; }
		public PresentationClasses.WallBin.MultitabLibraryControl TabControl { get; private set; }
		public PresentationClasses.OvernightsCalendar.OvernightsCalendarControl OvernightsCalendar { get; private set; }
		public PresentationClasses.IPad.IPadManagerControl IPadManager { get; private set; }
		public PresentationClasses.IPad.IPadUsersControl IPadUsers { get; private set; }

		public bool AllowToSave { get; set; }
		public bool StateChanged { get; set; }

		public LibraryDecorator(Library library)
		{
			this.Pages = new List<PageDecorator>();
			this.TabControl = new PresentationClasses.WallBin.MultitabLibraryControl();
			this.OvernightsCalendar = new OvernightsCalendar.OvernightsCalendarControl(this);
			this.IPadManager = new IPad.IPadManagerControl(this);
			this.IPadUsers = new IPadUsersControl(this);
			this.Library = library;
			BuildWallbin();
		}

		#region Wallbin Part
		private void BuildWallbin()
		{
			this.Pages.Clear();
			foreach (LibraryPage page in this.Library.Pages)
			{
				PageDecorator pageDecorator = new PageDecorator(page);
				pageDecorator.Parent = this;
				this.Pages.Add(pageDecorator);
				Application.DoEvents();
			}
			this.StateChanged = false;
		}

		public void ApplyWallbin(bool firstRun, bool reload = false)
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

			if (result == DialogResult.OK || reload)
				BuildWallbin();

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

		public void Save()
		{
			foreach (PageDecorator page in this.Pages)
				page.Save();
			this.Library.Save();
			this.StateChanged = false;
			this.IPadManager.UpdateVideoFiles();
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
		#endregion

		#region Overnights Calendar Part
		public void BuildOvernightsCalendar(bool forceBuild = false)
		{
			this.Library.OvernightsCalendar.LoadYears();
			if (this.Library.OvernightsCalendar.Enabled)
				this.OvernightsCalendar.Build(forceBuild);
		}

		public void ApplyOvernightsCalebdar()
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
			FormMain.Instance.ribbonBarCalendarFileGrabber.Enabled = this.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.TabOvernightsCalendar.Enabled = this.Library.OvernightsCalendar.Enabled;
			this.AllowToSave = true;

			UpdateCalendarFontButtonsStatus();
		}

		public void UpdateCalendarFontButtonsStatus()
		{
			FormMain.Instance.buttonItemCalendarFontUp.Enabled = ConfigurationClasses.SettingsManager.Instance.CalendarFontSize < 14;
			FormMain.Instance.buttonItemCalendarFontDown.Enabled = ConfigurationClasses.SettingsManager.Instance.CalendarFontSize > 10;
		}
		#endregion

		#region ProgramManager Part
		public void ApplyProgramManager()
		{
			this.AllowToSave = false;
			FormMain.Instance.buttonItemProgramManagerSyncDisabled.Checked = !this.Library.EnableProgramManagerSync;
			FormMain.Instance.buttonItemProgramManagerSyncEnabled.Checked = this.Library.EnableProgramManagerSync;
			FormMain.Instance.buttonEditProgramManagerLocation.EditValue = this.Library.ProgramManagerLocation;
			FormMain.Instance.ribbonBarProgramManagerLocation.Enabled = this.Library.EnableProgramManagerSync;
			this.AllowToSave = true;
		}
		#endregion

		#region IPad Part
		public void ApplyIPadManager()
		{
			this.AllowToSave = false;
			FormMain.Instance.buttonItemIPadSyncDisabled.Checked = !this.Library.IPadManager.Enabled;
			FormMain.Instance.buttonItemIPadSyncEnabled.Checked = this.Library.IPadManager.Enabled;
			FormMain.Instance.buttonEditIPadLocation.EditValue = !string.IsNullOrEmpty(this.Library.IPadManager.SyncDestinationPath) ? this.Library.IPadManager.SyncDestinationPath : null;
			FormMain.Instance.buttonEditIPadSite.EditValue = !string.IsNullOrEmpty(this.Library.IPadManager.Website) ? this.Library.IPadManager.Website : null;
			FormMain.Instance.buttonEditIPadLogin.EditValue = !string.IsNullOrEmpty(this.Library.IPadManager.Login) ? this.Library.IPadManager.Login : null;
			FormMain.Instance.buttonEditIPadPassword.EditValue = !string.IsNullOrEmpty(this.Library.IPadManager.Password) ? this.Library.IPadManager.Password : null;
			this.IPadManager.UpdateVideoFiles();
			this.IPadUsers.UpdateUsers(false);
			this.IPadManager.UpdateControlsState();
			if (!FormMain.Instance.TabIPadManager.Controls.Contains(this.IPadManager))
				FormMain.Instance.TabIPadManager.Controls.Add(this.IPadManager);
			if (!FormMain.Instance.TabIPadUsers.Controls.Contains(this.IPadUsers))
				FormMain.Instance.TabIPadUsers.Controls.Add(this.IPadUsers);
			this.IPadManager.BringToFront();
			this.AllowToSave = true;
		}
		#endregion
	}
}
