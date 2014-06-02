using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraTab;
using OvernightsCalendarViewer.ConfigurationClasses;
using OvernightsCalendarViewer.PresentationClasses.Decorators;
using OvernightsCalendarViewer.ToolForms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OvernightsCalendarViewer.PresentationClasses.OvernightsCalendar
{
	[ToolboxItem(false)]
	public partial class OvernightsCalendarControl : UserControl
	{
		private bool _buildInProgress;

		public OvernightsCalendarControl(LibraryDecorator parent)
		{
			InitializeComponent();
			ParentDecorator = parent;
			Dock = DockStyle.Fill;
			Years = new List<YearControl>();

			xtraTabControl.SelectedPageChanged += xtraTabControl_SelectedPageChanged;
		}

		public LibraryDecorator ParentDecorator { get; private set; }
		public List<YearControl> Years { get; private set; }
		public bool ViewBuilded { get; set; }

		public void Build()
		{
			if (!ViewBuilded)
			{
				_buildInProgress = true;
				xtraTabControl.TabPages.Clear();
				Years.Clear();
				foreach (CalendarYear year in ParentDecorator.Library.OvernightsCalendar.Years)
				{
					Years.Add(new YearControl(year));
					Application.DoEvents();
				}
				xtraTabControl.TabPages.AddRange(Years.ToArray());

				YearControl selectedTab = Years.FirstOrDefault(x => x.Data.Year.Equals(SettingsManager.Instance.SelectedCalendarYear));
				if (selectedTab == null)
					selectedTab = Years.FirstOrDefault();
				if (selectedTab != null)
					selectedTab.BuildControls();
				xtraTabControl.SelectedTabPage = selectedTab;

				_buildInProgress = false;
			}
			ViewBuilded = true;
		}

		public void RefreshColors()
		{
			foreach (YearControl year in Years)
				year.RefreshColors();
		}

		public void RefreshFont()
		{
			foreach (YearControl year in Years)
				year.RefreshFont();
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (_buildInProgress) return;
			var selectedYear = e.Page as YearControl;
			if (selectedYear == null || selectedYear.ViewBuilded) return;
			SettingsManager.Instance.SelectedCalendarYear = selectedYear.Data.Year;
			SettingsManager.Instance.SaveSettings();
			using (var formProgress = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				formProgress.TopMost = true;
				formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
				var thread = new Thread(() => Invoke((MethodInvoker)selectedYear.BuildControls));
				formProgress.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
				FormMain.Instance.ribbonControl.Enabled = true;
			}
		}
	}
}