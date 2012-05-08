using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SalesDepot
{
    class ActivityRecorder
    {
        private static ActivityRecorder instance = new ActivityRecorder();
        private string _activityPath = string.Format(@"{0}\newlocaldirect.com\01. file sync\BW-Data\Biz Wiz Data\", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        private string _nbwConfigFile = string.Format(@"{0}\newlocaldirect.com\New Biz Wizard\settings\NBWConfig.ini", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

        private List<ActivityRecord> activities = new List<ActivityRecord>();


        private ActivityRecorder()
        { 
        }

        public static ActivityRecorder Instance
        {
            get                
            {
                return instance;
            }
        }

        private FileInfo GetActivityFile()
        {
            FileInfo nbwConfig = new FileInfo(_nbwConfigFile);
            FileInfo activityFile = null;
            string activityFileName = "";
            if (nbwConfig.Exists)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(nbwConfig.FullName);
                if (doc.SelectSingleNode("/ROOT/FirstName").InnerXml.Trim() != "")
                    activityFileName = activityFileName + doc.SelectSingleNode("/ROOT/FirstName").InnerXml.Trim();
                if (doc.SelectSingleNode("/ROOT/LastName").InnerXml.Trim() != "")
                    activityFileName = activityFileName + " " + doc.SelectSingleNode("/ROOT/LastName").InnerXml.Trim();
                if (activityFileName.Trim() != "")
                    activityFileName = activityFileName + "-Activity.xml";
                else
                    activityFileName = "Not Configured-Activity.xml";
            }
            else
                activityFileName = "Not Configured-Activity.xml";
            
            activityFile = new FileInfo(_activityPath + activityFileName);
            return activityFile;
        }

        public void StartRecording()
        {
            activities.Clear();
        }

        public void WriteActivity()
        {
            activities.Add(new ActivityRecord(DateTime.Now));
        }

        public void StopRecording()
        {
            //int tempInteger = 0;
            //int srno = 0;
            //XmlElement newActivityRecord;
            //FileInfo activityFile = GetActivityFile();
            //if (activityFile.Exists)
            //{
            //    string fileName = activityFile.FullName;

            //    XmlDocument activityXml = new XmlDocument();
            //    try
            //    {
            //        activityXml.Load(fileName);
            //        foreach (XmlNode activityNode in activityXml.FirstChild.ChildNodes)
            //        {
            //            if (Int32.TryParse(activityNode.Attributes["Srno"].InnerXml, out tempInteger))
            //                if (srno < tempInteger)
            //                    srno = tempInteger;
            //        }
            //        srno++;
            //        for (int i = 0; i < activities.Count; i++)
            //        {
            //            newActivityRecord = activityXml.CreateElement("Activity");
            //            newActivityRecord.SetAttribute("Srno", (i + srno).ToString());
            //            newActivityRecord.SetAttribute("Date", activities[i].Date.ToString("d"));
            //            newActivityRecord.SetAttribute("Time", activities[i].Date.ToString("T"));
            //            newActivityRecord.SetAttribute("Type", "Sales Depot");
            //            activityXml.DocumentElement.AppendChild(newActivityRecord);
            //        }
            //        activityXml.Save(fileName);
            //        activities.Clear();
            //    }
            //    catch
            //    {
            //    }
            //}
        }
    }

    class ActivityRecord
    {
        DateTime date = DateTime.MinValue;
        string type = "Sales Depot";

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
        }

        public ActivityRecord(DateTime paramDate)
        {
            date = paramDate;
        }
    }

    class SDRecorder
    { 
        private static SDRecorder instance = new SDRecorder();
        private string _sdPath = string.Format(@"{0}\newlocaldirect.com\01. file sync\BW-Data\Biz Wiz Data\", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        private string _nbwConfigFile = string.Format(@"{0}\newlocaldirect.com\New Biz Wizard\settings\NBWConfig.ini", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

        private List<SDRecord> sdRecords = new List<SDRecord>();


        private SDRecorder()
        { 
        }

        public static SDRecorder Instance
        {
            get                
            {
                return instance;
            }
        }

        private void CreateSDFile()
        {
            FileInfo nbwConfig = new FileInfo(_nbwConfigFile);
            string userName = "";
            string sdFileName = "";
            if (nbwConfig.Exists)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(nbwConfig.FullName);
                if (doc.SelectSingleNode("/ROOT/FirstName").InnerXml.Trim() != "")
                {
                    sdFileName = sdFileName + doc.SelectSingleNode("/ROOT/FirstName").InnerXml.Trim();
                    userName += doc.SelectSingleNode("/ROOT/FirstName").InnerXml.Trim();
                }
                if (doc.SelectSingleNode("/ROOT/LastName").InnerXml.Trim() != "")
                {
                    sdFileName = sdFileName + " " + doc.SelectSingleNode("/ROOT/LastName").InnerXml.Trim();
                    userName += doc.SelectSingleNode("/ROOT/LastName").InnerXml.Trim();
                }
            }
            if (string.IsNullOrEmpty(sdFileName.Trim()))
                sdFileName = "Not Configured-SD.xml";
            else
                sdFileName = sdFileName + "-SD.xml";
            if (string.IsNullOrEmpty(userName.Trim()))
                userName = "NotConfigured";

            userName = userName.Replace(@"&", "").Replace("\"", "").Replace("'", "");
            if (!File.Exists(_sdPath + sdFileName))
            {
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<" + userName + ">");
                xml.AppendLine(@"</" + userName + ">");

                using (StreamWriter sw = new StreamWriter(_sdPath + sdFileName, false))
                {
                    sw.Write(xml.ToString());
                    sw.Flush();
                }
            }
        }

        private FileInfo GetSDFile()
        {
            FileInfo nbwConfig = new FileInfo(_nbwConfigFile);
            FileInfo sdFile = null;
            string sdFileName = "";
            if (nbwConfig.Exists)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(nbwConfig.FullName);
                if (doc.SelectSingleNode("/ROOT/FirstName").InnerXml.Trim() != "")
                    sdFileName = sdFileName + doc.SelectSingleNode("/ROOT/FirstName").InnerXml.Trim();
                if (doc.SelectSingleNode("/ROOT/LastName").InnerXml.Trim() != "")
                    sdFileName = sdFileName + " " + doc.SelectSingleNode("/ROOT/LastName").InnerXml.Trim();
                if (sdFileName.Trim() != "")
                    sdFileName = sdFileName + "-SD.xml";
                else
                    sdFileName = "Not Configured-SD.xml";
            }
            else
                sdFileName = "Not Configured-SD.xml";
            
            sdFile = new FileInfo(_sdPath + sdFileName);
            return sdFile;
        }

        public void StartRecording()
        {
            //CreateSDFile();
            sdRecords.Clear();
        }

        public void WriteSDEvent(string fileName)
        {
            SDRecord sdRecord = new SDRecord();
            sdRecord.Date = DateTime.Now;
            sdRecord.FileName = fileName;
            sdRecords.Add(sdRecord);
        }

        public void StopRecording()
        {
            //int tempInteger = 0;
            //int srno = 0;
            //XmlElement newSDRecord;
            //FileInfo sdFile = GetSDFile();
            //if (sdFile.Exists)
            //{
            //    string fileName = sdFile.FullName;

            //    XmlDocument sdXml = new XmlDocument();
            //    try
            //    {
            //        sdXml.Load(fileName);
            //        foreach (XmlNode sdNode in sdXml.FirstChild.ChildNodes)
            //        {
            //            if (Int32.TryParse(sdNode.Attributes["Srno"].InnerXml, out tempInteger))
            //                if (srno < tempInteger)
            //                    srno = tempInteger;
            //        }
            //        srno++;
            //        for (int i = 0; i < sdRecords.Count; i++)
            //        {
            //            newSDRecord = sdXml.CreateElement("SDFileClick");
            //            newSDRecord.SetAttribute("Srno", (i + srno).ToString());
            //            newSDRecord.SetAttribute("Date", sdRecords[i].Date.ToString("d"));
            //            newSDRecord.SetAttribute("Time", sdRecords[i].Date.ToString("T"));
            //            newSDRecord.SetAttribute("File", sdRecords[i].FileName);
            //            sdXml.DocumentElement.AppendChild(newSDRecord);
            //        }
            //        sdXml.Save(fileName);
            //        sdRecords.Clear();
            //    }
            //    catch
            //    {
            //    }
            //}
        }
    }

    class SDRecord
    {
        public DateTime Date { get; set; }
        public string FileName { get; set; }
    }
}
