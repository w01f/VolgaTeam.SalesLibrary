using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AutoSynchronizer.BusinessClasses
{
    public class LibraryPage
    {
        public Library Parent { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool EnableColumnTitles { get; set; }
        public bool ApplyForAllColumnTitles { get; set; }
        public List<LibraryFolder> Folders { get; set; }
        public List<ColumnTitle> ColumnTitles { get; set; }

        public int Index
        {
            get
            {
                return this.Order + 1;
            }
        }

        public LibraryPage(Library parent)
        {
            this.Parent = parent;
            this.Name = string.Format("Page {0}", this.Parent.Pages.Count + 1);
            this.Order = 0;
            this.EnableColumnTitles = false;
            this.ApplyForAllColumnTitles = false;
            this.Folders = new List<LibraryFolder>();
            this.ColumnTitles = new List<ColumnTitle>();

            ColumnTitle column = new ColumnTitle(this);
            column.Name = "Column 1";
            column.ColumnOrder = 0;
            this.ColumnTitles.Add(column);
            column = new ColumnTitle(this);
            column.Name = "Column 2";
            column.ColumnOrder = 1;
            this.ColumnTitles.Add(column);
            column = new ColumnTitle(this);
            column.Name = "Column 3";
            column.ColumnOrder = 2;
            this.ColumnTitles.Add(column);
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<Order>" + this.Order + @"</Order>");
            result.AppendLine(@"<EnableColumnTitles>" + this.EnableColumnTitles + @"</EnableColumnTitles>");
            result.AppendLine(@"<ApplyForAllColumnTitles>" + this.ApplyForAllColumnTitles + @"</ApplyForAllColumnTitles>");
            result.AppendLine("<Folders>");
            foreach (LibraryFolder folder in this.Folders)
                result.AppendLine(@"<Folder>" + folder.Serialize() + @"</Folder>");
            result.AppendLine("</Folders>");
            result.AppendLine("<ColumnTitles>");
            foreach (ColumnTitle columnTitle in this.ColumnTitles)
                result.AppendLine(@"<ColumnTitle>" + columnTitle.Serialize() + @"</ColumnTitle>");
            result.AppendLine("</ColumnTitles>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            int tempInt = 0;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        this.Name = childNode.InnerText;
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Order = tempInt;
                        break;
                    case "EnableColumnTitles":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableColumnTitles = tempBool;
                        break;
                    case "ApplyForAllColumnTitles":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllColumnTitles = tempBool;
                        break;
                    case "Folders":
                        this.Folders.Clear();
                        foreach (XmlNode folderNode in childNode.ChildNodes)
                        {
                            LibraryFolder folder = new LibraryFolder(this);
                            folder.Deserialize(folderNode);
                            this.Folders.Add(folder);
                        }
                        break;
                    case "ColumnTitles":
                        this.ColumnTitles.Clear();
                        foreach (XmlNode columnTitleNode in childNode.ChildNodes)
                        {
                            ColumnTitle columnTitle = new ColumnTitle(this);
                            columnTitle.Deserialize(columnTitleNode);
                            this.ColumnTitles.Add(columnTitle);
                        }
                        break;
                }
            }
        }
    }
}
