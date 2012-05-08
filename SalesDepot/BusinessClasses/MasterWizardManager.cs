using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.BusinessClasses
{
    class MasterWizardManager
    {
        public static string MasterWizardsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        private static MasterWizardManager _instance = new MasterWizardManager();
        public Dictionary<string, MasterWizard> MasterWizards { get; set; }


        private MasterWizardManager()
        {
            this.MasterWizards = new Dictionary<string, MasterWizard>();
            Load();
        }

        public static MasterWizardManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Load()
        {
            DirectoryInfo rootFolder = new DirectoryInfo(MasterWizardsFolder);
            if (rootFolder.Exists)
            {
                foreach (DirectoryInfo folder in rootFolder.GetDirectories())
                {
                    MasterWizard masterWizard = new MasterWizard();
                    masterWizard.Name = folder.Name;
                    masterWizard.Folder = folder;
                    masterWizard.Init();
                    if (masterWizard.HasSlideTemplate43)
                        this.MasterWizards.Add(masterWizard.Name, masterWizard);
                }
            }
        }
    }

    public class MasterWizard
    {
        public string Name { get; set; }
        public DirectoryInfo Folder { get; set; }
        public bool Has43 { get; set; }
        public bool HasSlideTemplate43 { get; set; }
        public string TemplatePath { get; set; }

        public MasterWizard()
        {
        }

        public void Init()
        {
            this.Has43 = Directory.Exists(Path.Combine(this.Folder.FullName, "Slides43"));
            if (this.Has43)
                this.TemplatePath = Directory.GetFiles(Path.Combine(this.Folder.FullName, "Slides43"), @"*.pot*").FirstOrDefault();
            this.HasSlideTemplate43 = !string.IsNullOrEmpty(this.TemplatePath);
        }
    }
}
