﻿using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects
{
    public class LineBreakProperties
    {
        public Guid Identifier { get; set; }
        public Color ForeColor { get; set; }
        public Font Font { get; set; }
        public Font BoldFont { get; set; }
        public bool EnableBanner { get; set; }
        public Image Banner { get; set; }
        public string Note { get; set; }

        public LineBreakProperties()
        {
            this.Identifier = Guid.NewGuid();
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.BoldFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            this.Note = string.Empty;
        }

        public string Serialize()
        {
            FontConverter fontConverter = new FontConverter();
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<Font>" + fontConverter.ConvertToString(this.Font) + @"</Font>");
            result.AppendLine(@"<ForeColor>" + this.ForeColor.ToArgb() + @"</ForeColor>");
            result.AppendLine(@"<Note>" + this.Note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            FontConverter converter = new FontConverter();
            int tempInt = 0;
            bool tempBool = false;
            Guid tempGuid;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "Font":
                        try
                        {
                            this.Font = converter.ConvertFromString(childNode.InnerText) as Font;
                            this.BoldFont = new Font(this.Font.Name, this.Font.Size, FontStyle.Bold);
                        }
                        catch
                        {
                        }
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeColor = Color.FromArgb(tempInt);
                        break;
                    case "Note":
                        this.Note = childNode.InnerText;
                        break;

                    #region Compatibility with old versions
                    case "EnableBanner":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableBanner = tempBool;
                        break;
                    case "Banner":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Banner = null;
                        else
                            this.Banner = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    #endregion
                }
            }
        }
    }
}