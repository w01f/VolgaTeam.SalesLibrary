using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace AutoSynchronizer.BusinessClasses
{
    public class ColumnTitle
    {
        public LibraryPage Parent { get; set; }
        public string Name { get; set; }
        public int ColumnOrder { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForeColor { get; set; }
        public Font HeaderFont { get; set; }
        public bool EnableText { get; set; }
        public Alignment HeaderAlignment { get; set; }
        public bool EnableWidget { get; set; }
        public Image Widget { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public ColumnTitle(LibraryPage parent)
        {
            this.Parent = parent;
            this.Name = string.Empty;
            this.ColumnOrder = 0;
            this.BackgroundColor = Color.White;
            this.ForeColor = Color.Black;
            this.HeaderFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            this.EnableText = true;
            this.HeaderAlignment = Alignment.Center;
            this.BannerProperties = new BannerProperties();
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<ColumnOrder>" + this.ColumnOrder + @"</ColumnOrder>");
            result.AppendLine(@"<BackgroundColor>" + this.BackgroundColor.ToArgb() + @"</BackgroundColor>");
            result.AppendLine(@"<ForeColor>" + this.ForeColor.ToArgb() + @"</ForeColor>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(this.HeaderFont) + @"</HeaderFont>");
            result.AppendLine(@"<EnableText>" + this.EnableText + @"</EnableText>");
            result.AppendLine(@"<HeaderAligment>" + ((int)this.HeaderAlignment).ToString() + @"</HeaderAligment>");
            result.AppendLine(@"<EnableWidget>" + this.EnableWidget + @"</EnableWidget>");
            result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(this.Widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt = 0;
            bool tempBool;
            FontConverter converter = new FontConverter();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        this.Name = childNode.InnerText;
                        break;
                    case "ColumnOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ColumnOrder = tempInt;
                        break;
                    case "BackgroundColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BackgroundColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeColor = Color.FromArgb(tempInt);
                        break;
                    case "HeaderFont":
                        try
                        {
                            this.HeaderFont = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "EnableText":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableText = tempBool;
                        break;
                    case "HeaderAligment":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.HeaderAlignment = (Alignment)tempInt;
                        break;
                    case "EnableWidget":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableWidget = tempBool;
                        break;
                    case "Widget":
                        if (!string.IsNullOrEmpty(childNode.InnerText))
                            this.Widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "BannerProperties":
                        this.BannerProperties.Deserialize(childNode);
                        break;
                }
            }
        }
    }
}
