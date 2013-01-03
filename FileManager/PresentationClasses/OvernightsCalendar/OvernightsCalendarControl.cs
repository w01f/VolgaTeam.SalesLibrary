using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolForms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.OvernightsCalendar
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

		public void DisposeCalendar()
		{
			xtraTabControl.SelectedPageChanged -= xtraTabControl_SelectedPageChanged;
			xtraTabControl.TabPages.Clear();
			foreach (var yearControl in Years)
			{
				yearControl.Parent = null;
				yearControl.Dispose();
				Application.DoEvents();
			}
		}

		public LibraryDecorator ParentDecorator { get; private set; }
		public List<YearControl> Years { get; private set; }
		public bool ViewBuilded { get; set; }

		public void Build(bool forceBuild)
		{
			if (!ViewBuilded || forceBuild)
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

				YearControl selectedTab = Years.Where(x => x.Data.Year.Equals(SettingsManager.Instance.SelectedCalendarYear)).FirstOrDefault();
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
			if (!_buildInProgress)
			{
				var selectedYear = e.Page as YearControl;
				if (selectedYear != null && !selectedYear.ViewBuilded)
				{
					SettingsManager.Instance.SelectedCalendarYear = selectedYear.Data.Year;
					SettingsManager.Instance.Save();
					using (var formProgress = new FormProgress())
					{
						FormMain.Instance.ribbonControl.Enabled = false;
						formProgress.TopMost = true;
						formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
						var thread = new Thread(delegate() { Invoke((MethodInvoker)delegate { selectedYear.BuildControls(); }); });
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
	}
}