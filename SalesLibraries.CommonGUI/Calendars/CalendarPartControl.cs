using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Calendars;

namespace SalesLibraries.CommonGUI.Calendars
{
	[ToolboxItem(false)]
	public sealed partial class CalendarPartControl : UserControl
	{
		private bool _readyToUse;
		private bool _buildInProgress;
		private readonly ICalendarContainerControl _calendarControl;
		public CalendarPart PartData { get; private set; }
		public List<YearControl> Years { get; private set; }

		public event EventHandler<CalendarYearSelectedEventArgs> YearSelected;

		public CalendarPartControl(CalendarPart data, ICalendarContainerControl calendarControl)
		{
			InitializeComponent();
			PartData = data;
			_calendarControl = calendarControl;
			Dock = DockStyle.Fill;
			Years = new List<YearControl>();
		}

		private void Build()
		{
			xtraTabControl.TabPages.Clear();
			Years.Clear();
			foreach (var year in PartData.Years)
			{
				Years.Add(new YearControl(year));
				Application.DoEvents();
			}
			xtraTabControl.TabPages.AddRange(Years.ToArray());
		}

		public void DisposePart()
		{
			xtraTabControl.SelectedPageChanging -= xtraTabControl_SelectedPageChanging;
			xtraTabControl.TabPages.Clear();
			foreach (var yearControl in Years)
			{
				yearControl.Parent = null;
				yearControl.Dispose();
				Application.DoEvents();
			}
			Years.Clear();
		}

		public void Show(int? year = null)
		{
			_buildInProgress = true;

			if (!_readyToUse)
			{
				_calendarControl.RunProcessInBackground("Loading Calendar...", (cancelationToken, formProgess) =>
					_calendarControl.InvokeInContainer(new MethodInvoker(() =>
					{
						Build();
						var selectedYear = Years.FirstOrDefault(y => year.HasValue && y.Data.Year == year) ??
							Years.FirstOrDefault(y => y.Data.Year == DateTime.Now.Year) ??
							Years.FirstOrDefault();
						if (selectedYear != null)
						{
							ShowYear(selectedYear);
							xtraTabControl.SelectedTabPage = selectedYear;
						}
					})));
				_readyToUse = true;
			}
			if (!_calendarControl.ContainerControl.Controls.Contains(this))
				_calendarControl.ContainerControl.Controls.Add(this);
			BringToFront();
			_buildInProgress = false;
		}

		public void RefreshView()
		{
			foreach (var year in Years.Where(y => y.ViewBuilded))
				year.RefreshView();
		}

		public void RefreshFont()
		{
			foreach (var year in Years.Where(y => y.ViewBuilded))
				year.RefreshFont(_calendarControl.CurrentFontSize);
		}

		private void ShowYear(YearControl yearControl)
		{
			if (!yearControl.ViewBuilded)
			{
				_calendarControl.RunProcessInBackground("Loading Calendar...", (cancelationToken, formProgess) =>
					_calendarControl.InvokeInContainer(new MethodInvoker(() =>
					{
						yearControl.BuildControls();
						yearControl.RefreshFont(_calendarControl.CurrentFontSize);
					})));
			}
		}

		private void xtraTabControl_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
		{
			if (_buildInProgress) return;
			var selectedYear = (YearControl)e.Page;
			_calendarControl.RunProcessInBackground("Loading Calendar...", (cancelationToken, formProgess) =>
				_calendarControl.InvokeInContainer(new MethodInvoker(() => ShowYear(selectedYear))));
			ShowYear(selectedYear);
			YearSelected?.Invoke(this, new CalendarYearSelectedEventArgs(selectedYear.Data.Year));
		}
	}
}