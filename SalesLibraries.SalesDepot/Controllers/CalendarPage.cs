using System;
using System.IO;
using System.Linq;
using DevComponents.DotNetBar;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.PresentationLayer.Calendars;

namespace SalesLibraries.SalesDepot.Controllers
{
	class CalendarPage : CalendarContainerControl, IPageController
	{
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		protected LibraryContext ActiveLibraryContext
		{
			get
			{
				var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
				return activeWallbin != null ?
					 activeWallbin.DataStorage :
					null;
			}
		}

		public CalendarPage()
		{
			NeedToUpdate = true;
		}

		public void InitController()
		{
			DataStateObserver.Instance.DataChanged += (o, e) =>
			{
				if (e.ChangeType != DataChangeType.LibrarySelected) return;
				OnLibraryChanged(o, e);
			};

			MainController.Instance.MainForm.buttonItemCalendarFontSizeLarger.Click += buttonItemCalendarFontUp_Click;
			MainController.Instance.MainForm.buttonItemCalendarFontSizeSmaler.Click += buttonItemCalendarFontDown_Click;
			MainController.Instance.MainForm.buttonItemCalendarHelp.Click += buttonItemCalendarHelp_Click;

			UpdateFontButtonsState();
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;

			if (NeedToUpdate)
			{
				NeedToUpdate = false;
				OnLibraryChanged(this, EventArgs.Empty);
			}

			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(this))
				MainController.Instance.MainForm.pnContainer.Controls.Add(this);
			BringToFront();
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			MainController.Instance.MainForm.ribbonTabItemCalendar.Enabled = ActiveLibraryContext.Library.Calendar.Enabled;

			if (!IsActive)
			{
				NeedToUpdate = true;
				return;
			}
			if (ActiveLibraryContext == null) return;
			LoadCalendarControls();
		}

		private void LoadCalendarControls()
		{
			var calendarSettings = ActiveLibraryContext.Library.Calendar.Clone<CalendarSettings>(null);
			calendarSettings.Path = Path.Combine(ActiveLibraryContext.Library.Path, Constants.OvernightsCalendarRootFolderName);
			LoadData(calendarSettings);
			LoadCalendarPartToggles();
		}

		private void LoadCalendarPartToggles()
		{
			MainController.Instance.MainForm.ribbonPanelCalendar.SuspendLayout();
			MainController.Instance.MainForm.ribbonBarCalendarParts.Items.Clear();
			var partToggles = GetPartToggleButtons().ToArray();
			if (partToggles.Any())
				MainController.Instance.MainForm.ribbonBarCalendarParts.Items.AddRange(partToggles);
			else
			{
				var emptyLabel = new LabelItem();
				emptyLabel.ForeColor = System.Drawing.Color.White;
				emptyLabel.Text = "No Calendars";
				MainController.Instance.MainForm.ribbonBarCalendarParts.Items.Add(emptyLabel);
			}
			MainController.Instance.MainForm.ribbonPanelCalendar.ResumeLayout();
			MainController.Instance.MainForm.ribbonBarCalendarParts.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelCalendar.PerformLayout();
		}

		private void UpdateFontButtonsState()
		{
			MainController.Instance.MainForm.buttonItemCalendarFontSizeLarger.Enabled = MainController.Instance.Settings.CalendarViewSettings.FontSize < 14;
			MainController.Instance.MainForm.buttonItemCalendarFontSizeSmaler.Enabled = MainController.Instance.Settings.CalendarViewSettings.FontSize > 10;
		}

		private void buttonItemCalendarFontUp_Click(object sender, EventArgs e)
		{
			UpFont();
			UpdateFontButtonsState();
		}

		private void buttonItemCalendarFontDown_Click(object sender, EventArgs e)
		{
			DownFont();
			UpdateFontButtonsState();
		}

		private void buttonItemCalendarHelp_Click(object sender, EventArgs e)
		{
			MainController.Instance.HelpManager.OpenHelpLink("overnights");
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// CalendarPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.Name = "CalendarPage";
			this.Size = new System.Drawing.Size(823, 904);
			this.ResumeLayout(false);

		}
	}
}
