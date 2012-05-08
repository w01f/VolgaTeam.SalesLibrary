using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileManager.PresentationClasses.Decorators
{
    public class DecoratorManager
    {
        private static DecoratorManager _instance = null;
        public List<PresentationClasses.Decorators.LibraryDecorator> Decorators { get; private set; }
        public PresentationClasses.Decorators.LibraryDecorator ActiveDecorator { get; set; }

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
            foreach (BusinessClasses.Library library in BusinessClasses.LibraryManager.Instance.LibraryCollection)
                this.Decorators.Add(new PresentationClasses.Decorators.LibraryDecorator(library));
        }
    }
}
