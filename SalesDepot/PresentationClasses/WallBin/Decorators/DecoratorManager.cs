using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin.Decorators
{
    public class DecoratorManager
    {
        private static DecoratorManager _instance = null;
        public List<Decorators.PackageDecorator> PackageViewers { get; private set; }
        public Decorators.PackageDecorator ActivePackageViewer { get; set; }

        public static DecoratorManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DecoratorManager();
                return _instance;
            }
        }

        private DecoratorManager()
        {
            this.PackageViewers = new List<Decorators.PackageDecorator>();
        }

        public void BuildPackageViewers()
        {
            this.PackageViewers.Clear();
            foreach (BusinessClasses.LibraryPackage package in BusinessClasses.LibraryManager.Instance.LibraryPackageCollection)
            {
                this.PackageViewers.Add(new Decorators.PackageDecorator(package));
                Application.DoEvents();
            }
        }

        public void BuildOvernightsCalendars()
        {
            foreach (PackageDecorator packageViwer in this.PackageViewers)
            {
                packageViwer.BuildOvernightsCalendar();
                Application.DoEvents();
            }
        }
    }
}
