using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.WallBin.Decorators
{
    public class DecoratorManager
    {
        private static DecoratorManager _instance = null;
        public List<PresentationClasses.WallBin.Decorators.LibraryDecorator> Decorators { get; private set; }
        public PresentationClasses.WallBin.Decorators.LibraryDecorator ActiveDecorator { get; set; }

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
            this.Decorators = new List<LibraryDecorator>();
        }

        public void BuildDecorators()
        {
            this.Decorators.Clear();
            foreach (SalesDepot.CoreObjects.BusinessClasses.Library library in BusinessClasses.LibraryManager.Instance.LibraryCollection)
            {
                Application.DoEvents();
                this.Decorators.Add(new PresentationClasses.WallBin.Decorators.LibraryDecorator(library));
            }
        }

        public void BuildOvernightsCalendars()
        {
            foreach (LibraryDecorator libraryDecorator in this.Decorators)
            {
                Application.DoEvents();
                libraryDecorator.BuildOvernightsCalendar();
            }
        }
    }
}
