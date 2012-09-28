using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class FileCard
    {
        public ILibraryFile Parent { get; private set; }
        public Guid Identifier { get; set; }
        public bool Enable { get; set; }
        public string Advertiser { get; set; }
        public DateTime? DateSold { get; set; }
        public double? BroadcastClosed { get; set; }
        public double? DigitalClosed { get; set; }
        public double? PublishingClosed { get; set; }
        public string SalesName { get; set; }
        public string SalesEmail { get; set; }
        public string SalesPhone { get; set; }
        public string SalesStation { get; set; }
        public List<string> Notes { get; set; }

        public FileCard(ILibraryFile parent)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid();
            this.Notes = new List<string>();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<Enable>" + this.Enable.ToString() + @"</Enable>");
            if (!string.IsNullOrEmpty(this.Advertiser))
                result.AppendLine(@"<Advertiser>" + this.Advertiser.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Advertiser>");
            if (this.DateSold.HasValue)
                result.AppendLine(@"<DateSold>" + this.DateSold.Value.ToString() + @"</DateSold>");
            if (this.BroadcastClosed.HasValue)
                result.AppendLine(@"<BroadcastClosed>" + this.BroadcastClosed.Value.ToString() + @"</BroadcastClosed>");
            if (this.DigitalClosed.HasValue)
                result.AppendLine(@"<DigitalClosed>" + this.DigitalClosed.Value.ToString() + @"</DigitalClosed>");
            if (this.PublishingClosed.HasValue)
                result.AppendLine(@"<PublishingClosed>" + this.PublishingClosed.Value.ToString() + @"</PublishingClosed>");
            if (!string.IsNullOrEmpty(this.SalesName))
                result.AppendLine(@"<SalesName>" + this.SalesName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesName>");
            if (!string.IsNullOrEmpty(this.SalesEmail))
                result.AppendLine(@"<SalesEmail>" + this.SalesEmail.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesEmail>");
            if (!string.IsNullOrEmpty(this.SalesPhone))
                result.AppendLine(@"<SalesPhone>" + this.SalesPhone.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesPhone>");
            if (!string.IsNullOrEmpty(this.SalesStation))
                result.AppendLine(@"<SalesStation>" + this.SalesStation.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SalesStation>");
            result.AppendLine(@"<Notes>");
            foreach (string note in this.Notes)
                result.AppendLine(@"<Note>" + note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
            result.AppendLine(@"</Notes>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            Guid tempGuid;
            bool tempBool;
            double tempDouble;
            DateTime tempDate;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "Enable":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Enable = tempBool;
                        break;
                    case "Advertiser":
                        this.Advertiser = childNode.InnerText;
                        break;
                    case "DateSold":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.DateSold = tempDate;
                        break;
                    case "BroadcastClosed":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.BroadcastClosed = tempDouble;
                        break;
                    case "DigitalClosed":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.DigitalClosed = tempDouble;
                        break;
                    case "PublishingClosed":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.PublishingClosed = tempDouble;
                        break;
                    case "SalesName":
                        this.SalesName = childNode.InnerText;
                        break;
                    case "SalesEmail":
                        this.SalesEmail = childNode.InnerText;
                        break;
                    case "SalesPhone":
                        this.SalesPhone = childNode.InnerText;
                        break;
                    case "SalesStation":
                        this.SalesStation = childNode.InnerText;
                        break;
                    case "Notes":
                        foreach (XmlNode noteNode in childNode.ChildNodes)
                            this.Notes.Add(noteNode.InnerText);
                        break;
                }
            }
        }
    }
}
