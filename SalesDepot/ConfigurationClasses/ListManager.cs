using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesDepot.ConfigurationClasses
{
    class ListManager
    {
        private static ListManager _instance = new ListManager();

        public string ListsFolder { get; set; }
        public string AccessRightsFolderFolder { get; set; }
        public string SearchGroupsLogoFolder { get; set; }

        public SearchTags SearchTags { get; set; }

        public List<ProgramManager.CoreObjects.OutputFont> HeaderFonts { get; private set; }
        public List<ProgramManager.CoreObjects.OutputFont> FooterFonts { get; private set; }
        public List<ProgramManager.CoreObjects.OutputFont> BodyFonts { get; private set; }

        private ListManager()
        {
            this.HeaderFonts = new List<ProgramManager.CoreObjects.OutputFont>();
            this.HeaderFonts.Add(new ProgramManager.CoreObjects.OutputFont("Arial", 12, true));
            this.HeaderFonts.Add(new ProgramManager.CoreObjects.OutputFont("Verdana", 12, true));
            this.HeaderFonts.Add(new ProgramManager.CoreObjects.OutputFont("Calibri", 12, true));
            this.HeaderFonts.Add(new ProgramManager.CoreObjects.OutputFont("Trebuchet MS", 12, true));

            this.FooterFonts = new List<ProgramManager.CoreObjects.OutputFont>();
            this.FooterFonts.Add(new ProgramManager.CoreObjects.OutputFont("Arial", 11));
            this.FooterFonts.Add(new ProgramManager.CoreObjects.OutputFont("Verdana", 11));
            this.FooterFonts.Add(new ProgramManager.CoreObjects.OutputFont("Calibri", 11));
            this.FooterFonts.Add(new ProgramManager.CoreObjects.OutputFont("Trebuchet MS", 11));

            this.BodyFonts = new List<ProgramManager.CoreObjects.OutputFont>();
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Arial", 8));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Arial", 9));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Arial", 10));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Verdana", 8));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Calibri", 8));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Trebuchet MS", 8));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Verdana", 9));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Calibri", 9));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Trebuchet MS", 9));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Verdana", 10));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Calibri", 10));
            this.BodyFonts.Add(new ProgramManager.CoreObjects.OutputFont("Trebuchet MS", 10));
        }

        public void Init()
        {
            this.ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SearchGroupsLogoFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\SDSearchButton", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            this.SearchTags = new SearchTags();
        }

        public static ListManager Instance
        {
            get
            {
                return _instance;
            }
        }
    }

    public class SearchTags
    {
        private string _listsFileName;
        private List<string> _groupLogoFilePaths = new List<string>();
        public List<SearchGroup> SearchGroups { get; set; }

        public SearchTags()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "SDSearch.xml");
            this.SearchGroups = new List<SearchGroup>();
            LoadLogoFiles();
            Load();
        }

        private void LoadLogoFiles()
        {
            if (Directory.Exists(ListManager.Instance.SearchGroupsLogoFolder))
                _groupLogoFilePaths.AddRange(Directory.GetFiles(ListManager.Instance.SearchGroupsLogoFolder, "*.png"));
            _groupLogoFilePaths.Sort((x, y) => x.CompareTo(y));
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
                    int i = 0;
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "Category":
                                SearchGroup group = new SearchGroup();
                                if (_groupLogoFilePaths.Count > i)
                                    group.Logo = new Bitmap(_groupLogoFilePaths[i]);
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
                                i++;
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
}
