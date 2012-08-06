using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace FileManager.BusinessClasses
{
    public class BannerProperties
    {
        public bool Configured { get; set; }

        public bool Enable { get; set; }
        public Image Image { get; set; }
        public bool ShowText { get; set; }
        public Alignment ImageAlignement { get; set; }
        public string Text { get; set; }
        public Color ForeColor { get; set; }
        public Font Font { get; set; }

        public BannerProperties()
        {
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.Text = string.Empty;
        }

        public string Serialize()
        {
            FontConverter fontConverter = new FontConverter();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Enable>" + this.Enable.ToString() + @"</Enable>");
            result.AppendLine(@"<Image>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Image>");
            result.AppendLine(@"<ImageAligement>" + ((int)this.ImageAlignement).ToString() + @"</ImageAligement>");
            result.AppendLine(@"<ShowText>" + this.ShowText.ToString() + @"</ShowText>");
            result.AppendLine(@"<Text>" + this.Text.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Text>");
            result.AppendLine(@"<Font>" + fontConverter.ConvertToString(this.Font) + @"</Font>");
            result.AppendLine(@"<ForeColor>" + this.ForeColor.ToArgb() + @"</ForeColor>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            FontConverter converter = new FontConverter();
            int tempInt = 0;
            bool tempBool = false;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Enable":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Enable = tempBool;
                        break;
                    case "Image":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Image = null;
                        else
                            this.Image = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "ImageAligement":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ImageAlignement = (Alignment)tempInt;
                        break;
                    case "ShowText":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowText = tempBool;
                        break;
                    case "Text":
                        this.Text = childNode.InnerText;
                        break;
                    case "Font":
                        try
                        {
                            this.Font = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeColor = Color.FromArgb(tempInt);
                        break;
                }
            }
            this.Configured = true;
        }
    }
}
