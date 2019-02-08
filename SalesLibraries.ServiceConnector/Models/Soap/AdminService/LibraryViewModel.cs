namespace SalesLibraries.ServiceConnector.AdminService
{
    public partial class LibraryViewModel
    {
        public override string ToString()
        {
            return name;
        }

        public bool selected { get; set; }
    }
}
