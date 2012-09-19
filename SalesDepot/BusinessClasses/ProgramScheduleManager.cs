using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    public class ProgramScheduleManager
    {
        private const string StationsRoot = "Stations";
        private const string OutputRoot = "Output Templates";

        private List<ProgramManager.CoreObjects.Station> _stations = new List<ProgramManager.CoreObjects.Station>();

        public Library Parent { get; set; }
        public ProgramManager.CoreObjects.Station SelectedStation { get; private set; }
        public ProgramManager.CoreObjects.Day SelectedDay { get; private set; }

        public event EventHandler<EventArgs> StationChanged;

        private string ProgramManagerRoot
        {
            get
            {
                return Path.Combine(this.Parent.StorageFolder.FullName, CoreObjects.Constants.ProgramManagerRootFolderName);
            }
        }

        public bool Enabled
        {
            get
            {
                return _stations.Count > 0;
            }
        }

        public string StationsFolderPath
        {
            get
            {
                return Path.Combine(this.ProgramManagerRoot, StationsRoot);
            }
        }

        public string ReporActivityListTemplatePath
        {
            get
            {
                return Path.Combine(this.ProgramManagerRoot, OutputRoot, "ActivityList.xls");
            }
        }

        public ProgramScheduleManager(Library parent)
        {
            this.Parent = parent;
        }

        public string GetReportWeekScheduleTemplatePath(bool landscape)
        {
            return Path.Combine(this.ProgramManagerRoot, OutputRoot, string.Format("WeekSchedule{0}.xls", landscape ? "Landscape" : "Portrait"));
        }

        public void LoadData()
        {
            foreach (ProgramManager.CoreObjects.Station station in _stations)
                station.ReleaseResources();
            _stations.Clear();
            if (Directory.Exists(this.StationsFolderPath))
            {
                DirectoryInfo rootFolder = new DirectoryInfo(this.StationsFolderPath);
                foreach (DirectoryInfo stationFolder in rootFolder.GetDirectories())
                {
                    ProgramManager.CoreObjects.Station station = new ProgramManager.CoreObjects.Station(stationFolder);
                    _stations.Add(station);
                }
            }
        }

        public string[] GetStationList()
        {
            return _stations.Select(x => x.Name).ToArray();
        }

        public string[] GetProgramList()
        {
            if (this.SelectedStation != null)
                return this.SelectedStation.ProgramNames.ToArray();
            else
                return new string[] { };
        }

        public void LoadStation(object sender, string selectedStationName)
        {
            this.SelectedStation = _stations.Where(x => x.Name.Equals(selectedStationName)).FirstOrDefault();
            if (this.SelectedStation != null)
            {
                if (this.StationChanged != null)
                    this.StationChanged(sender, new EventArgs());
            }
        }

        public void LoadDay(DateTime day)
        {
            if (this.SelectedStation != null)
                this.SelectedDay = this.SelectedStation.GetDay(day);
        }

        public void SaveDay()
        {
            if (this.SelectedDay != null)
                this.SelectedDay.Save();
        }

        public void ReportWeekSchedule(string stationName, ProgramManager.CoreObjects.Week[] weeks, bool convertToPDF, bool landscape)
        {
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        form.laProgress.Text = "Generating Program Schedule...";
                        form.TopMost = true;
                        form.Show();
                        Application.DoEvents();
                    });

                    ProgramManager.CoreObjects.Station station = _stations.Where(x => x.Name.Equals(stationName)).FirstOrDefault();
                    if (station != null)
                    {
                        if (!Directory.Exists(ConfigurationClasses.SettingsManager.Instance.OutputCache))
                            Directory.CreateDirectory(ConfigurationClasses.SettingsManager.Instance.OutputCache);

                        string destinationPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.OutputCache, string.Format("{0}.{1}", new string[] { DateTime.Now.ToString("MMddyy-hhmmtt"), convertToPDF ? "pdf" : "xls" }));

                        List<ProgramManager.CoreObjects.Day[]> daysByWeeks = new List<ProgramManager.CoreObjects.Day[]>();
                        foreach (ProgramManager.CoreObjects.Week week in weeks)
                            daysByWeeks.Add(station.GetDays(week.DateStart, week.DateEnd));

                        InteropClasses.ExcelHelper.Instance.ReportWeekSchedule(this.GetReportWeekScheduleTemplatePath(landscape), daysByWeeks.ToArray(), destinationPath, convertToPDF, landscape);

                        if (File.Exists(destinationPath))
                            Process.Start(destinationPath);
                    }

                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        form.Hide();
                        Application.DoEvents();
                    });

                }));
                thread.Start();

                while (thread.IsAlive)
                    Application.DoEvents();

                form.Close();
            }
        }

        public void ReportActivityList(ProgramManager.CoreObjects.ProgramActivity[] activities, bool convertToPDF)
        {
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        form.laProgress.Text = "Generating Program List...";
                        form.TopMost = true;
                        form.Show();
                        Application.DoEvents();
                    });

                    if (!Directory.Exists(ConfigurationClasses.SettingsManager.Instance.OutputCache))
                        Directory.CreateDirectory(ConfigurationClasses.SettingsManager.Instance.OutputCache);

                    string destinationPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.OutputCache, string.Format("{0}.{1}", new string[] { DateTime.Now.ToString("MMddyy-hhmmtt"), convertToPDF ? "pdf" : "xls" }));

                    InteropClasses.ExcelHelper.Instance.ReportActivityList(this.ReporActivityListTemplatePath, activities, destinationPath, convertToPDF);

                    if (File.Exists(destinationPath))
                        Process.Start(destinationPath);

                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        form.Hide();
                        Application.DoEvents();
                    });

                }));
                thread.Start();

                while (thread.IsAlive)
                    Application.DoEvents();

                form.Close();
            }
        }
    }
}
