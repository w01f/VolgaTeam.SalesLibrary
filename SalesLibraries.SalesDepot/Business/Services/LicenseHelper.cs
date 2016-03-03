using EO.WebBrowser;
using Vintasoft.Imaging;

namespace SalesLibraries.SalesDepot.Business.Services
{
	static class LicenseHelper
	{
		public static void Register()
		{
			ImagingGlobalSettings.RegisterImaging("William Byrd",
				"billy@newlocaldirect.com",
				"IhdrSwyenp4CiNJnDzdnhXPg2TUsJa0Rykz4Gpmc11FMGFz5vEt0bFKIrMR6VziCH7YfvX8Ofnznh9z7WuhhZCFCp4mtvRBq1OJhX2M/1PS2GifwTp2aQlioVFHW7VhIgKdFerrBs6YDG6M15DKyiWqFJo4Ks6wyqEH5ANvmj5EI");
			Runtime.AddLicense(
				"pfcan53Y+PbooW+mtsPasWmoubPL8q7ZyQkb6Kvc99IfvFuts8PdrmuntcfN" +
				"n6/c9gQU7qe0psLgoVmmwp61n1mXpM0e6KDl5QUg8Z610gLb4IbN1uMjt4/M" +
				"6sXexY+928T9wHa0wMAe6KDl5QUg8Z61kZvnrqXg5/YZ8p61kZt14+30EO2s" +
				"3MKetZ9Zl6TNF+ic3PIEEMidtbvD47ZrqLjE37B1pvD6DuSn6unaD71GgaSx" +
				"y5914+30EO2s3OnP566l4Of2GfKe3MKetZ9Zl6TNDOul5vvPuIlZl6Sxy59Z" +
				"l8DyD+NZ6/0BELxbvNO/7uer5vH2zZ+v3PYEFO6ntKbC4a1pmaTA6YxDl6Sx" +
				"y7to2PD9GvZ3hI6xy59Zs/MDD+SrwPI=");
		}
	}
}
