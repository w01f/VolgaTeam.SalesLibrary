using System.Drawing;

namespace SalesDepot.BusinessClasses
{
    interface IFileViewer
    {
        BusinessClasses.LibraryFile File { get; }
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
