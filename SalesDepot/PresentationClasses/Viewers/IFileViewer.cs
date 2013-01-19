using System.Drawing;

namespace SalesDepot.PresentationClasses.Viewers
{
    interface IFileViewer
    {
        BusinessClasses.LibraryLink File { get; }
        string DisplayName { get; }
        string CriteriaOverlap { get; }
        Image Widget { get; }

        void ReleaseResources();
        void Open();
        void Save();
        void Email();
        void Print();
    }
}
