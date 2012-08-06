using System;
using System.Text;
using System.Xml;

namespace AutoSynchronizer.BusinessClasses
{
    public class ExpirationDateOptions
    {
        public bool EnableExpirationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool LabelLinkWhenExpired { get; set; }
        public bool SendEmailWhenSync { get; set; }

        public ExpirationDateOptions()
        {
            this.EnableExpirationDate = false;
            this.ExpirationDate = DateTime.MinValue;
            this.LabelLinkWhenExpired = true;
            this.SendEmailWhenSync = false;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<EnableExpirationDate>" + this.EnableExpirationDate + @"</EnableExpirationDate>");
            result.AppendLine(@"<ExpirationDate>" + this.ExpirationDate + @"</ExpirationDate>");
            result.AppendLine(@"<LabelLinkWhenExpired>" + this.LabelLinkWhenExpired + @"</LabelLinkWhenExpired>");
            result.AppendLine(@"<SendEmailWhenSync>" + this.SendEmailWhenSync + @"</SendEmailWhenSync>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            DateTime tempDate = DateTime.Now;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "EnableExpirationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableExpirationDate = tempBool;
                        break;
                    case "ExpirationDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.ExpirationDate = tempDate;
                        break;
                    case "LabelLinkWhenExpired":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.LabelLinkWhenExpired = tempBool;
                        break;
                    case "SendEmailWhenSync":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.SendEmailWhenSync = tempBool;
                        break;
                }
            }
        }
    }
}
