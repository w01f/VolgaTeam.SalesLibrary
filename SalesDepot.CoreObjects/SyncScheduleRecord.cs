using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects
{
    public class SyncScheduleRecord
    {
        public DateTime Time { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public string DayString
        {
            get
            {
                List<string> result = new List<string>();
                if (this.Monday)
                    result.Add("Mo");
                if (this.Tuesday)
                    result.Add("Tu");
                if (this.Wednesday)
                    result.Add("We");
                if (this.Thursday)
                    result.Add("Th");
                if (this.Friday)
                    result.Add("Fr");
                if (this.Saturday)
                    result.Add("Sa");
                if (this.Sunday)
                    result.Add("Su");

                return string.Join(",", result.ToArray());
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Time>" + this.Time.ToString() + @"</Time>");
            result.AppendLine(@"<Monday>" + this.Monday.ToString() + @"</Monday>");
            result.AppendLine(@"<Tuesday>" + this.Tuesday.ToString() + @"</Tuesday>");
            result.AppendLine(@"<Wednesday>" + this.Wednesday.ToString() + @"</Wednesday>");
            result.AppendLine(@"<Thursday>" + this.Thursday.ToString() + @"</Thursday>");
            result.AppendLine(@"<Friday>" + this.Friday.ToString() + @"</Friday>");
            result.AppendLine(@"<Saturday>" + this.Saturday.ToString() + @"</Saturday>");
            result.AppendLine(@"<Sunday>" + this.Sunday.ToString() + @"</Sunday>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool;
            DateTime tempDateTime;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Time":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.Time = tempDateTime;
                        break;
                    case "Monday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Monday = tempBool;
                        break;
                    case "Tuesday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Tuesday = tempBool;
                        break;
                    case "Wednesday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Wednesday = tempBool;
                        break;
                    case "Thursday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Thursday = tempBool;
                        break;
                    case "Friday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Friday = tempBool;
                        break;
                    case "Saturday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Saturday = tempBool;
                        break;
                    case "Sunday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Sunday = tempBool;
                        break;
                }
            }
        }
    }

    #region Obsolete, Using for compatibility with old versions
    public class TimePoint
    {
        public DayOfWeek Day { get; set; }
        public DateTime Time { get; set; }

        public string DayString
        {
            get
            {
                switch (this.Day)
                {
                    case DayOfWeek.Sunday:
                        return "Sunday";
                    case DayOfWeek.Monday:
                        return "Monday";
                    case DayOfWeek.Tuesday:
                        return "Tuesday";
                    case DayOfWeek.Wednesday:
                        return "Wednesday";
                    case DayOfWeek.Thursday:
                        return "Thursday";
                    case DayOfWeek.Friday:
                        return "Friday";
                    case DayOfWeek.Saturday:
                        return "Saturday";
                    default:
                        return "Sunday";
                }
            }
            set
            {
                switch (value)
                {
                    case "Sunday":
                        this.Day = DayOfWeek.Sunday;
                        break;
                    case "Monday":
                        this.Day = DayOfWeek.Monday;
                        break;
                    case "Tuesday":
                        this.Day = DayOfWeek.Tuesday;
                        break;
                    case "Wednesday":
                        this.Day = DayOfWeek.Wednesday;
                        break;
                    case "Thursday":
                        this.Day = DayOfWeek.Thursday;
                        break;
                    case "Friday":
                        this.Day = DayOfWeek.Friday;
                        break;
                    case "Saturday":
                        this.Day = DayOfWeek.Saturday;
                        break;
                    default:
                        this.Day = DayOfWeek.Sunday;
                        break;
                }
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Day>" + ((int)this.Day).ToString() + @"</Day>");
            result.AppendLine(@"<Time>" + this.Time.ToString() + @"</Time>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt;
            DateTime tempDateTime;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Day":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Day = (DayOfWeek)tempInt;
                        break;
                    case "Time":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.Time = tempDateTime;
                        break;
                }
            }
        }
    }
    #endregion
}
