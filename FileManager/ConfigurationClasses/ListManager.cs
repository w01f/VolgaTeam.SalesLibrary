using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;

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
