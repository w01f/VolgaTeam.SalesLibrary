using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace FileManager.ConfigurationClasses
{
    class ListManager
    {
        private static ListManager _instance = new ListManager();

        public string ListsFolder { get; set; }
        public string WidgetFolder { get; set; }
        public string BannerFolder { get; set; }

        public SearchTags SearchTags { get; set; }
        public List<Widget> Widgets { get; private set; }
        public List<Banner> Banners { get; private set; }

        private ListManager()
        {
        }

        public void Init()
        {
            this.ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.WidgetFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Widgets", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.BannerFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Banners", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            this.SearchTags = new SearchTags();
            this.Widgets = new List<Widget>();
            this.Banners = new List<Banner>();
            LoadWidgets();
            LoadBanners();
        }

        public static ListManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void LoadWidgets()
        {
            this.Widgets.Clear();
            if (Directory.Exists(this.WidgetFolder))
            {
                string[] widgetFiles = Directory.GetFiles(this.WidgetFolder, "*.png");
                foreach (string widgetFile in widgetFiles)
                {
                    Widget widget = new Widget(widgetFile);
                    if (widget.Index > 0 && widget.Image != null)
                        this.Widgets.Add(widget);
                }
                this.Widgets.Sort((x, y) => x.Index.CompareTo(y.Index));
            }
        }

        private void LoadBanners()
        {
            this.Banners.Clear();
            if (Directory.Exists(this.BannerFolder))
            {
                string[] bannerFiles = Directory.GetFiles(this.BannerFolder, "*.png");
                foreach (string bannerFile in bannerFiles)
                {
                    Banner banner = new Banner(bannerFile);
                    if (banner.Index > 0 && banner.Image != null)
                        this.Banners.Add(banner);
                }
                this.Banners.Sort((x, y) => x.Index.CompareTo(y.Index));
            }
        }
    }

    public class SearchTags
    {
        private string _listsFileName;
        public List<SearchGroup> SearchGroups { get; set; }

        public SearchTags()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "SDSearch.xml");
            this.SearchGroups = new List<SearchGroup>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/SDSearch");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "Category":
                                SearchGroup group = new SearchGroup();
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            group.Name = attribute.Value;
                                            break;
                                        case "Description":
                                            group.Description = attribute.Value;
                                            break;
                                    }
                                }
                                foreach (XmlNode tagNode in childNode.ChildNodes)
                                {
                                    switch (tagNode.Name)
                                    {
                                        case "Tag":
                                            foreach (XmlAttribute attribute in tagNode.Attributes)
                                            {
                                                switch (attribute.Name)
                                                {
                                                    case "Value":
                                                        if (!string.IsNullOrEmpty(attribute.Value))
                                                            group.Tags.Add(attribute.Value);
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                                if (!string.IsNullOrEmpty(group.Name) && group.Tags.Count > 0)
                                    this.SearchGroups.Add(group);
                                break;
                        }
                    }
                }
            }
        }
    }

    public class SearchGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Logo { get; set; }
        public List<string> Tags { get; set; }

        public SearchGroup()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Tags = new List<string>();
        }
    }

    public class Widget
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public Image Image { get; set; }

        public Widget(string widgetFile)
        {
            int index = 0;
            int.TryParse(Path.GetFileName(widgetFile).Substring(0, 2), out index);
            this.Index = index;
            this.FileName = Path.GetFileNameWithoutExtension(widgetFile);
            this.Image = new Bitmap(widgetFile);
        }
    }

    public class Banner
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public Image Image { get; set; }

        public Banner(string bannerFile)
        {
            int index = 0;
            int.TryParse(Path.GetFileName(bannerFile).Substring(0, 2), out index);
            this.Index = index;
            this.FileName = Path.GetFileNameWithoutExtension(bannerFile);
            this.Image = new Bitmap(bannerFile);
        }
    }
}
