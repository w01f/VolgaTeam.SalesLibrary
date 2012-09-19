using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects
{
    public class AutoWidget
    {
        public string Extension { get; set; }
        public Image Widget { get; set; }

        public AutoWidget()
        {
            this.Extension = string.Empty;
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Extension>" + this.Extension + @"</Extension>");
            result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Extension":
                        this.Extension = childNode.InnerText;
                        break;
                    case "Widget":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Widget = null;
                        else
                            this.Widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                }
            }
        }
    }
}
