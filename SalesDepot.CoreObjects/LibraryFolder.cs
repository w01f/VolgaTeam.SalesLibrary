using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects
{
    public class LibraryFolder
    {
        public Guid Identifier { get; set; }
        public LibraryPage Parent { get; set; }
        public string Name { get; set; }
        public double RowOrder { get; set; }
        public int ColumnOrder { get; set; }
        public Color BorderColor { get; set; }
        public Color BackgroundWindowColor { get; set; }
        public Color ForeWindowColor { get; set; }
        public Color BackgroundHeaderColor { get; set; }
        public Color ForeHeaderColor { get; set; }
        public Font WindowFont { get; set; }
        public Font HeaderFont { get; set; }
        public Alignment HeaderAlignment { get; set; }
        public bool EnableWidget { get; set; }
        public Image Widget { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public List<ILibraryFile> Files { get; set; }

        public double AbsoluteRowOrder
        {
            get
            {
                return this.Parent.Folders.Where(x => x.ColumnOrder < this.ColumnOrder).Count() + this.Parent.Folders.Where(x => x.ColumnOrder == this.ColumnOrder).ToList().IndexOf(this);
            }
        }

        public LibraryFolder(LibraryPage parent)
        {
            this.Identifier = Guid.NewGuid();
            this.Parent = parent;
            this.Name = string.Empty;
            this.RowOrder = 0;
            this.ColumnOrder = 0;
            this.BorderColor = Color.Black;
            this.BackgroundWindowColor = Color.White;
            this.ForeWindowColor = Color.Black;
            this.BackgroundHeaderColor = Color.White;
            this.ForeHeaderColor = Color.Black;
            this.WindowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            this.HeaderFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.HeaderAlignment = Alignment.Center;

            this.BannerProperties = new BannerProperties();
            this.BannerProperties.Font = this.HeaderFont;
            this.BannerProperties.ForeColor = this.ForeHeaderColor;

            this.Files = new List<ILibraryFile>();
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<RowOrder>" + this.RowOrder + @"</RowOrder>");
            result.AppendLine(@"<ColumnOrder>" + this.ColumnOrder + @"</ColumnOrder>");
            result.AppendLine(@"<BorderColor>" + this.BorderColor.ToArgb() + @"</BorderColor>");
            result.AppendLine(@"<BackgroundWindowColor>" + this.BackgroundWindowColor.ToArgb() + @"</BackgroundWindowColor>");
            result.AppendLine(@"<ForeWindowColor>" + this.ForeWindowColor.ToArgb() + @"</ForeWindowColor>");
            result.AppendLine(@"<BackgroundHeaderColor>" + this.BackgroundHeaderColor.ToArgb() + @"</BackgroundHeaderColor>");
            result.AppendLine(@"<ForeHeaderColor>" + this.ForeHeaderColor.ToArgb() + @"</ForeHeaderColor>");
            result.AppendLine(@"<WindowFont>" + converter.ConvertToString(this.WindowFont) + @"</WindowFont>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(this.HeaderFont) + @"</HeaderFont>");
            result.AppendLine(@"<HeaderAligment>" + ((int)this.HeaderAlignment).ToString() + @"</HeaderAligment>");
            result.AppendLine(@"<EnableWidget>" + this.EnableWidget + @"</EnableWidget>");
            result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(this.Widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
            result.AppendLine("<Files>");
            foreach (ILibraryFile file in this.Files)
                result.AppendLine(@"<File>" + file.Serialize() + @"</File>");
            result.AppendLine("</Files>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt = 0;
            bool tempBool;
            FontConverter converter = new FontConverter();
            Guid tempGuid;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        this.Name = childNode.InnerText;
                        break;
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "RowOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.RowOrder = tempInt;
                        break;
                    case "ColumnOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ColumnOrder = tempInt;
                        break;
                    case "BorderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BorderColor = Color.FromArgb(tempInt);
                        break;
                    case "BackgroundWindowColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BackgroundWindowColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeWindowColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeWindowColor = Color.FromArgb(tempInt);
                        break;
                    case "BackgroundHeaderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BackgroundHeaderColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeHeaderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeHeaderColor = Color.FromArgb(tempInt);
                        break;
                    case "WindowFont":
                        try
                        {
                            this.WindowFont = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
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
                    case "Files":
                        this.Files.Clear();
                        foreach (XmlNode fileNode in childNode.ChildNodes)
                        {
                            ILibraryFile file = this.Parent.Parent.GetLinkInstance(this);
                            file.Deserialize(fileNode);
                            this.Files.Add(file);
                        }
                        break;
                }
            }
            if (!this.BannerProperties.Configured)
            {
                this.BannerProperties.Text = this.Name;
                this.BannerProperties.Font = this.HeaderFont;
                this.BannerProperties.ForeColor = this.ForeHeaderColor;
            }
        }

        public void RemoveFromParent()
        {
            this.Parent.Folders.Remove(this);
        }

        public ILibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (ILibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
            {
                bool fullMatch = true;
                bool partialMatch = false;
                foreach (SearchGroup group in searchCriteria.SearchGroups)
                {
                    SearchGroup fileSearchGroup = file.SearchTags.SearchGroups.Where(x => x.Name.Equals(group.Name)).FirstOrDefault();
                    if (fileSearchGroup != null)
                    {
                        foreach (string tag in group.Tags)
                            if (fileSearchGroup.Tags.Contains(tag))
                            {
                                partialMatch = true;
                                fullMatch = fullMatch & true;
                            }
                            else
                            {
                                fullMatch = fullMatch & false;
                            }
                    }
                    else
                        fullMatch = fullMatch & false;
                }
                if (partialMatch)
                {
                    file.CriteriaOverlap = fullMatch ? "meet ALL of your Search Criteria" : "meet SOME of your Search Criteria";
                    searchFiles.Add(file);
                }
                else
                    file.CriteriaOverlap = string.Empty;
            }
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            string[] templateParts = template.Split(' ');
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (ILibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
            {
                bool fullMatch = false;
                bool partialMatch = false;

                if (file.Name.ToLower().Equals(template) && !string.IsNullOrEmpty(template) && ((type == file.Type) || (type == FileTypes.Other)))
                {
                    fullMatch = true;
                    partialMatch = true;
                }
                else if (!string.IsNullOrEmpty(template) && ((type == file.Type) || (type == FileTypes.Other)))
                {
                    if (templateParts.Length > 1)
                    {
                        foreach (string templatePart in templateParts)
                            if (file.Name.ToLower().Contains(templatePart.Trim().ToLower()))
                            {
                                fullMatch = false;
                                partialMatch = true;
                                break;
                            }
                    }
                    else if (file.Name.ToLower().Contains(template))
                    {
                        fullMatch = false;
                        partialMatch = true;
                    }
                }

                if ((partialMatch && !fullMatchOnly) || fullMatch)
                {
                    file.CriteriaOverlap = fullMatch ? "meet ALL of your Search Criteria" : "meet SOME of your Search Criteria";
                    searchFiles.Add(file);
                }
            }
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (ILibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
            {
                bool fullMatch = false;

                if (file.AddDate >= startDate && file.AddDate <= endDate)
                    fullMatch = true;

                if (fullMatch)
                {
                    file.CriteriaOverlap = "meet ALL of your Search Criteria";
                    searchFiles.Add(file);
                }
                else
                    file.CriteriaOverlap = string.Empty;
            }
            return searchFiles.ToArray();
        }
    }
}
