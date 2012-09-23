using System;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class PresentationProperties
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public DateTime LastUpdate { get; set; }

        public string Orientation
        {
            get
            {
                if (this.Height < this.Width)
                    return "Landscape";
                else
                    return "Portrait";
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Height>" + this.Height + @"</Height>");
            result.AppendLine(@"<Width>" + this.Width + @"</Width>");
            result.AppendLine(@"<LastUpdate>" + this.LastUpdate + @"</LastUpdate>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            double tempDouble = 0;
            DateTime tempDateTime = DateTime.MinValue;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Height":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.Height = tempDouble;
                        break;
                    case "Width":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.Width = tempDouble;
                        break;
                    case "LastUpdate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.LastUpdate = tempDateTime;
                        break;
                }
            }
        }
    }
}
