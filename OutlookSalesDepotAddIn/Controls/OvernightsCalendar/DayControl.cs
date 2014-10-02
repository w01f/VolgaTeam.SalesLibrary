using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;
using OutlookSalesDepotAddIn.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.OvernightsCalendar
{
	[ToolboxItem(false)]
	public sealed partial class DayControl : Label
	{
		private Cursor _storedCursor;

		public DayControl(CalendarDay data)
		{
			InitializeComponent();
			Data = data;
			Text = data.Date.ToString("dd");
			Font = new Font(Font.Name, SettingsManager.CalendarFontSize, Font.Style);
			RefreshColors();

		}

		public CalendarDay Data { get; private set; }

		private void RefreshColors()
		{
			if (Data.IsSweepDay)
			{
				BackColor = Data.Parent.Parent.Parent.Parent.SweepBackColor;
				if (Data.LinkedFile == null)
					ForeColor = Data.Parent.Parent.Parent.Parent.DeadLinksForeColor;
				else
					ForeColor = Data.Parent.Parent.Parent.Parent.SweepForeColor;
			}
			else
			{
				BackColor = Data.Parent.Parent.Parent.Parent.MonthBodyBackColor;
				if (Data.LinkedFile == null)
					ForeColor = Data.Parent.Parent.Parent.Parent.DeadLinksForeColor;
				else
					ForeColor = Data.Parent.Parent.Parent.Parent.MonthBodyForeColor;
			}
			Refresh();
		}

		private void DayControl_MouseEnter(object sender, EventArgs e)
		{
			_storedCursor = Cursor;
			Cursor = Cursors.Hand;
		}

		private void DayControl_MouseLeave(object sender, EventArgs e)
		{
			Cursor = _storedCursor;
		}

		private void DayControl_MouseDown(object sender, MouseEventArgs e)
		{
			BackColor = Color.Blue;
			ForeColor = Color.White;
			contextMenuStrip.Show(sender as Control, e.Location);
		}

		private void DayControl_MouseUp(object sender, MouseEventArgs e)
		{
			RefreshColors();
		}

		private void toolStripMenuItemAttach_Click(object sender, EventArgs e)
		{
			if (Data.LinkedFile == null) return;
			var libraryFile = new LibraryLink(new LibraryFolder(new LibraryPage(Data.Parent.Parent.Parent.Parent.Parent)));
			libraryFile.OriginalPath = Data.LinkedFile.FullName;
			libraryFile.Type = FileTypes.OvernightsLink;
			LinkManager.Instance.AttachFile(libraryFile);
			using (var form = new FormOvernihtsCalendarAttach())
			{
				if (form.ShowDialog() != DialogResult.OK)
					FormMain.Instance.Close();
			}
		}

		private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			if (Data.LinkedFile == null) return;
			var libraryFile = new LibraryLink(new LibraryFolder(new LibraryPage(Data.Parent.Parent.Parent.Parent.Parent)));
			libraryFile.OriginalPath = Data.LinkedFile.FullName;
			libraryFile.Type = FileTypes.OvernightsLink;
			LinkManager.Instance.OpenLink(libraryFile);
		}
	}
}