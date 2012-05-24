using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    public class OvernightsCalendar
    {
        public Library Parent { get; set; }
        public bool Enabled { get; set; }
        public List<CalendarYear> Years { get; private set; }
        public List<FileInfo> Files { get; private set; }

        #region Color Settings
        public Color CalendarBackColor { get; set; }
        public Color CalendarBorderColor { get; set; }
        public Color CalendarHeaderBackColor { get; set; }
        public Color CalendarHeaderForeColor { get; set; }
        public Color MonthHeaderBackColor { get; set; }
        public Color MonthHeaderForeColor { get; set; }
        public Color MonthBodyBackColor { get; set; }
        public Color MonthBodyForeColor { get; set; }
        public Color SweepBackColor { get; set; }
        public Color SweepForeColor { get; set; }
        public Color DeadLinksForeColor { get; set; }
        #endregion

        public OvernightsCalendar(Library parent)
        {
            this.Parent = parent;

            this.Years = new List<CalendarYear>();
            this.Files = new List<FileInfo>();

            ResetColors();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            int tempInt;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Enabled":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Enabled = tempBool;
                        break;
                    case "CalendarBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarBackColor = Color.FromArgb(tempInt);
                        break;
                    case "CalendarBorderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarBorderColor = Color.FromArgb(tempInt);
                        break;
                    case "CalendarHeaderBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarHeaderBackColor = Color.FromArgb(tempInt);
                        break;
                    case "CalendarHeaderForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CalendarHeaderForeColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthHeaderBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthHeaderBackColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthHeaderForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthHeaderForeColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthBodyBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthBodyBackColor = Color.FromArgb(tempInt);
                        break;
                    case "MonthBodyForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MonthBodyForeColor = Color.FromArgb(tempInt);
                        break;
                    case "SweepBackColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SweepBackColor = Color.FromArgb(tempInt);
                        break;
                    case "SweepForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SweepForeColor = Color.FromArgb(tempInt);
                        break;
                    case "DeadLinksForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeadLinksForeColor = Color.FromArgb(tempInt);
                        break;
                }
            }
        }

        public void LoadYears()
        {
            this.Years.Clear();
            this.Files.Clear();
            string rootFolderPath = Path.Combine(this.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.OvernightsCalendarRootFolderName);
            if (Directory.Exists(rootFolderPath) && this.Enabled)
            {
                DirectoryInfo rootFolder = new DirectoryInfo(rootFolderPath);
                foreach (DirectoryInfo yearFolder in rootFolder.GetDirectories())
                {
                    int temp;
                    if (int.TryParse(yearFolder.Name, out temp))
                    {
                        CalendarYear year = new CalendarYear(this);
                        year.RootFolder = yearFolder;
                        year.Year = temp;
                        this.Files.AddRange(year.RootFolder.GetFiles());
                        this.Years.Add(year);
                        Application.DoEvents();
                    }
                }
                foreach (CalendarYear year in this.Years)
                {
                    year.LoadSweepPeriods();
                    year.LoadMonths();
                    Application.DoEvents();
                }
            }
        }

        public void ResetColors()
        {
            this.CalendarBackColor = Color.AliceBlue;
            this.CalendarBorderColor = Color.DarkGray;
            this.CalendarHeaderBackColor = Color.Azure;
            this.CalendarHeaderForeColor = Color.Black;
            this.MonthHeaderBackColor = Color.AliceBlue;
            this.MonthHeaderForeColor = Color.Black;
            this.MonthBodyBackColor = Color.AliceBlue;
            this.MonthBodyForeColor = Color.Black;
            this.SweepBackColor = Color.LightGray;
            this.SweepForeColor = Color.Black;
            this.DeadLinksForeColor = Color.Black;
        }
    }

    public class CalendarYear
    {
        public OvernightsCalendar Parent { get; private set; }
        public DirectoryInfo RootFolder { get; set; }
        public int Year { get; set; }
        public List<CalendarMonth> Months { get; private set; }
        public List<DateTime> SweepDays { get; private set; }

        public CalendarYear(OvernightsCalendar parent)
        {
            this.Parent = parent;
            this.Months = new List<CalendarMonth>();
            this.SweepDays = new List<DateTime>();
        }

        public void LoadMonths()
        {
            this.Months.Clear();
            for (int i = 1; i <= 12; i++)
            {
                CalendarMonth month = new CalendarMonth(this);
                month.MonthFirstDay = new DateTime(this.Year, i, 1);
                month.LoadDays();
                this.Months.Add(month);
                Application.DoEvents();
            }
        }

        public void LoadSweepPeriods()
        {
            this.SweepDays.Clear();
            if (this.RootFolder.Exists)
            {
                string sweepPeriodsConnfigFile = Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.SweepPeriodsFileName);
                if (File.Exists(sweepPeriodsConnfigFile))
                {
                    try
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(sweepPeriodsConnfigFile);
                        XmlNode node = document.SelectSingleNode(@"/SweepPeriods");
                        if (node != null)
                        {
                            foreach (XmlNode childNode in node.ChildNodes)
                            {
                                if (childNode.Name.Equals("SweepPeriod"))
                                {
                                    DateTime dateBegin = DateTime.MinValue;
                                    DateTime dateEnd = DateTime.MinValue;
                                    foreach (XmlAttribute attribute in childNode.Attributes)
                                    {
                                        switch (attribute.Name)
                                        {
                                            case "DateBegin":
                                                DateTime.TryParse(attribute.Value, out dateBegin);
                                                break;
                                            case "DateEnd":
                                                DateTime.TryParse(attribute.Value, out dateEnd);
                                                break;
                                        }
                                    }
                                    if (!dateBegin.Equals(DateTime.MinValue) && !dateEnd.Equals(DateTime.MinValue) && dateBegin < dateEnd)
                                    {
                                        while (dateBegin <= dateEnd)
                                        {
                                            this.SweepDays.Add(dateBegin);
                                            dateBegin = dateBegin.AddDays(1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    public class CalendarMonth
    {
        public CalendarYear Parent { get; private set; }
        public DateTime MonthFirstDay { get; set; }
        public List<CalendarDay> Days { get; private set; }

        public string Name
        {
            get
            {
                return this.MonthFirstDay.ToString("MMMM").ToUpper();
            }
        }

        public CalendarMonth(CalendarYear parent)
        {
            this.Parent = parent;
            this.Days = new List<CalendarDay>();
        }

        public void LoadDays()
        {
            this.Days.Clear();

            DateTime startDay = this.MonthFirstDay;
            while (startDay.DayOfWeek != DayOfWeek.Monday)
                startDay = startDay.AddDays(-1);

            DateTime lastDay = this.MonthFirstDay.AddMonths(1).AddDays(-1);
            while (lastDay.DayOfWeek != DayOfWeek.Sunday)
                lastDay = lastDay.AddDays(-1);

            while (startDay <= lastDay)
            {
                CalendarDay day = new CalendarDay(this);
                day.Date = startDay;
                day.IsSweepDay = this.Parent.SweepDays.Contains(day.Date);
                this.Days.Add(day);
                Application.DoEvents();

                startDay = startDay.AddDays(1);
            }
        }
    }

    public class CalendarDay
    {
        private LibraryFile _linkedFile = null;
        private bool _linkedFileReady = false;

        public CalendarMonth Parent { get; private set; }
        public DateTime Date { get; set; }

        public bool IsSweepDay { get; set; }

        public CalendarDay(CalendarMonth parent)
        {
            this.Parent = parent;
        }

        public LibraryFile LinkedFile
        {
            get
            {
                if (!_linkedFileReady)
                {
                    _linkedFileReady = true;
                    FileInfo file = this.Parent.Parent.Parent.Files.Where(x => x.Name.Contains(this.Date.ToString("MMddyy"))).FirstOrDefault();
                    if (file != null)
                    {
                        _linkedFile = new LibraryFile(null);
                        _linkedFile.RemotePath = file.FullName;
                        _linkedFile.SetProperties();
                    }
                }
                return _linkedFile;
            }
        }
    }
}
